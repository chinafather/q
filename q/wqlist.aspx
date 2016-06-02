<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wqlist.aspx.cs" Inherits="q.wqlist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
        <link href="lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css" />
    <script src="lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>    
    <script src="lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script> 
    <style type="text/css"> 
    body,html{height:100%;}
    body{ padding:0px; margin:0;   overflow:hidden;}  
    .l-link{ display:block; height:26px; line-height:26px; padding-left:10px; text-decoration:underline; color:#333;}
    .l-link2{text-decoration:underline; color:white; margin-left:2px;margin-right:2px;}
    .l-layout-top{background:#102A49; color:White;}
    .l-layout-bottom{ background:#E5EDEF; text-align:center;}
    #pageloading{position:absolute; left:0px; top:0px; background:white url('loading.gif') no-repeat center; width:100%; height:100%;z-index:99999;}
    .l-link{ display:block; line-height:22px; height:22px; padding-left:16px;border:1px solid white; margin:4px;}
    .l-link-over{ background:#FFEEAC; border:1px solid #DB9F00;} 
    .l-winbar{ background:#2B5A76; height:30px; position:absolute; left:0px; bottom:0px; width:100%; z-index:99999;}
    .space{ color:#E7E7E7;}
    /* 顶部 */ 
    .l-topmenu{ margin:0; padding:0; height:31px; line-height:31px; background:url('lib/images/top.jpg') repeat-x bottom;  position:relative; border-top:1px solid #1D438B;  }
    .l-topmenu-logo{ color:#E7E7E7; padding-left:35px; line-height:26px;background:url('lib/images/topicon.gif') no-repeat 10px 5px;}
    .l-topmenu-welcome{  position:absolute; height:24px; line-height:24px;  right:30px; top:2px;color:#070A0C;}
    .l-topmenu-welcome a{ color:#E7E7E7; text-decoration:underline} 

 </style>
    
    <script type="text/javascript">
        var manager;

        $(function ()
        {
            ////工具条
            //$("#toptoolbar").ligerToolBar({
            //    items: [
            //        { text: '新增领用单', id: 'add', click: itemclick, icon: 'add' },
            //        { text: '删除借用单', id: 'delete', click: itemclick, icon: 'modify' }
            //    ]
            //});
           window['g'] = 
            manager = $("#maingrid").ligerGrid({
                columns: [
               { display: '问卷编号', name: 'qno', width: 100, align: 'left' },
                { display: '问卷标题', name: 'qtitle', width: 200, type: 'int', align: 'left' },
                { display: '创建日期', name: 'createdate', width: 120, type: 'int', align: 'left' },
                 { display: '创建人', name: 'createusername', width: 100, type: 'int', align: 'left' },
                 { display: '状态', name: 'status', width: 100, align: 'left' },
                  { display: '备注', name: 'remark', width: 120, align: 'left' },
                
                ], width: '100%', pageSizeOptions: [5, 10, 15, 20], height: '97%',
                url: 'wqlist.aspx?Action=GetData',
                dataAction: 'server', //服务器排序
                usePager: false,       //服务器分页
                pageSize: 20,
                rownumbers:true,
                alternatingRow: false, 
                tree: { columnName: 'name' },
                toolbar: {
                    items: [
                   { text: '新增领用单', id: 'add', click: itemclick, icon: 'add' },
                   { line: true },
                    { text: '删除领用单', id: 'delete', click: itemclick, img: '../lib/ligerUI/skins/icons/delete.gif' },
                      { line: true }
                      <%if (secstr.IndexOf("确认领用") > -1)
                        {%>,
                    { text: '确认领用单', id: 'audit', click: itemclick, icon: 'edit' }
                    <%}%>
                    ]
                },
                onDblClickRow: function (data, rowindex, rowobj) {
                    showView('lyaudit.aspx?lyid=' + data.lyid, '确认领用单')
                    // $.ligerDialog.alert('选择的是' + data.pid);
                }
            }
            );
        });

        function itemclick(item) {

            if (item.id) {
                switch (item.id) {
                    case "add":
                        showView('wqedit.aspx', '新增问卷')
                        return;
                    case "audit":
                        var data = manager.getCheckedRows();
                         
                        var checkedids = [];
                        $(data).each(function () {
                            //checkedids.push(this.lyid);
                            showView('lyaudit.aspx?lyid=' + this.lyid, '编辑问卷')
                           
                        });
                       
                        return;
                    case "Gray":
                        $("#maingrid").addClass("l-grid-gray");
                        return;
                    case "modify":
                        var rowsdata = manager.getCheckedRows();
                        var str = "";
                        $(rowsdata).each(function () {
                            str += this.CustomerID + ",";
                        });
                        if (!rowsdata.length) alert('请选择行');
                        else
                            alert(str);
                        return;
                    case "delete":
                        var data = manager.getCheckedRows();
                        if (data.length == 0)
                            alert('请选择行');
                        else {
                            
                            var checkedIds = [];
                            var checkedids = [];
                            $(data).each(function () {
                                if (this.status != '申请中') {
                                    alert('只能删除申请中的记录');
                                    return false;
                                }
                                checkedids.push(this.lyid);
                                checkedIds.push(this.pname);
                                $.ligerDialog.confirm('确定删除' + checkedIds.join(',') + '?', function (type) {
                                    if(type)
                                        deletep(checkedids[0]);
                                });
                            });

                           
                        }
                        return;
                    case "Excel":
                    case "Word":
                    case "PDF":
                    case "TXT":
                    case "XML":
                        $.ligerDialog.waitting('导出中，请稍候...');
                        setTimeout(function () {
                            $.ligerDialog.closeWaitting();
                            if (item.id == "Excel")
                                $.ligerDialog.success('导出成功');
                            else
                                $.ligerDialog.error('导出失败');
                        }, 1000);
                        return;
                }
            }
            //alert(item.text);
        }
        function showView(src, title) {
         
            $.ligerDialog.open({
                title: title,
                url: src,
                width: $(window).width() * 0.6,
                height: $(window).height() * 0.6
            });

        };
        function deletep(pid) {

            $.ajax({
                loading: '正在删除中...',
                url: 'deally.aspx',
                data: {
                    ObjIds: pid,
                    ajaxaction: 'delete'
                },
                success: function () {
                    $.ligerDialog.success("删除成功!");
                    $("#maingrid").ligerGrid().reload();
                },
                error: function (message) {
                    LG.showError(message);
                }
            });

        }
</script>
</head>
<body  style="padding:0px;">
    <form id="form1" runat="server">
    <div>
        <div id="toptoolbar"></div> 
        <div class="l-clear"></div>
 
        <div id="maingrid"></div>  
    </div>
    </form>
</body>
</html>
