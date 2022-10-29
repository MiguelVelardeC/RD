<%@ Page Title="Cliente" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AseguradoraDetails.aspx.cs" Inherits="Aseguradora_AseguradoraDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                    Text="Nuevo Cliente">
                </asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Volver a la Lista de Clientes"
                        NavigateUrl="~/Aseguradora/AseguradoraList.aspx">
                    </asp:HyperLink>
                </div>

                <asp:FormView ID="AseguradoraFV" runat="server"
                    DataSourceID="AseguradoraODS">
                    <ItemTemplate>
                        <asp:HiddenField ID="ClienteIdFVHF" runat="server" Value='<%# Bind("ClienteId") %>' />
                        <asp:Panel runat="server"
                            GroupingText="Agregar Red Medica al Cliente"
                            DefaultButton="addRedMedicaRedCliente">
                            <div>
                                <span class="label">Asignar Red Medica</span>
                                <asp:DropDownList ID="RedMedicaDDL" runat="server"
                                    DataSourceID="RedMedicaODS"
                                    DataTextField="Nombre"
                                    DataValueField="RedMedicaId">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="RedMedicaODS" runat="server"
                                    TypeName="Artexacta.App.RedMedica.BLL.RedMedicaBLL"
                                    SelectMethod="GetRedMedicaList"
                                    OnSelected="RedMedicaODS_Selected" />

                                <div class="buttonsPanel">
                                    <asp:LinkButton ID="addRedMedicaRedCliente" runat="server" CssClass="button"
                                        OnClick="addRedMedicaRedCliente_Click">
                                    <span>Agregar Red Medica</span>
                                    </asp:LinkButton>
                                </div>

                                <asp:GridView ID="RedClienteRedMedicaGV" runat="server"
                                    AutoGenerateColumns="false"
                                    DataSourceID="RedClienteRedMedicaODS"
                                    OnRowCommand="RedClienteRedMedicaGV_RowCommand">
                                    <EmptyDataTemplate>
                                        <span>El Cliente no contiene Redes Medicas asignadas.</span>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-Height="26px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="DeleteButton" runat="server"
                                                    CssClass="deleteRow"
                                                    CommandName="DeleteRecord"
                                                    CommandArgument='<%# Bind("RedMedicaId") %>'>
                                                    <asp:Image ID="Image1" runat="server"
                                                        ImageUrl="~/Images/Neutral/delete.png"
                                                        ToolTip="Eliminar" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Codigo" HeaderText="Código" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre de la Red Medica" />
                                    </Columns>
                                </asp:GridView>
                        </asp:Panel>

                        <asp:ObjectDataSource ID="RedClienteRedMedicaODS" runat="server"
                            TypeName="Artexacta.App.RedMedica.BLL.RedMedicaBLL"
                            SelectMethod="getRedMedicaListByClienteId"
                            OnSelected="RedClienteRedMedicaODS_Selected">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        </div>

                        <div>
                            <span class="label">Código Cliente</span>
                            <asp:Label ID="CodigoTxt" runat="server"
                                Text='<%# Bind("CodigoCliente") %>'>
                            </asp:Label>

                            <span class="label">Nombre Jurídico</span>
                            <asp:Label ID="NombreTxt" runat="server"
                                Text='<%# Bind("NombreJuridico") %>'>
                            </asp:Label>

                            <span class="label">Nit</span>
                            <asp:Label ID="NitTxt" runat="server"
                                Text='<%# Bind("Nit") %>'>
                            </asp:Label>

                            <span class="label">Dirección</span>
                            <asp:Label ID="DireccionTxt" runat="server"
                                Text='<%# Bind("Direccion") %>'>
                            </asp:Label>

                            <span class="label">Teléfono 1</span>
                            <asp:Label ID="Telefono1Txt" runat="server"
                                Text='<%# Bind("Telefono1") %>'>
                            </asp:Label>

                            <span class="label">Teléfono 2</span>
                            <asp:Label ID="Telefono2Txt" runat="server"
                                Text='<%# Bind("Telefono2") %>'>
                            </asp:Label>

                            <span class="label">Nombre Contacto</span>
                            <asp:Label ID="NombrecontactoTxt" runat="server"
                                Text='<%# Bind("NombreContacto") %>'>
                            </asp:Label>

                            <span class="label">Email</span>
                            <asp:Label ID="EmailTxt" runat="server"
                                Text='<%# Bind("Email") %>'>
                            </asp:Label>

                            <span class="label">Costo de la consulta del internista</span>
                            <asp:Label ID="CostoConsultaInternistaTxt" runat="server"
                                Text='<%# Bind("CostoConsultaInternista") %>'>
                            </asp:Label>

                            <span class="label">Número de días hábiles para Reconsulta</span>
                            <asp:Label ID="NumeroDiasReconsultaTxt" runat="server"
                                Text='<%# Bind("NumeroDiasReconsulta") %>'>
                            </asp:Label>
                        </div>

                        <div class="buttonsPanel">
                            <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                                CommandName="EDIT"
                                ValidationGroup="RedMedica">
                                <span>Modificar</span>
                            </asp:LinkButton>

                            <asp:HyperLink ID="ReturnLB" runat="server"
                                NavigateUrl="~/Aseguradora/AseguradoraList.aspx"
                                Text="Volver a la lista de Clientes" />
                        </div>
                    </ItemTemplate>
                    <InsertItemTemplate>
                        <div>
                            <span class="label">Código Cliente</span>
                            <asp:TextBox ID="CodigoTxt" runat="server" CssClass="normalField"
                                MaxLength="20"
                                Text='<%# Bind("CodigoCliente") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RequiredFieldValidator ID="CodigoRFV" runat="server"
                                    ControlToValidate="CodigoTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Debe ingregar el Código del Cliente" />
                            </div>

                            <span class="label">Nombre Jurídico</span>
                            <asp:TextBox ID="NombreTxt" runat="server" CssClass="normalField"
                                MaxLength="250"
                                Text='<%# Bind("NombreJuridico") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="NombreTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Debe ingregar el Nombre Jurídico del Cliente" />
                            </div>

                            <span class="label">Nit</span>
                            <asp:TextBox ID="NitTxt" runat="server" CssClass="normalField"
                                MaxLength="100"
                                Text='<%# Bind("Nit") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RequiredFieldValidator ID="MontoRFV" runat="server"
                                    ControlToValidate="NitTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Debe ingregar el Nit del Cliente" />
                                <asp:RegularExpressionValidator ID="MontoREV" runat="server"
                                    ControlToValidate="NitTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Solo permite números el Nit del Cliente y no puede ser cero."
                                    ValidationExpression="<% $ Resources:Validations, NitFormat %>" />
                                <asp:CustomValidator ID="CVNit" runat="server"
                                    ErrorMessage="El Nit no puede ser menor o igual a cero."
                                    ControlToValidate="NitTxt" 
                                    Display="Dynamic"
                                    ValidationGroup="RedMedica"
                                    ClientValidationFunction="CVNit_Validate" />
                            </div>

                            <span class="label">Dirección</span>
                            <asp:TextBox ID="DireccionTxt" runat="server" CssClass="normalField"
                                MaxLength="100"
                                Text='<%# Bind("Direccion") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                    ControlToValidate="DireccionTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="No se permiten caracteres especiales para la dirección"
                                    ValidationExpression="<% $ Resources:Validations, DescriptionFormat %>" />
                            </div>

                            <span class="label">Telefono 1</span>
                            <asp:TextBox ID="Telefono1Txt" runat="server" CssClass="normalField"
                                MaxLength="20"
                                Text='<%# Bind("Telefono1") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RegularExpressionValidator ID="Telefono1REV" runat="server"
                                    ControlToValidate="Telefono1Txt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Solo permite números para el telefono 1 del Cliente"
                                    ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                            </div>

                            <span class="label">Telefono 2</span>
                            <asp:TextBox ID="Telefono2Txt" runat="server" CssClass="normalField"
                                MaxLength="20"
                                Text='<%# Bind("Telefono2") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ControlToValidate="Telefono2Txt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Solo permite números el telefono 2 del Cliente"
                                    ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                            </div>

                            <span class="label">Nombre Contacto</span>
                            <asp:TextBox ID="NombrecontactoTxt" runat="server" CssClass="normalField"
                                MaxLength="250"
                                Text='<%# Bind("NombreContacto") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                    ControlToValidate="NombrecontactoTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="No se permiten caracteres especiales para el nombre del contacto del Cliente"
                                    ValidationExpression="<% $ Resources:Validations, DescriptionFormat %>" />
                            </div>

                            <span class="label">Email</span>
                            <asp:TextBox ID="EmailTxt" runat="server" CssClass="normalField"
                                MaxLength="250"
                                Text='<%# Bind("Email") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                    ControlToValidate="EmailTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="El Email no tiene el formato correcto"
                                    ValidationExpression="<% $ Resources:Validations, EMailFormat %>" />
                            </div>

                            <span class="label">Costo de la consulta del internista</span>
                            <asp:TextBox ID="CostoConsultaInternistaTxt" runat="server" CssClass="normalField"
                                MaxLength="5"
                                Text='<%# Bind("CostoConsultaInternista") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="CostoConsultaInternistaTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Debe ingregar el Costo de la consulta del internista" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                    ControlToValidate="CostoConsultaInternistaTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Solo permite números el Costo de la consulta del internista"
                                    ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                            </div>

                            <span class="label">Número de Días hábiles para Reconsulta</span>
                            <asp:TextBox ID="NumeroDiasReconsultaTxt" runat="server" CssClass="normalField"
                                MaxLength="5"
                                Text='<%# Bind("NumeroDiasReconsulta") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="NumeroDiasReconsultaTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Debe ingregar los números de días hábiles para Reconsulta" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                    ControlToValidate="NumeroDiasReconsultaTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Solo permite números en los números de días hábiles para Reconsulta"
                                    ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                            </div>
                        </div>

                        <div class="buttonsPanel">
                            <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                                CommandName="INSERT"
                                ValidationGroup="RedMedica">
                        <span>Guardar</span>
                            </asp:LinkButton>
                            <asp:HyperLink ID="HyperLink2" runat="server" CssClass="secondaryButton"
                                Text="Cancelar"
                                NavigateUrl="~/Aseguradora/AseguradoraList.aspx">
                            </asp:HyperLink>
                        </div>
                    </InsertItemTemplate>

                    <EditItemTemplate>
                        <asp:HiddenField ID="ClienteIdFVHF" runat="server" Value='<%# Bind("ClienteId") %>' />
                        <div>
                            <span class="label">Código Cliente</span>
                            <asp:TextBox ID="CodigoTxt" runat="server" CssClass="normalField"
                                MaxLength="20"
                                Text='<%# Bind("CodigoCliente") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RequiredFieldValidator ID="CodigoRFV" runat="server"
                                    ControlToValidate="CodigoTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Debe ingregar el Código del Cliente" />
                            </div>

                            <span class="label">Nombre Jurídico</span>
                            <asp:TextBox ID="NombreTxt" runat="server" CssClass="normalField"
                                MaxLength="250"
                                Text='<%# Bind("NombreJuridico") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="NombreTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Debe ingregar el Nombre Jurídico del Cliente" />
                            </div>

                            <span class="label">Nit</span>
                            <asp:TextBox ID="NitTxt" runat="server" CssClass="normalField"
                                MaxLength="100"
                                Text='<%# Bind("Nit") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RequiredFieldValidator ID="MontoRFV" runat="server"
                                    ControlToValidate="NitTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Debe ingregar el Nit del Cliente" />
                                <asp:RegularExpressionValidator ID="MontoREV" runat="server"
                                    ControlToValidate="NitTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Solo permite números el Nit del Cliente"
                                    ValidationExpression="<% $ Resources:Validations, NitFormat %>" />
                                <asp:CustomValidator ID="CVNit" runat="server"
                                    ErrorMessage="El Nit no puede ser menor o igual a cero."
                                    ControlToValidate="NitTxt" 
                                    Display="Dynamic"
                                    ValidationGroup="RedMedica"
                                    ClientValidationFunction="CVNit_Validate" />
                            </div>

                            <span class="label">Dirección</span>
                            <asp:TextBox ID="DireccionTxt" runat="server" CssClass="normalField"
                                MaxLength="100"
                                Text='<%# Bind("Direccion") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                    ControlToValidate="DireccionTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="No se permiten caracteres especiales para la dirección"
                                    ValidationExpression="<% $ Resources:Validations, DescriptionFormat %>" />
                            </div>

                            <span class="label">Telefono 1</span>
                            <asp:TextBox ID="Telefono1Txt" runat="server" CssClass="normalField"
                                MaxLength="20"
                                Text='<%# Bind("Telefono1") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RegularExpressionValidator ID="Telefono1REV" runat="server"
                                    ControlToValidate="Telefono1Txt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Solo permite números para el telefono 1 del Cliente"
                                    ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                            </div>

                            <span class="label">Telefono 2</span>
                            <asp:TextBox ID="Telefono2Txt" runat="server" CssClass="normalField"
                                MaxLength="20"
                                Text='<%# Bind("Telefono2") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ControlToValidate="Telefono2Txt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Solo permite números el telefono 2 del Cliente"
                                    ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                            </div>

                            <span class="label">Nombre Contacto</span>
                            <asp:TextBox ID="NombrecontactoTxt" runat="server" CssClass="normalField"
                                MaxLength="250"
                                Text='<%# Bind("NombreContacto") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                    ControlToValidate="NombrecontactoTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="No se permiten caracteres especiales para el nombre del contacto del Cliente."
                                    ValidationExpression="<% $ Resources:Validations, DescriptionFormat %>" />
                            </div>

                            <span class="label">Email</span>
                            <asp:TextBox ID="EmailTxt" runat="server" CssClass="normalField"
                                MaxLength="250"
                                Text='<%# Bind("Email") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                    ControlToValidate="EmailTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="El Email no tiene el formato correcto"
                                    ValidationExpression="<% $ Resources:Validations, EMailFormat %>" />
                            </div>

                            <span class="label">Costo de la consulta del internista</span>
                            <asp:TextBox ID="CostoConsultaInternistaTxt" runat="server" CssClass="normalField"
                                MaxLength="5"
                                Text='<%# Bind("CostoConsultaInternista") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="CostoConsultaInternistaTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Debe ingregar el Costo de la consulta del internista" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                    ControlToValidate="CostoConsultaInternistaTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Solo permite números el Costo de la consulta del internista"
                                    ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                            </div>

                            <span class="label">Número de Días hábiles para Reconsulta</span>
                            <asp:TextBox ID="NumeroDiasReconsultaTxt" runat="server" CssClass="normalField"
                                MaxLength="5"
                                Text='<%# Bind("NumeroDiasReconsulta") %>'>
                            </asp:TextBox>
                            <div class="validators">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="NumeroDiasReconsultaTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Debe ingregar los números de días hábiles para Reconsulta" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                    ControlToValidate="NumeroDiasReconsultaTxt"
                                    ValidationGroup="RedMedica"
                                    Display="Dynamic"
                                    ErrorMessage="Solo permite números en los números de días hábiles para Reconsulta"
                                    ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                            </div>
                        </div>

                        <div class="buttonsPanel">
                            <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                                CommandName="UPDATE"
                                ValidationGroup="RedMedica">
                        <span>Guardar</span>
                            </asp:LinkButton>

                            <asp:LinkButton ID="CancelLB" Text="Cancelar" runat="server"
                                CssClass="secondaryButton"
                                CommandName="CANCEL" />
                        </div>
                    </EditItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="AseguradoraODS" runat="server"
                    TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                    SelectMethod="GetRedClienteByClienteId"
                    InsertMethod="InsertRedCliente"
                    UpdateMethod="UpdateRedCliente"
                    OnSelected="AseguradoraODS_Selected"
                    OnInserted="AseguradoraODS_Inserted"
                    OnUpdated="AseguradoraODS_Updated">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>

            <asp:HiddenField ID="ClienteIdHF" runat="server" />

            <asp:HiddenField ID="HiddenButton" runat="server" />
            <div id="dialog-confirm" title="Eliminar" style="display: none">
                <p>
                    <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
                    Esta seguro que desea eliminar la Red Medica seleccionada?
                </p>
            </div>

            <script type="text/javascript">
                $(function () {
                    $(".deleteRow").click(function () {
                        $("#<%= HiddenButton.ClientID %>").val($(this).attr("href"));
                        $("#dialog-confirm").dialog("open");
                        return false;
                    });

                    $("#dialog-confirm").dialog({
                        resizable: false,
                        height: 140,
                        modal: true,
                        autoOpen: false,
                        buttons: {
                            "Eliminar": function () {
                                eval($("#<%= HiddenButton.ClientID %>").val());
                                $(this).dialog("close");
                            },
                            "Cancelar": function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                });
            </script>
            <script type="text/javascript">
                function CVNit_Validate(sender, args) {
                    args.IsValid = true;
                    var Nit = $('#<%= AseguradoraFV.FindControl("NitTxt").ClientID %>').val();
                    if (Nit <= 0) {
                        args.IsValid = false;
                    }
                }
            </script>
        </div>
    </div>
</asp:Content>

