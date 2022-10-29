<%@ Page Title="Editar Usuario" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditUser.aspx.cs" Inherits="Security_EditUser" %>
<%@ Register TagPrefix="RedSalud" TagName="FileUpload" Src="~/UserControls/FileUpload.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="EditLabel" runat="server" Text="Editar Usuario" CssClass="title"></asp:Label>
            </div>
            <div class="columnContent">
                <div class="left" style="width: 30%;">
                    <asp:Label ID="UsernameTitLabel" runat="server" Text="Nombre de Usuario:" CssClass="label"></asp:Label>
                    <asp:Label ID="UsernameLabel" runat="server" ForeColor="#737164" Font-Bold="True"
                        CssClass="label"></asp:Label>
                    <asp:Label ID="NameLabel0" runat="server" Text="Nombre Completo:" CssClass="label"></asp:Label>
                    <asp:TextBox ID="NameTextBox" runat="server" CssClass="bigField"></asp:TextBox>
                    <div class="validation">
                        <asp:RegularExpressionValidator ID="NameLenRegularExpressionValidator" runat="server"
                            Display="Dynamic" ControlToValidate="NameTextBox" ErrorMessage="El nombre completo no puede exceder 250 caracteres."
                            ValidationExpression="[\w\W]{0,250}">*</asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="NameFormRegularExpressionValidator" runat="server"
                            Display="Dynamic" ControlToValidate="NameTextBox" ErrorMessage="Caracteres inválidos en el nombre completo."
                            ValidationExpression="^[A-Za-z0-9áéíóúñÁÉÍÓÚñÑàèìòùÀÈÌÒÙâêîôûÂÊÎÔÛäëïöüÿÄËÏÖÜÇçÆæ_\s€¥®©£¡!@$%&amp;\^\*\(\)\+=\|&quot;:;&lt;&gt;\¿\?\.,\-\/]+$">*</asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ControlToValidate="NameTextBox"
                            Display="Dynamic" ErrorMessage="El nombre completo es requerido.">*</asp:RequiredFieldValidator>
                    </div>
                    <asp:Label ID="EmailLabel" runat="server" Text="Correo Electrónico:" CssClass="label"></asp:Label>
                    <asp:TextBox ID="EmailTextBox" runat="server" CssClass="bigField"></asp:TextBox>
                    <div class="validation">
                        <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ControlToValidate="EmailTextBox"
                            Display="Dynamic" ErrorMessage="El correo electrónico es requerido.">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                            ControlToValidate="EmailTextBox" ErrorMessage="Correo electrónico inválido."
                            Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    </div>

                    <asp:Label ID="Label1" Text="Ciudad donde vive actualmente:" runat="server"
                        CssClass="label" />
                    <telerik:RadComboBox ID="CiudadDDL" runat="server"
                        ClientIDMode="Static"
                        DataSourceID="CiudadODS"
                        DataValueField="CiudadId"
                        DataTextField="Nombre"
                        EmptyMessage="Seleccione una Ciudad"
                        CssClass="bigField">
                    </telerik:RadComboBox>
                    <div class="validation">
                        <asp:CustomValidator ID="CiudadCV" runat="server"
                            ValidationGroup="CreateUserWizard1"
                            ErrorMessage="Debe seleccionar una Ciudad."
                            ClientValidationFunction="CiudadCV_Validate"
                            Display="Dynamic"
                            Text="*" />
                    </div>
                    <asp:ObjectDataSource ID="CiudadODS" runat="server"
                        TypeName="Artexacta.App.Ciudad.BLL.CiudadBLL"
                        OldValuesParameterFormatString="{0}"
                        SelectMethod="getCiudadList"
                        OnSelected="CiudadODS_Selected"></asp:ObjectDataSource>

                    <script type="text/javascript">
                        function CiudadCV_Validate(sender, args) {
                            args.IsValid = true;

                            var value = $find('CiudadDDL').get_value();

                            if (value == "") {
                                args.IsValid = false;
                            }
                        }
                    </script>

                    <asp:Label ID="AddressLabel" runat="server" Text="Dirección:" CssClass="label"></asp:Label>
                    <asp:TextBox ID="AddressTextBox" runat="server" CssClass="bigField" MaxLength="250"></asp:TextBox>
                    <div class="validation">
                        <asp:RegularExpressionValidator ID="DireccionFormatRegularExpressionValidator" runat="server"
                            Display="Dynamic" ErrorMessage="Caracteres inválidos en la dirección." Text="*"
                            ControlToValidate="AddressTextBox" ValidationExpression="<%$ Resources: Validations, DescriptionFormat %>"></asp:RegularExpressionValidator>
                    </div>
                    <asp:Label ID="CellLabel" runat="server" Text="Teléfono Movil:" CssClass="label"></asp:Label>
                    <asp:TextBox ID="CellPhoneTextBox" runat="server" CssClass="normalField" MaxLength="50"></asp:TextBox>
                    <div class="validation">
                        <asp:RegularExpressionValidator ID="PhoneFormatRegularExpressionValidator" runat="server"
                            ControlToValidate="CellPhoneTextBox" ErrorMessage="Caracteres inválidos en el telefóno móvil."
                            Display="Dynamic" ValidationExpression="<%$ Resources: Validations, PhoneNumberFormat %>">*</asp:RegularExpressionValidator>
                    </div>
                    <asp:Label ID="PhoneLabel" runat="server" Text="Teléfono:" CssClass="label"></asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="PaisAreaTextBox" runat="server" Width="60px" MaxLength="5"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="CiudadAreaTextBox" runat="server" Width="60px" MaxLength="5"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="AreaFormatRegularExpressionValidator" runat="server"
                                    Display="Dynamic" ControlToValidate="CiudadAreaTextBox" ErrorMessage="Caracteres inválidos en el código de área."
                                    ValidationExpression="<%$ Resources: Validations, PhoneNumberFormat %>">*</asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="NumeroTextBox" runat="server" Width="120px" MaxLength="12"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="NumeroFormatRegularExpressionValidator" runat="server"
                                    Display="Dynamic" ControlToValidate="NumeroTextBox" ErrorMessage="Caracteres inválidos en el número de teléfono."
                                    ValidationExpression="<%$ Resources: Validations, PhoneNumberFormat %>">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="PaisLabel" runat="server" Text="Código País" Font-Size="X-Small"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="AreaLabel" runat="server" Text="Código Área" Font-Size="X-Small"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="NumeroLabel" runat="server" Text="Número" Font-Size="X-Small"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div class="validation">
                        <asp:RegularExpressionValidator ID="PaisFormatRegularExpressionValidator" runat="server"
                            Display="Dynamic" ControlToValidate="PaisAreaTextBox" ErrorMessage="Caracteres inválidos en el código de país."
                            ValidationExpression="<%$ Resources: Validations, PhoneNumberFormat %>">*</asp:RegularExpressionValidator>
                    </div>
                    <div class="validation">
                        <asp:ValidationSummary ID="ValidationSummary" runat="server" HeaderText="Corrija los siguientes errores para continuar:" />
                    </div>
                    <div class="buttonsPanel">
                        <asp:LinkButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" CssClass="button">
                            <span>
                                <asp:Literal ID="SaveLabel" runat="server" Text="Guardar" /></span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="CancelButton" runat="server" CausesValidation="False" Text="Cancelar"
                            CssClass="secondaryButton" OnClick="CancelButton_Click" />
                    </div>

                    <div class="clear">
                    </div>
                </div>
                <div class="left" style="width: 70%;">
                    <asp:Panel ID="SOATClienteListPanel" runat="server" 
                        GroupingText="Clientes SOAT Asociados">
                        <asp:Label ID="ClienteLabel" Text="Cliente" runat="server" CssClass="label" />
                        <asp:DropDownList ID="ClienteDDL" runat="server"
                            DataSourceID="ClienteODS"
                            CssClass="bigField left"
                            DataValueField="ClienteId"
                            DataTextField="NombreJuridico"
                            style="margin-bottom: 10px;">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ClienteODS" runat="server"
                            TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                            OldValuesParameterFormatString="original_{0}"
                            SelectMethod="getRedClienteForSOATList"
                            OnSelected="ClienteODS_Selected">
                            <SelectParameters>
                                <asp:Parameter Name="getAll" Type="Boolean" DefaultValue="true" />
                            </SelectParameters>
                        </asp:ObjectDataSource>

                        <asp:LinkButton ID="NewUserClienteSOATLB" runat="server" 
                            CssClass="left" style="margin-left: 10px;"
                            OnClick="NewUserClienteSOATLB_Click">
                            <span>Asociar Cliente</span>
                        </asp:LinkButton>
                        <div class="clear"></div>
                        <telerik:RadGrid ID="UserClienteRadGrid" runat="server"
                            AutoGenerateColumns="false" DataSourceID="UserClienteODS"
                            AllowPaging="true" PageSize="5" MasterTableView-DataKeyNames="ClienteId">
                            <MasterTableView>
                                <NoRecordsTemplate>
                                    <asp:Label runat="server" Text="Este usuario no esta asignado a ningún Cliente"></asp:Label>
                                </NoRecordsTemplate>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="Eliminar" ItemStyle-Width="24px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="DeleteImageButton" runat="server"
                                                ImageUrl="~/Images/neutral/delete.png"
                                                OnCommand="DeleteImageButton_Command"
                                                OnClientClick="return confirm('¿Está seguro que desea quitar el Cliente?');"
                                                CommandArgument='<%# Eval("ClienteId") %>'
                                                Width="24px" CommandName="Eliminar"
                                                ToolTip="Eliminar"></asp:ImageButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="CodigoCliente" HeaderText="Código del Cliente" />
                                    <telerik:GridBoundColumn DataField="NombreCliente" HeaderText="Nombre del Cliente" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>

                        <asp:ObjectDataSource ID="UserClienteODS" runat="server"
                            TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                            OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetUserClienteSOATByUserId"
                            OnSelected="UserClienteODS_Selected">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="UserIdHiddenField" Name="UserId" PropertyName="Value" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </asp:Panel>
                    <asp:Panel ID="ClienteDesgravamenAsociado" runat="server" 
                        GroupingText="Cliente Desgravamen Asociado">
                    <asp:DropDownList ID="clientesComboBox" runat="server">                    
                    </asp:DropDownList>
                    </asp:Panel>
                    <asp:Panel ID="ClienteUsuarioAsociado" runat="server" 
                        GroupingText="Cliente Asociado para la Carga de Pólizas Diarias">
                    <asp:DropDownList ID="clientesUsuarioComboBox" CssClass="bigField left" runat="server">                    
                    </asp:DropDownList>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="PnlFirmaDigital" GroupingText="Firma Digital">
                        <asp:Image ID="FotoSignatureUrl" runat="server" ImageAlign="Right" />
                        <RedSalud:FileUpload ID="FotoFileUpload" runat="server" ShowMode="Normal" OnFilesLoaded="FotoFileUpload_FilesLoaded" />
                    </asp:Panel>
                </div>
                <div class="clear"></div>
        </div>
    </div>
    </div>
    <asp:HiddenField ID="OldClienteIdHiddenField" runat="server" />
    <asp:HiddenField ID="OldClienteUsuarioIdHiddenField" runat="server" />
    <asp:HiddenField ID="UsernameHiddenField" runat="server" />
    <asp:HiddenField ID="UserIdHiddenField" runat="server" />
    <asp:HiddenField ID="EmailHiddenField" runat="server" />    
    <asp:HiddenField ID="FotoSignatureIDHiddenField" runat="server" Value="0" />    
    <asp:HiddenField ID="FileIdHiddenField" runat="server" Value="0" />
</asp:Content>

