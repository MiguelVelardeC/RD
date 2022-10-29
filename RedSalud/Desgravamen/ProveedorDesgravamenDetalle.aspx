<%@ Page Title="Proveedor Desgravamen" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProveedorDesgravamenDetalle.aspx.cs" Inherits="Desgravamen_ProveedorDesgravamen" %>

<%@ Register TagPrefix="RedSalud" TagName="FileUpload" Src="~/UserControls/FileUpload.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Proveedor Desgravamen</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" NavigateUrl="~/Desgravamen/ProveedorDesgravamenLista.aspx"
                        Text="Volver a la Lista de Proveedores Desgravamen">
                    </asp:HyperLink>
                </div>
                <asp:HiddenField id="ProveedorHiddenField" Value="0" runat="server"/>
                <asp:HiddenField ID="UserIDHF" runat="server" Value=""/>
                <asp:HiddenField ID="UserNameHF" runat="server" />
                <span class="label">Usuario</span>
                <telerik:RadComboBox ID="UserRadComboBox" runat="server" 
                                        ShowMoreResultsBox="true" EnableVirtualScrolling="true" 
                                        AutoPostBack="false" EnableLoadOnDemand="true" CssClass="bigField"
                                        SelectedValue='<%# UserIDHF.Value %>'
                                        OnDataBinding="InsertRadComboBox_DataBinding">
                                        <WebServiceSettings Method="GetUsuarios" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                </telerik:RadComboBox>
                <span class="label" id="Username" runat="server"></span>
                <div class="validation">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserRadComboBox"
                        ErrorMessage="Debe ingresar el Nombre de Usuario"
                        Display="Dynamic" ValidationGroup="PA">
                    </asp:RequiredFieldValidator>
                </div>

                <span class="label">Nombre</span>
                <asp:TextBox ID="NombreTextBox" runat="server" CssClass="bigField"></asp:TextBox>
                <div class="validation">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="NombreTextBox"
                        ErrorMessage="Debe ingresar el Nombre"
                        Display="Dynamic" ValidationGroup="PA">
                    </asp:RequiredFieldValidator>
                </div>

                <span class="label">Ciudad</span>
                <asp:DropDownList ID="CiudadComboBox" runat="server">
                </asp:DropDownList>
                <div class="validators">
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="CiudadComboBox"
                        ErrorMessage="Se debe seleccionar un Proveedor"
                        ValidationGroup="PA"
                        Display="Dynamic" />
                </div>

                <span class="label">Duración de Cita (en minutos)</span>
                <telerik:RadNumericTextBox ID="DuracionCitaTextBox" MinValue="1" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" runat="server" CssClass="bigField"></telerik:RadNumericTextBox>
                <div class="validation">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="DuracionCitaTextBox"
                        ErrorMessage="Debe ingresar la Duración de la Cita"
                        Display="Dynamic" ValidationGroup="PA">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="buttonsPanel">
                    <asp:LinkButton ID="SaveButton" runat="server" ValidationGroup="PA" CssClass="button"
                        OnClick="SaveButton_Click">
                        <span>Guardar</span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="SaveAndContinueButton" runat="server" ValidationGroup="PA" CssClass="button"
                        OnClick="SaveAndContinueButton_Click">
                        <span>Configurar Estudios</span>
                    </asp:LinkButton>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Desgravamen/ProveedorDesgravamenLista.aspx" CssClass="secondaryButton"
                        Text="Cancelar">
                    </asp:HyperLink>
                </div>
            </div>
        </div>
    </div>
<script type="text/javascript">
    $(document).ready(function () {              
       
        $('#aaaa').blur(function () {

            var ci = $(this).val();

            $.ajax({
                type: "POST",
                url: "<%=ResolveClientUrl("~/AutoCompleteWS/ComboBoxWebServices.asmx") %>/CargarPAConCI",
                contentType: "application/json; charset=utf-8",
                data: { 'ci': ci },
                dataType: "json",
                success: function (result) {
                    alert('good');
                }
            });
        });

    });
</script>
</asp:Content>

