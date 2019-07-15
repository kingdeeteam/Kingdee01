using Kingdee.BOS.Core.DynamicForm.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using System.ComponentModel;

namespace Kingdee.Samples.PlugInDev.DynamicForm.Validate
{
    /*
      演示单据：采购订单
    */

    [Description("审核时数量检测")]
    [Kingdee.BOS.Util.HotUpdate]
    public class CheckCountValidatePlugIn : AbstractOperationServicePlugIn
    {
        /*
         * 操作插件中，需要用到的字段，操作引擎不会扫描到，因此，需要插件自行明确声明需要加载那些字段。
         * 如果插件需要用到的字段，没有在本事件中申明加载，到数据包取字段值时，会触发”属性不存在”的错误。
         */
        public override void OnPreparePropertys(PreparePropertysEventArgs e)
        {
            base.OnPreparePropertys(e);
            e.FieldKeys.Add("FQty");
        }

        public override void OnAddValidators(AddValidatorsEventArgs e)
        {
            base.OnAddValidators(e);
            var validator = new CheckCountValidator();
            e.Validators.Add(validator);
        }
    }
}
