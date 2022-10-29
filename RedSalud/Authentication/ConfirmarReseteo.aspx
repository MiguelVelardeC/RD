<%@ Page Title="<%$ Resources: LoginGlossary, TituloResetarContraseña %>" Language="C#" MasterPageFile="~/EmptyMasterPage.master"
    AutoEventWireup="true" CodeFile="ConfirmarReseteo.aspx.cs" Inherits="Authentication_ConfirmarReseteo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #headerMnu {
            float: right;
            margin: 27px 10px 5px 10px;
        }

        #header {
            background: #EEE none repeat scroll 0 0;
            color: #333333;
            margin: 0;
            min-height: 52px;
            overflow: hidden;
        }

        .Page {
            width: 1200px;
            margin: 0 auto;
            margin-top: 10px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            -moz-box-shadow: 0px 0px 10px #AAA;
            -webkit-box-shadow: 0px 0px 10px #AAA;
            box-shadow: 0px 0px 10px #AAA;
        }

        #bodyIn {
            font-family: Verdana, Geneva, sans-serif;
            font-size: 12px;
            color: #000000;
            background-color: #EEE;
        }

        .pageTitle {
            display: block;
            font-weight: bold;
            margin-top: 5px;
            margin-right: 5px;
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="Page">
        <div id="header">
            <div id="logoContent" style="vertical-align: middle;">
                <asp:Image ID="imgHeaderLogo" runat="server" ImageUrl="~/Images/logo_RedSalud.png" AlternateText="RedSalud"
                    Height="53" Style="float: left;" />
                <div class="left">
                    <asp:Label ID="Label1" CssClass="TitleCompany"
                        Text="<%$ Resources: Glossary, ProjectName %>"
                        runat="server" />
                    <asp:Label ID="Label3" Text="<%$ Resources: Glossary, ProjectNameSubTitle %>" CssClass="SubTitleCompany" runat="server" />
                </div>
            </div>
        </div>
        <div id="bodyIn">
            <span class="pageTitle">
                <asp:Literal ID="MessageLiteral" runat="server" Text="<% $  Resources : LoginGlossary, TituloResetarContraseña %>">
                </asp:Literal>
            </span>
            <br />
            <div>
                <asp:Label ID="Label2" runat="server" Text="Se envió su nueva contraseña al correo electronico ingresado."></asp:Label>
                &nbsp;<asp:LinkButton ID="LoginLinkButton" runat="server" Text="Ir a la página de inicio"
                    OnClick="LoginLinkButton_Click"></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>

