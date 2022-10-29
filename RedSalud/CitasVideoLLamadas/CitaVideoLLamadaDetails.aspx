<%@ Page Title="Cliente" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CitaVideoLLamadaDetails.aspx.cs" Inherits="CitaVideoLLamadaDetails" %>

<%@ Register Src="~/UserControls/FotoPaciente.ascx" TagPrefix="RedSalud" TagName="FotoPaciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="left" style="max-width: 100%; width: 100%; padding: 10px 10px 10px 0px;">
        <telerik:RadTabStrip ID="ClienteTab" runat="server" MultiPageID="ClienteMP" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="Cita" PageViewID="CitaRPV"></telerik:RadTab>
                <telerik:RadTab Text="Receta" ID="RecetaRT" runat="server"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>

        <telerik:RadMultiPage ID="ClienteMP" runat="server"
            CssClass="RadMultiPage"
            SelectedIndex="0">
            <telerik:RadPageView ID="CitaRPV" runat="server">
                <div>
                    <div class="frame">
                        <div class="columnHead">
                            <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                                Text="Nuevo Cliente">
                            </asp:Label>
                        </div>
                        <div class="columnContent">
                            <table>
                                <tr>
                                    <td style="vertical-align:top;">
                                        <div class="contentMenu">
                                            <asp:HyperLink ID="HyperLink1" runat="server" Text="Volver a la Lista de citas de video llamadas"
                                                NavigateUrl="~/CitasVideoLLamadas/CitasVideoLLamadasList.aspx">
                                            </asp:HyperLink>
                                        </div>
                                        <asp:HiddenField ID="CodigoRedMedicaHF" runat="server" />
                                        <asp:FormView ID="CitaFV" runat="server"
                                            DataSourceID="CitaODS">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="CitIdFVHF" runat="server" Value='<%# Bind("citId") %>' />
                                                <div>
                                                    <span class="label">Código de la cita</span>
                                                    <asp:Label ID="citIdLbl" runat="server"
                                                        Text='<%# Bind("citId") %>'>
                                                    </asp:Label>

                                                    <span class="label">Fecha de creación</span>
                                                    <asp:Label ID="citfechaCreacionLbl" runat="server"
                                                        Text='<%# Bind("FechaCreacion") %>'>
                                                    </asp:Label>

                                                    <span class="label">Médico</span>
                                                    <asp:Label ID="medicoLbl" runat="server"
                                                        Text='<%# Bind("Medico") %>'>
                                                    </asp:Label>

                                                    <span class="label">Especialidad</span>
                                                    <asp:Label ID="EspecialidadLbl" runat="server"
                                                        Text='<%# Bind("Especialidad") %>'>
                                                    </asp:Label>

                                                    <span class="label">Motivo de la cita</span>
                                                    <asp:Label ID="motivoLbl" runat="server"
                                                        Text='<%# Bind("Motivo") %>'>
                                                    </asp:Label>

                                                    <span class="label">Fecha de la cita</span>
                                                    <asp:Label ID="FechaCitaLbl" runat="server"
                                                        Text='<%# Bind("FechaCita") %>'>
                                                    </asp:Label>

                                                    <span class="label">Estado</span>
                                                    <asp:Label ID="EstoadLbl" runat="server"
                                                        Text='<%# Bind("Estado") %>'>
                                                    </asp:Label>

                                                    <span class="label">Diagnóstico presuntivo</span>
                                                    <asp:Label ID="Label9" runat="server"
                                                        Text='<%# Bind("Enfermedad1") %>'>
                                                    </asp:Label>
                                                    <br />
                                                    <asp:Label ID="Label10" runat="server"
                                                        Text='<%# Bind("Enfermedad2") %>'>
                                                    </asp:Label>
                                                    <br />
                                                    <asp:Label ID="Label11" runat="server"
                                                        Text='<%# Bind("Enfermedad3") %>'>
                                                    </asp:Label>

                                                    <span class="label">Observaciones</span>
                                                    <asp:Label ID="Label12" runat="server"
                                                        Text='<%# Bind("citObservaciones") %>'>
                                                    </asp:Label>

                                                    <span class="label">Recomendaciones</span>
                                                    <asp:Label ID="RecomendacionesLbl" runat="server"
                                                        Text='<%# Bind("Recomendaciones") %>'>
                                                    </asp:Label>

                                                    <span class="label">Calificación</span>
                                                    <asp:Label ID="calificacionLbl" runat="server"
                                                        Text='<%# Bind("Calificacion") %>'>
                                                    </asp:Label>

                                                    <span class="label">Comentario</span>
                                                    <asp:Label ID="ComentarioLbl" runat="server"
                                                        Text='<%# Bind("Comentario") %>'>
                                                    </asp:Label>

                                                </div>

                                                <div class="buttonsPanel">
                                                    <asp:HyperLink ID="ReturnLB" runat="server"
                                                        NavigateUrl="~/CitasVideoLLamadas/CitasVideoLLamadasList.aspx"
                                                        Text="Volver a la Lista de citas de video llamadas" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:FormView>
                                        <asp:ObjectDataSource ID="CitaODS" runat="server"
                                            TypeName="Artexacta.App.CitasVideoLLamada.BLL.CitaVideoLLamadaBLL"
                                            SelectMethod="GetCitaByCitaId"
                                            OnSelected="CitaODS_Selected">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="citIdHF" Name="CitaId" PropertyName="Value" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                    <td style="width:50px;"></td>
                                    <td style="vertical-align: top;">
                                        <div>
                                            <asp:Panel runat="server" GroupingText="Información Poliza">
                                                <asp:FormView ID="PolizaFV" runat="server"
                                                    DataSourceID="PolizaODS">
                                                    <ItemTemplate>

                                                        <div>
                                                            <span class="label">Numero de póliza</span>
                                                            <asp:Label ID="NumeroPolizaLbl" runat="server"
                                                                Text='<%# Bind("NumeroPoliza") %>'>
                                                            </asp:Label>

                                                            <span class="label">Nombre del plan</span>
                                                            <asp:Label ID="NombrePlanLbl" runat="server"
                                                                Text='<%# Bind("NombrePlan") %>'>
                                                            </asp:Label>

                                                            <span class="label">Válido hasta</span>
                                                            <asp:Label ID="FechaFinLbl" runat="server"
                                                                Text='<%# Convert.ToDateTime(Eval("FechaFin")).ToShortDateString() %>'>
                                                            </asp:Label>

                                                        </div>


                                                    </ItemTemplate>
                                                </asp:FormView>
                                                <asp:ObjectDataSource ID="PolizaODS" runat="server"
                                                    TypeName="Artexacta.App.CitasVideoLLamada.BLL.CitaVideoLLamadaBLL"
                                                    SelectMethod="GetPolizaById"
                                                    OnSelected="PolizaODS_Selected">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="polizaIdHF" Name="PolizaId" PropertyName="Value" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </asp:Panel>

                                        </div>
                                        <div>
                                            <asp:Panel runat="server" GroupingText="Información Asegurado">
                                                <asp:FormView ID="AseguradoFV" runat="server"
                                                    DataSourceID="AseguradoODS">
                                                    <ItemTemplate>

                                                        <div>
                                                            <table>
                                                                <tr>
                                                                    <td rowspan="1">
                                                                        <RedSalud:FotoPaciente runat="server" ID="FotoPaciente" Editable="false" PacienteId='<%# Bind("PacienteId") %>' FotoId='<%# Bind("FotoId") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <span class="label">Fecha de nacimiento</span>
                                                                        <asp:Label ID="FechaNacimientoLbl" runat="server"
                                                                            Text='<%#  Convert.ToDateTime(Eval("FechaNacimiento")).ToShortDateString() %>'>
                                                                        </asp:Label>
                                                                        <span class="label">Género</span>
                                                                        <asp:Label ID="GeneroLbl" runat="server"
                                                                            Text='<%# Bind("Genero") %>'>
                                                                        </asp:Label>
                                                                        <span class="label">Estado Civil</span>
                                                                        <asp:Label ID="EstadoCivilLbl" runat="server"
                                                                            Text='<%# Bind("EstadoCivil") %>'>
                                                                        </asp:Label>
                                                                        <span class="label">Dirección</span>
                                                                        <asp:Label ID="Label1" runat="server"
                                                                            Text='<%# Bind("Direccion") %>'>
                                                                        </asp:Label>
                                                                        <span class="label">Teléfono</span>
                                                                        <asp:Label ID="Label2" runat="server"
                                                                            Text='<%# Bind("Telefono") %>'>
                                                                        </asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span class="label">Carnet de identidad</span>
                                                                        <asp:Label ID="Label6" runat="server"
                                                                            Text='<%# Bind("CarnetIdentidad") %>'>
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <span class="label">Lugar de trabajo</span>
                                                                        <asp:Label ID="Label3" runat="server"
                                                                            Text='<%# Bind("LugarTrabajo") %>'>
                                                                        </asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span class="label">Nombre completo</span>
                                                                        <asp:Label ID="Label7" runat="server"
                                                                            Text='<%# Bind("Nombre") %>'>
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <span class="label">Teléfono de trabajo</span>
                                                                        <asp:Label ID="Label4" runat="server"
                                                                            Text='<%# Bind("TelefonoTrabajo") %>'>
                                                                        </asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span class="label">Edad</span>
                                                                        <asp:Label ID="Label8" runat="server"
                                                                            Text='<%# Bind("Edad") %>'>
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <span class="label">Nro de hijos</span>
                                                                        <asp:Label ID="Label5" runat="server"
                                                                            Text='<%# Bind("NroHijos") %>'>
                                                                        </asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:FormView>
                                                <asp:ObjectDataSource ID="AseguradoODS" runat="server"
                                                    TypeName="Artexacta.App.CitasVideoLLamada.BLL.CitaVideoLLamadaBLL"
                                                    SelectMethod="GetDatosPacienteByAseguradoId">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="aseguradoIdHF" Name="AseguradoId" PropertyName="Value" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:HiddenField ID="polizaIdHF" runat="server" />
                        <asp:HiddenField ID="aseguradoIdHF" runat="server" />
                        <asp:HiddenField ID="citIdHF" runat="server" />
                        <%--<asp:HiddenField ID="polizaIdHF" runat="server" />--%>
                        <asp:HiddenField ID="HiddenButton" runat="server" />
                        <div id="dialog-confirm" title="Eliminar" style="display: none">
                            <p>
                                <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
                                Esta seguro que desea eliminar la Red Medica seleccionada?
                            </p>
                        </div>

                        <script type="text/javascript">
                            $(function () {
                                $(".deleteRow").click(function () {
                                    $("#<%= HiddenButton.ClientID %>").val($(this).attr("href"));
                                    $("#dialog-confirm").dialog("open");
                                    return false;
                                });

                                $("#dialog-confirm").dialog({
                                    resizable: false,
                                    height: 140,
                                    modal: true,
                                    autoOpen: false,
                                    buttons: {
                                        "Eliminar": function () {
                                            eval($("#<%= HiddenButton.ClientID %>").val());
                                            $(this).dialog("close");
                                        },
                                        "Cancelar": function () {
                                            $(this).dialog("close");
                                        }
                                    }
                                });
                            });
                        </script>

                    </div>
                </div>
            </telerik:RadPageView>

            <telerik:RadPageView ID="RecetaRPV" runat="server">
                <div>
                    <asp:Label
                        ID="RecetaAlert"
                        Visible="false"
                        runat="server"
                        ForeColor="Red"
                        Text="No se puede ingresar el monto y el porcentaje para el CoPago.">
                    </asp:Label>
                    <telerik:RadGrid ID="RecetaRadGrid" runat="server"
                        AutoGenerateColumns="false"
                        DataSourceID="RecetaODS"
                        AllowPaging="true"
                        PageSize="20"
                        Visible="true">
                        <MasterTableView DataKeyNames="detId">
                            <NoRecordsTemplate>
                                <asp:Label runat="server" Text="No existen prestaciones configuradas para el cliente seleccionado"></asp:Label>
                            </NoRecordsTemplate>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Medicamento" UniqueName="Medicamento" HeaderText="Medicamento" ReadOnly="True" />
                                <telerik:GridNumericColumn DataField="Grupo" UniqueName="Grupo" HeaderText="Grupo" />
                                <telerik:GridNumericColumn DataField="SubGrupo" UniqueName="SubGrupo" HeaderText="Sub Grupo" />
                                <telerik:GridNumericColumn DataField="Presentacion" UniqueName="Presentacion" HeaderText="Presentacion" />
                                <telerik:GridNumericColumn DataField="Concentracion" UniqueName="Concentracion" HeaderText="Concentracion" />
                                <telerik:GridNumericColumn DataField="Cantidad" UniqueName="Cantidad" HeaderText="Cantidad" />
                                <telerik:GridNumericColumn DataField="InstruccionesUso" UniqueName="InstruccionesUso" HeaderText="Instrucciones de uso" />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="RecetaODS" runat="server"
                        TypeName="Artexacta.App.CitasVideoLLamada.BLL.CitaVideoLLamadaBLL"
                        OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetCitaRecetaByCitaId">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="citIdHF" Name="CitaId" PropertyName="Value" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
</asp:Content>

