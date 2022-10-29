<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PagoSOATAdvancedSearch.ascx.cs" Inherits="UserControls_AdvancedSearch_PagoSOATAdvancedSearch" %>

<%@ Register Src="../SearchUserControl/SC_TextSearchItem.ascx" TagName="SC_TextSearchItem" TagPrefix="uc1" %>
<%@ Register Src="../SearchUserControl/SC_DateSearchItem.ascx" TagName="SC_DateSearchItem" TagPrefix="uc4" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_BetweenDateSearchItem.ascx" TagPrefix="uc1" TagName="SC_BetweenDateSearchItem" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_DecimalSearchItem.ascx" TagPrefix="uc1" TagName="SC_DecimalSearchItem" %>

<div id="formClientsSearch">
    <uc1:SC_TextSearchItem ID="SC_NumeroFacturaSearchItem" runat="server" 
            ShowAndOrButtons="false"
            SearchColumnKey="NumeroFactura" 
            Title="Número de Factura" />
    <uc1:SC_TextSearchItem ID="SC_ProveedorSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="Proveedor" 
            Title="Proveedor" />
     <uc1:SC_DecimalSearchItem ID="SC_MontoSearchItem" runat="server" 
            ShowAndOrButtons="true" 
            SearchColumnKey="Monto" 
            Title="Monto" />
     <uc1:SC_TextSearchItem ID="SC_TipoGastoSearchItem" runat="server" 
            ShowAndOrButtons="true" 
            SearchColumnKey="TipoGasto" 
            Title="Tipo de Gasto" />
    <uc1:SC_BetweenDateSearchItem runat="server" ID="SC_FechaEmisionBetweenSearchItem"
            ShowAndOrButtons="true"
            SearchColumnKey="FechaEmision" 
            Title="Fecha de Recepción" />
     <uc1:SC_TextSearchItem ID="SC_UsuarioTextSearchItem" runat="server" 
            ShowAndOrButtons="true" 
            SearchColumnKey="Usuario" 
            Title="Nombre del Usuario" />
     <uc1:SC_TextSearchItem ID="SC_PacienteSearchItem" runat="server" 
            ShowAndOrButtons="true" 
            SearchColumnKey="Paciente" 
            Title="Nombre del Paciente" />
     <uc1:SC_TextSearchItem ID="SC_NroChequeTextSearchItem" runat="server" 
            ShowAndOrButtons="true" 
            SearchColumnKey="NroCheque" 
            Title="Número de Cheque" />
</div>