using System.ComponentModel;
using Kingdee.BOS.Core.List.PlugIn;
using Kingdee.BOS.Core.List.PlugIn.Args;
using Kingdee.BOS.Core.Metadata;

namespace Kingdee.Samples.PlugInDev.DynamicForm.List
{
    [Description("创建状态的记录字体颜色为红")]
    [Kingdee.BOS.Util.HotUpdate]
    public class SetRedColorOnCreatedList : AbstractListPlugIn
    {
        /*
            触发时机：
            列表显示，在获取数据之后，数据发送到前端之前触发，用来格式化列表上的单元格数据的显示效果

            应用场景：
            插件在加载列表数据时格式化显示样式

            案例演示：
            物料名称在列表显示时将状态为创建的记录的数据设置显示的前景色为红色
         */
        public override void OnFormatRowConditions(ListFormatConditionArgs args)
        {            
            base.OnFormatRowConditions(args);
            if (args.DataRow["FDocumentStatus"].ToString() == "A")
            {
                var fc = new FormatCondition();
                fc.ForeColor = "#" + System.Drawing.Color.Red.ToArgb().ToString("X6");
                fc.ApplayRow = false; //适用整行
                fc.Key = "FNAME";
                args.FormatConditions.Add(fc);
            }
        }
    }
}
