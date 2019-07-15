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
namespace ZGT2
{
    public class Class1 : AbstractBillPlugIn
    {
        public override void DataChanged(DataChangedEventArgs e)
        {
            if (e.Field.Key.ToUpper() == "F_WWW_TEXT")
            {
                String str1 = this.Model.GetValue("F_WWW_Text").ToString();               
                String constr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1) (PORT=1521)))(CONNECT_DATA=(SERVICE_NAME= ORCL)));User Id=system; Password=123456";
                OracleConnection conn = new OracleConnection(constr);
                conn.Open();
                string findstr = "select * from patient where id = " + str1;
                OracleCommand orada = new OracleCommand(findstr, conn);
                Person p1 = new Person();
                using (var dr = orada.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        p1.Name = dr.GetValue(1).ToString();
                        p1.Room= dr.GetValue(2).ToString();
                        p1.Money = dr.GetValue(3).ToString();
                    }
                }
                conn.Close();
                this.View.Model.SetValue("F_WWW_Text1",p1.Name);
                this.View.UpdateView("F_WWW_Text1");
                this.View.Model.SetValue("F_WWW_Text2", p1.Room);
                this.View.Model.SetValue("F_WWW_Text3", p1.Money);
                this.View.UpdateView("F_WWW_Text2");
                this.View.UpdateView("F_WWW_Text3");
            }

        }
    }
}
