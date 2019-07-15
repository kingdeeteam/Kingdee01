using System;
using System.ComponentModel;
using Kingdee.BOS.Util;
using Kingdee.BOS.Core.DynamicForm;
using Kingdee.BOS.Core.DynamicForm.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Kingdee.BOS.Core.DynamicForm.PlugIn.ControlModel;
using Kingdee.BOS.Orm.DataEntity;

namespace Kingdee.Samples.PlugInDev.DynamicForm.DynamicForm
{
    /*
     * 表单：PAEZ_HelpfulObject 常用对象演示
     *
     * 
     * 
     */

    [Description("插件开发常用对象")]
    [Kingdee.BOS.Util.HotUpdate]
    public class HelpfulObjectEdit : AbstractDynamicFormPlugIn
    {
        public override void ButtonClick(ButtonClickEventArgs e)
        {
            if (e.Key.EqualsIgnoreCase("F_PAEZ_ButtonView"))
            {
                //View对象
                var editor = this.View.GetFieldEditor<DecimalFieldEditor>("F_PAEZ_Decimal", 0);
                editor.Enabled = false;

                this.View.ShowMessage("已设置小数字段F_PAEZ_Decimal为不可用了");
            }
            else if (e.Key.EqualsIgnoreCase("F_PAEZ_ButtonModel"))
            {
                //Model对象
                var currRowIndex = this.Model.GetEntryCurrentRowIndex("F_PAEZ_Entity");
                var currRowCount = this.Model.GetEntryRowCount("F_PAEZ_Entity");
                var textValue = "";
                var decValue = "";
                var rows = this.Model.DataObject["PAEZ_K34b83d88"] as DynamicObjectCollection;
                if (currRowIndex >= 0)
                {
                    var currRow = rows[currRowIndex];
                    textValue = currRow["F_PAEZ_Text"] != null ? currRow["F_PAEZ_Text"].ToString() : "";
                    decValue = currRow["F_PAEZ_Decimal1"].ToString();
                }

                this.View.ShowMessage(string.Format("当前分录行索引:{0},文本值:{1},小数值:{2},当前分录行数量:{3}", currRowIndex, textValue, decValue, currRowCount));

            }
            else if (e.Key.EqualsIgnoreCase("F_PAEZ_NewEntryRow"))
            {
                //Model对象
                this.Model.CreateNewEntryRow("F_PAEZ_Entity");
            }
            else if (e.Key.EqualsIgnoreCase("F_PAEZ_ButtonGetSet"))
            {
                //Model对象
                var v1 = Convert.ToDecimal(this.Model.GetValue("F_PAEZ_Decimal"));
                this.Model.SetValue("F_PAEZ_Decimal", v1 + 1);
                var v2 = Convert.ToDecimal(this.Model.GetValue("F_PAEZ_Decimal"));
                this.View.ShowMessage(string.Format("原值{0},设置后{1}", v1, v2));
            }
        }
    }
}
