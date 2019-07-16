using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using Kingdee.BOS;
using Kingdee.BOS.Util;
using Kingdee.BOS.Core.Report;
using Kingdee.BOS.Contracts;
using Kingdee.BOS.Contracts.Report;
using Kingdee.BOS.App.Data;
using Kingdee.BOS.Orm.DataEntity;

namespace Kingdee.Samples.PlugInDev.Report
{
    [Description("报表扩展字段")]
    [HotUpdate]
    public class ExtendReportFieldPlugIn : SimpleReportPlugIn
    {
        //临时表名
        string tmpTbName = string.Empty;

        public override void BuilderReportSqlAndTempTable(IRptParams filter, string tableName)
        {
            //创建临时表用于存放基类产生的数据
            IDBService dbSrv = ServiceFactory.GetDBService(this.Context);
            tmpTbName = dbSrv.CreateTemporaryTableName(this.Context);
            //调用基类方法并将数据存入临时表
            base.BuilderReportSqlAndTempTable(filter, tmpTbName);

            var sql = string.Format(@"
select t.*, cl.FName as FCurrencyName into {0}
from {1} t 
left join T_BD_CURRENCY_L cl on t.FLOCALCURRID=cl.FCURRENCYID and cl.FLOCALEID=2052", tableName, tmpTbName);

            DBUtils.Execute(this.Context, sql);
        }

        public override void CloseReport()
        {
            base.CloseReport();
            if (tmpTbName.IsNullOrEmptyOrWhiteSpace())
            {
                return;
            }
            //删除自定义临时表
            IDBService dbSrv = ServiceFactory.GetDBService(this.Context);
            dbSrv.DeleteTemporaryTableName(this.Context, new string[] { tmpTbName });
        }

        public override ReportHeader GetReportHeaders(IRptParams filter)
        {
            var header = base.GetReportHeaders(filter);
            var currency = header.AddChild("FCurrencyName", new LocaleValue("币别"), SqlStorageType.Sqlnvarchar);
            currency.ColIndex = 10;
            return header;
        }
    }
}
