<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CasoMedicoAdvancedSearch.ascx.cs" Inherits="UserControls_AdvancedSearch_CasoMedicoAdvancedSearch" %>

<%@ Register Src="~/UserControls/SearchUserControl/SC_TextSearchItem.ascx" TagName="SC_TextSearchItem" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_IntegerSearchItem.ascx" TagName="SC_IntegerSearchItem" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_DateSearchItem.ascx" TagName="SC_DateSearchItem" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_BetweenDateSearchItem.ascx" TagPrefix="uc1" TagName="SC_BetweenDateSearchItem" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_DptosSearchItem.ascx" TagPrefix="uc1" TagName="SC_DptosSearchItem" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_TipoCasoMedicoSearchItem.ascx" TagPrefix="uc1" TagName="SC_TipoCasoMedicoSearchItem" %>

<div id="formClientsSearch">
    <uc1:SC_IntegerSearchItem ID="SC_CodigoSearchItem" runat="server" 
            ShowAndOrButtons="false"
            SearchColumnKey="CodigoCaso" 
            Title="Código del Caso" />
    <uc1:SC_TextSearchItem ID="SC_ProveedorSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="ProveedorId" 
            Title="Proveedor"
            Visible="false" />
    <uc1:SC_TextSearchItem ID="SC_NombrePacienteSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="NombrePaciente" 
            Title="Nombre del Paciente" />
    <uc1:SC_TextSearchItem ID="SC_CITextSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="CarnetIdentidad" 
            Title="Carnet de Identidad" />
    <uc1:SC_IntegerSearchItem runat="server" ID="SC_IntegerSearchItem"
            ShowAndOrButtons="true"
            SearchColumnKey="NumeroPoliza" 
            Title="Número de Poliza"  />
    <uc1:SC_BetweenDateSearchItem runat="server" ID="SC_FechaCreacionBetweenSearchItem"
            ShowAndOrButtons="true"
            SearchColumnKey="FechaCreacion" 
            Title="Fecha de Creacion" />
    <uc1:SC_TipoCasoMedicoSearchItem runat="server" ID="SC_TipoCasoMedicoSearchItem"
            ShowAndOrButtons="true" 
            SearchColumnKey="TipoConsulta" 
            Title="Tipo Consulta" />
    <uc1:SC_DptosSearchItem runat="server" ID="SC_CiudadSearchItem"
            ShowAndOrButtons="true" 
            EnableAll="true"
            SearchColumnKey="Ciudad" 
            Title="Ciudad" />
    <uc1:SC_TextSearchItem ID="SC_EnfermedadTextSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="Diagnostico" 
            Title="Diagnostico Presuntivo" />
</div>