using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kingdee.BOS.WebApi.Client;

namespace Kingdee.Samples.Integration.WebApi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var res = DoCallByTextJson();
            MessageBox.Show(string.Format("返回结果：{0}", res));
        }

        /// <summary>
        /// 根据JSON参数框中的输入的参数，进行测试
        /// </summary>
        /// <returns></returns>
        private string DoCallByTextJson()
        {
            try
            {
                // K/3 Cloud业务站点URL
                ApiClient client = new ApiClient(this.txtServer.Text);

                // 调用登录接口：
                // 参数说明：
                // dbid     : 数据中心id。到管理中心数据库搜索：
                //            select FDataCenterId, * from T_BAS_DataCenter
                // userName : 用户名
                // password ：原始密码（未加密）
                // loid     : 语言id，中文为2052，中文繁体为3076，英文为1033
                var loginResult = client.Login(
                    this.txtDbId.Text,
                    this.txtUser.Text,
                    this.txtPassword.Text,
                    2052);

                string result = "登录失败，请检查与站点地址、数据中心Id，用户名及密码！";

                // 登陆成功
                if (loginResult)
                {
                    string service = string.Empty;
                    string operation = this.cmbOperation.SelectedItem.ToString();
                    switch (operation)
                    {
                        case "BatchSave":
                            {// 批量保存
                                service = "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.BatchSave";
                                break;
                            }
                        case "保存":
                            {// 保存
                                service = "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Save";
                                break;
                            }
                        case "查看":
                            {// 查看（加载）
                                service = "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.View";
                                break;
                            }
                        case "提交":
                            {// 提交
                                service = "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Submit";
                                break;
                            }
                        case "审核":
                            {// 审核
                                service = "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Audit";
                                break;
                            }
                        case "反审核":
                            {// 反审核
                                service = "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.UnAudit";
                                break;
                            }
                        case "删除":
                            {// 删除
                                service = "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Delete";
                                break;
                            }
                        case "下推":
                            {// 下推
                                service = "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Push";
                                break;
                            }
                        case "自定义服务":
                            {
                                service = "Kingdee.Samples.Integration.CustomWebApi.HelloWorldService.ExecuteService,Kingdee.Samples.Integration.CustomWebApi";
                                return client.Execute<string>(service,new object[] { });
                            }
                    }
                    
                    result = client.Execute<string>( service, new object[] { this.txtFormId.Text, this.txtData.Text });
                }

                return result;
            }
            catch (Exception e)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("程序运行遇到了未知的错误：");
                sb.Append("错误提示：").AppendLine(e.Message);
                sb.Append("错误堆栈：").AppendLine(e.StackTrace);
                return sb.ToString();
            }
        }
    }
}
