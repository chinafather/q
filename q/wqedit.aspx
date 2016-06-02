<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wqedit.aspx.cs" Inherits="q.wqedit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
        <script src="../lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script> 
    <script src="../lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
        <script src="ligerComboBox.js" type="text/javascript"></script>
    <style type="text/css">
        body, html {
            height: 100%;
        }

        body {
            padding: 0px;
            margin: 0;
            overflow:scroll;
        }

        .l-link {
            display: block;
            height: 26px;
            line-height: 26px;
            padding-left: 10px;
            text-decoration: underline;
            color: #333;
        }

        .l-link2 {
            text-decoration: underline;
            color: white;
            margin-left: 2px;
            margin-right: 2px;
        }

        .l-layout-top {
            background: #102A49;
            color: White;
        }

        .l-layout-bottom {
            background: #E5EDEF;
            text-align: center;
        }

        #pageloading {
            position: absolute;
            left: 0px;
            top: 0px;
            background: white url('loading.gif') no-repeat center;
            width: 100%;
            height: 100%;
            z-index: 99999;
        }

        .l-link {
            display: block;
            line-height: 22px;
            height: 22px;
            padding-left: 16px;
            border: 1px solid white;
            margin: 4px;
        }

        .l-link-over {
            background: #FFEEAC;
            border: 1px solid #DB9F00;
        }

        .l-winbar {
            background: #2B5A76;
            height: 30px;
            position: absolute;
            left: 0px;
            bottom: 0px;
            width: 100%;
            z-index: 99999;
        }

        .space {
            color: #E7E7E7;
        }
        /* 顶部 */
        .l-topmenu {
            margin: 0;
            padding: 0;
            height: 31px;
            line-height: 31px;
            background: url('lib/images/top.jpg') repeat-x bottom;
            position: relative;
            border-top: 1px solid #1D438B;
        }

        .l-topmenu-logo {
            color: #E7E7E7;
            padding-left: 35px;
            line-height: 26px;
            background: url('lib/images/topicon.gif') no-repeat 10px 5px;
        }

        .l-topmenu-welcome {
            position: absolute;
            height: 24px;
            line-height: 24px;
            right: 30px;
            top: 2px;
            color: #070A0C;
        }

            .l-topmenu-welcome a {
                color: #E7E7E7;
                text-decoration: underline;
            }
    </style>
      <style type="text/css">
           body{ font-size:12px;}
        .l-table-edit {}
        .l-table-edit-td{ padding:4px;}
        .l-button-submit,.l-button-test{width:80px; float:left; margin-left:10px; padding-bottom:2px;}
        .l-verify-tip{ left:230px; top:120px;}
    </style>
    <script type="text/javascript">
        var groupicon = "../lib/ligerUI/skins/icons/communication.gif";
 
        function successclose() {

            //parent.window.initBaseInfoGrid();
            //parent.$.ligerDialog.close(); //关闭弹出窗; //关闭弹出窗
            //
           
            alert("保存成功！");
            parent.$(".l-dialog,.l-window-mask").css("display", "none"); //只隐藏遮罩层
            parent.$("#maingrid").ligerGrid().loadData(true);
            //var dialog = frameElement.dialog; //调用页面的dialog对象(ligerui对象)
            ////alert(dialog.);
            //dialog.close();//关闭dialog 
        }
        function errorclose(errorstr) {

            //parent.window.initBaseInfoGrid();
            //parent.$.ligerDialog.close(); //关闭弹出窗; //关闭弹出窗
            $.ligerDialog.error(errorstr);
           // parent.$(".l-dialog,.l-window-mask").css("display", "none"); //只隐藏遮罩层
            //parent.$("#maingrid").ligerGrid().loadData(true);
            
            //var dialog = frameElement.dialog; //调用页面的dialog对象(ligerui对象)
            ////alert(dialog.);
            //dialog.close();//关闭dialog 
        }
    </script>

    <script type="text/javascript">
        var condition = { fields: [{ name: 'qname', label: '问卷名称', width: 90, type: 'text' }] };
        
      
        function getGridOptions(checkbox) {
            var options = {
                columns: [
                { display: '问卷编号', name: 'qno', align: 'left', width: 100, minWidth: 60 },
                { display: '问卷名称', name: 'qtitle', minWidth: 120, width: 100 },
                ], switchPageSizeApplyComboBox: false,
                data: $.extend({}, jsonData),
                pageSize: 100,
                checkbox: checkbox
            };
            return options;
        }

        
        

</script>
</head>
<body style="padding:10px">






    <form id="form1" runat="server">
        <input name="pid" type="hidden" id="pid" runat="server"   />
         <table cellpadding="0" cellspacing="0" class="l-table-edit" >
            
             <tr>
                <td align="right" class="l-table-edit-td" valign="top">问卷编号:</td>
                <td align="left" class="l-table-edit-td" style="width:160px">
                    <input name="qno" type="text" id="qno"   value="<%=qno %>"  validate="{required:true,minlength:1,maxlength:200,notnull:true}" nullText="不能为空!"/>

                </td><td align="left"></td>
            </tr>  
            <tr>
                <td align="right" class="l-table-edit-td">名称:</td>
                <td align="left" class="l-table-edit-td" style="width:260px" ><input name="qtitle" type="text" id="qtitle"   value="<%=qtitle %>"  validate="{required:true,minlength:1,maxlength:200,notnull:true}" nullText="不能为空!"/></td>
                <td align="left"></td>
            </tr>
             
            
              <tr>
                <td align="right" class="l-table-edit-td" valign="top">备注:</td>
                <td align="left" class="l-table-edit-td" style="width:160px">
                    <textarea name="remark"  rows="5" id="remark" ltype="text"  ><%=remark %></textarea>
                    

                </td><td align="left"></td>
            </tr>  
          
        </table>
 <br />
<input type="submit" value="提交" id="Button1" class="l-button l-button-submit" /> 
<input type="button" value="关闭" class="l-button l-button-test" onclick='parent.$(".l-dialog,.l-window-mask").css("display", "none")'/>

 
    </form>
   


</body>
</html>
