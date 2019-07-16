using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS.ServiceFacade.KDServiceFx;
using Kingdee.BOS.WebApi.ServicesStub;
using Kingdee.BOS.JSON;

namespace Kingdee.Samples.Integration.CustomWebApi
{
    public class HelloWorldService : AbstractWebApiBusinessService
    {
        public HelloWorldService(KDServiceContext ctx)
            : base(ctx)
        { }

        /// <summary>
        /// 执行服务，根据参数来处理自己的业务并返回结果
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public JSONArray ExecuteService(string parameter)
        {
            JSONArray jsonArray = new JSONArray();
            jsonArray.Add("Hello World");
            return jsonArray;
        }

    }
}
