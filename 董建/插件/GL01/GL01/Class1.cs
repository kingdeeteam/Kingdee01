using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS.App.Data;
using Kingdee.BOS.Contracts;
using Kingdee.BOS.Core.Report;
using Kingdee.BOS.Util;
using Kingdee.K3.FIN.AP.App.Report;
using System.ComponentModel;
using Kingdee.BOS.Orm.DataEntity;

namespace GL01
{
    public class Class1 : APSumReportService
    {
        public string[] customRptTempTableNames;
        //public override void BuilderReportSqlAndTempTable(IRptParams filter, string tableName)
        //{
        //    base.BuilderReportSqlAndTempTable(filter, tableName);
        //    string strSql = "update TMP146B7C23AC5411E989ADD0C6375 set ";
        //    DynamicObject customFil = filter.FilterParameter.CustomFilter;
        //    string sWhere = string.Empty;
        //    sWhere = string.Format("Where FNumber = '{0}' AND FText = '{1}'", customFil["FNumber"], customFil["Ftext"]);
        //    DBUtils.Execute(this.Context,strSql);
            
        //}
        public override void BuilderReportSqlAndTempTable(IRptParams filter, string tableName)
        {
            //base.BuilderReportSqlAndTempTable(filter, tableName);
            //创建临时表，用于存放自己的数据
            IDBService dbservice = Kingdee.BOS.App.ServiceHelper.GetService<IDBService>();
            customRptTempTableNames = dbservice.CreateTemporaryTableName(this.Context, 1);
            string strTable = customRptTempTableNames[0];
            //调用基类方法，获取初步查询结果到时临时表
            base.BuilderReportSqlAndTempTable(filter, strTable);


            //获取快捷页签上的字段数据包
            DynamicObject customFil = filter.FilterParameter.CustomFilter;

            //过滤条件
            string sWhere = string.Empty;
            //创建人不为空时
            if (customFil["F_WWW_Text"] != null)
            {
                 
                string supplierNmae = customFil["F_WWW_Text"].ToString();
                sWhere = string.Format(" where t1.fsuppliergroupname='{0}' ", supplierNmae);
            }
            //对初步查询结果进行处理，然后写回基类默认的存放查询结果的临时表
            StringBuilder sb = new StringBuilder();
            string strSql = "/*dialect*/Select t1.*  into {0} "
            + "  from {1} t1 ";
            if (!sWhere.IsNullOrEmpty())
            {
                strSql = strSql + sWhere;
            }
            sb.AppendFormat(strSql, tableName, strTable);

           
            DBUtils.Execute(this.Context, sb.ToString());
        }
        public override void CloseReport()
        {
            if(customRptTempTableNames.IsNullOrEmptyOrWhiteSpace()){
                return;
            }
            IDBService dbService = Kingdee.BOS.App.ServiceHelper.GetService<Kingdee.BOS.Contracts.IDBService>();
            dbService.DeleteTemporaryTableName(this.Context,customRptTempTableNames);
            
            base.CloseReport();
        }
    }
}
