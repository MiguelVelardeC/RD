<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CasoAprobationAdvancedSearch.ascx.cs" Inherits="UserControls_AdvancedSearch_CasoAprobationAdvancedSearch" %>

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
    <uc1:SC_TextSearchItem ID="SC_NombrePacienteSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="NombrePaciente" 
            Title="Nombre del Paciente" />
    <uc1:SC_TextSearchItem ID="SC_CodigoAseguradoTextSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="CodigoAsegurado" 
            Title="Código del Asegurado" />
    <uc1:SC_TextSearchItem ID="SC_NumeroPolizaTextSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="NumeroPoliza" 
            Title="Número de Póliza" />
</div>