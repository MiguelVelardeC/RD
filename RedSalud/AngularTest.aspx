<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AngularTest.aspx.cs" Inherits="AngularTest" %>

<%@ Register Src="~/UserControls/PiezaOdontologia.ascx" TagPrefix="RedSalud" TagName="PiezaOdontologia" %>



<!DOCTYPE html>

<html>
  <head runat="server">
      <script src="Scripts/jquery-1.11.1.min.js"></script>
  </head>
  <body>
      <form runat="server" style="margin: 20px;">
        <div style="float: left; width: 50%;">
            <RedSalud:PiezaOdontologia runat="server" ID="PiezaOdontologia" />
        </div>
      </form>
  </body>
</html>
