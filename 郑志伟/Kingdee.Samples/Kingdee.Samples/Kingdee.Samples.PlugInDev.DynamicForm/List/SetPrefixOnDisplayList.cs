using System.ComponentModel;
using Kingdee.BOS.Core.List.PlugIn;
using Kingdee.BOS.Core.List.PlugIn.Args;

namespace Kingdee.Samples.PlugInDev.DynamicForm.List
{
    [Description("物料名称增加前缀A")]
    [Kingdee.BOS.Util.HotUpdate]
    public class SetPrefixOnDisplayList : AbstractListPlugIn
    {
        /*
            触发时机：
            列表显示，在获取数据之后，数据发送到前端之前触发，用来格式化列表上的单元格数据

            应用场景：
            插件在加载列表数据时格式化字段值，满足列表显示多样化的需求

            案例演示：单据 BD_MATERIAL
            物料名称在列表显示时增加前缀A
         */
        public override void FormatCellValue(FormatCellValueArgs args)
        {
            base.FormatCellValue(args);
            switch (args.Header.FieldName.ToUpperInvariant())
            {
                case "FNAME":
                    if (args.Value == null) break;
                    args.FormateValue = string.Format("{0}/{1}", "A", args.Value);
                    break;
            }
        }

        public override void PrepareFilterParameter(FilterArgs e)
        {
            base.PrepareFilterParameter(e);
        }
    }
}
