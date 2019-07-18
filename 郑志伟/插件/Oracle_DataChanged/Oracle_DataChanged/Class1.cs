using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Data;

namespace Oracle_DataChanged
{
    public class Class1
    {
        private String Source;
        private String UserID;
        private String Password;

        public Class1()
        {

            //数据源字符串其中192.168.1.21是你oracle数据库的机器ip,port是你oracle数据库的端口号,home2是你oracle数据库的服务名
            Source = "(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=192.168.0.10)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)))";
            UserID = "DUPM123";           //用户名字符串
            Password = "dupm123";  //密码
        }

        public OracleConnection GetConn()
        {
            OracleConnection OracleConn = new OracleConnection("Data Source=" + Source + "; User Id=" + UserID + ";Password=" + Password + ";Integrated Security=no;");
            OracleConn.Open();
            return OracleConn;
        }
    }
}
