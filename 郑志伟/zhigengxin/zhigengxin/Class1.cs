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

namespace zhigengxin
{
    public class Class1 : AbstractBillPlugIn
    {
        public override void DataChanged(DataChangedEventArgs e)
        {
            if (e.Field.Key.ToUpper() == "F_QRSR_TEXT")
            {
                string s1 = this.View.Model.GetValue("F_QRSR_TEXT").ToString();
                string s2 = "";
                /*SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
                scsb.DataSource = "127.0.0.1";
                scsb.UserID = "sa"; ;
                scsb.Password = "Zzw19970418";
                scsb.InitialCatalog = "AIS20190423145950";

                SqlConnection conn = new SqlConnection(scsb.ToString());
                conn.Open();
                string str = "select F_QRSR_TEXT1 from QRSR_t_Cust_Entry100063 where F_QRSR_TEXT = '" + s1 + "'";
                SqlDataAdapter sda = new SqlDataAdapter(str,conn);
                DataSet ds = new DataSet();

                sda.Fill(ds,"Test");
                conn.Close();

                s2 = ds.Tables[0].Rows[0][0].ToString();*/
                string strConnection = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.10) (PORT=1521)))(CONNECT_DATA=(SERVICE_NAME= ORCL)));User Id=DUPM123;Password=dupm123";  
                string str_sql = "select password from Table1 where id = '" + s1 + "'";
                OracleConnection conn = new OracleConnection(strConnection);
                conn.Open();
                OracleDataAdapter oda = new OracleDataAdapter(str_sql,conn);
                DataSet ds = new DataSet();
                oda.Fill(ds,"Test");
                
                conn.Close();
                s2 = ds.Tables[0].Rows[0][0].ToString();
               // string s2 = s1 + "123";
                this.View.Model.SetValue("F_QRSR_TEXT1", s2);
                this.View.UpdateView("F_QRSR_TEXT1");
            }
            //string s1 = this.View.Model.GetValue("F_QRSR_TEXT");
        }
    }
}
