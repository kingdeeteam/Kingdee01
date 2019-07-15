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

namespace Test02.ServicesStub
{
    public class Class1 : AbstractWebApiBusinessService
    {
        public Class1(KDServiceContext ctx)
            : base(ctx)
        {

        }
        public JSONArray ExecuteService(string parameter, string parameter2)
        {
            //接收表名
            string stree = parameter.ToString();
            //接收数据
            string stree2 = parameter2;
            //封装成json对象
            JObject jsooo = (JObject)JsonConvert.DeserializeObject(stree2);
            //定义json单数据对象
            JToken jto = jsooo;
            string stree3 = "";
           foreach(JProperty jtop in jto){
               if(stree3==""){
               stree3 = stree3 + jtop.Name;
               }
               else{
                   stree3 = stree3+"," + jtop.Name;
               }
           }
            string stree4 = "";
           foreach(JProperty jtop in jto){
               if (stree4 == "")
               {
                   stree4 = stree4 + "'"+jtop.Value+"'";
               }
               else
               {
                   stree4 = stree4 + ",'" + jtop.Value + "'";
               }
           }
            //连接数据库   
            //连接字符串  
           string constr = "server=.;database=AIS20190425233758;uid=sa;pwd=123";
            SqlConnection sqlc = new SqlConnection(constr);
            sqlc.Open();

            string findstr = "insert into "+stree+"("+ stree3+") values("+stree4+")";
            SqlCommand sqlda = new SqlCommand(findstr, sqlc);
            int Succnum = sqlda.ExecuteNonQuery();
            //结果集保存
            sqlc.Close();
            JSONObject jsob = new JSONObject();
            JSONArray jsonArray = new JSONArray();
            jsonArray.Add("保存成功"+ Succnum+new BM().GenerateBillNoById());
            return jsonArray;
        }
    }
}
