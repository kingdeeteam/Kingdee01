using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.BOS.Util;
using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Orm.DataEntity;
using Kingdee.BOS.BusinessEntity.BusinessFlow;
using Kingdee.BOS.Core.BusinessFlow;
using Kingdee.BOS.Core.BusinessFlow.PlugIn;
using Kingdee.BOS.Core.Metadata.FieldElement;
using Kingdee.BOS.Core.BusinessFlow.PlugIn.Args;


namespace Kingdee.Samples.PlugInDev.DynamicForm.WriteBack
{
    [Description("反写源单转出数量时，同步累加反写源单的其它数量")]
    [HotUpdate]
    public class WriteBackOtherQtyPlugIn : AbstractBusinessFlowServicePlugIn
    {

        /// <summary>
        /// 源单的其它数量字段Key
        /// </summary>
        private const string fldKeyOtherQtySrc = "F_PAEZ_QtyOther";   

        /// <summary>
        /// 反写插件需要监控的反写规则Id
        /// 来自于select FID from t_Bf_Writebackrule 
        ///       where FSOURCEFORMID = 'PAEZ_TakeOtherEntrySource'
        /// 反写规则相关表：
        /// t_Bf_Writebackrule_l 
        /// t_bf_writebackrulecust
        /// </summary>
        private const string writeBackRuleId = "9e89c122-7f4d-485f-9e11-a6af4e16cdc9";


        public override void BeforeTrackBusinessFlow(BeforeTrackBusinessFlowEventArgs e)
        {
            base.BeforeTrackBusinessFlow(e);
        }

        /// <summary>
        /// 在本事件中，指定要求加载反写规则未涉及到，不会自动加载的字段：其它数量
        /// </summary>
        /// <param name="e"></param>
        public override void AfterCustomReadFields(AfterCustomReadFieldsEventArgs e)
        {
            base.AfterCustomReadFields(e);
            if (e.Rule.Id.EqualsIgnoreCase(writeBackRuleId))
            {
                //追加加载“其它数量”字段
                e.AddFieldKey(fldKeyOtherQtySrc);
            }
        }

        /// <summary>
        /// 此事件，在反写了源单值之后触发：同步反写“其它数量”
        /// </summary>
        /// <param name="e"></param>
        public override void AfterCommitAmount(AfterCommitAmountEventArgs e)
        {
            base.AfterCommitAmount(e);
            if (e.Rule.Id.EqualsIgnoreCase(writeBackRuleId))
            {                
                // 取当前反写的源单行
                DynamicObject sourceActiveRow = e.SourceActiveRow;
                //取得“其它数量”字段
                var fieldSrc = e.SourceBusinessInfo.GetField(fldKeyOtherQtySrc);
                if (fieldSrc == null) return;
                //取得“其它数量”原来的值
                var valFldSrc = fieldSrc.GetFieldValue(sourceActiveRow);
                var otherQtyOld = valFldSrc != null ? Convert.ToDecimal(valFldSrc) : 0;

                // 取本次反写的转出数量
                // 引用Kingdee.BOS.BusinessEntity.dll
                WSRow<Id> writeBackSourceRow = e.WriteBackSourceRow as WSRow<Id>;
                decimal tranOutQty = writeBackSourceRow.Val;
                
                //将转出数量累加后重新赋值
                fieldSrc.DynamicProperty.SetValue(sourceActiveRow, otherQtyOld + tranOutQty);
            }

        }
    }
}
