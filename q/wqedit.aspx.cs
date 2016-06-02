using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weegle.FrameWork.DB;
using Weegle.FrameWork.WeegleFrameWork;
using Util;

namespace q
{
    public partial class wqedit : System.Web.UI.Page
    {
        public String qtitle = "";
        public String qno = "";
        public String jsonData = "";
        public String remark = "";
        public String wqid = "";
        public String status = "";

        Database db;

        protected void Page_Load(object sender, EventArgs e)
        {
            wqid = Request["wqid"];
            db = DatabaseFactory.GetDatabase();

            if (Page.IsPostBack)
            {
                save();

            }
            else
            {
                if (!String.IsNullOrEmpty(wqid))
                {
                    LoadData(); ;
                }

            }

        }
       
        private void LoadData()
        {
            if (!String.IsNullOrEmpty(wqid))
            {
                String strsql = "select * from  tblwq where status <> 'D'  and  wqid = '{0}' ";
                strsql = String.Format(strsql, wqid);
                DataView dv = db.Query(strsql);
                if (dv.Count > 0)
                {
                    qtitle = dv[0]["qtitle"].ToString();
                    qno = dv[0]["qno"].ToString();

                    remark = dv[0]["remark"].ToString();
                    
                    status = dv[0]["status"].ToString();
                    
                }
            }
        }
        private void save()
        {

            qtitle = CommonValidate.safeStr(Request["qtitle"]);
            qno = CommonValidate.safeStr(Request["qno"]);

            
            
            remark = CommonValidate.safeStr(Request["remark"]);
            String strsql = "";
            if (!String.IsNullOrEmpty(wqid))
            {
                strsql = " update  tblwq set qtitle='{1}',qno = {2},remark='{3}'  where wqid = '{0}';";
                strsql = String.Format(strsql, wqid, qtitle, qno,remark);
            }
            else
            {

                strsql = @"  insert into  tblwq([wqid]
                                                          ,[qtitle]
                                                          ,[qno]
                                                          ,[createuserid]
                                                          ,[createusername]
                                                          ,[createdate]
                                                          
                                                          ,[status]
                                                          ,[remark]) values(newid(),'{0}',{1},'{2}','{3}', getdate(),'创建','{4}')";

                strsql = String.Format(strsql, qtitle, qno,  Request.Cookies["uid"].Value, HttpUtility.UrlDecode(Request.Cookies["user"].Value), remark);
            }
            try
            {
                db.Execute(strsql);
                ClientScript.RegisterStartupScript(this.GetType(), "okclose", "<script>successclose();</script>");
                
            }
            catch (Exception ex)
            {

                //throw;
            }
            
            //Response.Write("<script>alert();windows.close();</script>");


        }
    }
}