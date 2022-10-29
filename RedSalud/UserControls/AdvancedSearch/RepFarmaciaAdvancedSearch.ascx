<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RepFarmaciaAdvancedSearch.ascx.cs" Inherits="UserControls_AdvancedSearch_RepFarmacia" %>

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
    <uc1:SC_TextSearchItem ID="SC_MedicamentoTextSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="Medicamento" 
            Title="Nombre del Medicamento" />
    <uc1:SC_TextSearchItem ID="SC_PresentacionTextSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="Presentacion" 
            Title="Presentación" />
    <uc1:SC_TextSearchItem ID="SC_ConcentracionTextSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="Concentracion" 
            Title="Concentración" />
    <uc1:SC_TextSearchItem ID="SC_TipoDocumentoTextSearchItem" runat="server" 
            ShowAndOrButtons="true"
            SearchColumnKey="TipoDocumento" 
            Title="Tipo de Documento" />
    <uc1:SC_BetweenDateSearchItem runat="server" ID="SC_FechaCreacionBetweenSearchItem"
            ShowAndOrButtons="true"
            SearchColumnKey="FechaCreacion" 
            Title="Fecha de Creacion" />
    <uc1:SC_BetweenDateSearchItem runat="server" ID="SC_FechaGastoBetweenDateSearchItem"
            ShowAndOrButtons="true"
            SearchColumnKey="FechaGasto" 
            Title="Fecha de Gasto" />
</div>