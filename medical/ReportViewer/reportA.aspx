﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reportA.aspx.cs" Inherits="medical.ReportViewer.reportA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="false" SizeToReportContent="true">
        </rsweb:ReportViewer> 
       
    </div>
    </form>
</body>
</html>
