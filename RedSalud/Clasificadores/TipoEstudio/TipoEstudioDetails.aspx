<%@ Page Title="Tipo de Estudio" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TipoEstudioDetails.aspx.cs" Inherits="Clasificadores_TipoEstudio_TipoEstudioDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                    Text="Nuevo Tipo de Estudio">
                </asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" Text="Volver a la Lista de Tipos de Estudio"
                        NavigateUrl="~/Clasificadores/TipoEstudio/TipoEstudioList.aspx">
                    </asp:HyperLink>
                </div>

                <span class="label">Nombre</span>
                <asp:TextBox ID="NombreTextBox" runat="server" CssClass="normalField"
                    MaxLength="100">
                </asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="NombreTextBox"
                        ValidationGroup="TipoEstudio" Display="Dynamic"
                        ErrorMessage="Debe ingregar el Nombre del Tipo de Estudio">
                    </asp:RequiredFieldValidator>
                </div>

                <asp:Label Text="Agrupar en" runat="server" CssClass="label" />
                <asp:DropDownList ID="ParentDDL" runat="server"
                    DataSourceID="ParentODS"
                    CssClass="bigField"
                    DataValueField="TipoEstudioId"
                    DataTextField="Nombre">
                </asp:DropDownList>

                <asp:ObjectDataSource ID="ParentODS" runat="server"
                    TypeName="Artexacta.App.TipoEstudio.BLL.TipoEstudioBLL"
                    OldValuesParameterFormatString="original_{0}"
                    SelectMethod="getTipoEstudioListForDDL"
                    OnSelected="ParentODS_Selected" />

                <div class="buttonsPanel">
                    <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                        OnClick="SaveButton_Click"
                        ValidationGroup="TipoEstudio">
                        <span>Guardar</span>
                    </asp:LinkButton>
                    <asp:HyperLink runat="server" CssClass="secondaryButton"
                        Text="Cancelar"
                        NavigateUrl="~/Clasificadores/TipoEstudio/TipoEstudioList.aspx">
                    </asp:HyperLink>
                </div>

            </div>
            <div class="clear"></div>
        </div>
    </div>
    <asp:Label ID="TipoEstudioHiddenLabel" runat="server" Text="0" Visible="false"></asp:Label>
</asp:Content>

