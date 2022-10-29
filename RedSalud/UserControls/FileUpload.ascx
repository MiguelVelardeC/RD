<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileUpload.ascx.cs" Inherits="UserControls_FileUpload" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadProgressManager ID="RadProgressManager1" Runat="server" Skin="Default" />
<telerik:RadUpload ID="RadUpload1" Runat="server" Skin="Default"
        MaxFileInputsCount="5" MaxFileSize="27000000" >
    <Localization Add="Añadir" Clear="Limpiar" Delete="Borrar" Remove="Quitar" Select="Seleccionar" />
</telerik:RadUpload>
<telerik:RadProgressArea ID="RadProgressArea1" Runat="server" Skin="Default"
 DisplayCancelButton="True" 
    ProgressIndicators="TotalProgressBar, TotalProgress, RequestSize, FilesCountBar, FilesCount, SelectedFilesCount, CurrentFileName, TimeElapsed, TimeEstimated, TransferSpeed">
<Localization Uploaded="Uploaded"></Localization>
</telerik:RadProgressArea>
    
<span id="uploadButtonContainer" runat="server">
    <asp:Button ID="UploadButton" runat="server"
        OnClick="UploadButton_Click" CssClass="ruButton ruUpload"
        Text="<%$ Resources: Glossary, UploadButton %>" />
</span>

<asp:HiddenField ID="MaxFileCountHiddenField" runat="server" Value="1" />
<asp:HiddenField ID="InitialFileCountHiddenField" runat="server" Value="1" />

<script type="text/javascript">
    Telerik.Web.UI.RadProgressArea.prototype.get_progressManagerFound = function () { return true; }
    //<![CDATA[
    $(document).ready(function () {
        try{
            $("#<%= RadUpload1.ClientID %> > ul > .ruActions").append($("#<%= uploadButtonContainer.ClientID %>").html());
            $("#<%= uploadButtonContainer.ClientID %>").html("");
            $("#<%= uploadButtonContainer.ClientID %>").hide();

            $("#<%= UploadButton.ClientID %>").on("click", function () {
                var upload = $find("<%= RadUpload1.ClientID %>");
                var fileInputs = upload.getFileInputs();
                var count = 0;
                for (var i = 0; i < fileInputs.length; i++) {
                    if (fileInputs[i].value != "")
                        count++;
                }
                return count > 0;
            });
        } catch (q) {
            console.error(q);
            alert('Error al iniciar los controles para subir la foto');
        }
	});

    function deleteInputFiles() {
        try{
		    var upload = $find("<%= RadUpload1.ClientID %>");
		    var fileInputs = upload.getFileInputs();
		    for (var i = fileInputs.length - 1; i >= 0; i--) {
			    upload.deleteFileInputAt(i);
		    }
		    upload.addFileInput();
        } catch (q) {
            console.error(q);
            alert('Error al quitar el archivo');
        }
	}
    //]]>
</script>
<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    var originalClientSubmit;

    if (!originalClientSubmit) {
        originalClientSubmit =
Telerik.Web.UI.RadProgressManager.prototype._clientSubmitHandler;
    }

    Telerik.Web.UI.RadProgressManager.prototype._clientSubmitHandler = function (e) {
        if (!prm._postBackSettings.async) {
            originalClientSubmit.apply(this, [e]);
        }
    }
    </script>