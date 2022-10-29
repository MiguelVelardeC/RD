<%@ Page Title="Nuevo Rol" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="NewRole.aspx.cs" Inherits="Security_NewRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" Text="Nuevo Rol" CssClass="title"></asp:Label>
            </div>
            <div class="columnContent">
                <asp:Label ID="RoleLabel" runat="server" Text="Rol:" CssClass="label"></asp:Label>
				<asp:TextBox ID="RoleNameTextBox" runat="server" Width="200px"></asp:TextBox>
				<div class="validators">
					<asp:RequiredFieldValidator ID="RoleNameRequiredFieldValidator" runat="server" ControlToValidate="RoleNameTextBox"
						Display="Dynamic" ErrorMessage="La descripción del rol es requerido.">*</asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="FormatRoleNameRegularExpressionValidator" runat="server" 
						ControlToValidate="RoleNameTextBox" Display="Dynamic" ValidationExpression="<%$ Resources: Validations, NameFormat %>"
						ErrorMessage="Caracteres inválidos en la descripción del rol.">*</asp:RegularExpressionValidator>
					<asp:RegularExpressionValidator ID="LengthRegularExpressionValidator1" runat="server" 
						ControlToValidate="RoleNameTextBox" Display="Dynamic" ValidationExpression="<%$ Resources: Validations, GenericLength50 %>"
						ErrorMessage="La descripción del rol no puede exceder 50 caracteres.">*</asp:RegularExpressionValidator>
				</div>
                <div class="validation">      
					<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Corrija los siguientes errores para continuar:" />
				</div>
				<div class="buttonsPanel">
					<asp:LinkButton ID="InsertButton" runat="server" OnClick="InsertButton_Click"
						CssClass="button" >
						<span>Insertar</span>	
					</asp:LinkButton>
					<asp:LinkButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click"
						CssClass="secondaryButton" >
						<span>Cancelar</span>
					</asp:LinkButton>
				</div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="PostBackPageHiddenField" runat="server" />
</asp:Content>

