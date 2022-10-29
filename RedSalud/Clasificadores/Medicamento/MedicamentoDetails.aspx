<%@ Page Title="Medicamento" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MedicamentoDetails.aspx.cs" Inherits="Clasificadores_Medicamento_MedicamentoDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                    Text="Nuevo Medicamento">
                </asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" Text="Volver a la Lista de Medicamentos"
                        NavigateUrl="~/Clasificadores/Medicamento/MedicamentoList.aspx">
                    </asp:HyperLink>
                </div>

                <span class="label">Nombre</span>
                <asp:TextBox ID="NombreTextBox" runat="server" CssClass="normalField"
                    MaxLength="200">
                </asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="NombreTextBox"
                        ValidationGroup="Medicamento" Display="Dynamic"
                        ErrorMessage="Debe ingregar el Nombre de la Medicamento">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="buttonsPanel">
                    <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                        OnClick="SaveButton_Click"
                        ValidationGroup="Medicamento">
                        <span>Guardar</span>
                    </asp:LinkButton>
                    <asp:HyperLink runat="server" CssClass="secondaryButton"
                        Text="Cancelar"
                        NavigateUrl="~/Clasificadores/Medicamento/MedicamentoList.aspx">
                    </asp:HyperLink>
                </div>

            </div>
            <div class="clear"></div>
        </div>
    </div>
    <asp:Label ID="MedicamentoHiddenLabel" runat="server" Text="0" Visible="false"></asp:Label>
</asp:Content>

