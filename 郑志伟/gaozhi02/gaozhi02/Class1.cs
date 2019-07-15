using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.BOS.Core.Bill.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Oracle.DataAccess;
using Oracle.DataAccess.Client;

namespace gaozhi02
{
    public class Class1:AbstractBillPlugIn
    {
        public override void BarItemClick(BarItemClickEventArgs e)
        {
            if (e.BarItemKey.ToUpper() =="TBBUTTON")
            {
                string s1 = this.View.Model.GetValue("F_QRSR_TEXT").ToString();
                string s2 = this.View.Model.GetValue("F_QRSR_TEXT1").ToString();
                string s3 = this.View.Model.GetValue("F_QRSR_TEXT2").ToString();
                string s4 = this.View.Model.GetValue("F_QRSR_TEXT3").ToString();
                string strConnection = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1) (PORT=1521)))(CONNECT_DATA=(SERVICE_NAME= ORCL)));User Id=system;Password=Zzw19970418";
                string str_sql = "insert into test02 values('" + s1 + "','" + s2 + "','" + s3 + "','" + s4 + "')";
                OracleConnection conn = new OracleConnection(strConnection);
                conn.Open();
                OracleCommand cmd1 = new OracleCommand(str_sql, conn);
                int count = cmd1.ExecuteNonQuery();
                conn.Close();
                if (count > 0)
                {
                    this.View.ShowMessage("保存成功");
                }

            }
        }
    }
}
