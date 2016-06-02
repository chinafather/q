using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;
using Weegle.FrameWork.WeegleFrameWork;
using Weegle.FrameWork.DB;
using System.Data;

namespace q
{
    public partial class main : System.Web.UI.Page
    {
        private Database db = DatabaseFactory.GetDatabase();
        public String systemName = "";
        public String secstr = "";
        public String userName = "";

        void getsecstr()
        {
            DataView dvSec = DatabaseFactory.GetDatabase().Query(String.Format("select  usersec from tbluser where id='{0}'", Request.Cookies["uid"].Value));
            if (dvSec.Count > 0)
            {
                secstr = dvSec[0]["usersec"].ToString();
                if (Request.Cookies["user"].Value == "admin")
                {
                    secstr = "[问卷管理][人员][报表]";
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            bind();
            getsecstr();
        }


        private void bind()
        {
            //String strsql = "select * from tblschool";
            //DataView dv = db.Query(strsql);
            //if (dv.Count > 0)
            //{
            //    systemName = dv[0]["schoolName"].ToString();
            //}
            //else
            //{
            //    systemName = "系统主页";
            //}
            systemName = "系统主页";
        }
    }
}