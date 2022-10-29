<%@ Page Title="Crear Usuario" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CreateUser.aspx.cs" Inherits="Security_CreateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" Text="Crear Usuario" CssClass="title"></asp:Label>
            </div>
            <div class="columnContent">

                <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" OnCreatedUser="CreateUserWizard1_CreatedUser"
                    OnCreatingUser="CreateUserWizard1_CreatingUser" Width="550px" CreateUserButtonText="Crear Usuario"
                    CreateUserButtonStyle-CssClass="button" CancelButtonStyle-CssClass="button" CancelButtonText="Cancelar"
                    DisplayCancelButton="True" OnSendingMail="CreateUserWizard1_SendingMail" 
                    OnSendMailError="CreateUserWizard1_SendMailError"
                    OnCancelButtonClick="CreateUserWizard1_CancelUser"
                    CancelDestinationPageUrl="~/Security/UserList.aspx">
                    <CreateUserButtonStyle CssClass="button"></CreateUserButtonStyle>
                    <MailDefinition BodyFileName="<%$ Resources: Files, MailCrearUsuario %>">
                    </MailDefinition>
                    <WizardSteps>
                        <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="UserNameLabel" CssClass="label" runat="server" AssociatedControlID="UserName"
                                    Text="Nombre de Usuario"></asp:Label>
                                <asp:TextBox ID="UserName" runat="server" CssClass="normalField"></asp:TextBox>
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        Display="Dynamic" ErrorMessage="El nombre de usuario es requerido." ToolTip="El nombre de usuario es requerido"
                                        ValidationGroup="CreateUserWizard1" Text="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="UserNameRegularExpressionValidator" runat="server"
                                        Display="Dynamic" ControlToValidate="UserName" ErrorMessage="Caracteres inválidos en el nombre de usuario."
                                        ValidationExpression="^[A-Za-z0-9_.]+$" ValidationGroup="CreateUserWizard1" Text="*"></asp:RegularExpressionValidator>
                                </div>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Text="Contraseña"
                                    CssClass="label"></asp:Label>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="normalField"></asp:TextBox>
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        Display="Dynamic" ErrorMessage="La contraseña es requerida." ToolTip="La contraseña es requerida."
                                        ValidationGroup="CreateUserWizard1" Text="*">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="PasswordRegularExpressionValidator" ControlToValidate="Password"
                                        Display="Dynamic" ErrorMessage="Error en la constraseña. Debe contener por lo menos 7 caracteres, entre números, letras alfanumericas y no alfanumericos."
                                        ValidationExpression="<%$ Resources: Validations, PasswordFormat %>" ValidationGroup="CreateUserWizard1"
                                        runat="server" Text="*"></asp:RegularExpressionValidator>
                                </div>
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword"
                                    CssClass="label" Text="Confirmación de Contraseña"></asp:Label>
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" CssClass="normalField"></asp:TextBox>
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                        Display="Dynamic" ErrorMessage="La confirmación de contraseña es requerida."
                                        ToolTip="La confirmación de contraseña es requerida." ValidationGroup="CreateUserWizard1"
                                        Text="*"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                        ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="La contraseña y confirmación de contraseña deben ser iguales."
                                        ValidationGroup="CreateUserWizard1" Text="*"></asp:CompareValidator>
                                </div>
                                <asp:Label ID="NameLabel" runat="server" Text="Nombre Completo:" CssClass="label"></asp:Label>
                                <asp:TextBox ID="FullNameTextBox" runat="server" CssClass="bigField" MaxLength="500"></asp:TextBox>
                                <div class="label">
                                    <asp:RequiredFieldValidator ID="FullNameRequired" runat="server" ControlToValidate="FullNameTextBox"
                                        Display="Dynamic" ErrorMessage="El nombre completo es requerido." ToolTip="El nombre completo es requerido."
                                        ValidationGroup="CreateUserWizard1" Text="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="FormatNombreRegularExpressionValidator" runat="server"
                                        Display="Dynamic" ErrorMessage="Caracteres inválidos en el nombre completo."
                                        Text="*" ControlToValidate="FullNameTextBox" ValidationExpression="<%$ Resources: Validations, DescriptionFormat %>"
                                        ValidationGroup="CreateUserWizard1"></asp:RegularExpressionValidator>
                                </div>
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" Text="Correo Electrónico:"
                                    CssClass="label"></asp:Label>
                                <asp:TextBox ID="Email" runat="server" CssClass="bigField"></asp:TextBox>
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                        Display="Dynamic" ErrorMessage="El correo electrónico es requerido." ToolTip="El correo electrónico es requerido."
                                        ValidationGroup="CreateUserWizard1" Text="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="FormatEmailRegularExpressionValidator" runat="server"
                                        Display="Dynamic" ControlToValidate="Email" ErrorMessage="Formato incorrecto del correo electrónico."
                                        ToolTip="Formato incorrecto del correo electrónico." ValidationGroup="CreateUserWizard1"
                                        Text="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    <asp:CustomValidator ID="ExistsEmailCustomValidator" runat="server" ErrorMessage="El correo electrónico se encuentra registrado."
                                        Display="Dynamic" Text="*" ToolTip="El correo electrónico se encuentra registrado."
                                        ValidationGroup="CreateUserWizard1" OnServerValidate="ExistsEmailCustomValidator_ServerValidate"></asp:CustomValidator>
                                </div>

                                <asp:Label Text="Ciudad donde vive actualmente:" runat="server" 
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

                                <asp:Label ID="AddressLabel" runat="server" Text="Dirección:" AssociatedControlID="AddressTextBox"
                                    CssClass="label"></asp:Label>
                                <asp:TextBox ID="AddressTextBox" runat="server" CssClass="bigField" MaxLength="250"></asp:TextBox>
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="DireccionFormatRegularExpressionValidator" runat="server"
                                        Display="Dynamic" ErrorMessage="Caracteres inválidos en la dirección." Text="*"
                                        ControlToValidate="AddressTextBox" ValidationExpression="<%$ Resources: Validations, DescriptionFormat %>"
                                        ValidationGroup="CreateUserWizard1"></asp:RegularExpressionValidator>
                                </div>
                                <asp:Label ID="CellPhoneLabel" runat="server" Text="Teléfono Móvil:" AssociatedControlID="CellPhoneTextBox"
                                    CssClass="label"></asp:Label>
                                <asp:TextBox ID="CellPhoneTextBox" runat="server" CssClass="normalField" MaxLength="50"></asp:TextBox>
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="PhoneFormatRegularExpressionValidator" runat="server"
                                        Display="Dynamic" ControlToValidate="CellPhoneTextBox" ErrorMessage="Caracteres inválidos en el telefóno móvil."
                                        ValidationExpression="<%$ Resources: Validations, PhoneNumberFormat %>" ValidationGroup="CreateUserWizard1">*</asp:RegularExpressionValidator>
                                </div>
                                <asp:Label ID="PhoneLabel" runat="server" Text="Teléfono:" CssClass="label"></asp:Label>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="PaisAreaTextBox" runat="server" Width="60px" MaxLength="5"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="PaisFormatRegularExpressionValidator" runat="server"
                                                ControlToValidate="PaisAreaTextBox" ErrorMessage="Caracteres inválidos en el código de país."
                                                ValidationExpression="<%$ Resources: Validations, PhoneNumberFormat %>" ValidationGroup="CreateUserWizard1">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="CiudadAreaTextBox" runat="server" Width="60px" MaxLength="5"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="AreaFormatRegularExpressionValidator" runat="server"
                                                ControlToValidate="CiudadAreaTextBox" ErrorMessage="Caracteres inválidos en el código de área."
                                                ValidationExpression="<%$ Resources: Validations, PhoneNumberFormat %>" ValidationGroup="CreateUserWizard1">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="NumeroTextBox" runat="server" Width="120px" MaxLength="12"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="NumeroFormatRegularExpressionValidator" runat="server"
                                                ControlToValidate="NumeroTextBox" ErrorMessage="Caracteres inválidos en el número de teléfono."
                                                ValidationExpression="<%$ Resources: Validations, PhoneNumberFormat %>" ValidationGroup="CreateUserWizard1">*</asp:RegularExpressionValidator>
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
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="CreateUserWizard1"
                                        HeaderText="Corrija los siguientes errores para continuar:" Width="600px" />
                                </div>
                                <asp:Label ID="MessageLabel" runat="server" CssClass="label"></asp:Label>

                            </ContentTemplate>
                            <CustomNavigationTemplate>
                                <div class="buttonsPanel">
                                    <asp:LinkButton ID="StepNextButton" runat="server" CommandName="MoveNext" CssClass="button"
                                        ValidationGroup="CreateUserWizard1">
										<span>Crear Usuario</span>	
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                        CssClass="secondaryButton" ValidationGroup="CreateUserWizard1">
										<span>Cancelar</span>	
                                    </asp:LinkButton>
                                </div>
                            </CustomNavigationTemplate>
                        </asp:CreateUserWizardStep>
                        <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="MensajeCrearLabel" runat="server" Text="El usuario fue creado satisfactoriamente. Para volver al listado de usuario presione el siguiente enlace:"></asp:Label>
                                <asp:HyperLink ID="ListaHyperLink" runat="server" NavigateUrl="~/Security/UserList.aspx"
                                    CssClass="linkButton">Lista de Usuarios</asp:HyperLink>
                            </ContentTemplate>
                        </asp:CompleteWizardStep>
                    </WizardSteps>
                    <CancelButtonStyle CssClass="secondaryButton"></CancelButtonStyle>
                    <StartNavigationTemplate>
                        <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="Next" />
                    </StartNavigationTemplate>
                </asp:CreateUserWizard>

                <div class="clear">
                </div>

            </div>
        </div>
    </div>

    <asp:HiddenField ID="FullNameHiddenField" runat="server" />
    <asp:HiddenField ID="MedicoCreateHF" runat="server" />
</asp:Content>