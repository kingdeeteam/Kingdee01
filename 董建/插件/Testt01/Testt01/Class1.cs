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

namespace Testt01
{
    public class Class1:AbstractBillPlugIn
    {
        public override void DataChanged(DataChangedEventArgs e)
        {
            if (e.Field.Key.ToUpper()== "F_WWW_TEXT")
            {
                String str1 = this.Model.GetValue("F_WWW_Text").ToString();
                //连接数据库   
                //连接字符串  
               // string constr = "server=.;database=ddd;uid=sa;pwd=123";
                //SqlConnection sqlc = new SqlConnection(constr);
               // sqlc.Open();

                //查看，若使用批量查询，需将where条件做字符串拼接，在for循环中加二次遍历
              
               // SqlDataAdapter sqlda = new SqlDataAdapter(findstr, sqlc);
                //结果集保存
                String constr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.10) (PORT=1521)))(CONNECT_DATA=(SERVICE_NAME= ORCL)));User Id=DUPM123; Password=dupm123";
                OracleConnection conn = new OracleConnection(constr);
                conn.Open();
                string findstr = "select * from Table1 where id = " + str1;
                //SqlCommand sqlda = new SqlCommand(findstr, conn);sql server
                OracleCommand orada = new OracleCommand(findstr, conn);
               // int Succnum = orada.ExecuteNonQuery();
                string str2="11";
                using (var dr = orada.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        str2 = dr.GetValue(1).ToString();
                    }
                }

                //结果集保存
                conn.Close();
                //DataSet ds = new DataSet();
                //orada.Fill(ds, "test");
               // String str2 = ds.Tables[0].Rows[0][0].ToString();


                this.View.Model.SetValue("F_WWW_Text1", str2);
                this.View.UpdateView("F_WWW_Text1");
            }

        }
       }
           
    }
