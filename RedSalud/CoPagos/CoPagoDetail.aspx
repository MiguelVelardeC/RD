<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CoPagoDetail.aspx.cs" Inherits="CasoMedico_CoPagoDetail" %>

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
            height: 10px;
        }

        .auto-style3 {
            width: 400px;
        }

        .auto-style6 {
            width: 400px;
            height: 13px;
        }

        .auto-style8 {
            width: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead noPrint">
                <span class="title">COBRO COPAGO</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu noPrint">
                    <asp:HyperLink ID="returnHL" NavigateUrl="~/CoPagos/CopagoLista.aspx" runat="server"
                        Text="VOLVER" />

                    <asp:Image ID="printIcon" runat="server" ImageUrl="~/Images/Neutral/ExportPrint.gif" />
                    <asp:ImageButton ID="pdfIcon" ImageUrl="~/Images/Neutral/pdf-icon-light.png" Width="25" Height="25" runat="server" OnClick="pdfIcon_Click" Visible="false" />
                </div>


                <br />
                <br />

            </div>
        </div>
    </div>

    <div class="threeColsLeft">
        <div class="frame">
            <div class="columnContent" style="margin-top: -5px;">
                <img src="../Images/LogoPrint.jpg" alt="Red Salud" style="height: 95px;" />
            </div>
        </div>
    </div>
    <div class="threeColsCenter" style="width: 42%; margin-left: -135px;">
        <div class="frame">
            <div class="columnContent">
                <h1 id="ordendeservicioh1title" runat="server">COBRO COPAGO-LABORATORIOS     </h1>
                <h2 id="ordenServicioCliente" runat="server" style="text-align: center; font-size: 17px;"></h2>
            </div>
        </div>
    </div>

    <div style="clear: both;"></div>

    <table id="SignosVitalesTable" cellpadding="0" cellspacing="8" runat="server">
        <tr>
            <td class="auto-style1">
                <b>Paciente:</b>
            </td>
            <td class="auto-style3">
                <asp:Literal ID="NombrePaciente" runat="server"></asp:Literal>
            </td>
            <td class="auto-style1">
                <b>
                    <asp:Label ID="TipoDeCoPago" runat="server" Visible="false" /></b>
            </td>
            <td class="auto-style3">
                <asp:Literal ID="CoPagoPorcentaje" runat="server" Visible="false"></asp:Literal>
            </td>

        </tr>
        <tr>
            <td class="auto-style1">
                <b>Cedula Identidad:</b>
            </td>
            <td class="auto-style3">
                <asp:Literal ID="Carnetidentidad" runat="server"></asp:Literal>
            </td>

            <td class="auto-style1">
                <b>CoPago a Cobrar :</b>
            </td>
            <td class="auto-style3">
                <asp:Literal ID="CoPagoPrecio" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <b>Número de Póliza:</b>
            </td>
            <td class="auto-style3">
                <asp:Literal ID="NumeroPoliza" runat="server"></asp:Literal>
            </td>


            <td class="auto-style1">
                <b>Especialidad:</b>
            </td>
            <td class="auto-style3">
                <asp:Literal ID="NombreEspecialidad" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <b>Nombre de Póliza:</b>
            </td>
            <td class="auto-style3">
                <asp:Literal ID="NombrePoliza" runat="server"></asp:Literal>
            </td>
            <td class="auto-style1">
                <b>Código Caso:</b>
            </td>
            <td class="auto-style3">
                <asp:Literal ID="CasoId" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <b>Médico:         </b>
            </td>
            <td class="auto-style6">
                <asp:Literal ID="NombreMedico" runat="server"></asp:Literal>
            </td>
            <td class="auto-style1"></td>

            <td class="auto-style3"></td>


        </tr>
        <tr>
            <td class="auto-style1">
                <b>
                    <asp:Label Text="Diagnóstico:" ID="LabelDiagnostico" runat="server" Visible="true"></asp:Label>
                </b>
            </td>
            <td class="auto-style3">
                <asp:Literal ID="DetalleDiagnostico" runat="server"></asp:Literal>
            </td>
            <td class="auto-style1"></td>
            <td class="auto-style3"></td>


        </tr>
    </table>

    
    <table>
        <tr>
            <td style="width: 292px;">
                <label id="LTIPOCASO" runat="server" visible="false"></label>
            </td>
            <td style="width: 280px;"></td>
            <td style="width: 350px;">
                <div id="EditarCoPagos" class="buttonsPanel">
                    <asp:LinkButton ID="EditStatusHL" runat="server"
                        CssClass="button" OnClick="EditStatusHL_Click">
                        <span id="spbutton" runat="server">COPAGO COBRADO</span>
                    </asp:LinkButton>
                </div>
            </td>
        </tr>
    </table>
    <div class="twoColsRight">
        <div class="frame">
            <div class="columnContent">
            </div>
        </div>
    </div>
    <div style="background-color: #EAEAEA; padding: 2px 2px; margin: 5px 0; font-weight: bold;">
    </div>

    <table id="Table1" cellpadding="0" cellspacing="8" runat="server">
        <tr>
            <td class="auto-style1">
                <b>Sres.:</b>
            </td>
            <td>
                <asp:Literal ID="Sres" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <table id="Table2" cellpadding="0" cellspacing="8" runat="server">
        <tr>
            <td>
                <b>Solicito:</b>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="PrestacionesCoPagoGrid" runat="server"
                    AutoGenerateColumns="false"
                    OnItemDataBound="PrestacioneCoPagoGrid_ItemDataBound"
                    AllowPaging="false"
                    AllowSorting="false"
                    MasterTableView-DataKeyNames="CasoId"
                    OnDetailTableDataBind="PrestacioneCoPagoGrid_DetailTableDataBind">
                    <MasterTableView>

                        <NoRecordsTemplate>
                            <div style="text-align: center;">No hay Copagos registrados</div>
                        </NoRecordsTemplate>
                        <HeaderStyle Font-Size="12px" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="Solicito" Visible="true"
                                HeaderText="Nombre" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="detprecio" Visible="True" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true"
                                HeaderText="Precio Bs." />
                            <telerik:GridBoundColumn DataField="detCoPagoReferencial" Visible="True" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true"
                                HeaderText="CoPago Referencial" />
                            <telerik:GridBoundColumn DataField="detCoPagoReferencialTipo" Visible="True" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true"
                                HeaderText="Tipo De CoPago Referencial" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>

            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="TipoSolicitud" runat="server" Visible="false"></asp:Literal>
                <asp:Label Text="Total : ......................." runat="server"></asp:Label>
                <asp:Literal ID="PrecioTotal" runat="server"></asp:Literal>
                <asp:Label Text="..BS" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <b>Observaciones:</b>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="Observaciones" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <div class="twoColsLeft">
        <div class="frame">
            <asp:Panel runat="server" ID="digitalSignatureLeftPadding" Visible="false" class="columnContent centerText" Height="170px">
            </asp:Panel>
            <b>FECHA:</b> <b>
                <asp:Label ID="lblFechaForm" runat="server" Text="Fecha :"></asp:Label></b>
            <div class="columnContent centerText" style="padding-left: 280px;">
                ----------------------------------------------
                <br />
                <asp:Literal ID="PANombreFirma" runat="server"></asp:Literal>
                <br />
                Firma y Sello Médico
            </div>
        </div>
    </div>
    <asp:HiddenField ID="CitaDesgravamenIdHiddenField" runat="server" Value="0" />
    <asp:HiddenField ID="PropuestoAseguradoIdHiddenField" runat="server" Value="0" />
    <asp:HiddenField ID="PaginaBackHiddenField" runat="server" />
    <asp:HiddenField ID="ProveedorMedicoIdHiddenField" runat="server" />

<%--    <div id="EditarCoPagos" class="buttonsPanel">
                    <asp:LinkButton ID="LinkButton1" runat="server"
                        CssClass="button" OnClick="EditStatusHL_Click">
                        <span id="Span1" runat="server">COPAGO COBRADO</span>
                    </asp:LinkButton>
                </div>--%>
   <%-- <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="EditStatusHL">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="spbutton"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="Content2"></telerik:AjaxUpdatedControl>

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                  </telerik:RadCodeBlock>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= printIcon.ClientID %>').click(function () {
                window.print();
                return false;
            });
        });
    </script>
</asp:Content>
