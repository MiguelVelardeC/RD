<%@ Page Title="Protocolos" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProtocoloDetails.aspx.cs" Inherits="Clasificadores_Protocolo_ProtocoloDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                    Text="Nuevo Protocolo">
                </asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" Text="Volver a la Lista de Protocolos"
                        NavigateUrl="~/Clasificadores/Protocolo/ProtocoloList.aspx">
                    </asp:HyperLink>
                </div>

                <span class="label">Nombre de la Enfermedad</span>
                <asp:TextBox ID="NombreTextBox" runat="server" CssClass="bigField"
                    MaxLength="100">
                </asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="NombreTextBox"
                        ValidationGroup="Protocolo" Display="Dynamic"
                        ErrorMessage="Debe ingregar el Nombre de la Enfermedad">
                    </asp:RequiredFieldValidator>
                </div>

                <span class="label">Tipo de Enfermedad</span>
                <asp:DropDownList ID="TipoEnfermedadDropDownList" runat="server"
                    CssClass="bigField"
                    DataTextField="Nombre"
                    DataValueField="TipoEnfermedadId"
                    DataSourceID="TipoEnfermedadDataSource"
                    OnDataBound="TipoEnfermedadDropDownList_DataBound">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="TipoEnfermedadDataSource" runat="server"
                    TypeName="Artexacta.App.TipoEnfermedad.BLL.TipoEnfermedadBLL"
                    OnSelected="TipoEnfermedadDataSource_Selected"
                    SelectMethod="getTipoEnfermedadList">
                </asp:ObjectDataSource>
                <div class="validators">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="TipoEnfermedadDropDownList"
                        ValidationGroup="Protocolo" Display="Dynamic"
                        ErrorMessage="Debe seleccionar un Tipo de Enfermedad">
                    </asp:RequiredFieldValidator>
                </div>

                <span class="label">Descripción del Protocolo</span>
                <asp:TextBox ID="DescipcionTextBox" runat="server" CssClass="biggerField"
                    TextMode="MultiLine" Rows="5">
                </asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="DescipcionTextBox"
                        ValidationGroup="Protocolo" Display="Dynamic"
                        ErrorMessage="Debe ingregar la Descripcion del Protocolo">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="buttonsPanel">
                    <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                        OnClick="SaveButton_Click"
                        ValidationGroup="Protocolo">
                        <span>Guardar</span>
                    </asp:LinkButton>
                    <asp:HyperLink runat="server" CssClass="secondaryButton"
                        Text="Cancelar"
                        NavigateUrl="~/Clasificadores/Protocolo/ProtocoloList.aspx">
                    </asp:HyperLink>
                </div>

            </div>
            <div class="clear"></div>
        </div>
    </div>
    <asp:Label ID="ProtocoloHiddenLabel" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="TipoEnfermedadSelectedHiddenLabel" runat="server" Visible="false"></asp:Label>
</asp:Content>

