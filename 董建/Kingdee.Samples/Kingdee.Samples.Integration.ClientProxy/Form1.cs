using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kingdee.BOS.Authentication;
using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Core.Metadata.FieldElement;
using Kingdee.BOS.Orm.DataEntity;
using Kingdee.BOS.ServiceFacade.KDServiceClient;
using Kingdee.BOS.ServiceFacade.KDServiceClient.BusinessData;
using Kingdee.BOS.ServiceFacade.KDServiceClient.Metadata;
using Kingdee.BOS.ServiceFacade.KDServiceClient.User;
using Kingdee.BOS.ServiceFacade.KDServiceClientFx;

namespace Kingdee.Samples.Integration.ClientProxy
{  
    public partial class Form1 : Form
    {
        UserServiceProxy userServiceProxy = new UserServiceProxy();
        MetadataServiceProxy metaServiceProxy = new MetadataServiceProxy();
        BusinessDataServiceProxy dataServiceProxy = new BusinessDataServiceProxy();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //登录系统
                userServiceProxy.HostURL = txtServer.Text;
                var loginInfo = new Kingdee.BOS.Authentication.LoginInfo();
                loginInfo.AcctID = txtDbId.Text;
                loginInfo.Lcid = 2052;
                loginInfo.Username = txtUser.Text;
                loginInfo.Password = txtPassword.Text;
                var res = userServiceProxy.ValidateUser(txtServer.Text, loginInfo);
                if (res.LoginResultType == LoginResultType.Success)
                {
                    metaServiceProxy.HostURL = txtServer.Text;
                    //据业务对象Id 取得元数据
                    FormMetadata meta = metaServiceProxy.GetFormMetadata("PAEZ_Phone");
                    BusinessInfo info = meta.BusinessInfo;
                    //创建手机数据包
                    DynamicObject row = new DynamicObject(info.GetDynamicObjectType());
                    row["F_PAEZ_Brand"] = txtBrand.Text;
                    row["F_PAEZ_Model"] = txtModel.Text;
                    
                    //保存至数据库
                    dataServiceProxy.HostURL = txtServer.Text;
                    var oResult = dataServiceProxy.SaveData(info, row);
                    if (oResult.IsSuccess)
                    {
                        MessageBox.Show("保存成功！");
                    }
                    else
                    {
                        MessageBox.Show("保存失败!");
                    }

                }
                else
                {
                    MessageBox.Show(string.Format("登录失败！{0}", res.Message));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("保存失败！{0}", ex.Message));
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }        
    }
}
