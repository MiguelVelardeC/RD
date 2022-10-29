<%@ Page Title="Polizas" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PolizaList.aspx.cs" Inherits="Poliza_PolizaList" %>
<%@ Register Src="../UserControls/SearchUserControl/SearchControl.ascx" TagName="SearchControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label1" Text="Lista de Polizas" runat="server" CssClass="title" />
            </div>
            <div class="contentMenu">
                <asp:HyperLink ID="HyperLink1" runat="server"
                    Text="Agregar un nuevo Poliza"
                    NavigateUrl="~/Paciente/PolizaDetails.aspx" />
            </div>
            <div>
                    <asp:Label ID="Label3" Text="Cliente" runat="server" 
                        CssClass="label left" />
                    <asp:DropDownList ID="ClienteDDL" runat="server"
                        DataSourceID="ClienteODS"
                        style="width: 346px; height:20px;"
                        DataValueField="ClienteId"
                        DataTextField="NombreJuridico"
                        AutoPostBack="false">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ClienteODS" runat="server"
                        TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                        OldValuesParameterFormatString="original_{0}"
                        SelectMethod="getRedClienteList"
                        OnSelected="ClienteODS_Selected" />
                    <div class="clear" style="margin-bottom: 5px;"></div>
                <uc1:SearchControl ID="UserSearchControl" runat="server" Title="Buscar: " DisplayHelp="false"
                    DisplayContextualHelp="false" CssSearch="CSearch" CssSearchError="CSearchErrorPanel"
                    CssSearchAdvanced="CSearch_Advanced_Mask"
                    CssSearchHelp="CSearchHelpPanel" ImageErrorUrl="~/Images/neutral/exclamation.png"
                    ImageHelpUrl="~/Images/neutral/Help.png" />
                    <div class="clear" style="margin-bottom: 5px;"></div>
            </div>

            <telerik:RadGrid ID="PolizaRadGrid" runat="server"
                AutoGenerateColumns="false"
                AllowPaging="true"
                PageSize="20"
                OnItemCommand="PolizaRadGrid_ItemCommand"
                MasterTableView-DataKeyNames="PolizaId">
                <MasterTableView>
                    <NoRecordsTemplate>
                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    </NoRecordsTemplate>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Editar">
                            <ItemTemplate>
                                <asp:ImageButton ID="DetailsImageButton" runat="server"
                                    ImageUrl="~/Images/Neutral/select.png"
                                    CommandArgument='<%# Bind("PolizaId") %>'
                                    Width="24px" CommandName="Select"
                                    ToolTip="Editar"></asp:ImageButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <%--<telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                            HeaderText="Eliminar"
                            CommandName="Eliminar"
                            ButtonType="ImageButton"
                            ItemStyle-Width="40px"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="40px"
                            HeaderStyle-HorizontalAlign="Center"
                            ImageUrl="~/Images/neutral/delete.png"
                            ConfirmText="¿Está seguro que desea eliminar la Poliza?" />--%>

                        <telerik:GridBoundColumn DataField="NumeroPoliza" HeaderText="Número Poliza" />
                        <telerik:GridBoundColumn DataField="NombreCompletoPaciente" HeaderText="Asegurado Titular" />
                        <telerik:GridBoundColumn DataField="NombrePlan" HeaderText="Nombre del Plan" />
                        <telerik:GridBoundColumn DataField="LugarForDisplay" HeaderText="Lugar" />
                        <telerik:GridBoundColumn DataField="FechaInicio" HeaderText="Fecha Inicio" DataFormatString="{0:d}" />
                        <telerik:GridBoundColumn DataField="FechaFin" HeaderText="Fecha Fin" />
                        <telerik:GridBoundColumn DataField="Estado" HeaderText="Estado" />
                        <telerik:GridBoundColumn DataField="GastoTotalForDisplay" HeaderText="Gasto Total" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <div class="right" style="margin-bottom: 10px; ">
                Mostrados
                <asp:Literal ID="LoadedFirstRecordLabel" runat="server" Text=""></asp:Literal>
                - 
                <asp:Literal ID="LoadedNumRecordsLabel" runat="server" Text=""></asp:Literal>
                de 
                <asp:Literal ID="TotalDBRecordsLabel" runat="server" Text=""></asp:Literal>
            </div>
            <div id="ButtonsPanel" runat="server" class="buttonsPanel">
                <asp:Button CssClass="button" ID="PrimeroButton" runat="server" Text="Primera" OnClick="PrimeroButton_Click" />
                <asp:Button CssClass="button" ID="AnteriorRapidoButton" runat="server" Text="-5 P&aacute;ginas" OnClick="AnteriorRapidoButton_Click" />
                <asp:Button CssClass="button" ID="AnteriorButton" runat="server" Text="Anterior" OnClick="AnteriorButton_Click" />
                <asp:Button CssClass="button" ID="SiguienteButton" runat="server" Text="Siguiente" OnClick="SiguienteButton_Click" />
                <asp:Button CssClass="button" ID="SiguienteRapidoButton" runat="server" Text="+5 P&aacute;ginas" OnClick="SiguienteRapidoButton_Click" />
                <asp:Button CssClass="button" ID="UltimoButton" runat="server" Text="Ultimo" OnClick="UltimoButton_Click" />
            </div>
            <asp:HiddenField ID="ActivePageHF" runat="server" Value="0" />
            <asp:HiddenField ID="TotalRowsHF" runat="server" Value="0" />
            <asp:HiddenField ID="FirstRowLoadedHF" runat="server" Value="-1" />
            <asp:HiddenField ID="LastRowLoadedHF" runat="server" Value="-1" />
        </div>
    </div>
</asp:Content>

