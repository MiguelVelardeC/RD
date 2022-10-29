<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FotoPaciente.ascx.cs" Inherits="UserControls_FotoPaciente" %>
<%@ Register Src="~/UserControls/FileUpload.ascx" TagPrefix="RedSalud" TagName="FileUpload" %>
<div>
    <div>
        <asp:Image ID="FotoPAUrl" runat="server" ImageUrl="~/Images/Neutral/paciente.jpg" Width="180px" />
    </div>
    <div style="text-align:center;" class="noPrint">
        <asp:LinkButton ID="EditPhotoLB" Text="cambiar foto" OnClick="EditPhotoLB_Click" runat="server" />
    </div>
    <div class="clear"></div>
    <div id="FileUploadDiv" runat="server" visible="false" class="noPrint">
        <RedSalud:FileUpload ID="FotoPAFileUpload" runat="server" ShowMode="Normal" />
        <asp:LinkButton ID="CancelLinkButton" Text="Cancelar"
            OnClick="CancelLinkButton_Click" runat="server" />
    </div>
    <asp:HiddenField ID="FotoIDHiddenField" runat="server" />
    <asp:HiddenField ID="PacienteIdHF" runat="server" />
</div>