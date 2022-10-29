<%@ Page Title="Medico" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MedicoList.aspx.cs" Inherits="Medico_MedicoList" %>
<%@ Register Src="../UserControls/SearchUserControl/SearchControl.ascx" TagName="SearchControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label1" Text="Lista de Medicos" runat="server" CssClass="title" />
            </div>
            <div class="contentMenu">
                <asp:LinkButton runat="server"
                    ID="CreateUser"
                    OnClick="CreateUser_Click"
                    Text="Agregar un nuevo Usuario" />
                <asp:HyperLink runat="server"
                    Text="Agregar un nuevo Médico"
                    NavigateUrl="~/Medico/MedicoDetails.aspx" />
            </div>
            <div class="contentMenu">
                <asp:Label Text="Para poder crear un Médico, este debe estar registrado en el Sistema como Usuario." runat="server" /><br />
                <asp:Label Text="Cree primero el usuario y a continuación cree al médico." runat="server" />
            </div>
            <div>
                <div style="margin-bottom: 10px; ">
                    <uc1:SearchControl ID="UserSearchControl" runat="server" Title="Buscar: " DisplayHelp="false"
                        DisplayContextualHelp="false" CssSearch="CSearch" CssSearchError="CSearchErrorPanel"
                        CssSearchAdvanced="CSearch_Advanced_Mask"
                        CssSearchHelp="CSearchHelpPanel" ImageErrorUrl="~/Images/neutral/exclamation.png"
                        ImageHelpUrl="~/Images/neutral/Help.png" />
                </div>
                <div class="right" style="margin-bottom: 10px; ">
                    Mostrados
                    <asp:Literal ID="LoadedFirstRecordLabel" runat="server" Text=""></asp:Literal>
                    - 
                    <asp:Literal ID="LoadedNumRecordsLabel" runat="server" Text=""></asp:Literal>
                    de 
                    <asp:Literal ID="TotalDBRecordsLabel" runat="server" Text=""></asp:Literal>
                </div>
                <div style="clear: both; margin-bottom: 5px;">
                    <telerik:RadGrid ID="MedicoRadGrid" runat="server"
                        AutoGenerateColumns="false"
                        AllowPaging="false"
                        OnItemCommand="MedicoRadGrid_ItemCommand"
                        MasterTableView-DataKeyNames="MedicoId">
                        <MasterTableView>
                            <NoRecordsTemplate>
                                <asp:Label runat="server" Text="No hay Medicos Registrados"></asp:Label>
                            </NoRecordsTemplate>
                            <Columns>

                                <telerik:GridButtonColumn UniqueName="SelectCommandColumn"
                                    HeaderText="Seleccionar"
                                    CommandName="Select"
                                    ButtonType="ImageButton"
                                    ItemStyle-Width="24px"
                                    ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="24px"
                                    HeaderStyle-HorizontalAlign="Center"
                                    ImageUrl="~/Images/neutral/select.png" />

                                <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                    HeaderText="Eliminar"
                                    CommandName="Eliminar"
                                    ButtonType="ImageButton"
                                    ItemStyle-Width="40px"
                                    ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="40px"
                                    HeaderStyle-HorizontalAlign="Center"
                                    ImageUrl="~/Images/neutral/delete.png"
                                    ConfirmText="¿Está seguro que desea eliminar el Medico?" />
                                <telerik:GridBoundColumn DataField="UserId" Visible="false" />
                                <telerik:GridBoundColumn DataField="NombreForDisplay" HeaderText="Nombre (ID)" />
                                <telerik:GridBoundColumn DataField="EspecialidadId" Visible="false" />
                                <telerik:GridBoundColumn DataField="Especialidad" HeaderText="Especialidad" />
                                <telerik:GridBoundColumn DataField="Sedes" HeaderText="Sedes" />
                                <telerik:GridBoundColumn DataField="ColegioMedico" HeaderText="Colegio Medico" />
                                <telerik:GridBoundColumn DataField="Estado" HeaderText="Estado" Visible="false" />
                                <telerik:GridTemplateColumn>
                                    <ItemTemplate>
                                        <asp:Literal Text='<%#Eval("Estado").ToString() == "ACTIVO" ? "Activo" : "Inactivo" %>' runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="FechaActualizacion" HeaderText="Fecha de Actualización" />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div id="ButtonsPanel" runat="server" class="buttonsPanel">
                    <asp:Button CssClass="button" ID="PrimeroButton" runat="server" Text="Primera" OnClick="PrimeroButton_Click" />
                    <asp:Button CssClass="button" ID="AnteriorRapidoButton" runat="server" Text="-5 P&aacute;ginas" OnClick="AnteriorRapidoButton_Click" />
                    <asp:Button CssClass="button" ID="AnteriorButton" runat="server" Text="Anterior" OnClick="AnteriorButton_Click" />
                    <asp:Button CssClass="button" ID="SiguienteButton" runat="server" Text="Siguiente" OnClick="SiguienteButton_Click" />
                    <asp:Button CssClass="button" ID="SiguienteRapidoButton" runat="server" Text="+5 P&aacute;ginas" OnClick="SiguienteRapidoButton_Click" />
                    <asp:Button CssClass="button" ID="UltimoButton" runat="server" Text="Ultimo" OnClick="UltimoButton_Click" />
                </div>
            </div>
            <asp:HiddenField ID="ActivePageHF" runat="server" Value="0" />
            <asp:HiddenField ID="TotalRowsHF" runat="server" Value="0" />
            <asp:HiddenField ID="FirstRowLoadedHF" runat="server" Value="-1" />
            <asp:HiddenField ID="LastRowLoadedHF" runat="server" Value="-1" />

        </div>
    </div>
</asp:Content>

