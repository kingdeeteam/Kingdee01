using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.BOS.Core.Bill.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Oracle.DataAccess;
using Oracle.DataAccess.Client;

namespace ZGT3
{
    public class Class1 : AbstractBillPlugIn
    {
        public override void BarItemClick(BarItemClickEventArgs e)
        {
            base.BarItemClick(e);
            if (e.BarItemKey == "tbButton")
            {
                String str1 = this.Model.GetValue("F_WWW_Text").ToString();
                String str2 = this.Model.GetValue("F_WWW_Text1").ToString();
                String constr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1) (PORT=1521)))(CONNECT_DATA=(SERVICE_NAME= ORCL)));User Id=system; Password=123456";
                OracleConnection conn = new OracleConnection(constr);
                conn.Open();
                string findstr = "insert into notes values('" + str1+"','"+str2+"')";
                OracleCommand orada = new OracleCommand(findstr, conn);
                int number = orada.ExecuteNonQuery();
                this.View.ShowMessage("审核成功"+number);
            }
        }
    }
}
