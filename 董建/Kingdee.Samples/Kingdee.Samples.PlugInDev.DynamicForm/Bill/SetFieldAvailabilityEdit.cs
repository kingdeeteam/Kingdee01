using System;
using System.ComponentModel;
using Kingdee.BOS.Core.Bill.PlugIn;

namespace Kingdee.Samples.PlugInDev.DynamicForm.Bill
{
    [Description("设置字段可用性")]
    [Kingdee.BOS.Util.HotUpdate]
    public class SetFieldAvailabilityEdit : AbstractBillPlugIn
    {
        /*
            触发时机：
            单据数据绑定完成后触发

            应用场景：
            数据绑定完成后，设置字段可用性；少量的字段值调整

            案例演示：
            设置字段可用性
        */

        public override void AfterBindData(EventArgs e)
        {
            base.AfterBindData(e);
            // 给长日期字段赋值
            var ctrlKey = "F_PAEZ_Datetime";
            var ctrl = this.View.GetControl(ctrlKey);
            if (ctrl != null)
            {
                // 禁用控件
                ctrl.Enabled = false;
                // 给控件赋值成两天前
                this.Model.SetValue(ctrlKey, DateTime.Now.AddDays(-2).ToString());               
            }
        }
    }
}
