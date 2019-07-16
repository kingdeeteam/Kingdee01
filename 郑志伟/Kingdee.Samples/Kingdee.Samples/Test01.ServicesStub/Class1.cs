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
using Kingdee.Samples.Integration.WebApi;
using System.Runtime.Serialization.Json;
namespace Test01.ServicesStub
{
    public class Class1 : AbstractWebApiBusinessService
    {
        public Class1(KDServiceContext ctx)
            : base(ctx)
        {
        }
        public JSONArray ExecuteService(string parameter,string parameter2)
        {
            string sss = parameter.ToString();
            string sss1 = parameter2.ToString();

            ApiClient client = new ApiClient("http://localhost/K3Cloud/");
            var loginResult = client.Login(
                  "5cbeb87deb77cd",
                  "demo",
                  "zzw19970418",
                  2052);
            // 登陆成功

            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "127.0.0.1";
            scsb.UserID = "sa"; ;
            scsb.Password = "Zzw19970418";
            scsb.InitialCatalog = "AIS20190423145950";

            JSONObject jo = JSONObject.Parse(sss);
            string[] arr = jo.Keys.ToArray();
            string tableNo = "";
            if(arr[0].ToString() == "Number"){
                object[] obj = jo.Values.ToArray();
                string[] arr1 = new string[obj.Length];
                obj.CopyTo(arr1,0);
                tableNo = arr1[0];
            }else
	        {
                  return null;
	        }
             SqlConnection conn = new SqlConnection(scsb.ToString());

                conn.Open();

            //QRSR_t_Cust_Entry100006  "+sss+"
                string str = "select * from " + sss1 + " where FBILLNO = '" + tableNo + "'";

           //string str = "select * from QRSR_t_Cust_Entry100006 where FBILLNO = '" + sss + "'";
                SqlCommand cmd = new SqlCommand(str,conn);
                SqlDataReader sdr = cmd.ExecuteReader();
            
            //string s1 = sdr["FID"].ToString();
                JSONArray jsonArray = new JSONArray();
            //string result1 = sdr.ToString();
            //jsonArray.Add(conn.ToString());
                while (sdr.Read())
                {
                    for (int i = 0; i < sdr.FieldCount; i++)
                    {
                        jsonArray.Add("{" + sdr.GetName(i) + ":" + sdr[i] + "}");
                    }
                } 

            //string sJson = "{\"Number\":\"1003\"}";
           // String result = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.View", new object[] { "kd070397c5fa245b28895ed66bb0e7d5a", sJson });
            
            //jsonArray.Add(sdr);
            jsonArray.Add(sss1);
            jsonArray.Add(jo);
            jsonArray.Add(tableNo);
            return jsonArray;
        }
    }
} 