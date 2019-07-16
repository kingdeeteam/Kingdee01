using System;
using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.BOS.Core.Validation;
using Kingdee.BOS.Orm.DataEntity;

namespace Kingdee.Samples.PlugInDev.DynamicForm.Validate
{
    /// <summary>
    /// 数量超额校验
    /// </summary>
    public class CheckCountValidator :AbstractValidator
    {   
        public override void InitializeConfiguration(ValidateContext validateContext, Context ctx)
        {
            base.InitializeConfiguration(validateContext, ctx);            
            if (validateContext.BusinessInfo != null)
            {
                EntityKey = validateContext.BusinessInfo.GetEntity(0).Key;
            }
        }

        public override void Validate(ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Context ctx)
        {
            if (dataEntities == null || dataEntities.Length <= 0)
            {
                return;
            }

            foreach (var et in dataEntities)
            {
                var dyEntrys = et.DataEntity["POOrderEntry"] as DynamicObjectCollection;
                if (dyEntrys != null)
                {                    
                    var count = 0;
                    foreach (var dy in dyEntrys)
                    {
                        count += Convert.ToInt32(dy["Qty"]);
                    }
                    if (count > 10)
                    {
                        validateContext.AddError(et, new ValidationErrorInfo(
                      "",
                      Convert.ToString(et.DataEntity[0]),
                      et.DataEntityIndex,
                      et.RowIndex,
                      "E1",
                     string.Format("单据{0}数量超额，总数量不能超过10个，当前物料总数量为:{1}", et.BillNo, count),
                      "数量超额检查"));
                    }
                }
            }
        }
    }
}
