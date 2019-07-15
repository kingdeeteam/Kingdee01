using System.ComponentModel;
using Kingdee.BOS.Util;
using Kingdee.BOS.Core.DynamicForm;
using Kingdee.BOS.Core.DynamicForm.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;

namespace Kingdee.Samples.PlugInDev.DynamicForm.DynamicForm
{
    [Description("弹出另一个表单")]
    [Kingdee.BOS.Util.HotUpdate]
    public class ShowFormEdit : AbstractDynamicFormPlugIn
    {
        /*
         * 案例演示： 动态表单标识 -- k2fbf554726e74faa9a27106077618f02  
         *                        
         * 创建一个动态表单，在该动态表单上添加一个按钮，点击该按钮后显示另外一个动态表单
         */
        public override void ButtonClick(ButtonClickEventArgs e)
        {
            base.ButtonClick(e);
            if(e.Key.EqualsIgnoreCase("F_PAEZ_Button"))
            {
                var formShowParam = new DynamicFormShowParameter();
                formShowParam.FormId = "PAEZ_BeShownForm";
                //formShowParam.OpenStyle.ShowType = ShowType.MainNewTabPage;
                formShowParam.ParentPageId = this.View.PageId;
                //自定义值
                //formShowParam.CustomParams.Add("自定义Key", "值");
                this.View.ShowForm(formShowParam);
            }
        }
    }
}
