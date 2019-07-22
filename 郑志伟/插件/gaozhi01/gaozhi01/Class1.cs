using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;//用于SQL Sever数据访问的命名空间
using System.Data;               //DataSet类的命名空间
using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.BOS.Core.Bill.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Oracle.DataAccess;
using Oracle.DataAccess.Client;

namespace gaozhi01
{
    public class Class1 : AbstractBillPlugIn
    {
        public override void DataChanged(DataChangedEventArgs e)
        {
            if (e.Field.Key.ToUpper() == "F_QRSR_TEXT")
            {
                string s1 = this.View.Model.GetValue("F_QRSR_TEXT").ToString();
                string s2 = "";
                string strConnection = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1) (PORT=1521)))(CONNECT_DATA=(SERVICE_NAME= ORCL)));User Id=system;Password=Zzw19970418";
                string str_sql = "select * from test01 where p_id = '" + s1 + "'";
                OracleConnection conn = new OracleConnection(strConnection);
                conn.Open();
               OracleDataAdapter oda = new OracleDataAdapter(str_sql, conn);

                DataSet ds = new DataSet();
                oda.Fill(ds, "TTx");
                s2 = ds.Tables[0].Rows[0][1].ToString();
                string s3 = ds.Tables[0].Rows[0][2].ToString();
                string s4 = ds.Tables[0].Rows[0][3].ToString();
                string s5 = ds.Tables[0].Rows[0][4].ToString();
                this.View.Model.SetValue("F_QRSR_TEXT1", s2);
                this.View.Model.SetValue("F_QRSR_Text2", s3);
                this.View.Model.SetValue("F_QRSR_TEXT3", s4);
                this.View.Model.SetValue("F_QRSR_TEXT4", s5);
                this.View.UpdateView("F_QRSR_TEXT1");
                this.View.UpdateView("F_QRSR_TEXT2");
                this.View.UpdateView("F_QRSR_TEXT3");
                this.View.UpdateView("F_QRSR_TEXT4");
                
            }
        }
    }
}
