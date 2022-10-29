<%@ Page Title="Historial" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="HistorialPrint.aspx.cs" Inherits="CasoMedico_Historial" %>

<%@ Register Src="~/UserControls/AngularControlPrint.ascx" TagPrefix="RedSalud" TagName="AngularControlPrint" %>
<%@ Register Src="~/UserControls/FileManager.ascx" TagPrefix="RedSalud" TagName="FileManager" %>
<%@ Register Src="~/UserControls/FotoPaciente.ascx" TagPrefix="RedSalud" TagName="FotoPaciente" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/angular.min.js"></script>
    <script src="../Scripts/angularControllerCreator.js"></script>
    <style type="text/css">

        @media print {
            .noPrintcss {
                display: none;
            }

            .noPrint, .PrintIcon, .ExpandCollapseIcon {
                display: none !important;
            }
        }
        
        @media hiqpdf {

            body {
                font-size: 12px !important;
            }

            table.tableAlignBottom {
            }

            .Page {
                box-shadow: none !important;
            }

            .RadGrid_Default .rgMasterTable, #body, .label {
            }

            #body {
                height: auto;
            }

            thead {
                display: table-header-group;
                height: 80px;
                overflow: hidden;
            }

            tbody {
                display: table-row-group;
            }

            tfoot {
                display: table-footer-group;
                margin-top: 40px;
                padding-top: 40px;
                height: 20px;
                overflow: hidden;
            }

            .fieldset {
                padding: 10px 0px 10px 0px;
            }

            table {
                -fs-table-paginate: paginate;
                height: 99%;
            }

            .labelTab {
                width: 100%;
                background-color: #DDD;
            }

            .noPrintcss {
                display: none;
            }

            .noPrint, .PrintIcon, .ExpandCollapseIcon {
                display: none !important;
            }

            fieldset {
                page-break-inside: avoid;
                border: none !important;
                padding: 10px 0px 10px 0px;
            }

            fieldset legend {
                font-weight: bold;
            }

            .AlternateDiv {
                background-color: #FFF;
            }

            .RadGrid_Default .rgAltRow,
            .RadGrid_Default .rgAltRow td {
                background: none!important;
            }

            .RadGrid .rgHeader {
                padding-top: 0;
                padding-bottom: 0;
                line-height: 10px!important;
            }
        }
        .casoMidSections {
            margin-top: 15px !important;
        }

        /*---*/
        body {
            font-size: 10px !important;
        }

        .AlternateDiv {
            background-color: #F7F7F7;
        }
        .Page {
            width: auto!important;
        }

        span, li {
        }

        table.tableAlignBottom {
        }

        .noPrint {
            display: none !important;
        }

        .Page {
            box-shadow: none !important;
        }

        .RadGrid_Default .rgMasterTable, #body, .label {
        }

        .labelTab {
            background-color: #DDD!important;
            width: 100%;
        }

        .labelTab {
            width: 100%;
            background-color: #DDD;
            overflow: hidden;
        }

        .RadGrid_Default .rgAltRow, .RadGrid_Default .rgAltRow td {
            background: none!important;
            font-size: 12px !important;
            line-height: 12px !important;
        }

        .RadGrid_Default .rgHeader {
            font-size: 12px !important;
            line-height: 12px !important;
        }

        .RadGrid_Default .rgNoRecords {
            font-size: 12px !important;
            line-height: 12px !important;
        }

        .legend {
            background-color: lightgray;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn" ng-app>
        <div class="frame">
            <div class="columnContent">
                <table style="width: 100%;">
                    <thead>
                        <tr>
                            <th>
                                <div class="columnHead">
                                    <div style="text-align: left; float: left; width: 30%;">
                                        <asp:Image ImageUrl="~/Images/LogoPrint.jpg" AlternateText="SISTEMA SISA" Height="80px" runat="server" />
                                    </div>
                                    <span class="title" style="text-align: left; float: left; width: 70%; margin: 30px 0 10px; font-size: 20px!important; box-sizing: border-box; padding-left: 50px;">Historia Clinica <asp:Image ID="printIcon" runat="server" ImageUrl="~/Images/Neutral/ExportPrint.gif" CssClass="noPrintcss" /></span>
                                    <%--<asp:LinkButton ID="ImprimirButton" runat="server" OnClick="ImprimirButton_Click" CssClass="noPrintcss" Text="Imprimir"></asp:LinkButton> --%>
                                    <%--<asp:Image ID="printIcon" runat="server" ImageUrl="~/Images/Neutral/ExportPrint.gif" CssClass="noPrintcss" />--%>
                                    <!-- <a class="noPrintcss" style="position:fixed; top:5px; right:5px;" onclick="window.print();window.close();">Imprimir</a>-->
                                </div>
                            </th>
                        </tr>
                    </thead>
                    <tfoot>
                        <tr>
                            <td>
                                <div class="clear"></div>
                                <div style="text-align: center; margin: 40px 2% 0; width: 100%; display: table; font-size: 10px;">
                                    <div style="display: table-row;">
                                        <div style="display: table-cell; width: 33%;"><b>FECHA:</b> <%= Artexacta.App.Configuration.Configuration.ConvertToClientTimeZone(DateTime.UtcNow).ToString("dd/MM/yyyy hh:mm tt") %></div>
                                        <div style="display: table-cell; width: 36%;"><b style="padding: 0 4%; border-top: 1pt solid #111;">FIRMA Y SELLO DEL MÉDICO</b></div>
                                        <div style="display: table-cell; width: 30%;"><b style="padding: 0 4%; border-top: 1pt solid #111; text-transform: uppercase;">FIRMA - <asp:Literal ID="LITPacienteNombre" runat="server"></asp:Literal></b></div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tfoot>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Panel ID="infoPaciente" CssClass="fieldset" runat="server">
                                    <asp:Label Text="Información del paciente Asegurado" CssClass="legend" runat="server" />
                                    <div style="float: left; width: 60%;">
                                        <span class="label">Nombre del paciente Asegurado</span>
                                        <asp:Label ID="NombreLabel" Text="" runat="server" />
                                        <span class="label">Género</span>
                                        <asp:Label ID="GeneroLabel" Text="" runat="server" />
                                        <span class="label">Fecha De Nacimiento</span>
                                        <asp:Label ID="FechaNacLabel" Text="" runat="server" />
                                        <span class="label">Edad</span>
                                        <asp:Label ID="EdadLabel" Text="" runat="server" />
                                        <span class="label">Teléfono</span>
                                        <asp:Label ID="TelefonoLabel" Text="" runat="server" />
                                        <asp:Repeater ID="EnfermedadesCronicasRepeater" runat="server">
                                            <HeaderTemplate>
                                                <span class="label">ENFERMEDADES CRÓNICAS DEL PACIENTE</span><ul>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <li style="font-weight: normal;">
                                                    <asp:Literal Text='<%# Eval("Nombre") %>' runat="server" /></li>
                                            </ItemTemplate>
                                            <FooterTemplate></ul></FooterTemplate>
                                        </asp:Repeater>

                                        <asp:Label Text="Antecedentes Personales y Patol&#243;gicos" runat="server" class="label" Style="margin: 20px 0; background-color: #EAEAEA;" />
                                        <RedSalud:AngularControlPrint runat="server" ID="Antecedentes" readOnly="true" maxLength="700" JSonData='<%# Bind("Antecedentes") %>' />                                        
                                        <asp:Label ID="AntecedentesGinecoNombreLabel" runat="server" CssClass="label" Style="background-color: #EAEAEA;">Antecedentes Ginecoobstetricos</asp:Label>
                                        <RedSalud:AngularControlPrint runat="server" ID="AntecedentesGinecoLabel" CssClass="AntecedentesGinecoobstetricosPanel" readOnly="true" maxLength="1950" />
                                    </div>
                                    <div style="float: right;">
                                        <RedSalud:FotoPaciente runat="server" ID="FotoPaciente" />
                                    </div>
                                    <div class="clear"></div>
                                </asp:Panel>
                                <br />
                                <asp:Repeater ID="HistorialRepeater" runat="server"
                                    DataSourceID="HistorialODS" OnItemDataBound="HistorialRepeater_ItemDataBound">
                                    <ItemTemplate>
                                        <div class='<%#(Container.ItemIndex % 2 == 0) ? "" : "AlternateDiv" %>' 
                                            style='<%# ((Container.ItemIndex % 2 == 0) ? "background-color: #FFF;" : "") + "padding: 0px 0px 10px 0;" %>'>
                                            <asp:Panel ID="pnlGroupHistorial" runat="server"
                                                CssClass="fieldset" Width="100%">
                                                <asp:Label Text='<%# Eval("GroupHistorial") %>' CssClass="legend" runat="server" />
                                                <div>
                                                    <asp:HiddenField ID="CasoIdHF" runat="server" Value='<%# Eval("CasoId") %>' />
                                                    <table class="tableAlignBottom" style="margin-top: 10px;">
                                                        <tr>
                                                            <td><span class="label">Código Caso</span></td>
                                                            <td>
                                                                <asp:Label ID="Label1" Text='<%# Eval("CodigoCaso") %>' runat="server" /></td>
                                                            <td style="padding-left: 15px;"><span class="label">Correlativo</span></td>
                                                            <td>
                                                                <asp:Label ID="Label2" Text='<%# Eval("Correlativo") %>' runat="server" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td><span class="label">Número de Póliza</span></td>
                                                            <td>
                                                                <asp:Label ID="Label4" Text='<%# Eval("NumeroPoliza") %>' runat="server" /></td>
                                                            <td style="padding-left: 15px;"><span class="label">Motivo de la Consulta</span></td>
                                                            <td>
                                                                <asp:Label ID="Label5" Text='<%# Eval("MotivoConsultaTipo") %>' runat="server" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td><span class="label">Fecha del Caso medico</span></td>
                                                            <td>
                                                                <asp:Label ID="Label3" Text='<%# Eval("FechaCreacion") %>' runat="server" />
                                                            </td>
                                                            <td style="padding-left: 15px;"><span class="label">Estado</span></td>
                                                            <td>
                                                                <asp:Label ID="Label6" Text='<%# Eval("Estado") %>' runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><span class="label">Medico</span></td>
                                                            <td><asp:Label ID="Label7" Text='<%# Eval("UserName") %>' runat="server" /></td>
                                                        </tr>
                                                    </table>                                                    
                                                    <div style="border-top: 1px dashed #AAA; height: 5px; margin-top: 5px;"></div>

                                                    <asp:Panel CssClass="casoMidSections" runat="server" Visible='<%# Eval("MotivoConsultaTipo").ToString() != "ENFERMERIA" %>'>
                                                        <asp:Label ID="Label14" runat="server" CssClass="label labelTab"
                                                            Text='<%# Eval("MotivoConsultaTipo").ToString() == "CASO MÉDICO"? "Motivo de la consulta" : "Observaciones para la emergencia" %>'></asp:Label>
                                                        <asp:Label ID="Label8" Text='<%# Eval("MotivoConsulta") %>' runat="server" />
                                                    </asp:Panel>

                                                    <asp:Panel CssClass="casoMidSections" runat="server" Visible='<%# Eval("MotivoConsultaTipo").ToString() == "CASO MÉDICO" %>'>
                                                        <asp:Label Text="Enfermedad Actual" runat="server" CssClass="label labelTab" />
                                                        <asp:Label ID="EnfermedadActualLabel" runat="server"
                                                            Text='<%# Bind("EnfermedadActual") %>' />
                                                    </asp:Panel>

                                                    <span class="label labelTab casoMidSections">Signos vitales</span>
                                                    <table class="tableAlignBottom">
                                                        <tr>
                                                            <td><span class="label">Presión Arterial</span></td>
                                                            <td>
                                                                <asp:Label ID="Label9" Text='<%# Eval("PresionArterial") %>' runat="server" /></td>
                                                            <td style="padding-left: 15px;"><span class="label">Temperatura</span></td>
                                                            <td>
                                                                <asp:Label ID="Label11" Text='<%# Eval("Temperatura") %>' runat="server" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td><span class="label">Pulso</span></td>
                                                            <td>
                                                                <asp:Label ID="Label10" Text='<%# Eval("Pulso") %>' runat="server" /></td>
                                                            <td style="padding-left: 15px;"><span class="label">Frecuencia Cardiaca</span></td>
                                                            <td>
                                                                <asp:Label ID="Label12" Text='<%# Eval("FrecuenciaCardiaca") %>' runat="server" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label16" Text="Peso" runat="server" CssClass="label" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label38" runat="server"
                                                                    Text='<%# Eval("PesoDisplay") %>' />
                                                            </td>
                                                            <td style="padding-left: 15px;">
                                                                <asp:Label ID="Label39" Text="Estatura (en metros)" runat="server" CssClass="label" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label40" runat="server"
                                                                    Text='<%# Eval("EstaturaM") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="IMC_Title" CssClass="label" Text="IMC:" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="IMC" Text='<%# Eval("IMC") %>' runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="IMC_DESC" Text='<%# Eval("IMC_DESC") %>' runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:Panel CssClass="casoMidSections" runat="server" Visible='<%# Eval("MotivoConsultaTipo").ToString() == "ENFERMERIA" %>'>
                                                        <asp:Label ID="MCLabel" Text="Motivo de la Consulta o Sintomatologia" runat="server" CssClass="label labelTab" />
                                                        <asp:Label ID="MotivoConsultaTxt" runat="server"
                                                            Text='<%# Bind("MotivoConsulta") %>' />
                                                        <asp:Label ID="EFRySLabel" Text="Observaciones" runat="server" CssClass="label labelTab" />
                                                        <asp:Label ID="ExFisico" runat="server" CssClass="label labelTab" Text='<%# Bind("ExFisicoRegionalyDeSistema") %>' />
                                                        <asp:Label ID="BHLabel" Text="Tratamiento" runat="server" CssClass="label labelTab" />
                                                        <asp:Label ID="BiometriaHematicaTxt" runat="server" CssClass="label labelTab"
                                                            Text='<%# Bind("BiometriaHematica") %>' />
                                                    </asp:Panel>
                                                    <asp:Panel CssClass="casoMidSections" ID="Panel6" runat="server" 
                                                        Visible='<%# Eval("MotivoConsultaTipo").ToString() != "ENFERMERIA" && 
                                                                Eval("MotivoConsultaTipo").ToString() != "ODONTOLOGÍA" %>'>
                                                        <asp:Label Text="Examen Físico Regional y de Sistema" CssClass="label labelTab" 
                                                            runat="server" Visible='<%# Eval("MotivoConsultaTipo").ToString() != "EMERGENCIA" %>' />
                                                        <div runat="server" visible='<%# Eval("MotivoConsultaTipo").ToString() == "EMERGENCIA" %>'>
                                                            <style>
                                                                .ng-scope ul {
                                                                    list-style:none;
                                                                    margin: 0 !important;
                                                                }
                                                            </style>
                                                        </div>
                                                        <asp:Literal ID="AngularScript" runat="server" />
                                                        <div id="DivController" runat="server" ng-init="LoadJson()">
                                                            <ul style="margin-left: 15px;">
                                                                <li ng-repeat="obj in master" style="list-style-position: outside; margin-top:15px;">
                                                                    <asp:Label ID="Label17" CssClass='<%# Eval("MotivoConsultaTipo").ToString() == "EMERGENCIA" ? "label labelTab" : "" %>' 
                                                                                style="font-weight: bold;" runat="server">{{obj.name}}:</asp:Label><br />
                                                                    <ul ng-repeat="sub in obj.group" ng-if="obj.group">
                                                                        <li style="list-style-position: outside; margin-top:10px; text-align:justify;">
                                                                            <span style="font-weight: bold;">{{sub.name}}:</span><br />
                                                                            {{sub.value || "&nbsp;"}}
                                                                        </li>
                                                                    </ul>
                                                                    <span ng-if="!obj.group" style="font-weight: normal; text-align:justify;">{{obj.value || "&nbsp;"}}</span>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <asp:HiddenField ID="exFisicoRegionalydeSistemaTxt" Value='<%# Eval("ExFisicoRegionalyDeSistema") %>' runat="server" />
                                                        
                                                        <div runat="server" class="casoMidSections" visible='<%# Eval("MotivoConsultaTipo").ToString() != "EMERGENCIA" %>'>
                                                            <span class="label labelTab">Resultados de laboratorio y otros exámenes</span>
                                                            <asp:Label ID="Label13" Text='<%# Eval("BiometriaHematica") %>' runat="server" Style="display: block; text-align:justify;" />
                                                        </div>

                                                        <asp:Label Text="Observaciones" runat="server" CssClass="label labelTab casoMidSections" />
                                                        <asp:Label ID="ObservacionesLabel" runat="server" CssClass="ObservacionesLabel" Style="display: block; text-align:justify;"
                                                            Text='<%# Bind("Observaciones") %>' />

                                                        <span class="label labelTab casoMidSections">Diagnostico presuntivo</span>
                                                        <asp:Label ID="DiagnosticoPresuntivoTxt" runat="server" Style="display: block; text-align:justify;"
                                                            Text='<%# Bind("DiagnosticoPresuntivoForDisplay") %>' />
                                                        <asp:Label ID="DiagnosticoPresuntivo2Txt" runat="server" Style="display: block; text-align:justify;"
                                                            Text='<%# Bind("Enfermedad2") %>' />
                                                        <asp:Label ID="DiagnosticoPresuntivo3Txt" runat="server" Style="display: block; text-align: justify;"
                                                            Text='<%# Bind("Enfermedad3") %>' />
                                                        <asp:Label ID="DiagnosticoPresuntivo4Txt" runat="server" style="display: block"
                                                            Text='<%# Bind("DiagnosticoPresuntivoExtra") %>' />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel1" runat="server" CssClass="tables casoMidSections"
                                                        Visible='<%# Eval("MotivoConsultaTipo").ToString() != "ENFERMERIA"%>'>
                                                        <span class="label labelTab">Lista de recetas</span>
                                                        <telerik:RadGrid ID="RecetaGrid" runat="server"
                                                            CssClass="RecetaGrid PDFExportRadGrid"
                                                            AutoGenerateColumns="false"
                                                            DataSourceID="RecetaODS"
                                                            Width="100%">
                                                            <MasterTableView>
                                                                <NoRecordsTemplate>
                                                                    <asp:Label ID="MsgRecetaNullLabel" runat="server" Text="No existen Recetas para este caso del paciente."></asp:Label>
                                                                </NoRecordsTemplate>
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="TipoMedicamentoNombre" HeaderText="Presentación" />
                                                                    <telerik:GridBoundColumn DataField="Medicamento" HeaderText="Medicamento" />
                                                                    <telerik:GridBoundColumn DataField="Indicaciones" HeaderText="Indicaciones" />
                                                                    <telerik:GridBoundColumn DataField="FechaCreacionString" HeaderText="Fecha Creación" />
                                                                    <telerik:GridTemplateColumn UniqueName="FileManager" HeaderText="Adjuntos"
                                                                        HeaderStyle-CssClass="noPrint"
                                                                        ItemStyle-CssClass="noPrint"
                                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                                                        <ItemTemplate>
                                                                            <div class="AdjuntosCont">
                                                                                <asp:ImageButton ID="FileManagerIB" runat="server"
                                                                                    ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                                                    CommandName="RECETAS"
                                                                                    CommandArgument='<%# Eval("DetalleId") %>'
                                                                                    OnCommand="FileManager_Command" />
                                                                                <asp:Label CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel2" runat="server" CssClass="tables casoMidSections"
                                                        Visible='<%# Eval("MotivoConsultaTipo").ToString() == "ENFERMERIA"%>'>
                                                        <span class="label labelTab">Lista de medicamentos</span>
                                                        <telerik:RadGrid ID="MedicamentosRadGrid" runat="server"
                                                            CssClass="MedicamentosGrid"
                                                            AutoGenerateColumns="false"
                                                            DataSourceID="MedicamentoODS"
                                                            Width="100%">
                                                            <MasterTableView>
                                                                <NoRecordsTemplate>
                                                                    <asp:Label ID="MsgRecetaNullLabel" runat="server" Text="No existen Medicamentos para este caso del paciente."></asp:Label>
                                                                </NoRecordsTemplate>
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="MedicamentoId" Visible="false" />
                                                                    <telerik:GridBoundColumn DataField="CasoId" Visible="false" />
                                                                    <telerik:GridBoundColumn DataField="TipoMedicamentoNombre" HeaderText="Presentación" />
                                                                    <telerik:GridBoundColumn DataField="MedicamentoNombre" HeaderText="Medicamento" />
                                                                    <telerik:GridBoundColumn DataField="Indicaciones" HeaderText="Indicaciones" />
                                                                    <telerik:GridBoundColumn UniqueName="FechaCreacion" DataField="FechaCreacionString" HeaderText="Fecha Creación" />
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                        <asp:ObjectDataSource ID="MedicamentoODS" runat="server"
                                                            TypeName="Artexacta.App.Medicamento.BLL.MedicamentoBLL"
                                                            OldValuesParameterFormatString="original_{0}"
                                                            SelectMethod="GetAllMedicamentoByCasoId"
                                                            OnSelected="MedicamentoODS_Selected">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>
                                                    </asp:Panel>

                                                    <div class="tables casoMidSections" runat="server" visible='<%# Eval("MotivoConsultaTipo").ToString() != "ENFERMERIA"%>'>
                                                        <span class="label labelTab" runat="server" visible='<%# !Eval("MotivoConsultaTipo").ToString().StartsWith("ODO")%>'>Lista Exámenes Complementarios / Derivación a especialista / Internación / Emergencia</span>
                                                        <span class="label labelTab" runat="server" visible='<%# Eval("MotivoConsultaTipo").ToString().StartsWith("ODO") %>'>Lista PRESTACIONES ODONTOLÓGICAS / Exámenes Complementarios / Derivación a especialista / Internación</span>
                                                        <telerik:RadGrid ID="ListaRadGrid" runat="server"
                                                            AutoGenerateColumns="false"
                                                            DataSourceID="CasoDetalleODS"
                                                            AllowPaging="true"
                                                            PageSize="20"
                                                            Width="100%">
                                                            <MasterTableView>
                                                                <NoRecordsTemplate>
                                                                    <asp:Label ID="CasoGridEmptyLabel" runat="server"
                                                                        Text="No existen datos para este caso medico."></asp:Label>
                                                                </NoRecordsTemplate>
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="NombreAnalisisRealizado" HeaderText="Prestaciones Médicas" />
                                                                    <telerik:GridBoundColumn DataField="TipoEstudio" HeaderText="Tipo de Estudio" />
                                                                    <telerik:GridBoundColumn DataField="NombreProveedor" HeaderText="Nombre del Proveedor" />
                                                                    <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" />
                                                                    <telerik:GridBoundColumn DataField="FechaCreacion" HeaderText="Fecha Creación" />
                                                                    <telerik:GridBoundColumn DataField="Table" HeaderText="Table" Visible="false" />
                                                                    <telerik:GridTemplateColumn UniqueName="FileManager" HeaderText="Adjuntos"
                                                                        HeaderStyle-CssClass="noPrint"
                                                                        ItemStyle-CssClass="noPrint"
                                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                                                        <ItemTemplate>
                                                                            <div class="AdjuntosCont">
                                                                                <asp:ImageButton ID="FileManagerIB" runat="server"
                                                                                    ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                                                    CommandName='<%# Eval("TipoFileManager") %>'
                                                                                    CommandArgument='<%# Eval("Id") %>'
                                                                                    OnCommand="FileManager_Command" />
                                                                                <asp:Label ID="Label15" CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </div>

                                                    <asp:ObjectDataSource ID="RecetaODS" runat="server"
                                                        TypeName="Artexacta.App.Receta.BLL.RecetaBLL"
                                                        OldValuesParameterFormatString="original_{0}"
                                                        SelectMethod="GetAllRecetaByCasoId"
                                                        OnSelected="RecetaODS_Selected">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>

                                                    <asp:ObjectDataSource ID="CasoDetalleODS" runat="server"
                                                        TypeName="Artexacta.App.Caso.CasoForAprobation.BLL.CasoForAprobationBLL"
                                                        OldValuesParameterFormatString="original_{0}"
                                                        SelectMethod="GetCasoListAprobated"
                                                        OnSelected="CasoDetalleODS_Selected">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </div>
                                            </asp:Panel>
                                            <div class="clear"></div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:ObjectDataSource ID="HistorialODS" runat="server"
                                    TypeName="Artexacta.App.Caso.BLL.CasoBLL"
                                    SelectMethod="HistorialPaciente"
                                    OnSelected="HistorialODS_Selected">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="PacienteIdHF" Name="PacienteId" PropertyName="Value" Type="Int32" />
                                        <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" Type="Int32" DefaultValue="0" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <asp:HiddenField ID="CasoIdHF" runat="server" />
                <asp:HiddenField ID="PacienteIdHF" runat="server" />
            </div>
        </div>
    </div>
    <RedSalud:FileManager runat="server" ID="FileManager" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= printIcon.ClientID %>').click(function () {
                window.print();
                return false;
            });
        });
    </script>
</asp:Content>
