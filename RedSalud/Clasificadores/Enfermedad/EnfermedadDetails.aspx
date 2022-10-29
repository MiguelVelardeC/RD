<%@ Page Title="Enfermedad" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EnfermedadDetails.aspx.cs" Inherits="Clasificadores_Enfermedad_EnfermedadDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                    Text="Nueva Enfermedad">
                </asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" Text="Volver a la Lista de Enfermedades"
                        NavigateUrl="~/Clasificadores/Enfermedad/EnfermedadList.aspx">
                    </asp:HyperLink>
                </div>

                <span class="label">Código</span>
                <asp:TextBox ID="CodigoTextBox" runat="server" CssClass="normalField"
                    MaxLength="10">
                </asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CodigoTextBox"
                        ValidationGroup="Enfermedad" Display="Dynamic"
                        ErrorMessage="Debe ingregar el Código de la Enfermedad">
                    </asp:RequiredFieldValidator>
                </div>

                <span class="label">Nombre</span>
                <asp:TextBox ID="NombreTextBox" runat="server" CssClass="normalField"
                    MaxLength="200">
                </asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="NombreTextBox"
                        ValidationGroup="Enfermedad" Display="Dynamic"
                        ErrorMessage="Debe ingregar el Nombre de la Enfermedad">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="buttonsPanel">
                    <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                        OnClick="SaveButton_Click"
                        ValidationGroup="Enfermedad">
                        <span>Guardar</span>
                    </asp:LinkButton>
                    <asp:HyperLink runat="server" CssClass="secondaryButton"
                        Text="Cancelar"
                        NavigateUrl="~/Clasificadores/Enfermedad/EnfermedadList.aspx">
                    </asp:HyperLink>
                </div>

            </div>
            <div class="clear"></div>
        </div>
    </div>
    <asp:Label ID="EnfermedadHiddenLabel" runat="server" Text="0" Visible="false"></asp:Label>
</asp:Content>

