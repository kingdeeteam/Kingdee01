using System.ComponentModel;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Kingdee.BOS.Mobile.PlugIn;
using Kingdee.BOS.Util;

namespace Kingdee.Samples.Mobile.DynamicForm
{
    [Description("移动表单插件演示")]
    [HotUpdate]
    public class MobilePlugInDemo :AbstractMobilePlugin
    {
        public override void ButtonClick(ButtonClickEventArgs e)
        {
            base.ButtonClick(e);
            if (e.Key.EqualsIgnoreCase("F_PAEZ_Button"))
            {
                this.View.ShowMessage("测试弹窗");
            }
        }
    }
}
