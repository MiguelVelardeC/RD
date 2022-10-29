<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TestWSAlianza.aspx.cs" Inherits="Test_TestWSAlianza" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <h1>Test Alianza</h1>
    <asp:Button ID="callWebSErvice" runat="server" Text="Call WS" OnClick="callWebSErvice_Click" />
    <asp:Button ID="getCsvFile" runat="server" Text="Get CSV From WS Call" OnClick="getCsvFile_Click" />
    <asp:Label ID="lblNumberOfAsegurados" runat="server" Text="Nb asegurados"></asp:Label>
    <br />

    <telerik:RadProgressManager ID="RadProgressManager1" runat="server" />
    <telerik:RadProgressArea ID="RadProgressArea1" runat="server" DisplayCancelButton="True"
        ProgressIndicators="FilesCountBar,
                            FilesCount,
                            SelectedFilesCount,                      
                            CurrentFileName">
    </telerik:RadProgressArea>

    <asp:Button ID="updateData" runat="server" Text="Update" OnClick="updateData_Click" />
    <asp:Literal ID="lblResultUpdate" runat="server" Text="Resultado update"></asp:Literal>
    <br />
    <asp:Label ID="ErrorLabel" runat="server" />
</asp:Content>

