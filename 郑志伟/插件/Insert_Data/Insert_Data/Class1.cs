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

using Oracle.DataAccess.Client;
using System.Data.Sql;

    

namespace Insert_Data
{
    public class Class1 : AbstractBillPlugIn
    {
        public override void BarItemClick(BarItemClickEventArgs e)
        {

            if (e.BarItemKey == "tbButton")
            {
                
                string s1 = this.View.Model.GetValue("F_QRSR_Text").ToString();
                string s2 = this.View.Model.GetValue("FBILLNO").ToString();
               // SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
               /* scsb.DataSource = "127.0.0.1";
                scsb.UserID = "sa"; ;
                scsb.Password = "Zzw19970418";
                scsb.InitialCatalog = "hello";

                SqlConnection conn = new SqlConnection(scsb.ToString());
                conn.Open();
                string sql = "insert into Table1 values('"+s2+"','"+s1+"')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                int count = cmd.ExecuteNonQuery();*/

                string strConnection = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.10) (PORT=1521)))(CONNECT_DATA=(SERVICE_NAME= ORCL)));User Id=DUPM123;Password=dupm123";        
                string str_sql = "insert into Table1 values('"+s2+"','"+s1+"')";
                OracleConnection conn = new OracleConnection(strConnection);
                conn.Open();
                OracleCommand cmd1 = new OracleCommand(str_sql, conn);
                int count = cmd1.ExecuteNonQuery();
                conn.Close();
                if (count > 0)
                {
                    this.View.ShowMessage("保存成功");
                }

                /*
                    if (e.BarItemKey == "tbButton")
              {
              String id = this.View.Model.GetValue("FBillNo").ToString();
              string pass = this.View.Model.GetValue("F_WWW_Text").ToString();
            
              String constr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.10) (PORT=1521)))(CONNECT_DATA=(SERVICE_NAME= ORCL)));User Id=DUPM123; Password=dupm123";
              OracleConnection conn = new OracleConnection(constr);
              conn.Open();

              string findstr = "insert into Table1 values(" + id+",'"+ pass+ "')";
              //SqlCommand sqlda = new SqlCommand(findstr, conn);sql server
              OracleCommand orada = new OracleCommand(findstr, conn);
              int Succnum = orada.ExecuteNonQuery();
              //结果集保存
              conn.Close();
              this.View.ShowMessage(Succnum.ToString());
            }
                 */
            }
        }
    }
}
