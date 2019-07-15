using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS.Core.Bill.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Kingdee.BOS;
using Kingdee.BOS.Core;

using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using Oracle.DataAccess.Client;


namespace Testt02
{
    public class Class1:AbstractBillPlugIn
    {
        public override void BarItemClick(BarItemClickEventArgs e)
           {
 	          base.BarItemClick(e);
              if (e.BarItemKey == "tbButton")
              {
              String id = this.View.Model.GetValue("FBillNo").ToString();
              string pass = this.View.Model.GetValue("F_WWW_Text").ToString();
             // string constr = "server=.;database=ddd;uid=sa;pwd=123";
            //  SqlConnection sqlc = new SqlConnection(constr);
             // sqlc.Open();
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
           }
         
    }
}
