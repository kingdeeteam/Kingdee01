using Kingdee.BOS.Core.Bill.PlugIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS.App.Core;
using Kingdee.BOS.Core.Bill.PlugIn.Args
;using Kingdee.BOS.Orm.DataEntity;
using System.ComponentModel;


namespace Test02.ServicesStub
{
    class BM : AbstractBillPlugIn
    {
         public string  BeforeSave(){

             string str = "test";
             //获取物料分组是否是特定编码规则分组        
             var groupData = this.Model.DataObject["MaterialGroup"];       
             if (groupData != null && groupData is DynamicObject) {            
                 const string specifiedGroupNum = "SpeCodeRule";           
                 var groupNum = ((DynamicObject)groupData)["Number"];          
                 if (groupNum != null && groupNum.ToString().Equals(specifiedGroupNum))  {    
                     //满足特定分组使用自定义编码规则生成单据编号                
                    str= GenerateBillNoById();             
                 }        
             }
             return str;
         }
               public string GenerateBillNoById(){         
                   /*             * 通过下面语句查询到FRULEID的值，得到 FRULEID=5c48033be79374      
                    * * select * from T_BAS_BILLCODERULE_L t where t.fname ='插件调用编码规则';                    
                    * */          
                   BusinessDataService dataService = new BusinessDataService();  
                   var businInfo = View.BillBusinessInfo;       
                   var dataObjs = new DynamicObject[] {};      
                   bool isUpdateMax = true;
                   const string specifiedRuleId = "00226432caadb9c411e3388d21953359";     
                   var billNoList = dataService.GetBillNo(Context, businInfo, dataObjs, isUpdateMax, specifiedRuleId);        
                   return billNoList[0].BillNo;      
               }
    }
}
