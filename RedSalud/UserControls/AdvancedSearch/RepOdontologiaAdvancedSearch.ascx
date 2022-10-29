<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RepOdontologiaAdvancedSearch.ascx.cs" Inherits="UserControls_AdvancedSearch_RepOdontologiaAdvancedSearch" %>

<%@ Register Src="~/UserControls/SearchUserControl/SC_TextSearchItem.ascx" TagName="SC_TextSearchItem" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_IntegerSearchItem.ascx" TagName="SC_IntegerSearchItem" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_DateSearchItem.ascx" TagName="SC_DateSearchItem" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_BetweenDateSearchItem.ascx" TagPrefix="uc1" TagName="SC_BetweenDateSearchItem" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_DptosSearchItem.ascx" TagPrefix="uc1" TagName="SC_DptosSearchItem" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_TipoCasoMedicoSearchItem.ascx" TagPrefix="uc1" TagName="SC_TipoCasoMedicoSearchItem" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_PrestacionSearchItem.ascx" TagPrefix="uc1" TagName="SC_PrestacionSearchItem" %>
<%@ Register Src="~/UserControls/SearchUserControl/SC_CiudadSearchItem.ascx" TagPrefix="uc1" TagName="SC_CiudadSearchItem" %>


<div id="formClientsSearch">
    <uc1:SC_IntegerSearchItem ID="SC_CodigoSearchItem" runat="server" 
            ShowAndOrButtons="false"
            SearchColumnKey="CodigoCaso" 
            Title="Código del Caso" />
    <uc1:SC_BetweenDateSearchItem runat="server" ID="SC_FechaCreacionBetweenSearchItem"
            ShowAndOrButtons="true"
            SearchColumnKey="FechaCreacion" 
            Title="Fecha de Creacion" />
    <uc1:SC_TextSearchItem ID="SC_TextSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="CodigoAsegurado" 
            Title="Código del Asegurado" />
    <uc1:SC_TextSearchItem runat="server" ID="SC_NumeroPolizaTextSearchItem"
            ShowAndOrButtons="true"
            SearchColumnKey="NumeroPoliza" 
            Title="Número de Poliza"  />
    <uc1:SC_PrestacionSearchItem runat="server" id="SC_PrestacionSearchItem"
            ShowAndOrButtons="true"
            SearchColumnKey="Prestacion" 
            Title="Prestación" />
    <uc1:SC_DptosSearchItem runat="server" ID="SC_CiudadSearchItem"
            ShowAndOrButtons="true" 
            SearchColumnKey="Ciudad" 
            Title="Ciudad" />
</div>