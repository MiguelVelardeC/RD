<%@ Page Title="Presentación de medicamento" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PresentacionDetails.aspx.cs" Inherits="Clasificadores_Presentacion_PresentacionDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
      <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                    Text="Nueva Presentación de medicamento">
                </asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Volver a la Lista de Presentaciones de medicamentos"
                        NavigateUrl="~/Clasificadores/Presentacion/PresentacionList.aspx">
                    </asp:HyperLink>
                </div>
                
                <span class="label">Presentacion Id</span>
                <asp:TextBox ID="PresentacionIdTxt" runat="server" CssClass="normalField"
                    MaxLength="10">
                </asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="PresentacionIdTxt"
                        ValidationGroup="Presentacion" 
                        Display="Dynamic"
                        ErrorMessage="Debe ingregar el Id de la Presentación">
                    </asp:RequiredFieldValidator>
                </div>

                <span class="label">Nombre</span>
                <asp:TextBox ID="NombreTxt" runat="server" CssClass="normalField"
                    MaxLength="100">
                </asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="NombreTxt"
                        ValidationGroup="Presentacion" 
                        Display="Dynamic"
                        ErrorMessage="Debe ingregar el Nombre de la Presentación">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="buttonsPanel">
                    <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                        OnClick="SaveButton_Click"
                        ValidationGroup="Presentacion">
                        <span>Guardar</span>
                    </asp:LinkButton>
                    <asp:HyperLink ID="HyperLink2" runat="server" CssClass="secondaryButton"
                        Text="Cancelar"
                        NavigateUrl="~/Clasificadores/Presentacion/PresentacionList.aspx">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <asp:HiddenField ID="TipoMedicamentoHF" runat="server" />
</asp:Content>

