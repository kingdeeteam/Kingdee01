using System;
using System.Collections.Generic;
using System.ComponentModel;
using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Core.Bill.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Kingdee.BOS.ServiceHelper;

namespace Kingdee.Samples.PlugInDev.DynamicForm.Bill
{
    [Description("菜单按钮行为干预")]  
    [Kingdee.BOS.Util.HotUpdate]
    public class BarItemInterveneEdit : AbstractBillPlugIn
    {

        /*
            触发时机：
            主菜单单击时触发

            应用场景：
            通常动态表单在设计时主菜单都会配置平台内置提供的服务，有时有些用户可能需要在调用平台内置功能前进行权限验证、数据检查等等，用户也可以在该事件中通过添加e.Cancel=true取消平台内置功能，完全自定义处理逻辑.

            案例演示：                       
            添加菜单按钮，点击后保存当前单据
            点击复制按钮时，已禁用的记录禁止复制
            添加菜单按钮，弹出消息框显示所有仓库的名称
        */
        public override void BarItemClick(BarItemClickEventArgs e)
        {
            base.BarItemClick(e);
            switch (e.BarItemKey)
            {
                //添加菜单按钮，点击后保存当前单据
                case "tbSaveTest":
                    var result = BusinessDataServiceHelper.Save(this.Context, this.View.BillBusinessInfo, this.Model.DataObject);
                    if (result.IsSuccess)
                    {
                        this.View.ShowMessage("单据保存成功！");
                        this.Model.DataChanged = false;
                    }
                    return;
                //点击复制按钮时，已禁用的记录禁止复制
                case "tbCopy":
                    var fieldValue = this.Model.GetValue("FForbidStatus");                    
                    if (fieldValue != null && fieldValue.ToString() == "B")
                    {
                        this.View.ShowMessage("当前数据已被禁用，禁止复制！");
                        e.Cancel = true;
                    }
                    return;
                //弹出消息框显示所有仓库的名称
                case "tbQueryStock":
                    var selectItems = SelectorItemInfo.CreateItems("FName");
                    var dyList = BusinessDataServiceHelper.Load(this.View.Context, "BD_STOCK", selectItems, null);
                    var warehouseNameList = new List<string>();
                    foreach (var dy in dyList)
                    {
                        warehouseNameList.Add(dy["Name"].ToString());
                    }
                    this.View.ShowMessage("仓库名称:" + string.Join(",", warehouseNameList));
                    return;
            }
        }
    }
}
