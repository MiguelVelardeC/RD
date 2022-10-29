<%@ Page Title="Tipo de Enfermedad" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TipoEnfermedadDetails.aspx.cs" Inherits="Clasificadores_TipoEnfermedad_TipoEnfermedadDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                    Text="Nuevo Tipo de Enfermedad">
                </asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" Text="Volver a la Lista de Tipos de Enfermedad"
                        NavigateUrl="~/Clasificadores/TipoEnfermedad/TipoEnfermedadList.aspx">
                    </asp:HyperLink>
                </div>

                <span class="label">Nombre</span>
                <asp:TextBox ID="NombreTextBox" runat="server" CssClass="normalField"
                    MaxLength="100">
                </asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="NombreTextBox"
                        ValidationGroup="TipoEnfermedad" Display="Dynamic"
                        ErrorMessage="Debe ingregar el Nombre del Tipo de Enfermedad">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="buttonsPanel">
                    <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                        OnClick="SaveButton_Click"
                        ValidationGroup="TipoEnfermedad">
                        <span>Guardar</span>
                    </asp:LinkButton>
                    <asp:HyperLink runat="server" CssClass="secondaryButton"
                        Text="Cancelar"
                        NavigateUrl="~/Clasificadores/TipoEnfermedad/TipoEnfermedadList.aspx">
                    </asp:HyperLink>
                </div>

            </div>
            <div class="clear"></div>
        </div>
    </div>
    <asp:Label ID="TipoEnfermedadHiddenLabel" runat="server" Text="0" Visible="false"></asp:Label>
</asp:Content>

