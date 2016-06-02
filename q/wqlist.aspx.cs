using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Linq;
using Weegle.FrameWork.DB;
using Weegle.FrameWork.WeegleFrameWork;
using System.Data;
namespace q
{
    public partial class wqlist : System.Web.UI.Page
    {
        Database db;
        public String secstr = "";
        void getsecstr()
        {
            DataView dvSec = DatabaseFactory.GetDatabase().Query(String.Format("select  usersec from tbluser where id='{0}'", Request.Cookies["uid"].Value));
            if (dvSec.Count > 0)
            {
                secstr = dvSec[0]["usersec"].ToString();
                if (Request.Cookies["user"].Value == "admin")
                {
                    secstr = "[申购审核][领用][借用][申购申请][库存管理][盘盈盘亏][部门][人员][报表][物品管理][学校基础信息][系统管理员][确认领用][确认借用]";
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            db = DatabaseFactory.GetDatabase();
            if (Request.Params["Action"] == "GetData")
            {
                GetData();
                Response.End();
            }
        }
        class Node
        {
            public string wqid { get; set; }
            public string qno { get; set; }
            public string qtitle { get; set; }
            public string createdate { get; set; }
            
            public string createusername { get; set; }
            public string createuserid { get; set; }
            public string status { get; set; }
            
            public String remark { get; set; }
        }
        void GetData()
        {

            String strsql = "select * from tbluser where id = '{0}'";
            strsql = String.Format(strsql, Request.Cookies["uid"].Value);
            DataView dv = db.Query(strsql);
            String userRank = "";
            if (dv.Count > 0)
            {
                userRank = dv[0]["rank"].ToString();
            }
            else
            {
                Response.End();
            }
            //if (userRank == "3")  //普通用户
            //{
            //    strsql = "select a.id,b.pno,b.pname,a.pnum,a.dsprice as price,b.dw,b.guige,a.askusername,a.askdate,a.auditusername,a.lydate,a.status,a.plydate,a.remark from [dbo].[tblout] a,[dbo].[tblproduct] b where a.pid = b.id  and a.askuser = '{0}' and a.status <> 'D' order by  a.askdate desc";
            //    strsql = String.Format(strsql, Request.Cookies["uid"].Value.ToString());
            //}
            //else
            //{
            //    strsql = "select a.id,b.pno,b.pname,a.pnum,a.dsprice as price,b.dw,b.guige,a.askusername,a.askdate,a.auditusername,a.lydate,a.status,a.plydate,a.remark from [dbo].[tblout] a,[dbo].[tblproduct] b where a.pid = b.id  and a.status <> 'D'    order by  a.askdate desc";
            //    strsql = String.Format(strsql);
            //}
            strsql = "select * from tblwq order by createdate desc ";

            dv = db.Query(strsql);

            string sortname = Request.Params["sortname"];
            string sortorder = Request.Params["sortorder"];
            int page = Convert.ToInt32(Request.Params["page"]);
            int pagesize = Convert.ToInt32(Request.Params["pagesize"]);
            IList<Node> list = new List<Node>();
            var total = dv.Count;
            for (var i = 0; i < dv.Count; i++)
            {
                list.Add(new Node()
                {
                    wqid = dv[i]["wqid"].ToString(),
                    qno = dv[i]["qno"].ToString(),
                    qtitle = dv[i]["qtitle"].ToString(),

                    createusername = dv[i]["createusername"].ToString(),
                    createdate = dv[i]["createdate"].ToString(),
                    createuserid = dv[i]["createuserid"].ToString(),
                    
                    status = dv[i]["status"].ToString(),
                    
                    remark = dv[i]["remark"].ToString()
                });
            }
            //if (sortorder == "desc")
            //    list = list.OrderByDescending(c => c.uid).ToList();
            //else
            //    list = list.OrderBy(c => c.uid).ToList();

            IList<Node> targetList = new List<Node>();
            for (var i = 0; i < dv.Count; i++)
            {
                if (i >= (page - 1) * pagesize && i <= (page==0?1:page) * pagesize)
                {
                    targetList.Add(list[i]);
                }
            }
            var griddata = new { Rows = list, Total = total };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }
    }
}