<%@ Page Title="Red Medica" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RedMedicaDetails.aspx.cs" Inherits="RedMedica_RedMedicaDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                    Text="Nueva Red Medica">
                </asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Volver a la Lista de Redes Medicas"
                        NavigateUrl="~/RedMedica/RedMedicaList.aspx">
                    </asp:HyperLink>
                </div>

                <span class="label">Código</span>
                <asp:TextBox ID="CodigoTxt" runat="server" CssClass="normalField"
                    MaxLength="10">
                </asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="CodigoRFV" runat="server" 
                        ControlToValidate="CodigoTxt"
                        ValidationGroup="RedMedica" 
                        Display="Dynamic"
                        ErrorMessage="Debe ingregar el Código de la Red Medica" />
                </div>

                <span class="label">Nombre</span>
                <asp:TextBox ID="NombreTxt" runat="server" CssClass="normalField"
                    MaxLength="100">
                </asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="NombreTxt"
                        ValidationGroup="RedMedica" 
                        Display="Dynamic"
                        ErrorMessage="Debe ingregar el Nombre de la Red Medica" />
                </div>

                <div class="buttonsPanel">
                    <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                        OnClick="SaveButton_Click"
                        ValidationGroup="RedMedica">
                        <span>Guardar</span>
                    </asp:LinkButton>
                    <asp:HyperLink ID="HyperLink2" runat="server" CssClass="secondaryButton"
                        Text="Cancelar"
                        NavigateUrl="~/RedMedica/RedMedicaList.aspx">
                    </asp:HyperLink>
                </div>
            </div>
            
            <asp:HiddenField ID="RedMedicaIdHF" runat="server" />
        </div>
    </div>
</asp:Content>

