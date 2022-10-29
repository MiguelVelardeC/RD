<%@ Page Title="Historial" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="Historial.aspx.cs" Inherits="CasoMedico_Historial" %>

<%@ Register Src="~/UserControls/AngularControl.ascx" TagPrefix="RedSalud" TagName="AngularControl" %>
<%@ Register Src="~/UserControls/FileManager.ascx" TagPrefix="RedSalud" TagName="FileManager" %>
<%@ Register Src="~/UserControls/FotoPaciente.ascx" TagPrefix="RedSalud" TagName="FotoPaciente" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ConvertToNumber(variable) {
            if (variable != null && variable != undefined) {
                var result = parseFloat(variable);
                if (!isNaN(result)) {
                    return result;
                }
            }
            return 0.00;
        }

        function calculateIMC(peso, estatura) {
            if (peso != null && peso != undefined && estatura != null && estatura != undefined) {
                if (estatura > 0 && peso > 0) {
                    var pesoSqd = (peso * peso);
                    return estatura / pesoSqd;
                }
            }

            return 0.00;
        }

        function getIMCDescription(imc) {
            switch (true) {
                case imc < 0:
                    return "";
                case imc == 0:
                    return "";
                case imc < 16.00:
                    return 'Infrapeso: Delgadez Severa';                    
                case imc >= 16.00 && imc <= 16.99:
                    return 'Infrapeso: Delgadez moderada';
                case imc >= 17.00 && imc <= 18.49:
                    return 'Infrapeso: Delgadez aceptable';
                case imc >= 18.50 && imc <= 24.99:
                    return 'Peso Normal';
                case imc >= 25.00 && imc <= 29.99:
                    return 'Sobrepeso';
                case imc >= 30.00 && imc <= 34.99:
                    return 'Obeso: Tipo I';
                case imc >= 35.00 && imc <= 40.00:
                    return 'Obeso: Tipo II';
                case imc > 40.00:
                    return 'Obeso: Tipo III';
            }
        }
    </script>
    <script src="../Scripts/angular.min.js"></script>
    <script src="../Scripts/angularControllerCreator.js"></script>
    <asp:Literal ID="cssCritic" runat="server" />
    <style id="printcss" type="text/css">
        .AlternateDiv {
            background-color: #F7F7F7;
        }
        @media print {
            table.tableAlignBottom {font-size: 14px !important; } 
            .Page { box-shadow: none !important; } 
            .RadGrid_Default .rgMasterTable, #body, .label {font-size:14px!important;}
            #body{ height:auto;font-size: 12px!important;}
            thead { display: table-header-group;height: 80px;overflow: hidden;}
            tbody {display: table-row-group;}
            tfoot {display: table-footer-group;margin-top: 40px;padding-top: 40px;height: 20px;overflow: hidden;}
            .fieldset{padding: 10px 0px 10px 0px;}
            table {-fs-table-paginate: paginate;height: 99%;}
            .labelTab {width: 100%;background-color: #DDD;}
            .noPrintcss {display: none}
            .noPrint,.PrintIcon, .ExpandCollapseIcon  {display: none !important;}
            fieldset {
                page-break-inside: avoid;
                border: none !important;
                padding: 10px 0px 10px 0px;
            }

                fieldset legend {
                    font-weight: bold;
                }

            body {
                font-size: 14px !important;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn" ng-app>
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                    Text="Historial de caso medico del Paciente">
                </asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu noPrintcss">
                    <asp:LinkButton ID="ReturlLB" Text="Volver al Caso Detalle del Paciente" runat="server"
                        OnClick="ReturlLB_Click" />
                    <asp:HyperLink ID="returnHL" NavigateUrl="~/CasoMedico/CasoMedicoLista.aspx" runat="server"
                        Text="Volver al Listado de Casos Medicos" />
                    <asp:HyperLink ID="Print" runat="server"
                        Text="Imprimir" />
                    <asp:LinkButton ID="RefreshHyperLink" OnClick="RefreshHyperLink_Click" runat="server"
                        Text="Actualizar" />
                </div>
                <div class="clear"></div>

                <asp:Panel ID="infoPaciente" CssClass="critic" runat="server" GroupingText="Información del paciente Asegurado">
                    <div style="float:left;width: 60%;">
                        <span class="label">Nombre del paciente Asegurado</span>
                        <asp:Label ID="NombreLabel" Text="" runat="server" />
                        <span class="label">Género</span>
                        <asp:Label ID="GeneroLabel" Text="" runat="server" />
                        <span class="label">Teléfono</span>
                        <asp:Label ID="TelefonoLabel" Text="" runat="server" />
                        <asp:Repeater ID="EnfermedadesCronicasRepeater" runat="server">
                            <HeaderTemplate><span class="label">ENFERMEDADES CRÓNICAS DEL PACIENTE</span><ul></HeaderTemplate>
                            <ItemTemplate><li style="font-weight:normal;"><asp:Literal ID="Literal1" Text='<%# Eval("Nombre") %>' runat="server" /></li></ItemTemplate>
                            <FooterTemplate></ul></FooterTemplate>
                        </asp:Repeater>
                        <!-- Antecedentes Familiares -->
                        <asp:Label Text="ANTECEDENTES PERSONALES Y PATOL&#243;GICOS" runat="server" class="label" />
                        <RedSalud:AngularControl runat="server" ID="Antecedentes" readOnly="true" maxLength="620" JSonData='<%# Bind("Antecedentes") %>' />

                        <asp:Label ID="AntecedentesGinecoNombreLabel" runat="server" CssClass="label">Antecedentes Ginecoobstetricos</asp:Label>
                        <RedSalud:AngularControl runat="server" ID="AntecedentesGinecoLabel" CssClass="AntecedentesGinecoobstetricosPanel" readOnly="true" maxLength="1950" />
                    </div>
                    <div style="float: right;">
                        <RedSalud:FotoPaciente runat="server" id="FotoPaciente" />
                    </div>
                    <div class="clear"></div>
                </asp:Panel>
                <br />
                <span class="label" style="margin-bottom: 5px; font-size: 14px;">Historial del paciente </span>
                <br />
                <asp:Repeater ID="HistorialRepeater" runat="server"
                    DataSourceID="HistorialODS" OnItemDataBound="HistorialRepeater_ItemDataBound">
                    <ItemTemplate>
                        <script type="text/javascript">                            
                        </script>
                        <div class='<%#(Container.ItemIndex % 2 == 0) ? "" : "AlternateDiv" %>' style='<%# ((Container.ItemIndex % 2 == 0) ? "background-color: #FFF;" : "") + "padding: 0px 0px 10px 10px;" %>'>
                            <asp:Panel ID="pnlGroupHistorial" runat="server"
                                GroupingText='<%# Eval("GroupHistorial") %>'
                                CssClass="critic ExpandCollapse" Width="100%">
                                <div style="display: none;">
                                    <asp:HiddenField ID="CasoIdHF" runat="server" Value='<%# Eval("CasoId") %>' />
                                    <table class="tableAlignBottom">
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
                                                <asp:Label ID="Label3" Text='<%# Eval("FechaCreacion") %>' runat="server" /></td>
                                            <td style="padding-left: 15px;"><span class="label">Estado</span></td>
                                            <td>
                                                <asp:Label ID="Label6" Text='<%# Eval("Estado") %>' runat="server" /></td>
                                        </tr>
                                    </table>
                                    <div style="border-top: 1px dashed #AAA;height:20px;"></div>

                                    <span class="label">Proveedor de la Emergencia</span>
                                    <asp:Label ID="Label17" Text='<%# Eval("NombreProveedorJuridico") %>' runat="server" />

                                    <span class="label">Medico</span>
                                    <asp:Label ID="Label7" Text='<%# Eval("UserName") %>' runat="server" />
                                    
                                    <asp:Panel ID="Panel3" runat="server" Visible='<%# Eval("MotivoConsultaTipo").ToString() != "ENFERMERIA" %>'>
                                        <asp:Label ID="Label14" runat="server" CssClass="label labelTab" 
                                            Text="Motivo de la consulta"></asp:Label>
                                        <asp:Label ID="Label8" Text='<%# Eval("MotivoConsulta") %>' runat="server" />
                                    </asp:Panel>
                                    
                                    <asp:Panel ID="Panel4" runat="server" Visible='<%# Eval("MotivoConsultaTipo").ToString() == "CASO MÉDICO" ||
                                        Eval("MotivoConsultaTipo").ToString() == "EMERGENCIA"%>'>
                                        <asp:Label ID="Label18" Text="Enfermedad Actual" runat="server" CssClass="label labelTab" />
                                        <asp:Label ID="EnfermedadActualLabel" runat="server"
                                            Text='<%# Bind("EnfermedadActual") %>' />
                                    </asp:Panel>

                                    <span class="label labelTab">Signos vitales</span>
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
                                                <asp:Label ID="IMC_TITLE" runat="server" CssClass="label"
                                                    Text='IMC:' />
                                            </td>
                                            <td>
                                                <asp:Label ID="IMC" runat="server" style=""
                                                    Text='<%# Eval("IMC") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="IMC_DESC" style="" runat="server" 
                                                    Text='<%# Eval("IMC_DESC") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="Panel5" runat="server" Visible='<%# Eval("MotivoConsultaTipo").ToString() == "ENFERMERIA" %>'>
                                        <asp:Label ID="MCLabel" Text="Motivo de la Consulta o Sintomatologia" runat="server" CssClass="label labelTab" />
                                        <asp:Label ID="MotivoConsultaTxt" runat="server"
                                            Text='<%# Bind("MotivoConsulta") %>' />
                                        <asp:Label ID="EFRySLabel" Text="Observaciones" runat="server" CssClass="label labelTab" />
                                        <asp:Label ID="ExFisico" runat="server" Text='<%# Bind("ExFisicoRegionalyDeSistema") %>'  />
                                        <asp:Label ID="BHLabel" Text="Tratamiento" runat="server" CssClass="label labelTab" />
                                        <asp:Label ID="BiometriaHematicaTxt" runat="server"
                                            Text='<%# Bind("BiometriaHematica") %>' />
                                    </asp:Panel>
                                    <asp:Panel ID="Panel6" runat="server" 
                                        Visible='<%# Eval("MotivoConsultaTipo").ToString() != "ENFERMERIA" && 
                                                Eval("MotivoConsultaTipo").ToString() != "ODONTOLOGÍA" %>'>
                                        <asp:Label ID="Label21" Text="Examen Físico Regional y de Sistema" CssClass="label labelTab" 
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
                                                <li ng-repeat="obj in master" style="list-style-position: outside;">{{obj.name}}<br />
                                                    <ul ng-repeat="sub in obj.group" ng-if="obj.group">
                                                        <li style="font-size: 11px; list-style-position: outside;">
                                                            <span style="font-weight: bold;">{{sub.name}}</span><br />
                                                            {{sub.value || "&nbsp;"}}
                                                        </li>
                                                    </ul>
                                                    <span ng-if="!obj.group" style="font-weight: normal;">{{obj.value || "&nbsp;"}}</span>
                                                </li>
                                            </ul>
                                        </div>
                                        <asp:HiddenField ID="exFisicoRegionalydeSistemaTxt" Value='<%# Eval("ExFisicoRegionalyDeSistema") %>' runat="server" />
                                        
                                        <div runat="server" visible='<%# Eval("MotivoConsultaTipo").ToString() != "EMERGENCIA" %>'>
                                            <span class="label labelTab">Resultados de laboratorio y otros exámenes</span>
                                            <asp:Label ID="Label13" Text='<%# Eval("BiometriaHematica") %>' runat="server" />
                                        </div>

                                        <asp:Label ID="Label19" Text="Observaciones" runat="server" CssClass="label labelTab" />
                                        <asp:Label ID="ObservacionesLabel" runat="server" CssClass="ObservacionesLabel"
                                            Text='<%# Bind("Observaciones") %>' />

                                        <span class="label labelTab">Diagnostico presuntivo</span>
                                            <asp:Label ID="DiagnosticoPresuntivoTxt" runat="server" style="display: block"
                                                Text='<%# Bind("DiagnosticoPresuntivoForDisplay") %>' />
                                            <asp:Label ID="DiagnosticoPresuntivo2Txt" runat="server" style="display: block"
                                                Text='<%# Bind("Enfermedad2") %>' />
                                            <asp:Label ID="DiagnosticoPresuntivo3Txt" runat="server" style="display: block"
                                                Text='<%# Bind("Enfermedad3") %>' />
                                            <asp:Label ID="DiagnosticoPresuntivo4Txt" runat="server" style="display: block"
                                                Text='<%# Bind("DiagnosticoPresuntivoExtra") %>' />
                                    </asp:Panel>
                                    <asp:Panel ID="Panel1" runat="server"  CssClass="tables"
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
                                                                <asp:Label ID="Label20" CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel2" runat="server" CssClass="tables"
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
                                    
                                    <div class="tables" runat="server" visible='<%# Eval("MotivoConsultaTipo").ToString() != "ENFERMERIA"%>'>
                                        <span id="Span1" class="label labelTab" runat="server" visible='<%# !Eval("MotivoConsultaTipo").ToString().StartsWith("ODO")%>'>Lista Exámenes Complementarios / Derivación a especialista / Internación</span>
                                        <span id="Span2" class="label labelTab" runat="server" visible='<%# Eval("MotivoConsultaTipo").ToString().StartsWith("ODO") %>'>Lista PRESTACIONES ODONTOLÓGICAS / Exámenes Complementarios / Derivación a especialista / Internación</span>
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
                    <SeparatorTemplate>
                        <div style="border-bottom: 1px dashed #AAA;"></div>
                    </SeparatorTemplate>
                </asp:Repeater>
                <asp:ObjectDataSource ID="HistorialODS" runat="server"
                    TypeName="Artexacta.App.Caso.BLL.CasoBLL"
                    SelectMethod="HistorialPaciente"
                    OnSelected="HistorialODS_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="PacienteIdHF" Name="PacienteId" PropertyName="Value" Type="Int32" />
                        <asp:Parameter Name="CasoId" Type="Int32" DefaultValue="0" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <script type="text/javascript">
                    $(document).ready(function () {
                        var divExpandCollapse = '<div class="ExpandCollapseIcon"></div>';
                        var divPrint = '<div class="PrintIcon"></div>';
                        $(".ExpandCollapse fieldset legend").append(divExpandCollapse);
                        $(".ExpandCollapse fieldset legend").append(divPrint);

                        if ($('#<%= ViewModeHF.ClientID %>').val() == "P") {
                            $(".ExpandCollapse fieldset legend .ExpandCollapseIcon").each(function () {
                                $(this).parent().parent().children("div").slideToggle('fast');
                            });
                        }

                        $(".ExpandCollapse fieldset legend .ExpandCollapseIcon").click(function () {
                            //$(this).parent().children("div").toggle("slow");

                            if ($(this).is('.Expand')) {//colapsar
                                $(this).removeClass('Expand')
                                $(this).parent().parent().children("div").slideToggle('slow');
                            }
                            else {
                                $(this).addClass('Expand')//expandir
                                $(this).parent().parent().children("div").slideToggle('slow');
                            }

                        });
                        $('#<%=Print.ClientID%>, .ExpandCollapse fieldset legend .PrintIcon').click(function () {
                            var casoId = 0;
                            if ($(this).hasClass('PrintIcon')) {
                                casoId = parseInt($(this).parent().next().children("input[type='hidden']").val());
                            }
                            imprimir(casoId);
                        });
                    });
                    $.fn.outerHTML = function () {
                        return $(this).clone().wrap('<div></div>').parent().html();
                    };
                    function imprimir(casoId) {
                        var Options = 'toolbar=no,location=no,directories=no,status=no, menubar=no, scrollbars=yes, resizable=no, width=800, height=600, top=' + ((screen.height / 2) - 300) + ', left=' + ((screen.width / 2) - 400);
                        var ids = '?PacienteId=' + $('#<%= PacienteIdHF.ClientID %>').val();
                        if (casoId != 0) {
                            ids += '&CasoId=' + casoId;
                        }

                        window.open('<%= ResolveClientUrl("~/CasoMedico/HistorialPrint.aspx")%>' + ids, 'Impresión de Historias Clinicas', Options);
                    }
                    function imprimirV1(casoId) {
                        var isChrome = /chrom(e|ium)/.test(navigator.userAgent.toLowerCase());
                        var bodyHtml = '';
                        var chromeFooter = '<div class="clear"></div>' +
                                   '<div style="text-align: center;margin:40px 2% 0 2%; width:96%;display: table;"><div style="display: table-row;">\n' +
                                   '<div style="display: table-cell;width:33%;"><b>FECHA:</b> <%= Artexacta.App.Configuration.Configuration.ConvertToClientTimeZone(DateTime.UtcNow).ToString("dd/MM/yyyy hh:mm tt") %></div>\n' +
                                   '<div style="display: table-cell;width:36%;"><b style="padding: 0 4%;border-top:1pt solid #111;">FIRMA Y SELLO DEL MÉDICO</b></div>\n' +
                                   '<div style="display: table-cell;width:30%;"><b style="padding: 0 4%;border-top:1pt solid #111;">FIRMA DEL PACIENTE</b></div>\n' +
                                   '</div></div>\n';
                        $("input[id$='CasoIdHF']").each(function () {
                            if ($(this).val() == casoId || casoId == 0) {
                                var container = $(this).parent().parent().parent();
                                if (container.hasClass("ExpandCollapse")) {
                                    var display = $(container).children('fieldset').children('div').css('display');
                                    if (display == 'none') $(container).children('fieldset').children('div').css('display', '');
                                    if (isChrome) {
                                        bodyHtml += '<div>' + container.html() + chromeFooter + '</div>\n';
                                    } else {
                                        bodyHtml += '<div>' + container.html() + '</div>\n';
                                    }
                                    if (display == 'none') $(container).children('fieldset').children('div').css('display', 'none');
                                    if (casoId > 0) { return false; }
                                }
                            }
                        });

                        var host = window.location.hostname;
                        var stylesheets = '';
                        $('link.Telerik_stylesheet').each(function () {
                            stylesheets += $(this).outerHTML().replace('href="', 'href="http://' + host) + '\n';
                        });
                        var footer = isChrome ? '' : '<div class="clear"></div>' +
                                   //position: fixed;bottom: 0;
                                   '<div style="text-align: center;margin:40px 2% 0; width:100%;display: table;font-size:10px;"><div style="display: table-row;">\n' +
                                   '<div style="display: table-cell;width:33%;"><b>FECHA:</b> <%= Artexacta.App.Configuration.Configuration.ConvertToClientTimeZone(DateTime.UtcNow).ToString("dd/MM/yyyy hh:mm tt") %></div>\n' +
                                   '<div style="display: table-cell;width:36%;"><b style="padding: 0 4%;border-top:1pt solid #111;">FIRMA Y SELLO DEL MÉDICO</b></div>\n' +
                                   '<div style="display: table-cell;width:30%;"><b style="padding: 0 4%;border-top:1pt solid #111;">FIRMA DEL PACIENTE</b></div>\n' +
                                   '</div></div>\n';
                        host = host == 'localhost' ? host + '/RS' : host;
                        var title = 'Historia_Clinica_[' + $('#<%=NombreLabel.ClientID%>').html().replace(/\s+/g, '_') + ']';
                        var print = '<a class="noPrintcss" style="position:fixed; top:5px; right:5px;" onclick="window.print();window.close();">Imprimir</a>';
                        var table = '<html>\n<head><title>' + title + '</title>' + stylesheets + 
                                   '<link rel="stylesheet" type="text/css" href="http://' + host + '/App_Themes/Default/CommonStyle.css">\n' +
                                   '<style> ' + $('#printcss').html() +
                                   '    span, li{font-size: 10px!important;}#body{overflow:} table.tableAlignBottom {font-size: 10px!important; } .noPrint {display: none !important;} .Page { box-shadow: none !important; } .RadGrid_Default .rgMasterTable, #body, .label {font-size:10px!important;} .labelTab {background-color: #DDD!important;width: 100%;}' +
                                   '    .labelTab {width: 100%;background-color: #DDD;overflow: hidden;} .RadGrid_Default .rgAltRow, .RadGrid_Default .rgAltRow td {background: none!important;} </style>\n</head>\n' +
                                   '<body>\n' +
                                   '<div id="body">\n' +
                                   '    <div class="pageContent">\n' +
                                   '        <div class="oneColumn">\n' +
                                   '            <div class="frame">\n' +
                                   '                <div class="columnContent">\n' +
                                   '<table><thead><tr><th>' +
                                   '<div class="columnHead">' +
                                   '    <div style="text-align:left;float:left;width:30%;">' + 
                                   '        <img src="http://' + host + '/Images/LogoPrint.jpg" alt="SISTEMA SISA" style="height: 80px;" />' + 
                                   '    </div>\n' +
                                   '    <span class="title" style="text-align:left;float:left;width:70%;margin: 30px 0 10px;font-size:20px!important;box-sizing:border-box;padding-left:50px;">Historia Clinica</span>' + print +
                                   '</div>\n' +
                                   '</th></tr></thead>' +
                                   '<tfoot><tr><td>' +
                                   footer +
                                   '</td></tr></tfoot>' +
                                   '<tbody><tr><td>' +
                                   '                    <div>' + $('#<%=infoPaciente.ClientID%>').html().replace('../ImageResize.aspx', 'http://' + host + '/ImageResize.aspx').replace('../Images/Neutral/paciente.jpg', 'http://' + host + '/Images/Neutral/paciente.jpg') + '</div>\n' +
                                    bodyHtml +
                                   '                    <div class="clear"></div>' +
                                   '</td></tr></tbody></table>' +
                                   '                </div>' +
                                   '            </div>' +
                                   '        </div>' +
                                   '    </div>' +
                                   '</div>' +
                                   '</body>\n</html>';

                        var win = window.open('', 'Impresión de Historias Clinicas', Options);
                        win.document.body.innerHTML = table.replace(/<fieldset/g, '<div class="fieldset"').replace(/legend/g, 'span class="legend"').replace('font-size: 11px', 'font-size: 10px');
                    }
                </script>
                <asp:HiddenField ID="CasoIdHF" runat="server" />
                <asp:HiddenField ID="PacienteIdHF" runat="server" />
                <asp:HiddenField ID="IsCriticoHF" runat="server" />
                <asp:HiddenField ID="ViewModeHF" runat="server" Value="N" />
                <asp:HiddenField ID="CitaDesgravamenIdHiddenField" runat="server" Value="0" />
            </div>
        </div>
    </div>
    <RedSalud:FileManager runat="server" ID="FileManager" />
</asp:Content>