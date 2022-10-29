<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ExamenMedicoImprimir.aspx.cs" Inherits="Desgravamen_ExamenMedicoImprimir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @media print {
            .noPrint {
                display: none;
                visibility: hidden;
            }

            html * {
                background-color: #ffffff;
                border-width: 0px;
                text-transform: uppercase;
            }

            .observacionesPregunta {
                -webkit-print-color-adjust: exact;
            }

            .cellTitle {
                -webkit-print-color-adjust: exact;
            }
        }

        html {
            text-transform: uppercase;
        }

        body {
            font-size: 14px !important;
        }

        .pregunta {
            float: left;
            margin-right: 15px;
            width: 45%;
        }

        .respuestaPregunta {
            float: left;
            width: 20px;
            margin-right: 15px;
        }

        .respuestaAfirmativa {
            font-weight: bold;
            float: left;
            width: 20px;
            margin-right: 15px;
        }

        .observacionesPregunta {
            float: left;
            width: 40%;
            border: 1px solid #000;
            padding: 3px;
            margin-bottom: 5px;
            background-color: #e4e4e4 !important;
        }

        .cellTitle {
            padding: 5px;
            background-color: #e4e4e4 !important;
        }

        .cellFirstCol {
            width: 100px;
        }

        .centerText {
            text-align: center;
        }

        .tablaDatos td {
            padding: 2px;
            margin: 0px;
            border: 1px solid #000;
        }

        .label {
            margin-bottom: 5px;
            !important;
            margin-top: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead noPrint">
                <span class="title">Revisión Médica</span>
            </div>
            <div class="columnContent noPrint">
                <div class="contentMenu">
                    <asp:LinkButton ID="cmdVolverACita" runat="server" OnClick="cmdVolverACita_Click" Text="Volver"></asp:LinkButton>

                    <asp:Image ID="printIcon" runat="server" ImageUrl="~/Images/Neutral/ExportPrint.gif" />
                </div>


                <br />
                <br />

            </div>
        </div>
    </div>

    <div style="clear: both;"></div>

    <!-- This is where the printing begins -->
    <table>
        <thead>
            <tr>
                <th>
                    <p style="text-align: left;">
                        <img src="../Images/LogoPrint.jpg" alt="Red Salud" style="text-align: left; height: 95px;" />
                    </p>
                </th>
                <th>
                    <p style="text-align: right; font-size: 16px;">
                        <asp:Literal ID="tituloRevision" runat="server"></asp:Literal>
                    </p>
                    <p style="text-align: right; font-size: 14px;">
                        <asp:Literal ID="clienteRevision" runat="server"></asp:Literal>
                    </p>
                </th>
            </tr>
        </thead>

        <tfoot>
            <tr>
                <td colspan="2">
                    <div style="text-align: justify; font-size: 9px; padding: 5px;">
                        <asp:Literal ID="formConsent" Visible="false" runat="server"></asp:Literal>
                    </div>
                    <div class="twoColsLeft">
                        <div class="frame">
                            <div class="columnContent centerText" style="font-size: 11px;">
                                <br />
                                -------------------------------------------------------
                <br />
                                <div>
                                    <asp:Literal ID="PANombreFirma" runat="server"></asp:Literal>
                                </div>
                                Firma Propuesto Asegurado
                            </div>
                        </div>
                    </div>

                    <div class="twoColsRight">
                        <div class="frame">
                            <div class="columnContent centerText" style="font-size: 11px;">
                                <br />
                                -------------------------------------------------------
                <br />
                                <div>
                                    <asp:Literal ID="MENombreFirma" runat="server"></asp:Literal>
                                </div>
                                Firma y Sello Médico Examinador
                            </div>
                        </div>
                    </div>


                </td>
            </tr>
        </tfoot>


        <tbody>
            <tr>
                <td colspan="2">

                    <div class="twoColsLeft">
                        <div class="frame">
                            <div class="columnContent">
                                Médico Examinador: <b>
                                    <asp:Literal ID="MENombre" runat="server"></asp:Literal></b>
                                <br />
                                Fecha: <b>
                                    <asp:Literal ID="FechaCita" runat="server"></asp:Literal></b>
                                <br />
                                Ciudad: <b>
                                    <asp:Literal ID="NombreCiudad" runat="server"></asp:Literal></b>
                                <br />
                                <br />
                                Propuesto Asegurado: <b>
                                    <asp:Literal ID="PANombre" runat="server"></asp:Literal></b>
                                <br />
                                Carnet de Identidad Nro: <b>
                                    <asp:Literal ID="PACI" runat="server"></asp:Literal></b>
                                <br />
                                Fecha de Nacimiento: <b>
                                    <asp:Literal ID="PAFechaNacimiento" runat="server"></asp:Literal></b>
                                <br />
                                Ocupación y/o profesión: <b>
                                    <asp:Literal ID="PAProfesion" runat="server"></asp:Literal></b>
                                <br />
                                Estado Civil: <b>
                                    <asp:Literal ID="chkEstadoCivil" runat="server"></asp:Literal></b>

                            </div>
                        </div>
                    </div>
                    <div class="twoColsRight">
                        <div class="frame">
                            <div class="columnContent">
                                <asp:Image ID="FotoPAUrl" runat="server" ImageUrl="~/Images/Neutral/paciente.jpg" ImageAlign="Right" Width="200" />
                            </div>
                        </div>
                    </div>

                    <div class="oneColumn">
                        <div class="frame">
                            <div class="columnContent">
                                <asp:Label ID="p1" runat="server" Text="1. Datos de consultas recientes" CssClass="label"></asp:Label>
                                <br />
                                <br />
                                a. Nombre y dirección de su médico particular:
                <asp:Label ID="PAMedicoParticular" runat="server" CssClass="bigField"></asp:Label>

                                <br />
                                b. Fecha y motivo de la consulta más reciente:
                <asp:Label ID="PAConsultaReciente" runat="server" CssClass="bigField" Rows="3" TextMode="MultiLine"></asp:Label>

                                <br />
                                c. Qué tratamiento o medicación se transcribió:
                <asp:Label ID="PATratamiento" runat="server" CssClass="bigField" Rows="3" TextMode="MultiLine"></asp:Label>

                                <br />
                                <br />
                                <asp:Label ID="p2" runat="server" Text="2. Ha sido tratado o tiene conocimiento de haber padecido de" CssClass="label"></asp:Label>
                                <br />
                                En caso de respuesta afirmativa a alguna pregunta indique el número de éste e incluya diagnóstco, fecha, duración, 
                grado de recuperación y nombre / dirección de médicos a quienes se pueda pedir información.
                <br />
                                <asp:Repeater ID="rptSeccion2" runat="server"
                                    DataSourceID="dsPreguntaSeccion2"
                                    OnItemDataBound="rptSeccion2_ItemDataBound">
                                    <ItemTemplate>
                                        <br />
                                        <div class="pregunta">
                                            <asp:Label ID="lblPreguntaSeccion2" runat="server" Text='<%# Eval("Inciso") + ". " + Eval("Pregunta") %>'></asp:Label>
                                            <asp:HiddenField ID="PAPreguntaSeccion2Id" runat="server" Value='<%# Eval("Inciso") %>' />
                                        </div>
                                        <asp:Label ID="PAPreguntaSeccion2" runat="server" CssClass="respuestaPregunta"></asp:Label>
                                        <asp:Panel ID="pnlObservaciones" runat="server"
                                            CssClass="observacionesPregunta" Visible="false">
                                            <asp:Literal ID="PAPreguntaAfirmativaSeccion" runat="server"
                                                Text='<%# Eval("Observacion") %>'></asp:Literal>
                                        </asp:Panel>
                                        <div style="clear: both;"></div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:ObjectDataSource ID="dsPreguntaSeccion2" runat="server"
                                    TypeName="Artexacta.App.Desgravamen.BLL.ConsultaPreguntaBLL"
                                    SelectMethod="GetConsultaPreguntaBySeccionCita"
                                    OnSelected="dsPreguntaSeccion2_Selected">
                                    <SelectParameters>
                                        <asp:Parameter Name="secciones" Type="String" DefaultValue="2" />
                                        <asp:ControlParameter Name="citaDesgravamenId" PropertyName="Value" ControlID="CitaDesgravamenIdHiddenField" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:Repeater ID="rptSeccion3456" runat="server"
                                    DataSourceID="dsPreguntaSeccion3456"
                                    OnItemDataBound="rptSeccion3456_ItemDataBound">
                                    <ItemTemplate>
                                        <br />
                                        <div class="pregunta">
                                            <asp:Label ID="lblPreguntaSeccion" runat="server"
                                                Text='<%# Eval("Seccion") + ". " + Eval("Pregunta") %>' CssClass="label"></asp:Label>
                                            <asp:HiddenField ID="PAPreguntaSeccion3456Id" runat="server" Value='<%# Eval("Seccion") %>' />

                                        </div>
                                        <asp:Label ID="PAPreguntaSeccion" runat="server" CssClass="respuestaPregunta"></asp:Label>
                                        <asp:Panel ID="pnlObservaciones" runat="server"
                                            CssClass="observacionesPregunta" Visible="false">
                                            <asp:Label ID="PAPreguntaAfirmativaSeccion" runat="server"
                                                Text='<%# Eval("Observacion") %>'></asp:Label>
                                        </asp:Panel>
                                        <div style="clear: both;"></div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:ObjectDataSource ID="dsPreguntaSeccion3456" runat="server"
                                    TypeName="Artexacta.App.Desgravamen.BLL.ConsultaPreguntaBLL"
                                    SelectMethod="GetConsultaPreguntaBySeccionCita"
                                    OnSelected="dsPreguntaSeccion2_Selected">
                                    <SelectParameters>
                                        <asp:Parameter Name="secciones" Type="String" DefaultValue="3,4,5,6" />
                                        <asp:ControlParameter Name="citaDesgravamenId" PropertyName="Value" ControlID="CitaDesgravamenIdHiddenField" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <br />
                                <asp:Label ID="p7" runat="server" Text="7. En los últimos 5 años" CssClass="label"></asp:Label>

                                <asp:Repeater ID="rptSeccion7" runat="server"
                                    DataSourceID="dsPreguntaSeccion7"
                                    OnItemDataBound="rptSeccion7_ItemDataBound">
                                    <ItemTemplate>
                                        <br />
                                        <div class="pregunta">
                                            <asp:Label ID="lblPreguntaSeccion7" runat="server" Text='<%# Eval("Inciso") + ". " + Eval("Pregunta") %>'></asp:Label>
                                            <asp:HiddenField ID="PAPreguntaSeccion7Id" runat="server" Value='<%# Eval("Inciso") %>' />

                                        </div>
                                        <asp:Label ID="PAPreguntaSeccion7" CssClass="respuestaPregunta" runat="server"></asp:Label>
                                        <asp:Panel ID="pnlObservaciones" runat="server"
                                            CssClass="observacionesPregunta" Visible="false">
                                            <asp:Label ID="PAPreguntaAfirmativaSeccion" runat="server"
                                                Text='<%# Eval("Observacion") %>'></asp:Label>
                                        </asp:Panel>
                                        <div style="clear: both;"></div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:ObjectDataSource ID="dsPreguntaSeccion7" runat="server"
                                    TypeName="Artexacta.App.Desgravamen.BLL.ConsultaPreguntaBLL"
                                    SelectMethod="GetConsultaPreguntaBySeccionCita"
                                    OnSelected="dsPreguntaSeccion7_Selected">
                                    <SelectParameters>
                                        <asp:Parameter Name="secciones" Type="String" DefaultValue="7" />
                                        <asp:ControlParameter Name="citaDesgravamenId" PropertyName="Value" ControlID="CitaDesgravamenIdHiddenField" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:Repeater ID="rptSeccion8910" runat="server"
                                    DataSourceID="dsPreguntaSeccion8910"
                                    OnItemDataBound="rptSeccion8910_ItemDataBound">
                                    <ItemTemplate>
                                        <br />
                                        <div class="pregunta">
                                            <asp:Label ID="lblPreguntaSeccion" runat="server"
                                                Text='<%# Eval("Seccion") + ". " + Eval("Pregunta") %>' CssClass="label"></asp:Label>
                                            <asp:HiddenField ID="PAPreguntaSeccion8910Id" runat="server" Value='<%# Eval("Seccion") %>' />
                                        </div>
                                        <asp:Label ID="PAPreguntaSeccion" runat="server" CssClass="respuestaPregunta"></asp:Label>
                                        <asp:Panel ID="pnlObservaciones" runat="server"
                                            CssClass="observacionesPregunta" Visible="false">
                                            <asp:Label ID="PAPreguntaAfirmativaSeccion" runat="server"
                                                Text='<%# Eval("Observacion") %>'></asp:Label>
                                        </asp:Panel>
                                        <div style="clear: both;"></div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:ObjectDataSource ID="dsPreguntaSeccion8910" runat="server"
                                    TypeName="Artexacta.App.Desgravamen.BLL.ConsultaPreguntaBLL"
                                    SelectMethod="GetConsultaPreguntaBySeccionCita"
                                    OnSelected="dsPreguntaSeccion8910_Selected">
                                    <SelectParameters>
                                        <asp:Parameter Name="secciones" Type="String" DefaultValue="8,9,10" />
                                        <asp:ControlParameter Name="citaDesgravamenId" PropertyName="Value" ControlID="CitaDesgravamenIdHiddenField" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <br />
                                <table class="tablaDatos" cellpadding="3" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td class="cellTitle">Edad si vive</td>
                                            <td colspan="2" class="cellTitle">Estado de salud / Causa de muerte / Obs</td>
                                            <td class="cellTitle">Edad al morir</td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td class="cellTitle cellFirstCol">Padre</td>
                                            <td>
                                                <asp:Label ID="txtEdadPadre" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="txtEstadoPadre" runat="server"></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="txtEdadMuertePadre" runat="server">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="cellTitle">Madre</td>
                                            <td>
                                                <asp:Label ID="txtEdadMadre" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="txtEstadoMadre" runat="server"></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="txtEdadMuerteMadre" runat="server">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td class="cellTitle">Nro. Vivos</td>
                                            <td class="cellTitle">Edad / Género
                                                <br />
                                                Hermanos y hermanas</td>
                                            <td class="cellTitle">Estado de salud / Causa de muerte / Obs</td>
                                            <td class="cellTitle">Nro. Muertos</td>
                                        </tr>
                                        <tr>
                                            <td class="cellTitle">Hermanos</td>
                                            <td>
                                                <asp:Label ID="nroHijosVivos" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtEdadGeneroHnos" runat="server"></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="txtEstadoHermanos" runat="server"></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="nroHijosMuertos" runat="server">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                                <br />
                                <asp:Label ID="Label2" runat="server" Text="11." CssClass="label"></asp:Label>
                                <br />
                                <asp:Label ID="p11a" runat="server" Text="a) Estatura (en metros)" CssClass="label"></asp:Label>
                                <asp:Label ID="p11Estatura" runat="server">
                                </asp:Label>

                                <br />
                                <asp:Label ID="p11b" runat="server" Text="b) Peso (en kg)" CssClass="label"></asp:Label>
                                <asp:Label ID="p11Peso" runat="server">
                                </asp:Label>


                                <asp:Repeater ID="rptSeccion12" runat="server"
                                    DataSourceID="dsPreguntaSeccion12"
                                    OnItemDataBound="rptSeccion12_ItemDataBound">
                                    <ItemTemplate>
                                        <br />
                                        <div class="pregunta">
                                            <asp:Label ID="lblPreguntaSeccion" runat="server"
                                                Text='<%# Eval("Seccion") + ". " + Eval("Pregunta") %>' CssClass="label"></asp:Label>
                                            <asp:HiddenField ID="PAPreguntaSeccion12Id" runat="server" Value='<%# Eval("Seccion") %>' />
                                        </div>
                                        <asp:Label ID="PAPreguntaSeccion" runat="server" CssClass="respuestaPregunta"></asp:Label>
                                        <asp:Panel ID="pnlObservaciones" runat="server"
                                            CssClass="observacionesPregunta" Visible="false">
                                            <asp:Label ID="PAPreguntaAfirmativaSeccion" runat="server"
                                                Text='<%# Eval("Observacion") %>'></asp:Label>
                                        </asp:Panel>
                                        <div style="clear: both;"></div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:ObjectDataSource ID="dsPreguntaSeccion12" runat="server"
                                    TypeName="Artexacta.App.Desgravamen.BLL.ConsultaPreguntaBLL"
                                    SelectMethod="GetConsultaPreguntaBySeccionCita"
                                    OnSelected="dsPreguntaSeccion12_Selected">
                                    <SelectParameters>
                                        <asp:Parameter Name="secciones" Type="String" DefaultValue="12" />
                                        <asp:ControlParameter Name="citaDesgravamenId" PropertyName="Value" ControlID="CitaDesgravamenIdHiddenField" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <br />
                                <asp:Label ID="p13" runat="server" Text="13. Si el Propuesto Asegurado es mujer" CssClass="label"></asp:Label>

                                <asp:Repeater ID="rptSeccion13" runat="server"
                                    DataSourceID="dsPreguntaSeccion13"
                                    OnItemDataBound="rptSeccion13_ItemDataBound">
                                    <ItemTemplate>
                                        <br />
                                        <div class="pregunta">
                                            <asp:Label ID="lblPreguntaSeccion13" runat="server" Text='<%# Eval("Inciso") + ". " + Eval("Pregunta") %>'></asp:Label>
                                            <asp:HiddenField ID="PAPreguntaSeccion13Id" runat="server" Value='<%# Eval("Inciso") %>' />
                                        </div>
                                        <asp:Label ID="PAPreguntaSeccion13" runat="server" CssClass="respuestaPregunta"></asp:Label>
                                        <asp:Panel ID="pnlObservaciones" runat="server"
                                            CssClass="observacionesPregunta" Visible="false">
                                            <asp:Label ID="PAPreguntaAfirmativaSeccion" runat="server"
                                                Text='<%# Eval("Observacion") %>'></asp:Label>
                                        </asp:Panel>
                                        <div style="clear: both;"></div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:ObjectDataSource ID="dsPreguntaSeccion13" runat="server"
                                    TypeName="Artexacta.App.Desgravamen.BLL.ConsultaPreguntaBLL"
                                    SelectMethod="GetConsultaPreguntaBySeccionCita"
                                    OnSelected="dsPreguntaSeccion13_Selected">
                                    <SelectParameters>
                                        <asp:Parameter Name="secciones" Type="String" DefaultValue="13" />
                                        <asp:ControlParameter Name="citaDesgravamenId" PropertyName="Value" ControlID="CitaDesgravamenIdHiddenField" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:Repeater ID="rptSeccion14151617" runat="server"
                                    DataSourceID="dsPreguntaSeccion14151617"
                                    OnItemDataBound="rptSeccion14151617_ItemDataBound">
                                    <ItemTemplate>
                                        <br />
                                        <div class="pregunta">
                                            <asp:Label ID="lblPreguntaSeccion" runat="server"
                                                Text='<%# Eval("Seccion") + ". " + Eval("Pregunta") %>' CssClass="label"></asp:Label>
                                            <asp:HiddenField ID="PAPreguntaSeccion14151617Id" runat="server" Value='<%# Eval("Seccion") %>' />
                                        </div>
                                        <asp:Label ID="PAPreguntaSeccion" runat="server" CssClass="respuestaPregunta"></asp:Label>
                                        <asp:Panel ID="pnlObservaciones" runat="server"
                                            CssClass="observacionesPregunta" Visible="false">
                                            <asp:Label ID="PAPreguntaAfirmativaSeccion" runat="server"
                                                Text='<%# Eval("Observacion") %>'></asp:Label>
                                        </asp:Panel>
                                        <div style="clear: both;"></div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:ObjectDataSource ID="dsPreguntaSeccion14151617" runat="server"
                                    TypeName="Artexacta.App.Desgravamen.BLL.ConsultaPreguntaBLL"
                                    SelectMethod="GetConsultaPreguntaBySeccionCita"
                                    OnSelected="dsPreguntaSeccion14151617_Selected">
                                    <SelectParameters>
                                        <asp:Parameter Name="secciones" Type="String" DefaultValue="14,15,16,17" />
                                        <asp:ControlParameter Name="citaDesgravamenId" PropertyName="Value" ControlID="CitaDesgravamenIdHiddenField" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <div runat="server" id="divSeccionCovid" visible="false">
                                    <br />
                                    <div class="pregunta">
                                        <asp:Label ID="lblPreguntaSeccionCovid1" runat="server" CssClass="label"></asp:Label>
                                    </div>
                                    <asp:Label ID="PAPreguntaSeccionCovid1" runat="server" CssClass="respuestaPregunta"></asp:Label>                                
                                    <div style="clear: both;"></div>
                                    <br />
                                    <div class="pregunta">
                                        <asp:Label ID="lblPreguntaSeccionCovid1a" runat="server" CssClass=""></asp:Label>
                                    </div>
                                    <asp:Label ID="PAPreguntaSeccionCovid1a" runat="server" CssClass=""></asp:Label>                                
                                    <div style="clear: both;"></div>
                                    <br />
                                    <div class="pregunta">
                                        <asp:Label ID="lblPreguntaSeccionCovid1b" runat="server" CssClass=""></asp:Label>
                                    </div>
                                    <asp:Label ID="PAPreguntaSeccionCovid1b" runat="server" CssClass=""></asp:Label>                                
                                    <div style="clear: both;"></div>
                                    <br />
                                    <div class="pregunta">
                                        <asp:Label ID="lblPreguntaSeccionCovid1c" runat="server" CssClass=""></asp:Label>
                                    </div>
                                    <asp:Label ID="PAPreguntaSeccionCovid1c" runat="server" CssClass=""></asp:Label>                                
                                    <div style="clear: both;"></div>
                                    <br />
                                    <div class="pregunta">
                                        <asp:Label ID="lblPreguntaSeccionCovid1d" runat="server" CssClass=""></asp:Label>
                                    </div>
                                    <asp:Label ID="PAPreguntaSeccionCovid1d" runat="server" CssClass=""></asp:Label>
                                
                                    <div style="clear: both;"></div>
                                    <br />
                                    <div class="pregunta">
                                        <asp:Label ID="lblPreguntaSeccionCovid2" runat="server" CssClass="label"></asp:Label>
                                    </div>
                                    <asp:Label ID="PAPreguntaSeccionCovid2" runat="server" CssClass="respuestaPregunta"></asp:Label>
                                    <div style="clear: both;"></div>
                                    <br />
                                    <div class="pregunta">
                                        <asp:Label ID="lblPreguntaSeccionCovid2a" runat="server" CssClass=""></asp:Label>
                                    </div>
                                    <asp:Label ID="PAPreguntaSeccionCovid2a" runat="server" CssClass=""></asp:Label>
                                    <div style="clear: both;"></div>
                                    <br />
                                    <div class="pregunta">
                                        <asp:Label ID="lblPreguntaSeccionCovid2a2" runat="server" CssClass=""></asp:Label>
                                    </div>
                                    <asp:Label ID="PAPreguntaSeccionCovid2a2" runat="server" CssClass=""></asp:Label>
                                    <div style="clear: both;"></div>
                                    <br />
                                    <div class="pregunta">
                                        <asp:Label ID="lblPreguntaSeccionCovid2b" runat="server" CssClass=""></asp:Label>
                                    </div>
                                    <asp:Label ID="PAPreguntaSeccionCovid2b" runat="server" CssClass=""></asp:Label>
                                    <div style="clear: both;"></div>
                                    <br />
                                    <div class="pregunta">
                                        <asp:Label ID="lblPreguntaSeccionCovid2c" runat="server" CssClass=""></asp:Label>
                                    </div>
                                    <asp:Label ID="PAPreguntaSeccionCovid2c" runat="server" CssClass=""></asp:Label>
                                    <div style="clear: both;"></div>
                                    <br />
                                    <div class="pregunta">
                                        <asp:Label ID="lblPreguntaSeccionCovid2d" runat="server" CssClass=""></asp:Label>
                                    </div>
                                    <asp:Label ID="PAPreguntaSeccionCovid2d" runat="server" CssClass=""></asp:Label>
                                </div>                                                                
                            </div>
                        </div>
                    </div>

                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <!-- ========================= EXAMEN CONFIDENCIAL ======================================== -->
                    <asp:Panel ID="pnlPreguntasConfidenciales" runat="server" CssClass="oneColumn">
                        <div class="frame">
                            <div class="columnContent">

                                <asp:Label ID="lblp18" runat="server" Text="18." CssClass="label"></asp:Label>

                                <br />
                                <asp:Label ID="Label3" runat="server" Text="a. Cuánto tiempo hace que conoce usted al Propuesto Asegurado?"
                                    CssClass="label"></asp:Label>
                                <asp:Label ID="txtConocePA" runat="server" CssClass="smallField"></asp:Label>

                                <asp:Label ID="Label4" runat="server" Text="b. Es paciente o familiar suyo?"
                                    CssClass="label"></asp:Label>
                                <asp:Label ID="txtParientePA" runat="server" CssClass="smallField"></asp:Label>

                                <table class="tablaDatos" cellpadding="3" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <td class="cellTitle">Pecho (Expiración forzada)</td>
                                            <td class="cellTitle">Pecho (Inspiración completa)</td>
                                            <td class="cellTitle">Abdomen (sobre el ombligo)</td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="txtPechoExpiracion" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtPechoInspiracion" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtAbdomen" runat="server">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <div class="pregunta">
                                    <asp:Label ID="lblAparentaPA" runat="server" CssClass="label"
                                        Text="Es el propuesto asegurado de aspecto enfermizo o aparenta más edad de la declarada?"></asp:Label>
                                </div>
                                <asp:Label ID="PAAparentaMasEdad" runat="server" CssClass="respuestaPregunta"></asp:Label>

                                <div style="clear: both;"></div>
                                <asp:Label ID="lblp19" runat="server" Text="19. Presión arterial (si superior a 140 sistólica o 90 diastólica, tómela 2 veces más)" CssClass="label"></asp:Label>
                                <table class="tablaDatos" cellpadding="3" cellspacing="0">
                                    <tbody>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td class="cellTitle">1era toma</td>
                                            <td class="cellTitle">2da toma</td>
                                            <td class="cellTitle">3era toma</td>
                                        </tr>
                                        <tr>
                                            <td class="cellTitle">Sistólica / Diastólica (5ta fase)</td>
                                            <td>
                                                <asp:Label ID="txtPresionArterial1" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtPresionArterial2" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtPresionArterial3" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                                <br />
                                <!-- 20 PULSO -->
                                <asp:Label ID="lblp20" runat="server" Text="20. Pulso" CssClass="label"></asp:Label>
                                <table class="tablaDatos" cellpadding="3" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td class="cellTitle">Frecuencia p.m.</td>
                                            <td class="cellTitle">Irregularidades p.m.</td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td class="cellTitle">En Descanso</td>
                                            <td>
                                                <asp:Label ID="txtFrecuenciaDescanso" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtIrregDescanso" runat="server">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="cellTitle">Después de Esfuerzo</td>
                                            <td>
                                                <asp:Label ID="txtFrecuenciaEsfuerzo" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtIrregEsfuerzo" runat="server">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="cellTitle">Pasados 5 minutos
                                            </td>
                                            <td>
                                                <asp:Label ID="txtFrecuencia5Minutos" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtIrreg5Minutos" runat="server">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                                <br />
                                <!-- 21. CORAZON -->
                                <asp:Label ID="Label1" runat="server" Text="21. Corazón. Hay:" CssClass="label"></asp:Label>

                                <div style="float: left; margin: 0px 10px 0px 25px;">
                                    Hipertrofia:    
                                </div>
                                <asp:Label ID="PACorazonHipertrofia" runat="server" CssClass="respuestaPregunta">
                                </asp:Label>

                                <div style="float: left; margin: 0px 10px 0px 25px;">
                                    Disnea:
                                    
                                </div>
                                <asp:Label ID="PACorazonDisnea" runat="server" CssClass="respuestaPregunta">
                                </asp:Label>
                                <div style="float: left; margin: 0px 10px 0px 25px;">
                                    Soplo:
                                </div>
                                <asp:Label ID="PACorazonSoplo" runat="server" CssClass="respuestaPregunta">
                                </asp:Label>
                                <div style="float: left; margin: 0px 10px 0px 25px;">
                                    Edema:
                                </div>
                                <asp:Label ID="PACorazonEdema" runat="server" CssClass="respuestaPregunta">
                                </asp:Label>

                                <div style="clear: both;"></div>

                                <!-- 21 CORAZON ========== SOPLO ================================= -->
                                <asp:Panel ID="situacionSoplo" runat="server">
                                    <br />
                                    Soplo:
                                    <table class="tablaDatos" cellpadding="3" cellspacing="0">
                                        <thead>
                                            <tr>
                                                <td class="cellTitle">Situación soplo</td>
                                                <td class="cellTitle">Descripción soplo</td>
                                            </tr>

                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="txtSituacionSoplo" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtDescripcionSoplo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <br />
                                    <div style="float: left; margin-right: 20px;">
                                        Constante:
                                    </div>
                                    <asp:Label ID="PASoploConstante" runat="server">
                                    </asp:Label>

                                    <div style="float: left; margin-right: 20px;">
                                        Inconstante:
                                    </div>
                                    <asp:Label ID="PASoploInconstante" runat="server">
                                    </asp:Label>

                                    <div style="float: left; margin-right: 20px;">
                                        Irradiado:
                                    </div>
                                    <asp:Label ID="PASoploIrradiado" runat="server">
                                    </asp:Label>

                                    <div style="float: left; margin-right: 20px;">
                                        Localizado:
                                    </div>
                                    <asp:Label ID="PASoploLocalizado" runat="server">
                                    </asp:Label>

                                    <div style="float: left; margin-right: 20px;">
                                        Sistólico:
                                    </div>
                                    <asp:Label ID="PASoploSistolico" runat="server">
                                    </asp:Label>

                                    <div style="float: left; margin-right: 20px;">
                                        Presistólico:
                                    </div>
                                    <asp:Label ID="PASoploPresistolico" runat="server">
                                    </asp:Label>

                                    <div style="float: left; margin-right: 20px;">
                                        Diastólico:
                                    </div>
                                    <asp:Label ID="PASoploDiastolico" runat="server">
                                    </asp:Label>

                                    <div style="clear: both;">
                                        <br />
                                    </div>
                                    <div style="float: left; margin-right: 20px;">
                                        Suave (Gr 1-2):
                                    </div>
                                    <asp:Label ID="PASoploSuave" runat="server">
                                    </asp:Label>

                                    <div style="float: left; margin-right: 20px;">
                                        Moderado (Gr 3-4):
                                    </div>
                                    <asp:Label ID="PASoploModerado" runat="server">
                                    </asp:Label>

                                    <div style="float: left; margin-right: 20px;">
                                        Fuerte (Gr 5-6):
                                    </div>
                                    <asp:Label ID="PASoploFuerte" runat="server">
                                    </asp:Label>

                                    <div style="float: left; margin-right: 20px;">
                                        Se acentúa:
                                    </div>
                                    <asp:Label ID="PASoploAcentua" runat="server">
                                    </asp:Label>

                                    <div style="float: left; margin-right: 20px;">
                                        Sin Cambios:

                                    </div>
                                    <asp:Label ID="PASoploSinCambios" runat="server">
                                    </asp:Label>
                                    <div style="float: left; margin-right: 20px;">
                                        Se Atenua:
                                    </div>
                                    <asp:Label ID="PASoploSeAtenua" runat="server">
                                    </asp:Label>

                                </asp:Panel>

                                <div style="clear: both;"></div>

                                <br />
                                <asp:Label ID="p22" runat="server" Text="22. Hay en el examen físico alguna anormalidad de:" CssClass="label"></asp:Label>

                                <asp:Repeater ID="rptSeccion22" runat="server" DataSourceID="dsPreguntaSeccion22"
                                    OnItemDataBound="rptSeccion22_ItemDataBound">
                                    <ItemTemplate>
                                        <br />
                                        <div class="pregunta">
                                            <asp:Label ID="lblPreguntaSeccion22" runat="server" Text='<%# Eval("Inciso") + ". " + Eval("Pregunta") %>'></asp:Label>
                                            <asp:HiddenField ID="PAPreguntaSeccion22Id" runat="server" Value='<%# Eval("Inciso") %>' />
                                        </div>
                                        <asp:Label ID="PAPreguntaSeccion22" CssClass="respuestaPregunta" runat="server"></asp:Label>
                                        <asp:Panel ID="pnlObservaciones" runat="server"
                                            CssClass="observacionesPregunta" Visible="false">
                                            <asp:Label ID="PAPreguntaAfirmativaSeccion" runat="server"
                                                Text='<%# Eval("Observacion") %>'></asp:Label>
                                        </asp:Panel>
                                        <div style="clear: both;"></div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:ObjectDataSource ID="dsPreguntaSeccion22" runat="server"
                                    TypeName="Artexacta.App.Desgravamen.BLL.ConsultaPreguntaBLL"
                                    SelectMethod="GetConsultaPreguntaBySeccionCita"
                                    OnSelected="dsPreguntaSeccion22_Selected">
                                    <SelectParameters>
                                        <asp:Parameter Name="secciones" Type="String" DefaultValue="22" />
                                        <asp:ControlParameter Name="citaDesgravamenId" PropertyName="Value" ControlID="CitaDesgravamenIdHiddenField" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <br />
                                <asp:Repeater ID="rptSeccion2324" runat="server"
                                    DataSourceID="dsPreguntaSeccion2324"
                                    OnItemDataBound="rptSeccion2324_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="pregunta">
                                            <asp:Label ID="lblPreguntaSeccion" runat="server"
                                                Text='<%# Eval("Seccion") + ". " + Eval("Pregunta") %>' CssClass="label"></asp:Label>
                                            <asp:HiddenField ID="PAPreguntaSeccion2324Id" runat="server" Value='<%# Eval("Seccion") %>' />
                                        </div>
                                        <asp:Label ID="PAPreguntaSeccion" CssClass="respuestaPregunta" runat="server"></asp:Label>
                                        <asp:Panel ID="pnlObservaciones" runat="server"
                                            CssClass="observacionesPregunta" Visible="false">
                                            <asp:Literal ID="PAPreguntaAfirmativaSeccion" runat="server"
                                                Text='<%# Eval("Observacion") %>'></asp:Literal>
                                        </asp:Panel>
                                        <div style="clear: both;"></div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:ObjectDataSource ID="dsPreguntaSeccion2324" runat="server"
                                    TypeName="Artexacta.App.Desgravamen.BLL.ConsultaPreguntaBLL"
                                    SelectMethod="GetConsultaPreguntaBySeccionCita"
                                    OnSelected="dsPreguntaSeccion2324_Selected">
                                    <SelectParameters>
                                        <asp:Parameter Name="secciones" Type="String" DefaultValue="23,24" />
                                        <asp:ControlParameter Name="citaDesgravamenId" PropertyName="Value" ControlID="CitaDesgravamenIdHiddenField" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <br />
                                <asp:Label ID="lblp25" runat="server" Text="25. Observaciones de Análisis de orina, densidad, albúmina, glucosa" CssClass="label"></asp:Label>
                                <div class="pregunta">Análisis de orina</div>
                                <asp:Label ID="PAOrina" runat="server"
                                    CssClass="observacionesPregunta"></asp:Label>
                                <div style="clear: both;"></div>

                                <div class="pregunta">Densidad</div>
                                <asp:Label ID="PADensidad" runat="server"
                                    CssClass="observacionesPregunta"></asp:Label>
                                <div style="clear: both;"></div>

                                <div class="pregunta">Albúmina</div>
                                <asp:Label ID="PAAlbumina" runat="server"
                                    CssClass="observacionesPregunta"></asp:Label>
                                <div style="clear: both;"></div>

                                <div class="pregunta">Glucosa</div>
                                <asp:Label ID="PAGlucosa" runat="server"
                                    CssClass="observacionesPregunta"></asp:Label>
                                <div style="clear: both;"></div>
                                <br />
                                <asp:Repeater ID="rptSeccion26" runat="server"
                                    DataSourceID="dsPreguntaSeccion26"
                                    OnItemDataBound="rptSeccion26_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="pregunta">
                                            <asp:Label ID="lblPreguntaSeccion" runat="server"
                                                Text='<%# Eval("Seccion") + ". " + Eval("Pregunta") %>' CssClass="label"></asp:Label>
                                            <asp:HiddenField ID="PAPreguntaSeccion26Id" runat="server" Value='<%# Eval("Seccion") %>' />
                                        </div>
                                        <asp:Label ID="PAPreguntaSeccion" CssClass="respuestaPregunta" runat="server"></asp:Label>
                                        <asp:Panel ID="pnlObservaciones" runat="server"
                                            CssClass="observacionesPregunta" Visible="false">
                                            <asp:Literal ID="PAPreguntaAfirmativaSeccion" runat="server"
                                                Text='<%# Eval("Observacion") %>'></asp:Literal>
                                        </asp:Panel>
                                        <div style="clear: both;"></div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:ObjectDataSource ID="dsPreguntaSeccion26" runat="server"
                                    TypeName="Artexacta.App.Desgravamen.BLL.ConsultaPreguntaBLL"
                                    SelectMethod="GetConsultaPreguntaBySeccionCita"
                                    OnSelected="dsPreguntaSeccion26_Selected">
                                    <SelectParameters>
                                        <asp:Parameter Name="secciones" Type="String" DefaultValue="26" />
                                        <asp:ControlParameter Name="citaDesgravamenId" PropertyName="Value" ControlID="CitaDesgravamenIdHiddenField" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:Label ID="lblComentariosConfidenciales" runat="server"
                                    Text="Observaciones confidenciales (no se verán en la Revisión Médica)"
                                    CssClass="label"></asp:Label>
                                <asp:Label ID="txtComentariosConfidenciales" runat="server" CssClass="observacionesPregunta"></asp:Label>
                            </div>
                        </div>
                    </asp:Panel>


                </td>
            </tr>
        </tbody>

    </table>

    <asp:HiddenField ID="CitaDesgravamenIdHiddenField" runat="server" Value="0" />
    <asp:HiddenField ID="PaginaBackHiddenField" runat="server" />

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= printIcon.ClientID %>').click(function () {
                window.print();
                return false;
            });
        });
    </script>
</asp:Content>

