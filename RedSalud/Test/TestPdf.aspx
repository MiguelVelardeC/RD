<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TestPdf.aspx.cs" Inherits="Test_TestPdf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <asp:Button ID="btnHacerPDf" runat="server" Text="Convertir PDF" OnClick="btnHacerPDf_Click" />
</asp:Content>

