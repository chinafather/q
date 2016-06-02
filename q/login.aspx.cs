using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;
using Weegle.FrameWork.WeegleFrameWork;
using  Weegle.FrameWork.DB;
namespace q
{
    public partial class login : System.Web.UI.Page
    {
        String _ConnectString = ConfigurationManager.AppSettings["ConnectString"];
        String _ProviderName = ConfigurationManager.AppSettings["ProviderName"];
        private Database db =DatabaseFactory.GetDatabase();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                 
                String strsql = "select * from tbluser t where username='{0}' and pwd= '{1}'";
                String login = CommonValidate.safeStr(Request["username"]).Replace(" ","");
                String password = CommonValidate.safeStr(Request["password"]).Replace(" ", "");
                strsql = String.Format(strsql, login, password);
                DataView dv = db.Query(strsql);
                if (dv.Count > 0)
                {
                    Session["user"] = dv[0]["username"].ToString();
                    Session["uid"] = dv[0]["id"].ToString();
                    Response.Cookies["user"].Value = HttpUtility.UrlEncode(dv[0]["username"].ToString());
                    Response.Cookies["uid"].Value = dv[0]["id"].ToString();
                    Response.Redirect("main.aspx");
                }
                else
                {
                    Response.Write("请确认用户名和口令！" + login +"用户！");
                }
                //DataSet ds = new DataSet();
                //ds = OracleHelper.ExecuteDataset(_ConnectString, System.Data.CommandType.Text,
                //       strsql);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    Session["user"] = ds.Tables[0].Rows[0]["user_Name"].ToString();
                //    Response.Redirect("index.aspx");
                //}
            }
            else
            {
                Session.Clear();
            }
        }
    }
}