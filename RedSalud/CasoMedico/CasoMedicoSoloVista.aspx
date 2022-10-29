<%@ Page Title="Caso Medico" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CasoMedicoSoloVista.aspx.cs" Inherits="CasoMedico_CasoMedicoDetalle" %>

<%@ Register Src="~/UserControls/AngularControl.ascx" TagPrefix="RedSalud" TagName="AngularControl" %>
<%@ Register Src="~/UserControls/TipoEstudio.ascx" TagPrefix="RedSalud" TagName="TipoEstudio" %>
<%@ Register Src="~/UserControls/FotoPaciente.ascx" TagPrefix="RedSalud" TagName="FotoPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:Literal ID="cssCritic" runat="server" />
    <style type="text/css">
        ol.DiagnosticoList li
        {
            list-style-position: inside;
            margin: 5px 0;
        }
    </style>
    <script type="text/javascript" src="../Scripts/angular.min.js"></script>
    <script type="text/javascript" src="../Scripts/angularControllerCreator.js"></script>
    <script type="text/javascript">
        function validateAngularJS() {
            var valid = true;
            $('span.angularValidation').each(function () {
                if ($(this).css('display') != 'none') {
                    valid = false;
                }
            });
            return valid;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <!--Angular Controllers-->
    <div class="oneColumn" ng-app>
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="CasoMedicoTitle" CssClass="title" runat="server" />
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink ID="returnHL" NavigateUrl="~/CasoMedico/CasoMedicoLista.aspx" runat="server"
                        Text="Volver al Listado de Casos Medicos" />
                </div>
                <div class="left" style="max-width: 68%; width: 68%;border: 1px solid #AAA;padding: 10px;margin-top: 8px;">
                    <asp:FormView ID="CasoFV" runat="server"
                        DataSourceID="CasoODS">
                        <ItemTemplate>
                            <asp:Panel runat="server">
                                <asp:HiddenField ID="CasoMedicoIdHF" runat="server" Value='<%# Bind("CasoId") %>' />
                                <asp:HiddenField ID="CitaIdHF" runat="server" Value='<%# Bind("CitaId") %>' />
                                <asp:HiddenField ID="CorrelativoHF" runat="server" Value='<%# Bind("Correlativo") %>' />
                                <asp:HiddenField ID="MotivoConsultaIdHF" runat="server" Value='<%# Bind("MotivoConsultaId") %>' />
                                <asp:HiddenField ID="EstadoHF" runat="server" Value='<%# Bind("Estado") %>' />
                                <asp:HiddenField ID="PacienteIdHF" runat="server" Value='<%# Bind("PacienteId") %>' />

                                <asp:HiddenField ID="HistoriaIdHF" runat="server" Value='<%# Bind("HistoriaId") %>' />

                                <asp:Label ID="Label33" Text="Código del Caso" runat="server" CssClass="label" />
                                <asp:Label ID="CodigoCasoLabel" Text='<%# Bind("CodigoCaso") %>' runat="server" />

                                <asp:Label ID="Label37" Text='<%# Convert.ToBoolean(Eval("IsReconsulta").ToString())? "Fecha de Reconsulta":"Fecha de creación" %>' CssClass="label" runat="server" ></asp:Label>
                                <asp:Label ID="FechaCLabel" Text='<%# Bind("FechaCreacionString") %>' runat="server" />

                                <br /><br />
                                <asp:CheckBox ID="CheckBox1" CssClass="CasoCriticoEnfermedadCronica" Text="Paciente con enfermedades cronicas" runat="server" Checked='<%# Eval("CasoCritico") %>' Enabled="false" />
                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var titleCronicas = '';
                                        if ($(".CasoCriticoEnfermedadCronica input").prop("checked")) {
                                            titleCronicas = ' <span style="color: #f00;">[Paciente con enfermedades Crónicas]</span>';
                                            $('#PacienteCronicoPanel').show();
                                        } else {
                                            $('#PacienteCronicoPanel').hide();
                                        }
                                        $('#<%= CasoMedicoTitle.ClientID %>').html('<%# Eval("MotivoConsultaIdforDisplay")%>' + titleCronicas);
                                    });
                                </script>
                                <div id="PacienteCronicoPanel" style="display: none;" >
                                    <asp:Repeater ID="EnfermedadesCronicasRepeater" runat="server" DataSourceID="EnfermedadesCronicasODS">
                                        <HeaderTemplate><span class="label">ENFERMEDADES CRÓNICAS DEL PACIENTE</span><ul></HeaderTemplate>
                                        <ItemTemplate><li style="font-weight:normal;"><asp:Literal ID="Literal1" Text='<%# Eval("Nombre") %>' runat="server" /></li></ItemTemplate>
                                        <FooterTemplate></ul></FooterTemplate>
                                    </asp:Repeater>
                                </div>
                                
                                <asp:Label ID="Label10" runat="server" CssClass="label" 
                                        Text='<%# Eval("MotivoConsultaIdforDisplay").ToString() == "EMERGENCIA"? "Observaciones para la emergencia" : "Motivo de la consulta" %>'></asp:Label>
                                <asp:Label ID="MotivoConsultaTxt" runat="server"
                                    Text='<%# Bind("MotivoConsulta") %>' />

                                <asp:Label ID="Label11" Text="Enfermedad Actual" runat="server" CssClass="label" />
                                <asp:Label ID="EnfermedadActualLabel" runat="server"
                                    Text='<%# Bind("EnfermedadActual") %>' />
                                <br />
                                <br />
                                <asp:Panel runat="server" GroupingText="Antecedentes Familiares"
                                    CssClass="ExpandCollapse">
                                    <div style="display: none;">
                                        <RedSalud:AngularControl runat="server" ID="Antecedentesfamiliares" readOnly="true" maxLength="620" JSonData='<%# Bind("Antecedentes") %>'
                                            JSonDefaultData='<%# defaultJSonAntecedentes %>' />
                                    </div>
                                </asp:Panel>
                                <br />
                                <asp:Panel ID="AntecedentesGinecoobstetricosPanel" runat="server" GroupingText="Antecedentes Ginecoobstetricos"
                                    CssClass="ExpandCollapse AntecedentesGinecoobstetricosPanel" Visible='<%# !IsOdontologia %>'>
                                    <div style="display: none;">
                                        <RedSalud:AngularControl runat="server" ID="AntecedentesGinecoobstetricos" readOnly="true" maxLength="1950"
                                            JSonData='<%# Bind("AntecedentesGinecoobstetricos") %>' />
                                    </div>
                                </asp:Panel>

                                <asp:Label ID="SignosVitalesLabel" Text="Signos vitales" runat="server" CssClass="label"
                                    Visible='<%# !IsOdontologia %>' />
                                <table id="SignosVitalesTable" runat="server" Visible='<%# !IsOdontologia %>'>
                                    <tr>
                                        <td>
                                            <asp:Label ID="PALabel" Text="Presión arterial:" runat="server" CssClass="label" />
                                        </td>
                                        <td>
                                            <asp:Label ID="PresionArterialTxt" runat="server"
                                                Text='<%# Bind("PresionArterial") %>' />
                                        </td>
                                        <td style="padding-left: 15px;">
                                            <asp:Label ID="TempLabel" Text="Temperatura (° C):" runat="server" CssClass="label" />
                                        </td>
                                        <td>
                                            <asp:Label ID="TemperaturaTxt" runat="server"
                                                Text='<%# Bind("Temperatura") %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="PLabel" Text="Pulso p/min:" runat="server" CssClass="label" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Pulsotxt" runat="server"
                                                Text='<%# Bind("Pulso") %>' />
                                        </td>
                                        <td style="padding-left: 15px;">
                                            <asp:Label ID="FCLabel" Text="Frecuencia cardiaca p/min:" runat="server" CssClass="label" />
                                        </td>
                                        <td>
                                            <asp:Label ID="FrecuenciaCTxt" runat="server"
                                                Text='<%# Bind("FrecuenciaCardiaca") %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" Text="Peso:" runat="server" CssClass="label" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label38" runat="server"
                                                Text='<%# Bind("PesoDisplay") %>' />
                                        </td>
                                        <td style="padding-left: 15px;">
                                            <asp:Label ID="Label39" Text="Estatura:" runat="server" CssClass="label" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label40" runat="server"
                                                Text='<%# Bind("Talla") %>' />
                                        </td>
                                    </tr>
                                </table>

                                
                                <asp:Panel ID="Panel3" runat="server" Visible='<%# Eval("MotivoConsultaIdforDisplay").ToString() == "ENFERMERIA" %>'>
                                    <asp:Label ID="Label2" Text="Motivo de la Consulta o Sintomatologia" runat="server" CssClass="label" />
                                    <asp:Label ID="Label3" runat="server"
                                        Text='<%# Bind("MotivoConsulta") %>' />
                                    <asp:Label ID="Label4" Text="Observaciones" runat="server" CssClass="label" />
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ExFisicoRegionalyDeSistema") %>'  />
                                    <asp:Label ID="Label6" Text="Tratamiento" runat="server" CssClass="label" />
                                    <asp:Label ID="Label7" runat="server"
                                        Text='<%# Bind("BiometriaHematica") %>' />
                                </asp:Panel>
                                <asp:Panel ID="Panel2" runat="server" 
                                        Visible='<%# Eval("MotivoConsultaIdforDisplay").ToString() == "CASO MÉDICO" || Eval("MotivoConsultaIdforDisplay").ToString() == "RECONSULTA"%>'>
                                    <asp:Label ID="EFRySLabel" Text="Examen físico regional" runat="server" CssClass="label"
                                        Visible='<%# !IsOdontologia %>' />
                                    <RedSalud:AngularControl runat="server" ID="ExFisico" readOnly="true" maxLength="440" 
                                        Visible='<%# !IsOdontologia %>'
                                        JSonData='<%# Bind("ExFisicoRegionalyDeSistema") %>' />

                                    <asp:Label ID="BHLabel" Text="Resultados de laboratorio y otros exámenes" runat="server" CssClass="label" />
                                    <asp:Label ID="BiometriaHematicaTxt" runat="server"
                                        Text='<%# Bind("BiometriaHematica") %>' />

                                    <asp:Label ID="Label12" Text="Observaciones" runat="server" CssClass="label" />
                                    <asp:Label ID="ObservacionesLabel" runat="server" CssClass="ObservacionesLabel"
                                        Text='<%# Bind("Observaciones") %>' />

                                    <asp:Label ID="DPLabel" Text="Diagnostico Presuntivo" runat="server" CssClass="label" />
                                    <asp:Label ID="DiagnosticoPresuntivoTxt" runat="server" style="display: block"
                                        Text='<%# Bind("DiagnosticoPresuntivoForDisplay") %>' />
                                    <asp:Label ID="DiagnosticoPresuntivo2Txt" runat="server" style="display: block"
                                        Text='<%# Bind("Enfermedad2") %>' />
                                    <asp:Label ID="DiagnosticoPresuntivo3Txt" runat="server" style="display: block"
                                        Text='<%# Bind("Enfermedad3") %>' />
                                </asp:Panel>
                            </asp:Panel>
                                <asp:Panel ID="EmergenciaDiagnosticoPanel" runat="server" 
                                        Visible='<%# Eval("MotivoConsultaIdforDisplay").ToString() == "EMERGENCIA" || Eval("MotivoConsultaIdforDisplay").ToString() == "ENFERMERIA"%>'>
                                    <asp:Label ID="Label8" Text="Diagnostico Presuntivo" runat="server" CssClass="label" />
                                    <asp:Label ID="Label9" runat="server" style="display: block"
                                        Text='<%# Bind("DiagnosticoPresuntivoForDisplay") %>' />
                                </asp:Panel>
                            
                            <asp:Panel runat="server" 
                                Visible='<%# Eval("MotivoConsultaIdforDisplay").ToString() == "CASO MÉDICO" || Eval("MotivoConsultaIdforDisplay").ToString() == "RECONSULTA"
                                                || Eval("MotivoConsultaIdforDisplay").ToString() == "ODONTOLOGÍA"%>'>

                                <span class="label">Lista de recetas</span>
                                <telerik:RadGrid ID="RecetaGrid" runat="server"
                                    CssClass="RecetaGrid"
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
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>

                                <span class="label" runat="server" visible='<%# !Eval("MotivoConsultaIdforDisplay").ToString().StartsWith("ODO")%>'>Lista Exámenes Complementarios / Derivación a especialista / Internación / Emergencia</span>
                                    <span class="label" runat="server" visible='<%# Eval("MotivoConsultaIdforDisplay").ToString().StartsWith("ODO") %>'>Lista PRESTACIONES ODONTOLÓGICAS / Exámenes Complementarios / Derivación a especialista / Internación / Emergencia</span>
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
                                                <telerik:GridBoundColumn DataField="NombreAnalisisRealizado" HeaderText="Analisis Realizado" />
                                                <telerik:GridBoundColumn DataField="TipoEstudio" HeaderText="Tipo de Estudio" />
                                                <telerik:GridBoundColumn DataField="NombreProveedor" HeaderText="Nombre del Proveedor" />
                                                <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" />
                                                <telerik:GridBoundColumn DataField="FechaCreacion" HeaderText="Fecha Creación" />
                                                <telerik:GridBoundColumn DataField="Table" HeaderText="Table" Visible="false" />
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>

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
                            </asp:Panel>
                            <asp:Panel runat="server" Visible='<%# Eval("MotivoConsultaIdforDisplay").ToString() == "ENFERMERIA" %>'>
                                <span class="label">Lista de recetas</span>
                                <telerik:RadGrid ID="MedicamentoGrid" runat="server"
                                    AutoGenerateColumns="false"
                                    DataSourceID="MedicamentoODS">
                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="MedicamentoId">
                                        <NoRecordsTemplate>
                                            <asp:Label ID="MsgMedicamentoNullLabel" runat="server" Text="No existen Medicamentos para este caso del paciente."></asp:Label>
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
                        </ItemTemplate>
                    </asp:FormView>

                    <asp:ObjectDataSource ID="CasoODS" runat="server"
                        TypeName="Artexacta.App.Caso.BLL.CasoBLL"
                        DataObjectTypeName="Artexacta.App.Caso.Caso"
                        OldValuesParameterFormatString="Original_{0}"
                        SelectMethod="GetCasoByCasoId"
                        OnSelected="CasoODS_Selected">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="CasoIdHF" PropertyName="value" Name="CasoId" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                </div>
                <div class="right" style="width: 29%;">
                    <asp:Panel ID="Panel1" runat="server" GroupingText="Información póliza">
                        <asp:Label ID="AseguradoLabel" Text="" runat="server" />
                        <br />
                        <span class="label">Nro. Póliza - Nombre del Plan</span>
                        <asp:Label ID="PolizaLabel" Text="" runat="server" />
                        <br />
                        <span class="label">Valido hasta:</span>
                        <asp:Label ID="FechaFinLabel" Text="" runat="server" />
                        <br />
                        <span class="label">Siniestralidad:</span>
                        <div id="DivSiniestralidad" runat="server">
                            <asp:Panel runat="server" ID="SinistralidadMonto">
                                <asp:Label ID="SiniestralidadLabel" Text="" runat="server" />
                            </asp:Panel>
                            <asp:Panel runat="server" ID="SiniestralidadPlan" Visible="false" CssClass="table" style="width: 100%;">
                                        <div>
                                            <div class="header">Estudio</div>
                                            <div class="header" style="width: 65px;">Cantidad Permitida</div>
                                            <div class="header" style="width: 65px;">Cantidad de Uso</div>
                                        </div>
                                <asp:Repeater runat="server" ID="PlanUsoRepeater">
                                    <ItemTemplate>
                                        <div style="display:table-row;">
                                            <div><%#Eval("Nombre") %></div>
                                            <div style="text-align: center;"><%#Eval("Cantidad") %></div>
                                            <div style="text-align: center;"><%#Eval("CantidadUso") %></div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="Panel2" runat="server" CssClass="critic" GroupingText="Información asegurado">
                        <asp:FormView ID="PacienteFV" runat="server"
                            DataSourceID="PacienteODS">
                            <ItemTemplate>
                                <asp:HiddenField ID="GeneroAsegurado" Value='<%# Bind("Genero")%>' runat="server" />
                                <div class="twoColsLeft">
                                    <div style="margin: 0 auto; width:120px;">
                                        <RedSalud:FotoPaciente runat="server" ID="FotoPaciente" Editable="false" Width="120px"
                                            FotoId='<%# Eval("FotoId")%>' PacienteId='<%# Eval("PacienteId")%>' />
                                    </div>

                                    <asp:Label ID="CITitle" Text="Carnet de Indentidad" runat="server" CssClass="label" />
                                    <asp:Label ID="CILabel" Text='<%# Bind("CarnetIdentidad")%>' runat="server" />

                                    <asp:Label ID="NombreTitle" Text="Nombre Completo" runat="server" CssClass="label" />
                                    <asp:Label ID="NombreLabel" Text='<%# Bind("Nombre")%>' runat="server" />

                                    <asp:Label ID="Label3" Text="EDAD" runat="server" CssClass="label" />
                                    <asp:Label ID="Label11" Text='<%# Eval("Edad")%>' runat="server" />
                                </div>
                                <div class="twoColsRight">
                                    <asp:Label ID="FechaNacTitle" Text="Fecha de nacimiento" runat="server" CssClass="label" />
                                    <asp:Label ID="FechaNacLabel" Text='<%# Bind("FechaNacimientoString")%>' runat="server" />

                                    <asp:Label ID="GeneroTitle" Text="Género" runat="server" CssClass="label" />
                                    <asp:Label ID="GeneroLabel" Text='<%# Bind("GeneroForDisplay")%>' runat="server" />

                                    <asp:Label ID="EstadoCivilTitle" Text="Estado civil" runat="server" CssClass="label" />
                                    <asp:Label ID="EstadoCivilLabel" Text='<%# Bind("EstadoCivil")%>' runat="server" />

                                    <asp:Label ID="DirTitle" Text="Dirección" runat="server" CssClass="label" />
                                    <asp:Label ID="DirLabel" Text='<%# Bind("Direccion")%>' runat="server" />

                                    <asp:Label ID="tlfTitle" Text="Teléfono" runat="server" CssClass="label" />
                                    <asp:Label ID="TlfLabel" Text='<%# Bind("Telefono")%>' runat="server" />

                                    <asp:Label ID="LugarTTitle" Text="Lugar de trabajo" runat="server" CssClass="label" />
                                    <asp:Label ID="LugarTLabel" Text='<%# Bind("LugarTrabajo")%>' runat="server" />

                                    <asp:Label ID="TlfOffTitle" Text="Teléfono de Oficina" runat="server" CssClass="label" />
                                    <asp:Label ID="tlfOffLabel" Text='<%# Bind("TelefonoTrabajo")%>' runat="server" />

                                    <asp:Label ID="NroHijosTitle" Text="Nro. de hijos" runat="server" CssClass="label" />
                                    <asp:Label ID="NroHijosLabel" Text='<%# Bind("NroHijos")%>' runat="server" />
                                </div>
                            </ItemTemplate>
                        </asp:FormView>
                        <asp:ObjectDataSource ID="PacienteODS" runat="server"
                            TypeName="Artexacta.App.Paciente.BLL.PacienteBLL"
                            DataObjectTypeName="Artexacta.APP.Paciente.Paciente"
                            OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetPacienteByPacienteId"
                            OnSelected="PacienteODS_Selected">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="PacienteIdHF" Name="PacienteId" PropertyName="value" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>

                        <asp:HiddenField ID="PacienteIdHF" runat="server" />
                    </asp:Panel>
                </div>
                <asp:ObjectDataSource ID="EnfermedadesCronicasODS" runat="server"
                    TypeName="Artexacta.App.EnfermedadCronica.BLL.EnfermedadCronicaBLL"
                    OldValuesParameterFormatString="{0}"
                    SelectMethod="GetEnfermedadCronicaByAseguradoId"
                    OnSelected="EnfermedadesCronicasODS_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="AseguradoIdHF" Name="AseguradoId" PropertyName="Value" DbType="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <div class="clear"></div>

                <asp:HiddenField ID="AseguradoIdHF" runat="server" />
                <asp:HiddenField ID="CasoIdHF" runat="server" />
                <asp:HiddenField ID="MotivoConsultaIdHF" runat="server" />
                <asp:HiddenField ID="CiudadHF" runat="server" />
                <asp:HiddenField ID="RedMedicaIdHF" runat="server" />
                <asp:HiddenField ID="ClienteIdHF" runat="server" />
                <asp:HiddenField ID="CitaIdHF" runat="server" />

                <asp:HiddenField ID="ExportIDHF" runat="server" />
                <asp:HiddenField ID="RadGridExported" runat="server" />
                <asp:HiddenField ID="MedicoNameHF" runat="server" />
                <asp:HiddenField ID="EspecialidadHF" runat="server" />

                <asp:HiddenField ID="FechaEstadoRecetaHF" runat="server" />
                <asp:HiddenField ID="FechaEstadoExamenesHF" runat="server" />
                <asp:HiddenField ID="FechaEstadoEspecialistaHF" runat="server" />

                <asp:HiddenField ID="ReconsultaHF" runat="server" />
                <asp:HiddenField ID="ModeHF" runat="server" />
                
                <script type="text/javascript">
                    
                    $(document).ready(function () {
                        var divExpandCollapse = '<div class="ExpandCollapseIcon"></div>';
                        $(".ExpandCollapse fieldset legend").append(divExpandCollapse);

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
                    });
                </script>
            </div>
        </div>
    </div>
</asp:Content>