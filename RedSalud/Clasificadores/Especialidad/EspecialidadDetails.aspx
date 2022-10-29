<%@ Page Title="Tipo de Enfermedad" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EspecialidadDetails.aspx.cs" Inherits="Clasificadores_Especialidad_EspecialidadDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                    Text="Nueva Especialidad">
                </asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" Text="Volver a la Lista de Especialidades"
                        NavigateUrl="~/Clasificadores/Especialidad/EspecialidadList.aspx">
                    </asp:HyperLink>
                </div>

                <span class="label">Nombre</span>
                <asp:TextBox ID="NombreTextBox" runat="server" CssClass="normalField"
                    MaxLength="100">
                </asp:TextBox>
                <span class="label">Estado</span>
                <asp:DropDownList runat="server" ID="EstadoDropDownList" CssClass="normalField">
                    <asp:ListItem Value="1" Text="Activo" />
                    <asp:ListItem Value="0" Text="Inactivo" />
                </asp:DropDownList>
                <span class="label">Tiempo de atención (en minutos)</span>
                <asp:TextBox runat="server" ID="TiempoAtencion" MaxLength="3"></asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="NombreTextBox"
                        ValidationGroup="Especialidad" Display="Dynamic"
                        ErrorMessage="Debe ingregar el Nombre de la Especialidad">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="buttonsPanel">
                    <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                        OnClick="SaveButton_Click"
                        ValidationGroup="Especialidad">
                        <span>Guardar</span>
                    </asp:LinkButton>
                    <asp:HyperLink runat="server" CssClass="secondaryButton"
                        Text="Cancelar"
                        NavigateUrl="~/Clasificadores/Especialidad/EspecialidadList.aspx">
                    </asp:HyperLink>
                </div>

            </div>
            <div class="clear"></div>
        </div>
    </div>
    <asp:Label ID="EspecialidadHiddenLabel" runat="server" Text="0" Visible="false"></asp:Label>
</asp:Content>

