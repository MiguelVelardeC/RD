<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ExamenMedico.aspx.cs" Inherits="Desgravamen_ExamenMedico" %>

<%@ Register TagPrefix="RedSalud" TagName="FileUpload" Src="~/UserControls/FileUpload.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function ValidateEstadoCivil(sender, args) {
            var checkBoxList = document.getElementById("<%=chkEstadoCivil.ClientID %>");
        var checkboxes = checkBoxList.getElementsByTagName("input");
        var isValid = false;
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                isValid = true;
                break;
            }
        }
        args.IsValid = isValid;
        }
        $(document).ready(function () {
            $("#<%= cmdFichaMedica.ClientID %>").click(function () {
                var isRevisionCreated = $("#<%= IsRevisionCreated.ClientID %>").val();
                if (isRevisionCreated != "0") {                   
                    return true;
                }
                alert("SE DEBE GUARDAR LA REVISION MÉDICA ANTES DE LA HISTORIA CLÍNICA");
                return false;
            });
        });
    </script>
    <style>
        html {
            text-transform: uppercase;
        }
        .mensajeIncompleto {
            border:1px solid #808080; background-color: #ffd800; width:80%; margin: 10px auto; padding: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <asp:HiddenField ID="IsRevisionCreated" Value="0" runat="server" />
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Revisión Médica</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink ID="cmdVolver" runat="server" NavigateUrl="AgendaMedico.aspx" Text="Volver a la agenda"></asp:HyperLink>

                    <asp:LinkButton ID="cmdImprimir" runat="server" OnClick="cmdImprimir_Click" Text="Imprimir Exámen Médico"></asp:LinkButton>

                    <asp:LinkButton ID="cmdFichaMedica" runat="server" Text="Historia Clínica" OnClick="cmdFichaMedica_Click"></asp:LinkButton>
                </div>

                <asp:Panel ID="MensajeDeIncompleto" runat="server" CssClass="mensajeIncompleto" Visible="false">
                    Debe llenar la Historia Clínica también por favor
                </asp:Panel>

                <div class="buttonsPanel">
                    <asp:LinkButton ID="btnSaveExamenMedico2" Text="" runat="server" 
                        CssClass="button" OnClick="btnSaveExamenMedico_Click"
                        ValidationGroup="examenMedico">
                        <span>Guardar la revisión médica</span>
                    </asp:LinkButton>
                </div>

                <asp:ValidationSummary ID="summaryErrores" runat="server" ValidationGroup="examenMedico" ShowSummary="false" ShowValidationErrors="true" ShowMessageBox="true" />

                Médico Examinador: <asp:Literal ID="MENombre" runat="server"></asp:Literal>
                <br /><br />
                Propuesto Asegurado: <asp:Literal ID="PANombre" runat="server"></asp:Literal>
            </div>
        </div>
    </div>

    <div class="threeColsLeft">
        <div class="frame">
            <div class="columnContent">

                <asp:Label ID="lblProfesion" runat="server" CssClass="label" Text="Ocupación y/o profesión:"></asp:Label> 
                <asp:TextBox ID="PAProfesion" runat="server" CssClass="bigField"></asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="rqrPAProfesion" runat="server"
                        ControlToValidate="PAProfesion"
                        ErrorMessage="Debe colocar la profesión del propuesto asegurado"
                        ValidationGroup="examenMedico"
                        Display="Dynamic" />
                </div>

                <asp:Label ID="lblEstado" runat="server" CssClass="label" Text="Estado Civil"></asp:Label>
                <asp:RadioButtonList ID="chkEstadoCivil" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Soltero" Value="SOLTERO"></asp:ListItem>
                    <asp:ListItem Text="Casado" Value="CASADO"></asp:ListItem>
                    <asp:ListItem Text="Divorciado" Value="DIVORCIADO"></asp:ListItem>
                    <asp:ListItem Text="Viudo" Value="VIUDO"></asp:ListItem>
                    <asp:ListItem Text="Separado" Value="SEPARADO"></asp:ListItem>
                </asp:RadioButtonList>
                <div class="validators">
                    <asp:CustomValidator ID="rqrchkEstadoCivil" ErrorMessage="Seleccione el estado civil"
                        ValidationGroup="examenMedico"
                        Display="Dynamic" 
                        ClientValidationFunction="ValidateEstadoCivil" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div class="threeColsCenter">
        <div class="frame">
            <div class="columnContent">
                Fecha de Nacimiento: <asp:Literal ID="PAFechaNacimiento" runat="server"></asp:Literal>
                <br /><br />
                Carnet de Identidad Nro: <asp:Literal ID="PACI" runat="server"></asp:Literal>

                <br /><br />
                Foto del Propuesto Asegurado

                <RedSalud:FileUpload ID="FotoPAFileUpload" runat="server" ShowMode="Normal" OnFilesLoaded="FotoPAFileUpload_FilesLoaded" />
            </div>
        </div>
    </div>
    <div class="threeColsRight">
        <div class="frame">
            <asp:Image ID="FotoPAUrl" runat="server" ImageUrl="~/Images/Neutral/paciente.jpg" ImageAlign="Right" Width="200" />
        </div>
    </div>

    <div class="oneColumn">
        <div class="frame">
            <div class="columnContent">
                <asp:Label ID="p1" runat="server" Text="1. Datos de consultas recientes" CssClass="label"></asp:Label>
                <br /><br />
                a. Nombre y dirección de su médico particular:
                <asp:TextBox ID="PAMedicoParticular" runat="server" CssClass="bigField"></asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="rqrPAMedicoParticular" runat="server"
                        ControlToValidate="PAMedicoParticular"
                        ErrorMessage="Debe colocar el nombre del médico particular (o la palabra ninguno)"
                        ValidationGroup="examenMedico"
                        Display="Dynamic" />
                </div>
                <br />
                b. Fecha y motivo de la consulta más reciente:
                <asp:TextBox ID="PAConsultaReciente" runat="server" CssClass="bigField" Rows="3" TextMode="MultiLine"></asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="rqrPAConsultaReciente" runat="server"
                        ControlToValidate="PAConsultaReciente"
                        ErrorMessage="Fecha o año de la consulta más reciente"
                        ValidationGroup="examenMedico"
                        Display="Dynamic" />
                </div>
                <br />
                c. Qué tratamiento o medicación se prescribió:
                <asp:TextBox ID="PATratamiento" runat="server" CssClass="bigField" Rows="3" TextMode="MultiLine"></asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="rqrPATratamiento" runat="server"
                        ControlToValidate="PATratamiento"
                        ErrorMessage="Indique el último tratamiento que recuerde"
                        ValidationGroup="examenMedico"
                        Display="Dynamic" />
                </div>
                <br /><br />
                <asp:Label ID="p2" runat="server" Text="2. Ha sido tratado o tiene conocimiento de haber padecido de" CssClass="label"></asp:Label>
                <br />
                En caso de respuesta afirmativa a alguna pregunta indique el número de éste e incluya diagnóstico, fecha, duración, 
                grado de recuperación y nombre / dirección de médicos a quienes se pueda pedir información.
                <br />
                <asp:Repeater ID="rptSeccion2" runat="server" 
                    DataSourceID="dsPreguntaSeccion2"
                    OnItemDataBound="rptSeccion2_ItemDataBound">
                    <ItemTemplate>
                        <br />
                        <asp:Label ID="lblPreguntaSeccion2" runat="server" Text='<%# Eval("Inciso") + ". " + Eval("Pregunta") %>'></asp:Label>
                        <asp:HiddenField ID="PAPreguntaSeccion2Id" runat="server" Value='<%# Eval("Inciso") %>' />                        
                        <asp:RadioButtonList ID="PAPreguntaSeccion2" runat="server" 
                            CssClass="preguntaBoolean" RepeatDirection="Horizontal"> 
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="validators">
                            <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccion2" runat="server"
                                ControlToValidate="PAPreguntaSeccion2"
                                ErrorMessage='<%# "Debe colocar si o no en opción " + Eval("Inciso") + " de la pregunta 2" %>'
                                ValidationGroup="examenMedico"
                                Display="Dynamic" />
                        </div>
                        <div class="observacionesPregunta" style="display:none; ">
                            <asp:Label ID="lblPreguntaAfirmativaSeccion2" runat="server" Text="Observaciones"></asp:Label>
                            <br />
                            <asp:TextBox ID="PAPreguntaAfirmativaSeccion2" runat="server" 
                                CssClass="bigField" Rows="3" TextMode="MultiLine"
                                Text='<%# Eval("Observacion") %>'></asp:TextBox>
                        </div>
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
                        <asp:Label ID="lblPreguntaSeccion" runat="server" 
                            Text='<%# Eval("Seccion") + ". " + Eval("Pregunta") %>' CssClass="label"></asp:Label>
                        <asp:HiddenField ID="PAPreguntaSeccion3456Id" runat="server" Value='<%# Eval("Seccion") %>' />
                        <asp:RadioButtonList ID="PAPreguntaSeccion" runat="server" CssClass="preguntaBoolean" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="validators">
                            <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccion" runat="server"
                                ControlToValidate="PAPreguntaSeccion"
                                ErrorMessage='<%# "Debe colocar si o no en pregunta " + Eval("Seccion") %>'
                                ValidationGroup="examenMedico"
                                Display="Dynamic" />
                        </div>
                        <div class="observacionesPregunta" style="display:none; ">
                            <asp:Label ID="lblPreguntaAfirmativaSeccion" runat="server" Text="Observaciones"></asp:Label>
                            <br />
                            <asp:TextBox ID="PAPreguntaAfirmativaSeccion" runat="server" 
                                CssClass="bigField" Rows="3" TextMode="MultiLine"
                                Text='<%# Eval("Observacion") %>'></asp:TextBox>
                        </div>
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
                        <asp:Label ID="lblPreguntaSeccion7" runat="server" Text='<%# Eval("Inciso") + ". " + Eval("Pregunta") %>'></asp:Label>
                        <asp:HiddenField ID="PAPreguntaSeccion7Id" runat="server" Value='<%# Eval("Inciso") %>' />
                        <asp:RadioButtonList ID="PAPreguntaSeccion7" runat="server" CssClass="preguntaBoolean" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="validators">
                            <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccion7" runat="server"
                                ControlToValidate="PAPreguntaSeccion7"
                                ErrorMessage='<%# "Debe colocar si o no en opción " + Eval("Inciso") + " de la pregunta 7" %>'
                                ValidationGroup="examenMedico"
                                Display="Dynamic" />
                        </div>
                        <div class="observacionesPregunta" style="display:none; ">
                            <asp:Label ID="lblPreguntaAfirmativaSeccion7" runat="server" Text="Observaciones"></asp:Label>
                            <br />
                            <asp:TextBox ID="PAPreguntaAfirmativaSeccion7" runat="server" 
                                CssClass="bigField" Rows="3" TextMode="MultiLine"                                
                                Text='<%# Eval("Observacion") %>'></asp:TextBox>
                        </div>
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
                        <asp:Label ID="lblPreguntaSeccion" runat="server" 
                            Text='<%# Eval("Seccion") + ". " + Eval("Pregunta") %>' CssClass="label"></asp:Label>
                        <asp:HiddenField ID="PAPreguntaSeccion8910Id" runat="server" Value='<%# Eval("Seccion") %>' />
                        <asp:RadioButtonList ID="PAPreguntaSeccion" runat="server" CssClass="preguntaBoolean" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="validators">
                            <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccion8910" runat="server"
                                ControlToValidate="PAPreguntaSeccion"
                                ErrorMessage='<%# "Debe colocar si o no en pregunta " + Eval("Seccion") %>'
                                ValidationGroup="examenMedico"
                                Display="Dynamic" />
                        </div>
                        <div class="observacionesPregunta" style="display:none; ">
                            <asp:Label ID="lblPreguntaAfirmativaSeccion" runat="server" Text="Observaciones"></asp:Label>
                            <br />
                            <asp:TextBox ID="PAPreguntaAfirmativaSeccion" runat="server" 
                                CssClass="bigField" Rows="3" TextMode="MultiLine"
                                Text='<%# Eval("Observacion") %>'></asp:TextBox>
                        </div>
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
                <table>
                    <thead>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Edad si vive</td>
                            <td colspan="2">Estado de salud / Causa de muerte / Obs</td>
                            <td>Edad al morir</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Padre</td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtEdadPadre" runat="server"
                                    CssClass="smallField" IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false"
                                    MinValue="0" MaxValue="200">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtEstadoPadre" runat="server" CssClass="largeField" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                <div class="validators">
                                    <asp:RequiredFieldValidator ID="rqrtxtEstadoPadre" runat="server"
                                        ControlToValidate="txtEstadoPadre"
                                        ErrorMessage="Colocar el estado de salud"
                                        ValidationGroup="examenMedico"
                                        Display="Dynamic" />
                                </div>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtEdadMuertePadre" runat="server"
                                    CssClass="smallField" IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false"
                                    MinValue="0" MaxValue="200">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Madre</td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtEdadMadre" runat="server"
                                    CssClass="smallField" IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false"
                                    MinValue="0" MaxValue="200">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtEstadoMadre" runat="server" CssClass="largeField" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                <div class="validators">
                                    <asp:RequiredFieldValidator ID="rqrtxtEstadoMadre" runat="server"
                                        ControlToValidate="txtEstadoMadre"
                                        ErrorMessage="Colocar el estado de salud"
                                        ValidationGroup="examenMedico"
                                        Display="Dynamic" />
                                </div>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtEdadMuerteMadre" runat="server"
                                    CssClass="smallField" IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false"
                                    MinValue="0" MaxValue="200">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Nro. Vivos</td>
                            <td>Edad / Género <br />Hermanos y hermanas</td>
                            <td>Estado de salud / Causa de muerte / Obs</td>
                            <td>Nro. Muertos</td>
                        </tr>
                        <tr>
                            <td>Hermanos</td>
                            <td>
                                <telerik:RadNumericTextBox ID="nroHijosVivos" runat="server"
                                    CssClass="smallField" IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false"
                                    MinValue="0" MaxValue="20">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEdadGeneroHnos" runat="server" CssClass="smallField"></asp:TextBox>
                                <div class="validators">
                                    <asp:RequiredFieldValidator ID="rqrtxtEdadGeneroHnos" runat="server"
                                        ControlToValidate="txtEdadGeneroHnos"
                                        ErrorMessage="Edad y Género hermanos y hermanas"
                                        ValidationGroup="examenMedico"
                                        Display="Dynamic" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEstadoHermanos" runat="server" CssClass="largeField" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                <div class="validators">
                                    <asp:RequiredFieldValidator ID="rqrtxtEstadoHermanos" runat="server"
                                        ControlToValidate="txtEstadoHermanos"
                                        ErrorMessage="Colocar el estado de salud de los hermanos"
                                        ValidationGroup="examenMedico"
                                        Display="Dynamic" />
                                </div>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="nroHijosMuertos" runat="server"
                                    CssClass="smallField" IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false"
                                    MinValue="0" MaxValue="20">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <br />
                <asp:Label ID="Label2" runat="server" Text="11." CssClass="label"></asp:Label>
                <br />
                <asp:Label ID="p11a" runat="server" Text="a) Estatura (en metros)" CssClass="label"></asp:Label>
                <telerik:RadNumericTextBox ID="p11Estatura" runat="server" 
                    CssClass="normalField" MinValue="0.3" MaxValue="3" NumberFormat-DecimalDigits="2"
                    IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                </telerik:RadNumericTextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="rqrP11a" runat="server"
                        ControlToValidate="p11Estatura"
                        ErrorMessage="Debe colocar estatura"
                        ValidationGroup="examenMedico"
                        Display="Dynamic" />
                </div>
                <br />
                <div>
                <div style="width:33%;">
                <asp:Label ID="p11b" runat="server" Text="b) Peso (en kg)" CssClass="label"></asp:Label>
                <telerik:RadNumericTextBox ID="p11Peso" runat="server" 
                    CssClass="normalField" MinValue="1" MaxValue="500" NumberFormat-DecimalDigits="2"
                    IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                </telerik:RadNumericTextBox>
                <asp:Button ID="ConfirmarAP" Text="Confirmar Altura y Peso" AutoPostBack="false" style="float: right; width: 180px; height: 22px; text-align: center;" runat="server" />
                <div class="validators">
                    <asp:RequiredFieldValidator ID="rqrP11b" runat="server"
                        ControlToValidate="p11Peso"
                        ErrorMessage="Debe colocar peso"
                        ValidationGroup="examenMedico"
                        Display="Dynamic" />
                </div>
                </div>

                <asp:Repeater ID="rptSeccion12" runat="server" 
                    DataSourceID="dsPreguntaSeccion12"
                    OnItemDataBound="rptSeccion12_ItemDataBound">
                    <ItemTemplate>
                        <br />
                        <asp:Label ID="lblPreguntaSeccion" runat="server" 
                            Text='<%# Eval("Seccion") + ". " + Eval("Pregunta") %>' CssClass="label"></asp:Label>
                        <asp:HiddenField ID="PAPreguntaSeccion12Id" runat="server" Value='<%# Eval("Seccion") %>' />
                        <asp:RadioButtonList ID="PAPreguntaSeccion" runat="server" CssClass="preguntaBoolean" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="validators">
                            <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccion12" runat="server"
                                ControlToValidate="PAPreguntaSeccion"
                                ErrorMessage='<%# "Debe colocar si o no en pregunta " + Eval("Seccion") %>'
                                ValidationGroup="examenMedico"
                                Display="Dynamic" />
                        </div>
                        <div class="observacionesPregunta" style="display:none; ">
                            <asp:Label ID="lblPreguntaAfirmativaSeccion" runat="server" Text="Observaciones"></asp:Label>
                            <br />
                            <asp:TextBox ID="PAPreguntaAfirmativaSeccion" runat="server" 
                                CssClass="bigField" Rows="3" TextMode="MultiLine"
                                Text='<%# Eval("Observacion") %>'></asp:TextBox>
                        </div>
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
                        <asp:Label ID="lblPreguntaSeccion13" runat="server" Text='<%# Eval("Inciso") + ". " + Eval("Pregunta") %>'></asp:Label>
                        <asp:HiddenField ID="PAPreguntaSeccion13Id" runat="server" Value='<%# Eval("Inciso") %>' />
                        <asp:RadioButtonList ID="PAPreguntaSeccion13" runat="server" CssClass="preguntaBoolean" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="validators">
                            <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccion13" runat="server"
                                ControlToValidate="PAPreguntaSeccion13"
                                ErrorMessage='<%# "Debe colocar si o no en opción " + Eval("Inciso") + " de la pregunta 13" %>'
                                ValidationGroup="examenMedico"
                                Display="Dynamic" />
                        </div>
                        <div class="observacionesPregunta" style="display:none; ">
                            <asp:Label ID="lblPreguntaAfirmativaSeccion13" runat="server" Text="Observaciones"></asp:Label>
                            <br />
                            <asp:TextBox ID="PAPreguntaAfirmativaSeccion13" runat="server" 
                                CssClass="bigField" Rows="3" TextMode="MultiLine"
                                Text='<%# Eval("Observacion") %>'></asp:TextBox>
                        </div>
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
                        <asp:Label ID="lblPreguntaSeccion" runat="server" 
                            Text='<%# Eval("Seccion") + ". " + Eval("Pregunta") %>' CssClass="label"></asp:Label>
                        <asp:HiddenField ID="PAPreguntaSeccion14151617Id" runat="server" Value='<%# Eval("Seccion") %>' />
                        <asp:RadioButtonList ID="PAPreguntaSeccion" runat="server" CssClass="preguntaBoolean" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="validators">
                            <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccion14151617" runat="server"
                                ControlToValidate="PAPreguntaSeccion"
                                ErrorMessage='<%# "Debe colocar si o no en pregunta " + Eval("Seccion") %>'
                                ValidationGroup="examenMedico"
                                Display="Dynamic" />
                        </div>
                        <div class="observacionesPregunta" style="display:none; ">
                            <asp:Label ID="lblPreguntaAfirmativaSeccion" runat="server" Text="Observaciones"></asp:Label>
                            <br />
                            <asp:TextBox ID="PAPreguntaAfirmativaSeccion" runat="server" 
                                CssClass="bigField" Rows="3" TextMode="MultiLine"
                                Text='<%# Eval("Observacion") %>'></asp:TextBox>
                        </div>
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

                <br />
                <asp:Label ID="lblp18" runat="server" Text="18." CssClass="label"></asp:Label>
                
                <br />
                <asp:Label ID="Label3" runat="server" Text="a. Cuánto tiempo hace que conoce usted al Propuesto Asegurado?"
                    CssClass="label"></asp:Label>
                <asp:TextBox ID="txtConocePA" runat="server" CssClass="smallField"></asp:TextBox>

                <asp:Label ID="Label4" runat="server" Text="b. Es paciente o familiar suyo?"
                    CssClass="label"></asp:Label>
                <asp:TextBox ID="txtParientePA" runat="server" CssClass="smallField"></asp:TextBox>

                <table>
                    <thead>
                        <tr>
                            <td>Pecho (Expiración forzada)</td>
                            <td>Pecho (Inspiración completa)</td>
                            <td>Abdomen (sobre el ombligo)</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <telerik:RadNumericTextBox ID="txtPechoExpiracion" runat="server"
                                    NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="300"
                                    IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtPechoInspiracion" runat="server"
                                    NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="300"
                                    IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtAbdomen" runat="server"
                                    NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="300"
                                    IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <asp:Label ID="lblAparentaPA" runat="server" CssClass="label"
                    Text="Es el propuesto asegurado de aspecto enfermizo o aparenta más edad de la declarada?"></asp:Label>
                <asp:RadioButtonList ID="PAAparentaMasEdad" runat="server" 
                    RepeatDirection="Horizontal"> 
                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="rqrPAAparentaMasEdad" runat="server"
                        ControlToValidate="PAAparentaMasEdad"
                        ErrorMessage="Debe colocar si o no"
                        ValidationGroup="examenMedico"
                        Display="Dynamic" />
                </div>

                <br />
                <asp:Label ID="lblp19" runat="server" Text="19. Presión arterial (si superior a 140 sistólica o 90 diastólica, tómela 2 veces más)" CssClass="label"></asp:Label>
                <table>
                    <tbody>
                        <tr>
                            <td>Sistólica / Diastólica (5ta fase)</td>
                            <td>
                                <asp:TextBox ID="txtPresionArterial1" runat="server" CssClass="smallField"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPresionArterial2" runat="server" CssClass="smallField"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPresionArterial3" runat="server" CssClass="smallField"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="rqrtxtPresionArterial1" runat="server"
                        ControlToValidate="txtPresionArterial1"
                        ErrorMessage="Presión Arterial (1) es requerido"
                        ValidationGroup="examenMedico"
                        Display="Dynamic" />                    
                </div>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="rqrtxtPresionArterial2" runat="server"
                        ControlToValidate="txtPresionArterial2"
                        ErrorMessage="Presión Arterial (2) es requerido"
                        ValidationGroup="examenMedico"
                        Display="Dynamic" />
                </div>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="rqrtxtPresionArterial3" runat="server"
                    ControlToValidate="txtPresionArterial3"
                    ErrorMessage="Presión Arterial (3) es requerido"
                    ValidationGroup="examenMedico"
                    Display="Dynamic" />
                </div>                

                <br />
                <!-- 20 PULSO -->
                <asp:Label ID="lblp20" runat="server" Text="20. Pulso" CssClass="label"></asp:Label>
                <table>
                    <thead>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Frecuencia p.m.</td>
                            <td>Irregularidades p.m.</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>En Descanso</td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtFrecuenciaDescanso" runat="server"
                                    NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="300"
                                    IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtIrregDescanso" runat="server"
                                    NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="300"
                                    IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>Después de Esfuerzo</td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtFrecuenciaEsfuerzo" runat="server"
                                    NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="300"
                                    IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtIrregEsfuerzo" runat="server"
                                    NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="300"
                                    IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Pasados 5 minutos
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtFrecuencia5Minutos" runat="server"
                                    NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="300"
                                    IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtIrreg5Minutos" runat="server"
                                    NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="300"
                                    IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <br />
                <!-- 21. CORAZON -->
                <asp:Label ID="Label1" runat="server" Text="21. Corazón. Hay:" CssClass="label"></asp:Label>

                <div style="float:left; margin-right:20px;">
                    Hipertrofia:
                    <asp:RadioButtonList ID="PACorazonHipertrofia" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                    <div class="validators">
                        <asp:RequiredFieldValidator ID="rqrPACorazonHipertrofia" runat="server"
                            ControlToValidate="PACorazonHipertrofia"
                            ErrorMessage="Debe colocar si o no"
                            ValidationGroup="examenMedico"
                            Display="Dynamic" />
                    </div>
                </div>
                <div style="float:left; margin-right:20px;">
                    Disnea:
                    <asp:RadioButtonList ID="PACorazonDisnea" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                    <div class="validators">
                        <asp:RequiredFieldValidator ID="rqrPACorazonDisnea" runat="server"
                            ControlToValidate="PACorazonDisnea"
                            ErrorMessage="Debe colocar si o no"
                            ValidationGroup="examenMedico"
                            Display="Dynamic" />
                    </div>
                </div>
                <div style="float:left; margin-right:20px;">
                    Soplo:
                    <asp:RadioButtonList ID="PACorazonSoplo" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                    <div class="validators">
                        <asp:RequiredFieldValidator ID="rqrPACorazonSoplo" runat="server"
                            ControlToValidate="PACorazonSoplo"
                            ErrorMessage="Debe colocar si o no"
                            ValidationGroup="examenMedico"
                            Display="Dynamic" />
                    </div>
                </div>
                <div style="float:left; margin-right:20px;">
                    Edema:
                    <asp:RadioButtonList ID="PACorazonEdema" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                    <div class="validators">
                        <asp:RequiredFieldValidator ID="rqrPACorazonEdema" runat="server"
                            ControlToValidate="PACorazonEdema"
                            ErrorMessage="Debe colocar si o no"
                            ValidationGroup="examenMedico"
                            Display="Dynamic" />
                    </div>
                </div>
                
                <div style="clear:both;"></div>
                
                <!-- 21 CORAZON ========== SOPLO ================================= -->
                <div id="situacionSoplo" style="display:none;">
                    <br />
                    Soplo:
                    <table>
                        <thead>
                            <tr>
                                <td>Situación soplo</td>
                                <td>Descripción soplo</td>
                            </tr>

                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtSituacionSoplo" runat="server" 
                                        CssClass="largeField" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescripcionSoplo" runat="server" 
                                        CssClass="largeField" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <div style="float:left; margin-right:20px;">
                        Constante:
                        <asp:RadioButtonList ID="PASoploConstante" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="float:left; margin-right:20px;">
                        Inconstante:
                        <asp:RadioButtonList ID="PASoploInconstante" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="float:left; margin-right:20px;">
                        Irradiado:
                        <asp:RadioButtonList ID="PASoploIrradiado" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="float:left; margin-right:20px;">
                        Localizado:
                        <asp:RadioButtonList ID="PASoploLocalizado" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="float:left; margin-right:20px;">
                        Sistólico:
                        <asp:RadioButtonList ID="PASoploSistolico" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="float:left; margin-right:20px;">
                        Presistólico:
                        <asp:RadioButtonList ID="PASoploPresistolico" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="float:left; margin-right:20px;">
                        Diastólico:
                        <asp:RadioButtonList ID="PASoploDiastolico" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="clear:both;"><br /></div>
                    <div style="float:left; margin-right:20px;">
                        Suave (Gr 1-2):
                        <asp:RadioButtonList ID="PASoploSuave" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="float:left; margin-right:20px;">
                        Moderado (Gr 3-4):
                        <asp:RadioButtonList ID="PASoploModerado" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="float:left; margin-right:20px;">
                        Fuerte (Gr 5-6):
                        <asp:RadioButtonList ID="PASoploFuerte" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="float:left; margin-right:20px;">
                        Se acentúa:
                        <asp:RadioButtonList ID="PASoploAcentua" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="float:left; margin-right:20px;">
                        Sin Cambios:
                        <asp:RadioButtonList ID="PASoploSinCambios" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="float:left; margin-right:20px;">
                        Se Atenua:
                        <asp:RadioButtonList ID="PASoploSeAtenua" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>

                <div style="clear:both;"></div>

                <br />
                <asp:Label ID="p22" runat="server" Text="22. Hay en el examen físico alguna anormalidad de:" CssClass="label"></asp:Label>

                <asp:Repeater ID="rptSeccion22" runat="server" DataSourceID="dsPreguntaSeccion22"
                    OnItemDataBound="rptSeccion22_ItemDataBound">
                    <ItemTemplate>
                        <br />
                        <asp:Label ID="lblPreguntaSeccion22" runat="server" Text='<%# Eval("Inciso") + ". " + Eval("Pregunta") %>'></asp:Label>
                        <asp:HiddenField ID="PAPreguntaSeccion22Id" runat="server" Value='<%# Eval("Inciso") %>' />
                        <asp:RadioButtonList ID="PAPreguntaSeccion22" runat="server" CssClass="preguntaBoolean" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="validators">
                            <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccion22" runat="server"
                                ControlToValidate="PAPreguntaSeccion22"
                                ErrorMessage='<%# "Debe colocar si o no en opción " + Eval("Inciso") + " de la pregunta 22" %>'
                                ValidationGroup="examenMedico"
                                Display="Dynamic" />
                        </div>
                        <div class="observacionesPregunta" style="display:none; ">
                            <asp:Label ID="lblPreguntaAfirmativaSeccion22" runat="server" Text="Observaciones"></asp:Label>
                            <br />
                            <asp:TextBox ID="PAPreguntaAfirmativaSeccion22" runat="server" 
                                CssClass="bigField" Rows="3" TextMode="MultiLine"
                                Text='<%# Eval("Observacion") %>'></asp:TextBox>
                        </div>
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

                <asp:Repeater ID="rptSeccion2324" runat="server" 
                    DataSourceID="dsPreguntaSeccion2324"
                    OnItemDataBound="rptSeccion2324_ItemDataBound">
                    <ItemTemplate>
                        <br />
                        <asp:Label ID="lblPreguntaSeccion" runat="server" 
                            Text='<%# Eval("Seccion") + ". " + Eval("Pregunta") %>' CssClass="label"></asp:Label>
                        <asp:HiddenField ID="PAPreguntaSeccion2324Id" runat="server" Value='<%# Eval("Seccion") %>' />
                        <asp:RadioButtonList ID="PAPreguntaSeccion" runat="server" CssClass="preguntaBoolean" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="validators">
                            <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccion2324" runat="server"
                                ControlToValidate="PAPreguntaSeccion"
                                ErrorMessage='<%# "Debe colocar si o no en pregunta " + Eval("Seccion") %>'
                                ValidationGroup="examenMedico"
                                Display="Dynamic" />
                        </div>
                        <div class="observacionesPregunta" style="display:none; ">
                            <asp:Label ID="lblPreguntaAfirmativaSeccion" runat="server" Text="Observaciones"></asp:Label>
                            <br />
                            <asp:TextBox ID="PAPreguntaAfirmativaSeccion" runat="server" 
                                CssClass="bigField" Rows="3" TextMode="MultiLine"
                                Text='<%# Eval("Observacion") %>'></asp:TextBox>
                        </div>
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
                <asp:Label ID="lblp25" runat="server" Text="25." CssClass="label"></asp:Label>
                <table>
                    <thead>
                        <tr>
                            <td>Análisis de orina</td>
                            <td>Densidad</td>
                            <td>Albúmina</td>
                            <td>Glucosa</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:TextBox ID="PAOrina" runat="server" 
                                    CssClass="normalField" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                <div class="validators">
                                    <asp:RequiredFieldValidator ID="rqrPAOrina" runat="server"
                                        ControlToValidate="PAOrina"
                                        ErrorMessage="Colocar obs análisis de orina"
                                        ValidationGroup="examenMedico"
                                        Display="Dynamic" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="PADensidad" runat="server" 
                                    CssClass="normalField" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                <div class="validators">
                                    <asp:RequiredFieldValidator ID="rqrPADensidad" runat="server"
                                        ControlToValidate="PADensidad"
                                        ErrorMessage="Colocar obs densidad de orina"
                                        ValidationGroup="examenMedico"
                                        Display="Dynamic" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="PAAlbumina" runat="server" 
                                    CssClass="normalField" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                <div class="validators">
                                    <asp:RequiredFieldValidator ID="rqrPAAlbumina" runat="server"
                                        ControlToValidate="PAAlbumina"
                                        ErrorMessage="Colocar obs albumina"
                                        ValidationGroup="examenMedico"
                                        Display="Dynamic" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="PAGlucosa" runat="server" 
                                    CssClass="normalField" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                <div class="validators">
                                    <asp:RequiredFieldValidator ID="rqrPAGlucosa" runat="server"
                                        ControlToValidate="PAGlucosa"
                                        ErrorMessage="Colocar obs glucosa"
                                        ValidationGroup="examenMedico"
                                        Display="Dynamic" />
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <asp:Repeater ID="rptSeccion26" runat="server" 
                    DataSourceID="dsPreguntaSeccion26"
                    OnItemDataBound="rptSeccion26_ItemDataBound">
                    <ItemTemplate>
                        <br />
                        <asp:Label ID="lblPreguntaSeccion" runat="server" 
                            Text='<%# Eval("Seccion") + ". " + Eval("Pregunta") %>' CssClass="label"></asp:Label>
                        <asp:HiddenField ID="PAPreguntaSeccion26Id" runat="server" Value='<%# Eval("Seccion") %>' />
                        <asp:RadioButtonList ID="PAPreguntaSeccion" runat="server" CssClass="preguntaBoolean" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="validators">
                            <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccion26" runat="server"
                                ControlToValidate="PAPreguntaSeccion"
                                ErrorMessage='<%# "Debe colocar si o no en pregunta " + Eval("Seccion") %>'
                                ValidationGroup="examenMedico"
                                Display="Dynamic" />
                        </div>
                        <div class="observacionesPregunta" style="display:none; ">
                            <asp:Label ID="lblPreguntaAfirmativaSeccion" runat="server" Text="Observaciones"></asp:Label>
                            <br />
                            <asp:TextBox ID="PAPreguntaAfirmativaSeccion" runat="server" 
                                CssClass="bigField" Rows="3" TextMode="MultiLine"
                                Text='<%# Eval("Observacion") %>'></asp:TextBox>
                        </div>
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

                <div runat="server" id="divSeccionCovid" visible="false">
                    <br />
                    <asp:Label ID="lblPAPreguntaSeccionCovid1" runat="server" CssClass="label"></asp:Label>
                    <asp:RadioButtonList ID="PAPreguntaSeccionCovid1" runat="server" CssClass="" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                    <div class="validators">
                        <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccionCovid1" runat="server"
                            ControlToValidate="PAPreguntaSeccionCovid1"
                            ErrorMessage='<%# "Debe colocar si o no en la pregunta 27" %>'
                            ValidationGroup="examenMedico"
                            Display="Dynamic" />
                    </div><span></span>

                    <br />
                    <asp:Label ID="lblPAPreguntaSeccionCovid1a" runat="server" CssClass=""></asp:Label>
                    <br />
                    <asp:TextBox ID="PAPreguntaSeccionCovid1a" runat="server" CssClass="normalField"></asp:TextBox>
                    <div class="validators">
                        <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccionCovid1a" runat="server"
                            ControlToValidate="PAPreguntaSeccionCovid1b"
                            ErrorMessage="colocar nombre de vacuna"
                            ValidationGroup="examenMedico"
                            Display="Dynamic" />
                    </div>

                    <br />
                    <table>
                        <thead>
                            <tr>
                                <td><asp:Label runat="server" ID="lblPAPreguntaSeccionCovid1b"></asp:Label></td>
                                <td><asp:Label runat="server" ID="lblPAPreguntaSeccionCovid1c"></asp:Label></td>
                                <td><asp:Label runat="server" ID="lblPAPreguntaSeccionCovid1d"></asp:Label></td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:TextBox ID="PAPreguntaSeccionCovid1b" runat="server" CssClass="normalField"></asp:TextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccionCovid1b" runat="server"
                                            ControlToValidate="PAPreguntaSeccionCovid1b"
                                            ErrorMessage="colocar primera fecha vacuna"
                                            ValidationGroup="examenMedico"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="PAPreguntaSeccionCovid1c" runat="server" CssClass="normalField"></asp:TextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccionCovid1c" runat="server"
                                            ControlToValidate="PAPreguntaSeccionCovid1c"
                                            ErrorMessage="colocar segunda fecha vacuna"
                                            ValidationGroup="examenMedico"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="PAPreguntaSeccionCovid1d" runat="server" CssClass="normalField" ></asp:TextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccionCovid1d" runat="server"
                                            ControlToValidate="PAPreguntaSeccionCovid1d"         
                                            ErrorMessage="colocar otras fecha vacuna"
                                            ValidationGroup="examenMedico"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>

                    <br />
                    <asp:Label ID="lblPAPreguntaSeccionCovid2" runat="server" CssClass="label"></asp:Label>
                    <asp:RadioButtonList ID="PAPreguntaSeccionCovid2" runat="server" CssClass="preguntaBoolean" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                    <div class="validators">
                        <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccionCovid2" runat="server"
                            ControlToValidate="PAPreguntaSeccionCovid2"
                            ErrorMessage='<%# "Debe colocar si o no en la pregunta 28" %>'
                            ValidationGroup="examenMedico"
                            Display="Dynamic" />
                    </div><span></span>

                    <asp:Label ID="lblPAPreguntaSeccionCovid2s1" runat="server" CssClass="label" Text="Si la respuesta es afirmativa (SI), favor aclarar:"></asp:Label>                        
                    <table>
                        <thead>
                            <tr>
                                <td><asp:Label runat="server" ID="lblPAPreguntaSeccionCovid2a"></asp:Label></td>
                                <td><asp:Label runat="server" ID="lblPAPreguntaSeccionCovid2a2"></asp:Label></td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:TextBox ID="PAPreguntaSeccionCovid2a" runat="server" CssClass="normalField"></asp:TextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccionCovid2a" runat="server"
                                            ControlToValidate="PAPreguntaSeccionCovid2a"
                                            ErrorMessage="colocar fecha"
                                            ValidationGroup="examenMedico"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="PAPreguntaSeccionCovid2a2" runat="server" CssClass="normalField"></asp:TextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccionCovid2a2" runat="server"
                                            ControlToValidate="PAPreguntaSeccionCovid2a2"
                                            ErrorMessage="colocar fecha"
                                            ValidationGroup="examenMedico"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>

                    <asp:Label ID="lblPAPreguntaSeccionCovid2b" runat="server" CssClass=""></asp:Label>
                    <asp:TextBox ID="PAPreguntaSeccionCovid2b" runat="server" CssClass="normalField" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    <div class="validators">
                        <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccionCovid2b" runat="server"
                            ControlToValidate="PAPreguntaSeccionCovid2b"
                            ErrorMessage="colocar detalle de tratamiento"
                            ValidationGroup="examenMedico"
                            Display="Dynamic" />
                    </div>
                    
                    <asp:Label ID="lblPAPreguntaSeccionCovid2c" runat="server" CssClass=""></asp:Label>
                    <asp:TextBox ID="PAPreguntaSeccionCovid2c" runat="server" CssClass="normalField" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    <div class="validators">
                        <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccionCovid2c" runat="server"
                            ControlToValidate="PAPreguntaSeccionCovid2c"
                            ErrorMessage="colocar tiempo de hospitalizacion"
                            ValidationGroup="examenMedico"
                            Display="Dynamic" />
                    </div>
                    
                    <asp:Label ID="lblPAPreguntaSeccionCovid2d" runat="server" CssClass=""></asp:Label>
                    <asp:TextBox ID="PAPreguntaSeccionCovid2d" runat="server" CssClass="normalField" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    <div class="validators">
                        <asp:RequiredFieldValidator ID="rqrPAPreguntaSeccionCovid2d" runat="server"
                            ControlToValidate="PAPreguntaSeccionCovid2d"
                            ErrorMessage="Debe colocar secuelas"
                            ValidationGroup="examenMedico"
                            Display="Dynamic" />
                    </div>
                </div>                
                
                <br />
                <asp:Label ID="lblComentariosConfidenciales" runat="server" 
                    Text="Observaciones confidenciales (no se verán en la Revisión Médica)" 
                    CssClass="label"></asp:Label>
                <asp:TextBox ID="txtComentariosConfidenciales" runat="server" 
                    CssClass="largeField" Rows="3" TextMode="MultiLine"></asp:TextBox>

                <div class="buttonsPanel">
                    <asp:LinkButton ID="btnSaveExamenMedico" Text="" runat="server" 
                        CssClass="button" OnClick="btnSaveExamenMedico_Click"
                        ValidationGroup="examenMedico">
                        <span>Guardar la revisión médica</span>
                    </asp:LinkButton>
                </div>                
                </div>            
            </div>
        </div>
    </div>
    <div id="ConfirmDialog" title="Desea confirmar el formulario?">
        <h2 style="color:red;">
            No podra modificar los datos una vez confirmado. Desea continuar?</h2>
    </div>
    <asp:HiddenField ID="CitaDesgravamenIdHiddenField" runat="server" Value="1" />
    <asp:HiddenField ID="PaginaBackHiddenField" runat="server" />
    <asp:HiddenField ID="FotoIDHiddenField" runat="server" Value="0" />
    <asp:HiddenField ID="PropuestoAseguradoIdHiddenField" runat="server" Value="0" />

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#ConfirmDialog").hide();

            

            $("#<%= btnSaveExamenMedico.ClientID %>").click(function () {                
                IsConsultaInsert();
                return false;
            });

            $("#<%= btnSaveExamenMedico2.ClientID %>").click(function () {
                IsConsultaInsert();
                return false;
            });

            function IsConsultaInsert() {
                $.ajax({
                    type: "POST",
                    url: "ExamenMedico.aspx/IsConsultaInsert",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ 'strCitaDesgravamen': $("#<%= CitaDesgravamenIdHiddenField.ClientID %>").val()}),
                    dataType: "json",
                    success: function (result) {
                        //alert("good");
                        if (result.d != true) {
                            $("#ConfirmDialog").dialog({
                                resizable: false,
                                height: 180,
                                width: 400,
                                modal: true,
                                buttons: {
                                    "Aceptar": function () {
                                        $(this).dialog("close");
                                        eval($("#<%= btnSaveExamenMedico.ClientID %>").attr("href"));
                                    },
                                    Cancel: function () {
                                        $(this).dialog("close");
                                    }
                                }
                            });

                            /*if (confirm("Los datos no seran editables una vez sean guardados, Desea confirmar y guardar los datos?")) {
                                
                            }*/
                        } else {
                            eval($("#<%= btnSaveExamenMedico.ClientID %>").attr("href"));
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
            }


            $("#<%= ConfirmarAP.ClientID%>").click(function () {
                //'Confirmar Altura y Peso'
                var alturaObj = $("#<%=p11Estatura.ClientID %>");
                var pesoObj = $("#<%=p11Peso.ClientID %>");

                if (!alturaObj.attr('readonly') == true && !pesoObj.attr('readonly') == true) {
                    alturaObj.attr('readonly', true);
                    pesoObj.attr('readonly', true);
                    $(this).val('Editar Altura y Peso');
                } else {
                    alturaObj.removeAttr('readonly');
                    pesoObj.removeAttr('readonly');
                    $(this).val('Confirmar Altura y Peso');
                }

                return false;
                //alert("altura: "+altura);
                });

            $('input:radio[id*="PAPreguntaSeccion"]').change(function() {
                if ($(this).is(":checked") && $(this).val() == 1) {
                    $(this).parent().parent().parent().parent().next().next().show();
                }
                if ($(this).is(":checked") && $(this).val() == 0) {
                    $(this).parent().parent().parent().parent().next().next().hide();
                }
            });

            $('input:radio[id*="PACorazonSoplo"]').change(function () {
                if ($(this).is(":checked") && $(this).val() == 1) {
                    $('#situacionSoplo').show();
                }
                if ($(this).is(":checked") && $(this).val() == 0) {
                    $('#situacionSoplo').hide();
                }
                
            });

            $('input:radio[id*="PAPreguntaSeccion"]').each(function () {
                if ($(this).is(":checked") && $(this).val() == 1) {
                    $(this).parent().parent().parent().parent().next().next().show();
                }
                if ($(this).is(":checked") && $(this).val() == 0) {
                    $(this).parent().parent().parent().parent().next().next().hide();
                }
            });

            $('input:radio[id*="PACorazonSoplo"]').each(function() {
                if ($(this).is(":checked") && $(this).val() == 1) {
                    $('#situacionSoplo').show();
                }
                if ($(this).is(":checked") && $(this).val() == 0) {
                    $('#situacionSoplo').hide();
                }
            });
        });
    </script>
</asp:Content>

