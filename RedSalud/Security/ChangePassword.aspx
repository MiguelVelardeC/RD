<%@ Page Title="Cambiar Contraseña" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="ChangePassword.aspx.cs" Inherits="Security_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="ChangePassword" runat="server" Text="Cambiar Contraseña" CssClass="title"></asp:Label>
            </div>
            <div class="columnContent">
                <asp:ChangePassword ID="ChangePassword1" runat="server" ContinueDestinationPageUrl="~/MainPage.aspx"
                    OnCancelButtonClick="ChangePassword1_CancelButtonClick" 
                    ChangePasswordFailureText="Contraseña incorrecta o nueva contraseña inválida. El largo de la nueva contraseña debe ser de 7 caracteres como mínimo. Los caracteres deben ser alfanuméricos.">
                    <CancelButtonStyle />
                    <InstructionTextStyle />
                    <PasswordHintStyle />
                    <ChangePasswordButtonStyle />
                    <ContinueButtonStyle />
                    <TitleTextStyle />
                    <ChangePasswordTemplate>
                        <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword"
                            Text="Contraseña:" CssClass="label"></asp:Label>
                        <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                        <div class="validation">
						    <asp:RequiredFieldValidator ID="CurrentRequiredFieldValidator" runat="server" ErrorMessage="La contraseña es requerida." Display="Dynamic"
							    ToolTip="La contraseña es requerida." Text="*" ControlToValidate="CurrentPassword"></asp:RequiredFieldValidator>
					    </div>
                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword"
                            Text="Nueva Contraseña:" CssClass="label"></asp:Label>
                        <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
					    <div class="validation">
						    <asp:RequiredFieldValidator ID="NewPasswordRequiredFieldValidator" runat="server" Display="Dynamic"
							    ErrorMessage="La nueva contraseña es requerida." ControlToValidate="NewPassword"
							    ToolTip="La nueva contraseña es requerida." Text="*"></asp:RequiredFieldValidator>
						    <asp:RegularExpressionValidator ID="PasswordRegularExpressionValidator" ControlToValidate="NewPassword" Display="Dynamic"
							    ErrorMessage="Error en la constraseña. Debe contener por lo menos 7 caracteres, entre números, letras alfanumericas y no alfanumericos."
							    ValidationExpression="<%$ Resources: Validations, PasswordFormat %>" runat="server"
							    Text="*"></asp:RegularExpressionValidator>
					    </div>
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" Text="Confirmar Contraseña:" CssClass="label"></asp:Label>
                                
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
					    <div class="validation">
						    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" Display="Dynamic"
							    ErrorMessage="Confirmación de contraseña requerida." ToolTip="Confirmación de contraseña requerida.">*</asp:RequiredFieldValidator>
						    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" Display="Dynamic"
							    ControlToValidate="ConfirmNewPassword" ErrorMessage="La contraseña y confirmación de contraseña deben ser iguales."
							    Text="*"></asp:CompareValidator>
					    </div>
					    <div class="validation">
						    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
						    <asp:ValidationSummary ID="PCValidationSummary" runat="server" HeaderText="Corrija los siguientes errores para continuar:" Display="Dynamic" />
					    </div>
					    <div class="buttonsPanel">
						    <asp:LinkButton ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
							    CausesValidation="true" CssClass="button" >
							    <span>Cambiar Contraseña</span>	
						    </asp:LinkButton>
						    <asp:LinkButton ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel"
							    CssClass="secondaryButton" >
							    <span>Cancelar</span>
						    </asp:LinkButton>
					    </div>
                    </ChangePasswordTemplate>
                    <SuccessTemplate>
                        <table border="0" cellpadding="4" cellspacing="0" style="border-collapse: collapse;">
                            <tr>
                                <td>
                                    <asp:Label ID="Label" runat="server" Text="Su contraseña fue modificado existosamente."></asp:Label>
                                    <br />
                                    <br />
                                    <asp:LinkButton ID="ContinuePushButton" runat="server" CausesValidation="False" CommandName="Continue"
                                        CssClass="button" >
										<span>Continuar</span>
									</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </SuccessTemplate>
                    <TextBoxStyle />
                </asp:ChangePassword>

                <div class="clear"></div>
            </div>
        </div>
    </div>
</asp:Content>

