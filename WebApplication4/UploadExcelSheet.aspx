<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadExcelSheet.aspx.cs" Inherits="WebApplication4.UploadExcelSheet" %>

<!DOCTYPE html>

<html>
<head>
<title>Pie Chart For Department wise Sales figure</title>
     <link href="/css/Common.css" rel="stylesheet" type="text/css">
    <script type="text/javascript" language="javascript" src="/js/jquery-1.10.2.js"></script>
    
   <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
   <script type="text/javascript">
       google.charts.load('current', { packages: ['corechart'] });
       google.load("visualization", "1", { 'packages': ['corechart']});
   </script>
</head>
<body>
 <form id="form1" runat="server">
   <div>
   <input name="file" type="file" id="inpExcel" runat="server"  /> 
   <asp:Button ID="uploadExcel" Text="Generate PieChart" runat="server" OnClick="UploadExcel_Click" />
       <span class="hide error errorFile"> Please select a file to upload</span>
       <asp:HiddenField ID="hdnData" runat="server" />
       </div>
     </form>
<div id="chartContainer" style="width: 550px; height: 400px; margin: 0 auto"></div>
</body> 
 <script type="text/javascript" src="/js/UploadExcel.js"></script> <script type="text/javascript">
     UploadExcel.excelData = $('#hdnData').val();
     UploadExcel.pageLoad();
     if (UploadExcel.excelData != "") {
         google.charts.setOnLoadCallback(UploadExcel.drawPieChart());
     }
 </script>
</html>