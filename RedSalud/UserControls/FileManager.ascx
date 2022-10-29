<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileManager.ascx.cs" Inherits="UserControls_FileManager" %>
<%@ Register Src="~/UserControls/FileUpload.ascx" TagPrefix="RedSalud" TagName="FileUpload" %>

 <asp:Panel ID="AttacheFilePanel" runat="server" CssClass="Default_Popup" GroupingText="Adjuntar Archivo" Visible="true">
     <asp:HyperLink ID="CloseAttacheFileLB" runat="server" Text="cerrar" 
         style="background-color: #fff;padding: 0 1px;position: absolute;right: 15px;top: 8px;" />
     <span class="label">Subir Archivo</span>
     <asp:CheckBox ID="IsVisibleCheckBox" Text="Visible para la parte médica." runat="server" 
         style="display: block;margin: 10px 0;" />
     <RedSalud:FileUpload runat="server" ID="FileUpload" MaxFileInputCount="10" OnFilesLoaded="FileUpload_FilesLoaded" />
 </asp:Panel>
 <asp:Panel ID="FilesListPanel" runat="server" CssClass="Default_Popup" GroupingText="Archivos Adjuntos">
    <asp:HyperLink ID="NewFileHL" runat="server" Text="Adjuntar Archivo" />

    <telerik:RadGrid ID="FileRadGrid" runat="server"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="20" 
        OnItemCommand="FileRadGrid_ItemCommand"
        OnItemDataBound="FileRadGrid_ItemDataBound"
        MasterTableView-DataKeyNames="FileID">
        <MasterTableView>
            <NoRecordsTemplate>
                <asp:Label runat="server" Text="No hay Archivos Adjuntados"></asp:Label>
            </NoRecordsTemplate>
            <Columns>
                <telerik:GridTemplateColumn HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="DeleteImageButton" runat="server"
                            ImageUrl="~/Images/neutral/delete.png"
                            CommandArgument='<%# Eval("FileID") %>'
                            OnClientClick="return confirm('¿Está seguro que desea eliminar el Archivo?');"
                            Width="18px" CommandName="DELETE"
                            ToolTip="Eliminar Archivo"></asp:ImageButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="Nombre" />
                <telerik:GridBoundColumn DataField="SizeForDisplay" HeaderText="Tamaño" />
                <telerik:GridBoundColumn DataField="DateUploadedForDisplay" HeaderText="Fecha de Subida" />
                <telerik:GridTemplateColumn HeaderText="Descargar" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="DownloadImageButton" runat="server"
                            ImageUrl='<%# Eval("Icon") %>'
                            CommandArgument='<%# Eval("FileStoragePath") %>'
                            Width="18px" CommandName='<%# Eval("Name") %>'
                            ToolTip="Descargar Archivo"></asp:ImageButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <asp:HiddenField runat="server" ID="ObjectIdHF"  />
    <asp:HiddenField runat="server" ID="ObjectNameHF"  />

     <asp:HyperLink ID="CloseFileListHL" runat="server" Text="Cerrar"
         style="background-color: #fff;padding: 0 1px;position: absolute;right: 15px;top: 8px;" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=CloseFileListHL.ClientID%>').click(function () {
                hidePopup('#<%=FilesListPanel.ClientID%>');
            });
            $('#<%=CloseAttacheFileLB.ClientID%>').click(function () {
                hidePopup('#<%=AttacheFilePanel.ClientID%>');
            });
            $('#<%=NewFileHL.ClientID%>').click(function () {
                hidePopup('#<%=FilesListPanel.ClientID%>');
                showPopup('#<%=AttacheFilePanel.ClientID%>');
            });
        });
        function openFileManager() {
            hidePopup('#<%=AttacheFilePanel.ClientID%>');
            showPopup('#<%=FilesListPanel.ClientID%>');
        }
    </script>
</asp:Panel>
<asp:HiddenField runat="server" ID="IsVisibleHF"  />
<asp:HiddenField runat="server" ID="CanDeleteFilesHF" Value="true" />
<asp:HiddenField runat="server" ID="UploadedFilesNumberHF" />
<asp:HiddenField runat="server" ID="TotalEstudiosRealizadosNumberHF" />
<asp:HiddenField ID="ShowingModeHiddenField" runat="server" Value="Popup" />
<asp:HiddenField ID="ProveedorMedicoIdHiddenField" runat="server" Value="0" />
<asp:HiddenField ID="EstudioIdCitaLaboHiddenField" runat="server" Value="0" />
<asp:HiddenField ID="SiniestroIdHiddenField" runat="server" Value="0" />