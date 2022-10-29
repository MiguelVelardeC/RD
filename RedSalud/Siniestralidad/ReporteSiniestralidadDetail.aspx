<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="ReporteSiniestralidadDetail.aspx.cs" Inherits="Siniestralidad_ReporteSiniestralidadDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <style type="text/css">
      @media hiqpdf {
         .noPrintcss {
            display: none;
         }

         .noPrint, .PrintIcon, .ExpandCollapseIcon {
            display: none !important;
         }

         .Page {
            -moz-box-shadow: 0 !important;
            -webkit-box-shadow: 0 !important;
            box-shadow: 0 !important;
         }


         .tHeader {
            background: #EAEAEA !important;
         }
         /** rest of settings **/

      }


      @media print {
         .noPrint {
            display: none;
            visibility: hidden;
         }

         html * {
            background-color: #ffffff;
            border-width: 0px;
         }

         body {
            font-size: 14px !important;
         }
      }

      html * {
         text-transform: uppercase;
      }

      html {
         text-transform: uppercase;
      }

      .pregunta {
         float: left;
         margin-right: 20px;
         width: 45%;
      }

      .observacionesPregunta {
         float: left;
         width: 45%;
      }

      .cellTitle {
         padding: 5px;
         background-color: #e4e4e4;
      }

      .cellFirstCol {
         width: 100px;
      }

      .centerText {
         text-align: center;
      }

      .facturarPA {
         width: 60%;
         margin-left: auto;
         margin-right: auto;
         padding: 10px;
         border: solid 1px;
         text-align: center;
         font-weight: bold;
         background-color: #EAEAEA;
      }

      .row {
         background-color: #C9DFFC;
         font-size: 11px;
      }

      .altRow {
         background-color: #E8F1FF;
         font-size: 11px;
      }

      .headerRow {
         font-size: 11px;
      }

      h1 {
         text-align: center;
      }

      .UpdatedCenterWidth {
         width: 32%;
      }

      .addressContainer {
         /*margin-right: 20px;
        width: 150px;*/
         text-align: left;
      }

      .addressTitle {
         font-weight: bold;
         font-size: 8px;
      }

      .addressBody {
         font-size: 8px;
         margin: 0 !important;
      }

      .borderB {
         border-bottom: 1px solid #828282;
      }

      .borderL {
         border-left: 1px solid #828282;
      }

      .borderR {
         border-right: 1px solid #828282;
      }

      .borderT {
         border-top: 1px solid #828282;
      }

      tr.trC {
         border-collapse: collapse !important;
         padding: 5px 10px !important;
         border: 1px solid #828282 !important;
      }

      td.tdC {
         padding: 2px 10px;
      }

      .tHeader {
         padding: 0 15px !important;
         margin-right: 15px;
         width: 100px;
         background: linear-gradient(white, #EAEAEA);
      }

      .auto-style1 {
         height: 13px;
      }

      .auto-style2 {
         height: 13px;
         width: 180px;
      }

      .auto-style3 {
         width: 233px;
      }

      .auto-style4 {
         width: 200px;
      }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">

   <div class="oneColumn">
      <div class="frame">
         <div class="columnContent">
            <div class="contentMenu noPrint">
               <asp:HyperLink ID="returnHL" NavigateUrl="~/Siniestralidad/ReporteSiniestralidad.aspx" runat="server"
                  Text="VOLVER" />

            </div>
         </div>
      </div>
   </div>
   <div class="threeColsCenter" style="width: 100%; margin-left: -60px;">
      <div class="frame">
         <div style="align-self: center" class="columnContent">
            <h1 id="ordendeservicioh1title" runat="server"><b>REPORTE DE SINIESTRALIDAD</b>   </h1>
            <h2 id="ordenServicioCliente" runat="server" style="text-align: center; font-size: 17px;"></h2>
         </div>
      </div>
   </div>

   <div style="clear: both;"></div>

   <table id="ReporteSiniestralidadxPoliza" cellpadding="0" cellspacing="8" runat="server">
      <tr>
         <td class="auto-style1">
            <b>Paciente:</b>
         </td>
         <td class="auto-style4">
            <asp:Literal ID="NombrePaciente" runat="server"></asp:Literal>
         </td>

         <td class="auto-style2"></td>
         <td class="auto-style1">
            <b>Desde:</b>
         </td>
         <td class="auto-style4">
            <asp:Literal ID="FechaIni" runat="server"></asp:Literal>
         </td>

         <td class="auto-style1">
            <b>Hasta:</b>
         </td>
         <td>
            <asp:Literal ID="FechaFin" runat="server"></asp:Literal>
         </td>
      </tr>
      <tr>
         <td class="auto-style1">
            <b>Cedula Identidad:</b>
         </td>
         <td class="auto-style4">
            <asp:Literal ID="Carnetidentidad" runat="server"></asp:Literal>
         </td>
         <td class="auto-style2"></td>

         <td class="auto-style1">
            <b>MONTO TOPE PRODUCTO:</b>
         </td>
         <td class="auto-style4">
            <asp:Literal ID="MontoTopeProducto" runat="server"></asp:Literal>
         </td>
      </tr>
      <tr>
         <td class="auto-style1">
            <b>Número de Póliza:</b>
         </td>
         <td class="auto-style4">
            <asp:Literal ID="NumeroPoliza" runat="server"></asp:Literal>
         </td>

         <td class="auto-style2"></td>
         <td class="auto-style">
            <b>Monto Acumulado Gestión:</b>
         </td>
         <td class="auto-style4">
            <asp:Literal ID="MontoAcumuladoGestion" runat="server"></asp:Literal>
         </td>
      </tr>
      <tr>
         <td class="auto-style1">
            <b>Nombre de Póliza:</b>
         </td>
         <td class="auto-style4">
            <asp:Literal ID="NombrePoliza" runat="server"></asp:Literal>
         </td>

         <td class="auto-style2"></td>
         <td class="auto-style1">
            <b>Estado:</b>
         </td>
         <td class="auto-style4">
            <asp:Literal ID="Estado" runat="server"></asp:Literal>
         </td>
      </tr>
   </table>
   <b>Cifras en Base a Gestión</b>
   <b>
      <asp:Label ID="GestionLabel" runat="server" /></b>
   <b>/Montos Expresados en Bs.</b>
   <br />
   <br />
     <div class="right RadGrid_Default" style="border: none;">
                    <asp:LinkButton ID="ExportToExcelMatrizNacional" runat="server" OnClick="export_Excel">
                            <span class="left rgExpXLS" style="width:14px; height: 16px;margin-right:5px;"></span>
                            <span class="left">Exportar a Excel </span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="realExportMatrizNacional" runat="server" OnClick="export_Excel"></asp:LinkButton>
                </div>
       <div class="clear" style="margin-bottom: 5px;"></div>
   <telerik:RadGrid ID="ReporteSiniestralidadDetailGrid" runat="server"
      AutoGenerateColumns="false"
      AllowPaging="false"
      AllowSorting="false"
      OnDetailTableDataBind="ReporteSiniestralidadDetailGrid_DetailTableDataBind"
      OnItemDataBound="ReporteSiniestralidadDetailGrid_ItemDataBound">
      <MasterTableView Width="100%">
         <NoRecordsTemplate>
            <div style="text-align: center;">No hay Reportes de Siniestralidad registrados para esa Poliza</div>
         </NoRecordsTemplate>
         <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
         <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
         <HeaderStyle Font-Size="11px" />

         <Columns>
            <telerik:GridBoundColumn DataField="NombrePrestacion" Visible="true"
               HeaderText="Prestacion" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true" />
            <telerik:GridNumericColumn DataField="MontoTope" Visible="true" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true"
               HeaderText="Monto Tope" DataType="System.Decimal" DecimalDigits="2" DataFormatString="{0:f}" />
            <telerik:GridNumericColumn DataField="MontoAcumulado" Visible="true" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true"
               HeaderText="Monto Acumulado" DataType="System.Decimal" DecimalDigits="2" DataFormatString="{0:f}" />
    <%--         <telerik:GridNumericColumn DataField="ValorCoPago" Visible="true" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true"
               HeaderText="Valor CoPago"  DecimalDigits="2" DataFormatString="{0:f}" />  --%>          
            <telerik:GridNumericColumn DataField="CoPagoAcumulado" Visible="true" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true"
               HeaderText="Co-Pago Acumulado" DataType="System.Decimal" DecimalDigits="2" DataFormatString="{0:f}" />
            <telerik:GridBoundColumn DataField="ConsultasPorAnos" Visible="true" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true"
               HeaderText="Consultas Año" DataFormatString="{0:d}" />
            <telerik:GridBoundColumn DataField="ConsultasAcumuladas" Visible="true" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true"
               HeaderText="Consultas Acumuladas" />
         </Columns>

      </MasterTableView>
   </telerik:RadGrid>
    
   <br />
   <br />

   <b>FECHA:</b> <b>
      <asp:Label ID="lblFechaForm" runat="server" Text="Fecha :"></asp:Label></b>
   <br />
   <br />
   <asp:HiddenField ID="CitaDesgravamenIdHiddenField" runat="server" Value="0" />
   <asp:HiddenField ID="PropuestoAseguradoIdHiddenField" runat="server" Value="0" />
   <asp:HiddenField ID="PaginaBackHiddenField" runat="server" />
   <asp:HiddenField ID="ProveedorMedicoIdHiddenField" runat="server" />
   <asp:HiddenField ID="ClienteIdHF" runat="server" />
   <asp:HiddenField ID="PolizaIdHF" runat="server" />


</asp:Content>
