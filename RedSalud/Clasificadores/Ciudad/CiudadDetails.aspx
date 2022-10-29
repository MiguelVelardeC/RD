<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CiudadDetails.aspx.cs" Inherits="Clasificadores_Ciudad_CiudadDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                    Text="Nueva Ciudad">
                </asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Volver a la Lista de Ciudades"
                        NavigateUrl="~/Clasificadores/Ciudad/CiudadList.aspx">
                    </asp:HyperLink>
                </div>

                <span class="label">Ciudad Id</span>
                <asp:TextBox ID="CiudadIdTxt" runat="server" CssClass="normalField"
                    MaxLength="10">
                </asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="CiudadRFV" runat="server"
                        ControlToValidate="CiudadIdTxt"
                        ValidationGroup="Ciudad"
                        Display="Dynamic"
                        ErrorMessage="Debe ingregar el Id de la Ciudad">
                    </asp:RequiredFieldValidator>
                </div>

                <span class="label">Nombre</span>
                <asp:TextBox ID="NombreTxt" runat="server" CssClass="normalField"
                    MaxLength="100">
                </asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="NombreRFV" runat="server"
                        ControlToValidate="NombreTxt"
                        ValidationGroup="Ciudad"
                        Display="Dynamic"
                        ErrorMessage="Debe ingregar el Nombre de la Ciudad">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="buttonsPanel">
                    <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                        OnClick="SaveButton_Click"
                        ValidationGroup="Ciudad">
                        <span>Guardar</span>
                    </asp:LinkButton>
                    <asp:HyperLink ID="HyperLink2" runat="server" CssClass="secondaryButton"
                        Text="Cancelar"
                        NavigateUrl="~/Clasificadores/Ciudad/CiudadList.aspx">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <asp:HiddenField ID="CiudadIdHF" runat="server" />
</asp:Content>

