using Kingdee.BOS.WebApi.ServicesStub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS.ServiceFacade.KDServiceFx;
using Kingdee.BOS.JSON;
using Kingdee.BOS;
using Kingdee.BOS.WebApi.Client;
using System.Data.SqlClient;//用于SQL Sever数据访问的命名空间
using System.Data;               //DataSet类的命名空间
using System.Windows.Forms;  //DataGridView控件类的命名空间
using Kingdee.BOS.App.Core;
using Kingdee.BOS.Core.Bill.PlugIn;
using Kingdee.BOS.Core.Bill.PlugIn.Args;
using Kingdee.BOS.Orm.DataEntity;
namespace Test02.ServicesStub
{
    public class Class1 : AbstractWebApiBusinessService
    {
        public Class1(KDServiceContext ctx)
            : base(ctx)
        {
        }

        private string GenerateBillNoById()
        {
            BusinessDataService dataService = new BusinessDataService();
            
            return "";
        }

        public JSONArray ExecuteService(string parameter,string parameter2)
        {
            string s1 = parameter.ToString();
            string s2 = parameter2.ToString();


            ApiClient client = new ApiClient("http://localhost/K3Cloud/");
            var loginResult = client.Login(
                 "5cbeb87deb77cd",
                 "demo",
                 "zzw19970418",
                 2052);
            // 登陆成功

            JSONObject jo = JSONObject.Parse(s1);
            string[] FieldName = jo.Keys.ToArray();

            

            string sql_insert = "";
            StringBuilder sb = new StringBuilder(sql_insert);
            sb.Append("INSERT INTO "+ s2 + " (");
            for (int i = 0; i < FieldName.Length - 1; i++)
            {
                sb.Append(FieldName[0] + ",");
            }
            sb.Append(FieldName[FieldName.Length - 1]);
            sb.Append(") VALUES (");
            object[] sbj = jo.Values.ToArray();
            string[] array = new string[sbj.Length];
            sbj.CopyTo(array, 0);
            for (int i = 0; i < array.Length - 1; i++)
            {
                sb.Append("'" + array[i] + "'" + ",");
            }
            sb.Append("'" + array[array.Length - 1] + "')");
           
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "127.0.0.1";
            scsb.UserID = "sa"; ;
            scsb.Password = "Zzw19970418";
            scsb.InitialCatalog = "AIS20190423145950";
            //scsb.ToString()
            SqlConnection conn = new SqlConnection(scsb.ToString());
            conn.Open();
            //string sql = "select * from  QRSR_t_Cust_Entry100006 where FBILLNO ='1001'";
            //string sql1 = "INSERT INTO QRSR_t_Cust_Entry100006 VALUES ('Gates', 'Bill', 'Xuanwumen 10', 'Beijing')";
            SqlCommand cmd = new SqlCommand(sb.ToString(),conn);
            int count = cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
            string result = "";
            if(count>0)
            {
                result = "success";
            }else{
                result = "unsuccess";
            }
            //int i = cmd.ExecuteNonQuery();
            //SqlDataReader sqr = cmd.ExecuteReader();
            

            //string sql_insert = "INSERT INTO QRSR_t_Cust_Entry100006 (LastName, Address) VALUES ('Wilson', 'Champs-Elysees')";
           /* string sql_insert = "";
            StringBuilder sb = new StringBuilder(sql_insert);
            sb.Append("INSERT INTO QRSR_t_Cust_Entry100006 (");
            for (int i = 0; i < sqr.FieldCount-1; i++)
            {
                sb.Append(sqr.GetName(i)+",");
            }
            sb.Append(sqr.GetName(sqr.FieldCount-1));
            sb.Append(") VALUES (");
*/
            
            //string sJson = "{\"Model\":{\"FBillNo\":\"1111\"}}";
            //String result = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Save",
            //new object[] { "kd070397c5fa245b28895ed66bb0e7d5a", sJson });

            JSONArray jsonArray = new JSONArray();
            jsonArray.Add(result);
           // jsonArray.Add(sqr);
            jsonArray.Add(sb.ToString());
            return jsonArray;

        }
    }
}
