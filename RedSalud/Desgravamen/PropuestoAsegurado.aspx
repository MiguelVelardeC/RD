<%@ Page Title="Propuesto Asegurado" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PropuestoAsegurado.aspx.cs" Inherits="Desgravamen_PropuestoAsegurado" %>

<%@ Register TagPrefix="RedSalud" TagName="FileUpload" Src="~/UserControls/FileUpload.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span id="LblPropuestoAseguradoTitle" runat="server" class="title">Propuesto Asegurado</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" NavigateUrl="~/Desgravamen/PropuestoAseguradoLista.aspx"
                        Text="Volver a la Lista de Propuestos Asegurado">
                    </asp:HyperLink>
                    <asp:LinkButton ID="CrearCitaButton" runat="server" Visible="false"
                        OnClick="CrearCitaButton_Click">
                        Crear Cita
                    </asp:LinkButton>
                    <asp:LinkButton ID="EliminarFotoButton" runat="server" 
                        OnClick="EliminarFotoButton_Click">
                        Eliminar Foto
                    </asp:LinkButton>
                </div>

                <asp:Image ID="FotoPAUrl" runat="server" ImageUrl="~/Images/Neutral/paciente.jpg" ImageAlign="Right" Width="200" />

                <span class="label">Cédula de Identidad</span>
                <asp:TextBox ID="CedulaTextBox" runat="server" CssClass="normalField" AutoPostBack="false"></asp:TextBox>
                <asp:LinkButton id="btn_CargaPA" runat="server">Cargar Propuesto Asegurado</asp:LinkButton>               
                <!--<asp:HyperLink ID="lnkNacionalVida" runat="server" Text="Enlace Nacional Vida"></asp:HyperLink>-->
                <div class="validation">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CedulaTextBox"
                        ErrorMessage="Debe ingresar la Cédula de Identidad"
                        Display="Dynamic" ValidationGroup="PA">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegexValidatorCI" runat="server" ControlToValidate="CedulaTextBox"
                        ErrorMessage="El formato correcto son numeros seguidos de dos letras o de e para extranjeros"
                        Display="Dynamic" ValidationGroup="PA"
                        ValidationExpression="[0-9]{5,8}(sc|lp|cb|po|ch|be|or|ta|pa|e|SC|LP|CB|PO|CH|BE|OR|TA|PA|E|)" >
                    </asp:RegularExpressionValidator>
                </div>
                <span class="label">Nombre</span>
                <asp:TextBox ID="NombreTextBox" runat="server" CssClass="bigField"></asp:TextBox>
                <div class="validation">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="NombreTextBox"
                        ErrorMessage="Debe ingresar el Nombre"
                        Display="Dynamic" ValidationGroup="PA">
                    </asp:RequiredFieldValidator>
                </div>

                <span class="label">Género</span>
                <asp:RadioButtonList ID="GeneroRadioButton" runat="server" CssClass="normalField" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                    <asp:ListItem Text="Femenino" Value="F"></asp:ListItem>
                </asp:RadioButtonList>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="rqrPAGeneroRadio" runat="server"
                        ControlToValidate="GeneroRadioButton"
                        ErrorMessage="Colocar si es masculino o femenino"
                        ValidationGroup="PA"
                        Display="Dynamic" />
                </div>

                <span class="label">Fecha de Nacimiento</span>
                <telerik:RadDatePicker ID="FechaNacimientoDatePicker" runat="server"
                    MinDate="1890-01-01">
                </telerik:RadDatePicker>
                <div class="validation">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="FechaNacimientoDatePicker"
                        ErrorMessage="Debe ingresar la Fecha de Nacimiento"
                        Display="Dynamic" ValidationGroup="PA">
                    </asp:RequiredFieldValidator>
                </div>

                <span class="label">Telefono Celular</span>
                <asp:TextBox ID="TelefonoCelularTextBox" runat="server" CssClass="normalField"></asp:TextBox>
                <div class="validation">
                    <asp:RequiredFieldValidator ID="rqrTelefonoCelularTextBox" runat="server" 
                        ControlToValidate="TelefonoCelularTextBox"
                        ErrorMessage="Debe ingresar un teléfono de contacto"
                        Display="Dynamic" ValidationGroup="PA">
                    </asp:RequiredFieldValidator>
                </div>

                <!--<span class="label">Foto del Propuesto Asegurado</span>-->

                <RedSalud:FileUpload ID="FotoPAFileUpload" runat="server" ShowMode="Normal" Visible="false" />

                <div class="buttonsPanel">
                    <asp:LinkButton ID="SaveAndContinueButton" runat="server" ValidationGroup="PA" CssClass="button"
                        OnClick="SaveAndContinueButton_Click">
                        <span>Guardar y Crear Cita</span>
                    </asp:LinkButton>

                    <asp:LinkButton ID="SaveButton" runat="server" ValidationGroup="PA" CssClass="button"
                        OnClick="SaveButton_Click">
                        <span>Guardar</span>
                    </asp:LinkButton>

                    <asp:HyperLink runat="server" NavigateUrl="~/Desgravamen/PropuestoAseguradoLista.aspx" CssClass="secondaryButton"
                        Text="Cancelar">
                    </asp:HyperLink>
                    <asp:HiddenField ID="PropuestoAseguradoIdHiddenField" runat="server" Value="0" />
                    <asp:HiddenField ID="ClienteIdHiddenField" runat="server" Value="0" />
                    <asp:HiddenField ID="FotoIDHiddenField" runat="server" Value="0" />
                </div>
            </div>
        </div>
    </div>
<script type="text/javascript">
    $(document).ready(function () {
        $('#<%= lnkNacionalVida.ClientID %>').click(function () {
            alert('Enlace con el web service de Nacional Vida para traer datos con el CI');
            return false;
        });
        $("#<%= btn_CargaPA.ClientID %>").click(function () {
            CargarPAAjax();
            return false;
        });
        <%--
        $("#<%= CedulaTextBox.ClientID %>").keyup(function (e) {
            if (e.keyCode == 13) {
                CargarPAAjax();
            }
            return false;
        });
        --%>


        $('#aaaa').blur(function () {

            var ci = $(this).val();

            $.ajax({
                type: "POST",
                url: "<%=ResolveClientUrl("~/AutoCompleteWS/ComboBoxWebServices.asmx") %>/CargarPAConCI",
                contentType: "application/json; charset=utf-8",
                data: { 'ci': ci },
                dataType: "json",
                success: function (result) {
                    alert('good');
                }
            });
        });

        function CargarPAAjax() {
            var PA_parametersJSON = {
                ci: $("#<%= CedulaTextBox.ClientID %>").val(),
                clienteId: $("#<%= ClienteIdHiddenField.ClientID %>").val()
            };
            $.ajax({
                type: "POST",
                url: "PropuestoAsegurado.aspx/CargarPAAjax",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(PA_parametersJSON),
                dataType: "json",
                success: function (result) {
                    var a = "PropuestoAseguradoIdHiddenField CedulaTextBox NombreTextBox GeneroRadioButton " +
                        "FechaNacimientoDatePicker TelefonoCelularTextBox FotoIDHiddenField FotoPAUrl.ImageUrl";
                    var resultJson = result.d;
                    if (resultJson.PropuestoAseguradoId > 0) {
                        fillForm(resultJson);
                    } else {
                        cleanForm();
                        alert("No existe un Propuesto Asegurado con ese Numero de CI");
                    }

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        }
        function fillForm(resultJson) {
            $("#<%= PropuestoAseguradoIdHiddenField.ClientID %>").val(resultJson.PropuestoAseguradoId);
            $("#<%= CedulaTextBox.ClientID %>").val(resultJson.CarnetIdentidad);
            $("#<%= NombreTextBox.ClientID %>").val(resultJson.NombreCompleto);
            var radios = $("#<%= GeneroRadioButton.ClientID %> input"); //.val(resultJson.Genero);
            //$("# GeneroRadioButton.ClientID")[0].FindControl(resultJson.Genero).Selected = true;
            //var generoVal = resultJson.Genero;
            //var nameString = "input[name='" +  GeneroRadioButton.ClientID  + "'][value='" + generoVal + "']";

            //$(nameString).attr('checked', true);
            for (var i=0;i<radios.length;i++){
                if (radios[i].value == resultJson.Genero)
                {
                    $(radios[i]).attr("checked", "checked");
                    $(radios[i]).prop("checked", "checked");
                }
            }
        
            //var birthDate = new Date(resultJson.FechaNacimiento);
            $("#<%= TelefonoCelularTextBox.ClientID %>").val(resultJson.TelefonoCelular);
            $("#<%= FotoIDHiddenField.ClientID %>").val(resultJson.FotoId);
            $("#<%= FotoPAUrl.ClientID %>").attr('src', resultJson.FotoUrl_Ajax);
            var rawDate = resultJson.FechaNacimientoLong.split("-");
            var MyDate = new Date(rawDate[0], rawDate[1]-1,rawDate[2]);
            //$("#FechaNacimientoDatePicker.ClientID _dateInput").val(resultJson.FechaNacimientoLong);
            $find("<%= FechaNacimientoDatePicker.ClientID %>").set_selectedDate(MyDate);
        }
        function cleanForm() {
            $("#<%= PropuestoAseguradoIdHiddenField.ClientID %>").val("0");
            //$("# CedulaTextBox.ClientID ").val(resultJson.CarnetIdentidad);
            $("#<%= NombreTextBox.ClientID %>").val("");
            var radios = $("#<%= GeneroRadioButton.ClientID %> input");
            for (var i = 0; i < radios.length; i++) {
                if (radios[i].checked) {
                    $(radios[i]).removeAttr("checked");
                    $(radios[i]).removeProp("checked");
                }
            }
            $find("<%= FechaNacimientoDatePicker.ClientID %>").set_selectedDate(null);
            $("#<%= TelefonoCelularTextBox.ClientID %>").val("");
            $("#<%= FotoIDHiddenField.ClientID %>").val(0);
            $("#<%= FotoPAUrl.ClientID %>").attr('src', "../Images/Neutral/paciente.jpg");
        }
    });
</script>
</asp:Content>

