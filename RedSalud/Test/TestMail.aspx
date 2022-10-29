<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestMail.aspx.cs" Inherits="Test_TestMail" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Mail System</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            To:
            <asp:TextBox ID="AddressTextBox" runat="server" Width="400px"></asp:TextBox>
            <asp:Button ID="SendMailButton" runat="server" Text="Send Test Mail" OnClick="SendMailButton_Click" />
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="AddressTextBox"
                ErrorMessage="Invalid Email Address" ValidationExpression="<%$ Resources : Validations , EMailFormat %>">*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="AddressTextBox"
                ErrorMessage="Email Address is required">*</asp:RequiredFieldValidator><br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <br />
            <asp:Label ID="ResultsLabel" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
