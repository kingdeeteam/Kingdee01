using System.ComponentModel;
using Kingdee.BOS.Core.Bill.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;

namespace Kingdee.Samples.PlugInDev.DynamicForm.Bill
{
    [Description("显示字段变更前后的值")]
    [Kingdee.BOS.Util.HotUpdate]
    public class ShowChangedFieldValueEdit : AbstractBillPlugIn
    {
        /*
            触发时机：
            字段值更新之后触发

            应用场景：
            字段值更新之后可能需要级联触发修改其他字段，针对字段值改变做一些附加处理。

            案例演示：弹出消息框显示文本框变更前后的值
            特别注意：文本框字段应勾选即时触发值更新事件
        */
        public override void DataChanged(DataChangedEventArgs e)
        {
            base.DataChanged(e);
            var ctrlKey = "F_PAEZ_Text";
            if (e.Field.Key == ctrlKey)
            {
                var msg = string.Format("{0}的值被改变了\r\n原值：{1}\r\n新值：{2}"
                    , ctrlKey, e.OldValue, e.NewValue);
                this.View.ShowMessage(msg);
            }
        }
    }
}
