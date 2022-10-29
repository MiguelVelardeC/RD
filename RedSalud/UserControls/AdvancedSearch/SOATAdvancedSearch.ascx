<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SOATAdvancedSearch.ascx.cs" Inherits="UserControls_AdvancedSearch_SOATAdvancedSearch" %>

<%@ Register Src="../SearchUserControl/SC_TextSearchItem.ascx" TagName="SC_TextSearchItem" TagPrefix="uc1" %>
<%@ Register Src="../SearchUserControl/SC_DateSearchItem.ascx" TagName="SC_DateSearchItem" TagPrefix="uc4" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_BetweenDateSearchItem.ascx" TagPrefix="uc1" TagName="SC_BetweenDateSearchItem" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_DptosSearchItem.ascx" TagPrefix="uc1" TagName="SC_DptosSearchItem" %>

<div id="formClientsSearch">
    <uc1:SC_TextSearchItem ID="SC_NombreTitularSearchItem" runat="server" 
            ShowAndOrButtons="false"
            SearchColumnKey="NombreTitular" 
            Title="Nombre del Titular" />
    <uc1:SC_TextSearchItem ID="SC_CITitularSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="CITitular" 
            Title="Carnet de Identidad del Titular" />
    <uc1:SC_TextSearchItem ID="SC_PlacaSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="Placa" 
            Title="Placa del Vehiculo" />
     <uc1:SC_TextSearchItem ID="SC_NumeroSiniestroSearchItem" runat="server" 
            ShowAndOrButtons="true" 
            SearchColumnKey="NumeroSiniestro" 
            Title="Número de Siniestro" />
     <uc1:SC_TextSearchItem ID="SC_NumeroPolizaSearchItem" runat="server" 
            ShowAndOrButtons="true" 
            SearchColumnKey="NumeroPoliza" 
            Title="Número de Póliza" />
     <uc1:SC_TextSearchItem ID="SC_SindicatoSearchItem" runat="server" 
            ShowAndOrButtons="true" 
            SearchColumnKey="Sindicato" 
            Title="Sindicato" />
    <uc1:SC_DptosSearchItem runat="server" ID="SC_DptosSearchItem"
            ShowAndOrButtons="true" 
            SearchColumnKey="Departamento" 
            Title="Departamento" />
     <uc1:SC_TextSearchItem ID="SC_NombreAccidentadoSearchItem" runat="server" 
            ShowAndOrButtons="true" 
            SearchColumnKey="NombreAccidentado" 
            Title="Nombre del Accidentado" />
     <uc1:SC_TextSearchItem ID="SC_CIAccidentadoSearchItem" runat="server" 
            ShowAndOrButtons="true" 
            SearchColumnKey="CIAccidentado" 
            Title="Carnet de Identidad del Accidentado" />
     <uc1:SC_TextSearchItem ID="SC_NombreAuditorTextSearchItem" runat="server" 
            ShowAndOrButtons="true" 
            SearchColumnKey="NombreAuditor" 
            Title="Nombre del Auditor" />
     <uc1:SC_TextSearchItem ID="SC_TextSearchItem5" runat="server" 
            ShowAndOrButtons="true" 
            SearchColumnKey="NumeroSiniestro" 
            Title="Número de Siniestro" />
    <uc1:SC_BetweenDateSearchItem runat="server" ID="SC_FechaSiniestroBetweenSearchItem"
            ShowAndOrButtons="true"
            SearchColumnKey="FechaSiniestro" 
            Title="Fecha de Siniestro" />
    <uc1:SC_BetweenDateSearchItem runat="server" ID="SC_FechaDenunciaBetweenSearchItem"
            ShowAndOrButtons="true"
            SearchColumnKey="FechaDenuncia" 
            Title="Fecha de Denuncia" />
</div>