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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test01.ServicesStub
{
    public class Class1 : AbstractWebApiBusinessService
    {
        public Class1(KDServiceContext ctx)
            : base(ctx)
        {

        }
        public JSONArray ExecuteService(string parameter, string parameter2)
        {
            string stree = parameter.ToString();
            string stree2 = parameter2.ToString();
            JObject jsooo = (JObject)JsonConvert.DeserializeObject(stree2);
            //连接数据库   
            //连接字符串  
            string constr = "server=.;database=AIS20190425233758;uid=sa;pwd=123";
            SqlConnection sqlc = new SqlConnection(constr);
            sqlc.Open();

            //查看，若使用批量查询，需将where条件做字符串拼接，在for循环中加二次遍历
            string findstr = "select * from "+stree+" where FBILLNO = '" + jsooo["Number"].ToString()+"'";
            SqlDataAdapter sqlda = new SqlDataAdapter(findstr, sqlc);
            //结果集保存
            DataSet ds = new DataSet();
            sqlda.Fill(ds, "test");
            sqlc.Close();
            JSONObject jsob = new JSONObject();
            JSONArray jsonArray = new JSONArray();
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                string ssh = ds.Tables[0].Columns[i].ToString();
                string sss = ds.Tables[0].Rows[0][ssh].ToString();

                jsob.Add(ssh, sss);
                jsonArray.Add("{" + jsob.ElementAt(i).Key + ":" + jsob.ElementAt(i).Value + "}");
            }
            return jsonArray;
        }
    }
}




