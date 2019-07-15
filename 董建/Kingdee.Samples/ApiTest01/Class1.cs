using Kingdee.BOS.WebApi.ServicesStub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS.ServiceFacade.KDServiceFx;
using Kingdee.BOS.JSON;

namespace ApiTest01
{
    class Class1 : AbstractWebApiBusinessService
    {
        public Class1(KDServiceContext ctx)
            : base(ctx)
        { Console.WriteLine("测试接口22"); }

        public void test02(){
             Console.WriteLine("测试接口");
        }
    }
}
