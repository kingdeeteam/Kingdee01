using System;
using System.Collections.Generic;
using System.Linq;
using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.BOS.Core.Validation;
using Kingdee.BOS.App;
using Kingdee.BOS.Contracts;
using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Core.SqlBuilder;

namespace Kingdee.Samples.PlugInDev.DynamicForm.Operation
{
    /// <summary>
    /// 字符重复校验
    /// </summary>
    public class FieldRepeatValidator : AbstractValidator
    {
        /// <summary>
        /// 校验字段
        /// </summary>
        public string FieldKey { get; set; }

        public override void InitializeConfiguration(ValidateContext validateContext, Context ctx)
        {
            base.InitializeConfiguration(validateContext, ctx);
            if (validateContext.BusinessInfo != null)
            {
                //设置要校验的实体
                EntityKey = validateContext.BusinessInfo.GetEntity(0).Key;
            }
        }

        public override void Validate(ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Context ctx)
        {
            if (dataEntities == null || dataEntities.Length <= 0)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(FieldKey))
            {
                throw new Exception("校验字段不能为空");
            }

            var field = validateContext.BusinessInfo.GetField(FieldKey);
            if (field == null)
            {
                throw new Exception("字段不存在:" + FieldKey);
            }

            var fieldName = field.Name.ToString();
            //循环取得实体数据包及字段值
            var valueList = new List<Tuple<string, ExtendedDataEntity>>();
            foreach (var et in dataEntities)
            {
                var fieldValue = field.GetFieldValue(et.DataEntity);
                if (fieldValue == null)
                {
                    continue;
                }
                var value = fieldValue.ToString();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    valueList.Add(new Tuple<string, ExtendedDataEntity>(value, et));
                }
            }

            #region 检查当前集合是否有重复
            //按字段值分组
            var valueGroupList = valueList.GroupBy(x => new { Key = x.Item1 })
                .Select(y => new { y.Key.Key, Count = y.Count() }).ToList();
            //选出重复字段值的数据 
            var repeatValueList = valueGroupList.Where(o => o.Count > 1).ToList();
            var repeatValues = repeatValueList.Select(o => o.Key).ToList();

            if (repeatValueList.Count > 0)
            {                
                var repeatDataEntityList = valueList.Where(o => repeatValues.Contains(o.Item1)).ToList();
                foreach (var repeatDataEntity in repeatDataEntityList)
                {
                    var item = repeatDataEntity.Item2;
                    validateContext.AddError(item, new ValidationErrorInfo(
                        "",
                        Convert.ToString(item.DataEntity[0]),
                        item.DataEntityIndex,
                        item.RowIndex,
                        "E1",
                        fieldName + "重复:" + repeatDataEntity.Item1,
                        "字段唯一性检查"));
                }
            }

            #endregion

            #region 检查数据库是否有重复
            
            foreach (var item in valueList)
            {
                //上一步已检查重复的数据就跳过
                if (repeatValues.Contains(item.Item1))
                {
                    continue;
                }

                var fldValue = item.Item1;
                var extendDataEntity = item.Item2;
                var pkValue = Convert.ToString(extendDataEntity.DataEntity[0]);
                var queryPara = new QueryBuilderParemeter();
                queryPara.FormId = validateContext.BusinessInfo.GetForm().Id;
                queryPara.BusinessInfo = validateContext.BusinessInfo;
                queryPara.SelectItems.Add(new SelectorItemInfo(validateContext.BusinessInfo.GetForm().PkFieldName));
                queryPara.FilterClauseWihtKey = string.Format("{0}='{1}'", FieldKey, fldValue);
                var dyList = ServiceHelper.GetService<IQueryService>().GetDynamicObjectCollection(Context, queryPara);
                if (dyList != null && dyList.Count > 0)
                {
                    // 字段已存在
                    if (dyList.Count > 1 || (dyList.Count == 1 && dyList[0][0].ToString() != pkValue))
                    {                        
                        validateContext.AddError(extendDataEntity, new ValidationErrorInfo(
                            "",
                            pkValue,
                            extendDataEntity.DataEntityIndex,
                            extendDataEntity.RowIndex,
                            "E1",
                            fieldName + "重复:" + fldValue + "!",
                            "字段唯一性检查"));
                    }
                }
            }
            
            #endregion
        }
    }
}
