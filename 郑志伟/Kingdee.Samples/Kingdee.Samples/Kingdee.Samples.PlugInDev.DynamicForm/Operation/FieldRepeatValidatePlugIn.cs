using Kingdee.BOS.Core.DynamicForm.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using System.ComponentModel;

namespace Kingdee.Samples.PlugInDev.DynamicForm.Operation
{
    /*
     演示单据：A员工  k6650ee518edf412087940cee92355a53  
    */

    [Description("校验员工文本字段值不能重复")]
    [Kingdee.BOS.Util.HotUpdate]
    public class FieldRepeatValidatePlugIn : AbstractOperationServicePlugIn
    {
        /*
            触发时机：
            在单据数据加载后，数据校验前触发；

            应用场景：
            用于注册自定义的操作校验器，增加校验；或者移除预置的校验器，避开校验；

            案例演示：
            校验员工的文本字段非空情况下不能重复
        */
        public override void OnAddValidators(AddValidatorsEventArgs e)
        {
            base.OnAddValidators(e);
            var validator = new FieldRepeatValidator();
            validator.FieldKey = "F_PAEZ_Text1";
            e.Validators.Add(validator);
        }
    }
}
