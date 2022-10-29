<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PolizaAdvancedSearch.ascx.cs" Inherits="UserControls_AdvancedSearch_CasoMedicoAdvancedSearch" %>

<%@ Register Src="../SearchUserControl/SC_TextSearchItem.ascx" TagName="SC_TextSearchItem" TagPrefix="uc1" %>
<%@ Register Src="../SearchUserControl/SC_IntegerSearchItem.ascx" TagName="SC_IntegerSearchItem" TagPrefix="uc1" %>
<%@ Register Src="../SearchUserControl/SC_DateSearchItem.ascx" TagName="SC_DateSearchItem" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_BetweenDateSearchItem.ascx" TagPrefix="uc1" TagName="SC_BetweenDateSearchItem" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_DptosSearchItem.ascx" TagPrefix="uc1" TagName="SC_DptosSearchItem" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_EstadoPolizaSearchItem.ascx" TagPrefix="uc1" TagName="SC_EstadoPolizaSearchItem" %>


<div id="formClientsSearch">
    <uc1:SC_IntegerSearchItem ID="SC_CodigoSearchItem" runat="server" 
            ShowAndOrButtons="false"
            SearchColumnKey="CodigoAsegurado" 
            Title="Código del Asegurado" />
    <uc1:SC_TextSearchItem ID="SC_NumeroPolizaSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="NumeroPoliza" 
            Title="Número de la Póliza" />
    <uc1:SC_TextSearchItem ID="SC_NombrePacienteSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="NombrePaciente" 
            Title="Nombre del Paciente" />
    <uc1:SC_TextSearchItem runat="server" ID="SC_CarnetIdentidadSearchItem"
            ShowAndOrButtons="true" 
            SearchColumnKey="CarnetIdentidad" 
            Title="Carnet de Identidad del Paciente" />
    <uc1:SC_EstadoPolizaSearchItem runat="server" ID="SC_EstadoPolizaSearchItem"
            ShowAndOrButtons="true" 
            SearchColumnKey="Estado" 
            Title="Estado de la Poliza" />
</div>