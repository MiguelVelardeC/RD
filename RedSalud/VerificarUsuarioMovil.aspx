<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VerificarUsuarioMovil.aspx.cs" Inherits="VerificarUsuarioMovil" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 750px;
            height: 582px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%; padding-top: 50px;">
            <tr>
                <td style="width: 20%"></td>
                <td style="width: 60%; align-content: center; text-align: center;">
                    <asp:Panel runat="server" ID="pnlRegistroExitoso">
                        <p>
                            <img src="Images/logoVerificarUsuario.png" style="height:100%; max-height:120px"/>
                        </p>
                        <asp:Label runat="server" ID="lblMensaje" Text="lo lograste" Font-Bold="true">

                        </asp:Label>
                        <p>
                            <img src="Images/completado.png" style="height:70px; width:70px;"/>
                        </p>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlLoSentimos">
                        <p>
                            <img src="Images/logoVerificarUsuario.png" style="height:100%; max-height:120px"/>
                        </p>
                        <asp:Label runat="server" ID="lblBadMensaje" Text="Lo sentimos mucho." Font-Bold="true"></asp:Label>
                        <p>
                            <img src="Images/advertencia.png" style="height:70px; width:70px;" />
                        </p>
                    </asp:Panel>
                </td>
                <td style="width: 20%"></td>
            </tr>
        </table>
        <div>
        </div>
    </form>

</body>
</html>
