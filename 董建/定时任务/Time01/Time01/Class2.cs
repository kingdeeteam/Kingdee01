using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.BOS.Core.Bill.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using System.ComponentModel;
using Kingdee.BOS.Contracts;
using Kingdee.BOS.ServiceHelper;

namespace Time01
{
    public class Class2 : IScheduleService
    {
        public void Run(Context ctx, Schedule schedule)
        {
            DBServiceHelper.Execute(ctx, "/*dialect*/update WWW_t_Cust_Entry100038 set F_WWW_TEXT4 = '二次更新'");
        }

    }
}