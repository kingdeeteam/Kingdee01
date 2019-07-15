using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS.App.Core;
using Kingdee.BOS.Core.Bill.PlugIn;
using Kingdee.BOS.Core.Bill.PlugIn.Args;
using Kingdee.BOS.Orm.DataEntity;
using System.ComponentModel;


namespace Test02.ServicesStub
{
    class Plugin
    {
        public string ReBill() { 
         BusinessDataService dataService = new BusinessDataService();  
            var businInfo = View.BillBusinessInfo;       
            var dataObjs = new DynamicObject[] { };    
            bool isUpdateMax = true;     
            const string specifiedRuleId = "5c48033be79374";     
            var billNoList = dataService.GetBillNo(Context, businInfo, dataObjs, isUpdateMax, specifiedRuleId);    
            return businInfo.GetBillNoField().Key + billNoList[0].BillNo;
 
        }

    }
}
