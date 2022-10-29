<%@ Page Title="<%$ Resources: LoginGlossary, LoginTitle %>" Language="C#" MasterPageFile="~/EmptyMasterPage.master"
    AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Authentication_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #loginContainer {
            background-color: #EEEEEE;
            border: 1px solid #000000;
            border-radius: 7px 7px 7px 7px;
            box-shadow: 0 0 6px #F9F6D7;
            margin-top: 10px;
            -moz-box-shadow: 0 0 6px #F9F6D7;
            -webkit-box-shadow: 0 0 6px #F9F6D7;
            -moz-border-radius: 7px;
            -webkit-border-radius: 7px;
        }

        #loginLogoContainer {
            float: left;
            margin: 0 5px;
            width: 200px;
        }

        #loginFormContainer {
            float: left;
            margin-left: 5px;
            width: 200px;
        }

        #loginMsg {
            color: #333333;
            float: left;
            line-height: 12px;
            margin-left: 22px;
            margin-top: 6px;
            width: 167px;
        }

        #loginTitle {
            border-bottom: 1px solid #CCCCCC;
            font-family: Verdana, Geneva, sans-serif;
            padding: 9px 12px;
            text-shadow: 2px 2px #DDDDDD;
        }

        .SystemName {
            display: inline-block;
            font-size: 22px;
            margin: 4px 0 0 10px;
            text-shadow: 2px 2px #DDDDDD;
        }

        #loginOptionsContainer {
            border-top: 1px solid #CCCCCC;
            padding: 7px;
            text-align: center;
        }

        #line {
            background-color: #39495C;
            border-top: 1px solid #666666;
            box-shadow: 6px 0 11px 2px #000000;
            height: 1px;
        }
        input{
            text-transform: none!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div style="height: 235px; background-color: #39495C;">
        &nbsp;
    </div>
    <div id="line">
        &nbsp;
    </div>
    <div style="z-index: 1;">
        <div style="margin: -139px auto 0; width: 620px;">
            <div id="loginContainer">
                <div id="loginTitle">
                    <asp:Image ID="Image1" ImageUrl="~/Images/logo_sisa.png" Width="100%" runat="server" />
                    <span class="SystemName" style="display:none">
                        <asp:Literal ID="Literal1" Text="<%$ Resources: LoginGlossary, LoginTitle %>" runat="server" />
                    </span>
                </div>
                <div style="margin-bottom: 15px; margin-top: 15px;">
                    <div id="loginLogoContainer">
                        <asp:Image ID="LogoImage" runat="server" ImageUrl="~/Images/logo_3.png" Width="100%" />
                    </div>

                    <div id="loginFormContainer">
                        <asp:Login ID="Login1" runat="server" OnLoggingIn="Login1_LoggingIn" OnLoggedIn="Login1_LoggedIn"
                            OnLoginError="Login1_LoginError" FailureText="Su inicio de sesión no fue exitoso. Por favor intente nuevamente.">
                            <LayoutTemplate>
                                <asp:Panel ID="pnlLoginControl" runat="server" DefaultButton="LoginButton">
                                    <asp:Label ID="UserNameLabel" runat="server" CssClass="label" AssociatedControlID="UserName"
                                        Text="<%$ Resources: LoginGlossary, UserNameLabel %>" />
                                    <div style="display: block;">
                                        <asp:TextBox ID="UserName" runat="server" CssClass="normalField" />
                                    </div>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" Display="Dynamic"
                                        ControlToValidate="UserName" ErrorMessage="<%$ Resources: LoginGlossary, UserNameRequiredErrorMessage %>"
                                        Text="<%$ Resources: LoginGlossary, UserNameRequiredErrorMessage %>" CssClass="validation"
                                        ToolTip="<%$ Resources: LoginGlossary, UserNameRequiredErrorMessage %>" ValidationGroup="Login1">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ID="UserNameLengthValidator" ControlToValidate="UserName"
                                        ValidationExpression="<% $ Resources:Validations, GenericLength50 %>" CssClass="validation"
                                        Display="Dynamic" ValidationGroup="Login1" ErrorMessage="<%$ Resources: LoginGlossary, UserNameLengthErrorMessage %>">
                                    </asp:RegularExpressionValidator>

                                    <asp:Label ID="PasswordLabel" runat="server" CssClass="label" AssociatedControlID="Password" Text="<%$ Resources: LoginGlossary, PasswordLabel %>" />
                                    <div style="display: block;">
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="normalField" />
                                    </div>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" Display="Dynamic"
                                        ControlToValidate="Password" Text="<%$ Resources: LoginGlossary, PasswordRequiredErrorMessage %>"
                                        CssClass="validation" ErrorMessage="<%$ Resources: LoginGlossary, PasswordRequiredErrorMessage %>"
                                        ToolTip="<%$ Resources: LoginGlossary, PasswordRequiredErrorMessage %>" ValidationGroup="Login1">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ID="PasswordLengthValidator" ControlToValidate="Password"
                                        ValidationExpression="<% $ Resources:Validations, GenericLength50 %>" CssClass="validation"
                                        Display="Dynamic" ValidationGroup="Login1" ErrorMessage="<%$ Resources: LoginGlossary, PasswordLengthErrorMessage %>">
                                    </asp:RegularExpressionValidator>
                                    <asp:Label ID="FailureText" runat="server" CssClass="error" EnableViewState="False" />

                                    <div class="buttonsPanel">
                                        <asp:LinkButton ID="LoginButton" runat="server" CssClass="button" CommandName="Login" ValidationGroup="Login1">
                                            <asp:Label ID="lblLoginButton" runat="server" Text="<%$ Resources: LoginGlossary, LoginButton %>" />
                                        </asp:LinkButton>
                                        <a id="loginHelp" href="#" style="float: right;" class="help"></a>
                                    </div>

                                </asp:Panel>
                            </LayoutTemplate>
                        </asp:Login>
                    </div>

                    <div id="loginMsg">
                        <asp:Literal ID="Literal3" Text="<%$ Resources: LoginGlossary, WelcomeMessage %>" runat="server" />
                    </div>

                    <div class="clear">
                    </div>

                </div>

                <div id="loginOptionsContainer">
                    <asp:Literal ID="lblRecover" runat="server" Text="<%$Resources: LoginGlossary, CantRememberPasswordLabel %>" />
                    <asp:LinkButton ID="RecoverLinkButton" runat="server" Text="<% $ Resources : LoginGlossary, RecoverPasswordLink %>"
                        OnClick="RecoverLinkButton_Click" />
                </div>

            </div>
        </div>
    </div>

    <script type="text/javascript">
        // HACK: default button + linkbutton no funciona en firefox por que <a> no tiene evento click definido
        // definimos click para LoginButton
        var b = document.getElementById('<%= Login1.FindControl("LoginButton").ClientID %>');
        if (b && typeof (b.click) == 'undefined') {
            b.click = function () {
                var result = true;
                if (b.onclick)
                    result = b.onclick();
                if (typeof (result) == 'undefined' || result) {
                    eval(b.getAttribute('href'));
                }
            }
        }
    </script>

</asp:Content>

