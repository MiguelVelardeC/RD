<%@ Page Title="Medico Desgravamen" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MedicoDesgravamenDetalle.aspx.cs" Inherits="Desgravamen_MedicoDesgravamen" %>

<%@ Register TagPrefix="RedSalud" TagName="FileUpload" Src="~/UserControls/FileUpload.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Medico Desgravamen</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" NavigateUrl="~/Desgravamen/MedicoDesgravamenLista.aspx"
                        Text="Volver a la Lista de Medicos Desgravamen">
                    </asp:HyperLink>
                </div>

                <span class="label">Usuario</span>
                <telerik:RadComboBox ID="UserRadComboBox" runat="server" 
                                        ShowMoreResultsBox="true" EnableVirtualScrolling="true" 
                                        AutoPostBack="false" EnableLoadOnDemand="true" CssClass="bigField"
                                        SelectedValue='<%# UserIDHF.Value %>'
                                        OnDataBinding="InsertRadComboBox_DataBinding">
                                        <WebServiceSettings Method="GetUsuarios" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                </telerik:RadComboBox>
                <span class="label" id="Username" runat="server"></span>
                <div class="validation">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NombreTextBox"
                        ErrorMessage="Debe ingresar el Nombre"
                        Display="Dynamic" ValidationGroup="PA">
                    </asp:RequiredFieldValidator>
                </div>

                <span class="label">Nombre</span>
                <asp:TextBox ID="NombreTextBox" runat="server" CssClass="bigField"></asp:TextBox>
                <div class="validation">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="NombreTextBox"
                        ErrorMessage="Debe ingresar el Nombre"
                        Display="Dynamic" ValidationGroup="PA">
                    </asp:RequiredFieldValidator>
                </div>

                <span class="label">Proveedor</span>
                <asp:DropDownList ID="ProveedorComboBox" runat="server">
                </asp:DropDownList>
                <div class="validators">
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="ProveedorComboBox"
                        ErrorMessage="Se debe seleccionar un Proveedor"
                        ValidationGroup="PA"
                        Display="Dynamic" />
                </div>

                <span class="label">Direccion</span>
                <asp:TextBox ID="DireccionTextBox" runat="server" CssClass="bigField"></asp:TextBox>
                <div class="validation">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="DireccionTextBox"
                        ErrorMessage="Debe ingresar la Direccion"
                        Display="Dynamic" ValidationGroup="PA">
                    </asp:RequiredFieldValidator>
                </div>
                <span class="label">Horarios</span>
                <asp:DropDownList ID="ClientesComboBox" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ClientesComboBox_SelectedIndexChanged">
                </asp:DropDownList>
                <div>
                <table id="tblHorarios" class="">
                    <thead class="">
                        <tr>
                          <th></th>
                          <th>Cliente</th>
                          <th>Hora Inicio</th>                            
                          <th>Hora Fin</th>
                        </tr>                        
                    </thead>
                    <tbody>
                        <asp:PlaceHolder ID="DataPlaceHolder" runat="server"></asp:PlaceHolder>
                    </tbody>
                </table>
                    <div>
                        <input type="button" id="AddSchedule" value="Añadir Horario"/>
                        <!--<input type="button" id="DelSchedule" value ="Eliminar Horario" />-->
                    </div>
                </div>                                                
                <div class="buttonsPanel">
                    <asp:LinkButton ID="SaveButton" runat="server" ValidationGroup="PA" CssClass="button"
                        OnClick="SaveButton_Click">
                        <span>Guardar y Volver al Listado</span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="SaveAndContinueButton" runat="server" ValidationGroup="PA" CssClass="button"
                        OnClick="SaveAndContinueButton_Click">
                        <span>Guardar</span>
                    </asp:LinkButton>
                    <asp:HyperLink runat="server" NavigateUrl="~/Desgravamen/MedicoDesgravamenLista.aspx" CssClass="secondaryButton"
                        Text="Cancelar">
                    </asp:HyperLink>
                    <asp:HiddenField ID="MedicoDesgravamenIdHiddenField" runat="server" Value="0" />
                    <asp:HiddenField ID="UserIDHF" runat="server" Value=""/>
                    <asp:HiddenField ID="UserNameHF" runat="server" />
                </div>
                <div id="AddDialog" title="Adicion de Horarios">
                    <p>El formato de ingreso es de 24Hrs (14:00)</p><br />
                  <span>Hora Inicio</span><br />
                    <asp:DropDownList class="HorarioInput" ID="HoraInicioText" title="Hora Inicio" runat="server">
                        <asp:ListItem Text="07:00 am" Value="07:00"></asp:ListItem>
                        <asp:ListItem Text="07:30 am" Value="07:30"></asp:ListItem>
                        <asp:ListItem Text="08:00 am" Value="08:00"></asp:ListItem>
                        <asp:ListItem Text="08:30 am" Value="08:30"></asp:ListItem>
                        <asp:ListItem Text="09:00 am" Value="09:00"></asp:ListItem>
                        <asp:ListItem Text="09:30 am" Value="09:30"></asp:ListItem>
                        <asp:ListItem Text="10:00 am" Value="10:00"></asp:ListItem>
                        <asp:ListItem Text="10:30 am" Value="10:30"></asp:ListItem>
                        <asp:ListItem Text="11:00 am" Value="11:00"></asp:ListItem>
                        <asp:ListItem Text="11:30 am" Value="11:30"></asp:ListItem>
                        <asp:ListItem Text="12:00 am" Value="12:00"></asp:ListItem>
                        <asp:ListItem Text="12:30 am" Value="12:30"></asp:ListItem>
                        <asp:ListItem Text="01:00 pm" Value="13:00"></asp:ListItem>
                        <asp:ListItem Text="01:30 pm" Value="13:30"></asp:ListItem>
                        <asp:ListItem Text="02:00 pm" Value="14:00"></asp:ListItem>
                        <asp:ListItem Text="02:30 pm" Value="14:30"></asp:ListItem>
                        <asp:ListItem Text="03:00 pm" Value="15:00"></asp:ListItem>
                        <asp:ListItem Text="03:30 pm" Value="15:30"></asp:ListItem>
                        <asp:ListItem Text="04:00 pm" Value="16:00"></asp:ListItem>
                        <asp:ListItem Text="04:30 pm" Value="16:30"></asp:ListItem>
                        <asp:ListItem Text="05:00 pm" Value="17:00"></asp:ListItem>
                        <asp:ListItem Text="05:30 pm" Value="17:30"></asp:ListItem>
                        <asp:ListItem Text="06:00 pm" Value="18:00"></asp:ListItem>
                        <asp:ListItem Text="06:30 pm" Value="18:30"></asp:ListItem>
                        <asp:ListItem Text="07:00 pm" Value="19:00"></asp:ListItem>
                        <asp:ListItem Text="07:30 pm" Value="19:30"></asp:ListItem>
                        <asp:ListItem Text="08:00 pm" Value="20:00"></asp:ListItem>
                        <asp:ListItem Text="08:30 pm" Value="20:30"></asp:ListItem>
                        <asp:ListItem Text="09:00 pm" Value="21:00"></asp:ListItem>
                        <asp:ListItem Text="09:30 pm" Value="21:30"></asp:ListItem>
                        <asp:ListItem Text="10:00 pm" Value="22:00"></asp:ListItem>                        
                        <asp:ListItem Text="10:30 pm" Value="22:30"></asp:ListItem>
                        <asp:ListItem Text="11:00 pm" Value="23:00"></asp:ListItem>
                        <asp:ListItem Text="11:30 pm" Value="23:30"></asp:ListItem>
                    </asp:DropDownList><br />
                   
                   <%--  <input class="HorarioInput" id="HoraInicioText" title="Hora Inicio" value="" runat="server" pattern="\d{1,2}:\d{2}([ap]m)?" /><br />                   --%>
                     
                    <div class="validation">
                        <asp:RequiredFieldValidator ID="HoraInicioValidator" runat="server" ControlToValidate="HoraInicioText"
                            ErrorMessage="Debe ingresar la Hora de Inicio"
                            Display="Dynamic" ValidationGroup="AddHorario">
                        </asp:RequiredFieldValidator>
                   </div>
                  <span>Hora Fin</span><br />
                   <%--<input class="HorarioInput" id="HoraFinText" title="Hora Fin" value="" runat="server" pattern="\d{1,2}:\d{2}([ap]m)?"  />--%>
                    <asp:DropDownList class="HorarioInput" ID="HoraFinText" title="Hora Fin" runat="server">
                        <asp:ListItem Text="07:00 am" Value="07:00"></asp:ListItem>
                        <asp:ListItem Text="07:30 am" Value="07:30"></asp:ListItem>
                        <asp:ListItem Text="08:00 am" Value="08:00"></asp:ListItem>
                        <asp:ListItem Text="08:30 am" Value="08:30"></asp:ListItem>
                        <asp:ListItem Text="09:00 am" Value="09:00"></asp:ListItem>
                        <asp:ListItem Text="09:30 am" Value="09:30"></asp:ListItem>
                        <asp:ListItem Text="10:00 am" Value="10:00"></asp:ListItem>
                        <asp:ListItem Text="10:30 am" Value="10:30"></asp:ListItem>
                        <asp:ListItem Text="11:00 am" Value="11:00"></asp:ListItem>
                        <asp:ListItem Text="11:30 am" Value="11:30"></asp:ListItem>
                        <asp:ListItem Text="12:00 am" Value="12:00"></asp:ListItem>
                        <asp:ListItem Text="12:30 am" Value="12:30"></asp:ListItem>
                        <asp:ListItem Text="01:00 pm" Value="13:00"></asp:ListItem>
                        <asp:ListItem Text="01:30 pm" Value="13:30"></asp:ListItem>
                        <asp:ListItem Text="01:00 pm" Value="14:00"></asp:ListItem>
                        <asp:ListItem Text="02:30 pm" Value="14:30"></asp:ListItem>
                        <asp:ListItem Text="03:00 pm" Value="15:00"></asp:ListItem>
                        <asp:ListItem Text="03:30 pm" Value="15:30"></asp:ListItem>
                        <asp:ListItem Text="04:00 pm" Value="16:00"></asp:ListItem>
                        <asp:ListItem Text="04:30 pm" Value="16:30"></asp:ListItem>
                        <asp:ListItem Text="05:00 pm" Value="17:00"></asp:ListItem>
                        <asp:ListItem Text="05:30 pm" Value="17:30"></asp:ListItem>
                        <asp:ListItem Text="06:00 pm" Value="18:00"></asp:ListItem>
                        <asp:ListItem Text="06:30 pm" Value="18:30"></asp:ListItem>
                        <asp:ListItem Text="07:00 pm" Value="19:00"></asp:ListItem>
                        <asp:ListItem Text="07:30 pm" Value="19:30"></asp:ListItem>
                        <asp:ListItem Text="08:00 pm" Value="20:00"></asp:ListItem>
                        <asp:ListItem Text="08:30 pm" Value="20:30"></asp:ListItem>
                        <asp:ListItem Text="09:00 pm" Value="21:00"></asp:ListItem>
                        <asp:ListItem Text="09:30 pm" Value="21:30"></asp:ListItem>
                        <asp:ListItem Text="10:00 pm" Value="22:00"></asp:ListItem>                        
                        <asp:ListItem Text="10:30 pm" Value="22:30"></asp:ListItem>
                        <asp:ListItem Text="11:00 pm" Value="23:00"></asp:ListItem>
                        <asp:ListItem Text="11:30 pm" Value="23:30"></asp:ListItem>
                    </asp:DropDownList><br />
                   <div class="validation">
                        <asp:RequiredFieldValidator ID="HoraFinValidator" runat="server" ControlToValidate="HoraFinText"
                            ErrorMessage="Debe ingresar la Hora Final"
                            Display="Dynamic" ValidationGroup="AddHorario">
                        </asp:RequiredFieldValidator>
                   </div>
                <p id="ErrorMessage"></p>
                </div>
                <div id="ConfirmDelete" title="Confirmacion de Eliminación">

                </div>
            </div>
        </div>
    </div>
<script type="text/javascript">
    $(document).ready(function () {        
        $('#AddDialog').hide();
        $('#ConfirmDelete').hide();
        $('.inputDelete').click(function () {
            var index = $(this).parent().parent().index();
            if (index == null) {
                alert("No se escogió una fila correctamente");
            } else {
                if (confirm("Desea Eliminar del registro?")) {
                    var MedicoHorarioJSON = {
                        horariosId: $('#medicoHorarioId' + index).val()
                    };
                    $.ajax({
                        type: "POST",
                        url: "MedicoDesgravamenDetalle.aspx/EliminarHorarios",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify(MedicoHorarioJSON),
                        dataType: "json",
                        success: function (result) {
                            loadHorarios();
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            //alert(xhr.status);
                            //alert(thrownError);
                        }
                    });
                }

            }

            return false;
        });
        
        /*
        $("#ClientesComboBox.ClientID").change(function () {
            loadHorarios();
        });*/
        //validations on inputs for Horario Dialog
        $(".HorarioInput").blur(function (e) {
            var TextInput = $(e.target);
            var IsValid = checkTime(TextInput.val(), TextInput);
            var TextInputValue = $.trim($(TextInput).val());
            if (!IsValid && TextInputValue.length > 0) {
                $('#ErrorMessage').text("Se debe Ingresar una hora correcta, por ejemplo: 00:00 - 23:59");
                TextInput.focus();
            }
        });
        $('#AddSchedule').click(function () {
            $("#ErrorMessage").val("");
            $("#AddDialog").dialog({
                resizable: false,
                height: 320,
                modal: true,
                buttons: {
                    "Insertar Horario": function () {
                        var isValid = addHorario();
                        if (isValid)
                            $(this).dialog("close");

                        $('.HorarioInput').val('');
                    },
                    Cancel: function () {
                        $('.HorarioInput').val('');
                        $(this).dialog("close");
                    }
                }
            });
        });


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

        function loadHorarios() {
            $.ajax({
                type: "POST",
                url: "MedicoDesgravamenDetalle.aspx/loadTable",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ 'clienteId': $("#<%= ClientesComboBox.ClientID %>").val(), 'medicoId': $("#<%= MedicoDesgravamenIdHiddenField.ClientID %>").val() }),
                dataType: "json",
                success: function (result) {
                    //alert("good");
                    var list = result.d;
                    var ObjTable = $('#tblHorarios');
                    //clears table contents
                    ObjTable.find('tbody:last').html("");
                    for (var i = 0; i < list.length; i++) {
                        addHorarioRow('tblHorarios', list[i]);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    //alert(xhr.status);
                    //alert(thrownError);
                }
            });
        }

        function addHorario() {
            var ObjHoraInicio = $("#<%= HoraInicioText.ClientID %>");
            var ObjHoraFin = $("#<%= HoraFinText.ClientID %>");
            var ObjError = $('#ErrorMessage');

            var horaInicio = ObjHoraInicio.val();
            var horaFin = ObjHoraFin.val();

            if ($("#<%= MedicoDesgravamenIdHiddenField.ClientID %>").val() == null || $("#<%= MedicoDesgravamenIdHiddenField.ClientID %>").val() == '0') {
                alert("Se debe presionar la opcion guardar para poder registrar horarios");
            } else {

                if (horaFin.trim().length > 0 && horaFin.trim().length > 0) {
                    var checkInicio = checkTime(horaInicio, ObjHoraInicio[0]);
                    var checkFin = checkTime(horaFin, ObjHoraFin[0]);

                    if (!checkInicio) {
                        ObjError.text("Se debe Ingresar una hora correcta, por ejemplo: 00:00 - 23:59");
                        ObjHoraInicio[0].focus();

                        return false;
                    } else if (!checkFin) {
                        ObjError.text("Se debe Ingresar una hora correcta, por ejemplo: 00:00 - 23:59");
                        ObjHoraInicio[0].focus();

                        return false;
                    } else {


                        var arrInicio = horaInicio.split(":");
                        var DateInicio = new Date('2016', '01' - 1, '01', arrInicio[0], arrInicio[1], '00');
                        var arrFin = horaFin.split(":");
                        var DateFin = new Date('2016', '01' - 1, '01', arrFin[0], arrFin[1], '00');

                        if (DateFin > DateInicio) {
                            ObjError.text("");
                            $.ajax({
                                type: "POST",
                                url: "MedicoDesgravamenDetalle.aspx/insertHorario",
                                contentType: "application/json; charset=utf-8",
                                data: JSON.stringify({
                                    'medicoId': $("#<%= MedicoDesgravamenIdHiddenField.ClientID %>").val(),
                                    'clienteId': $("#<%= ClientesComboBox.ClientID %>").val(),
                                    'horaInicio': ObjHoraInicio.val(),
                                    'horaFin': ObjHoraFin.val()
                                }),
                                dataType: "json",
                                success: function (result) {
                                    //alert("good");
                                    if (result.d == null) {
                                        alert("No se pudo ingresar el nuevo horario, favor verificar que no exista ningun cruce de horarios");
                                    } else {
                                        //var jsonResponse = JSON.parse(result.d);
                                        addHorarioRow('tblHorarios', result.d);
                                        $('.HorarioInput').val('');
                                    }
                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    //alert(xhr.status);
                                    //alert(thrownError);
                                    $('.HorarioInput').val('');
                                }
                            });

                            return true;
                        }
                        ObjError.text("La Hora inicial debe ser mayor que la final");
                        return false;
                    }

                } else {
                    ObjError.text("Se deben rellenar todos los campos");

                    if (horaFin.trim().length() < horaFin.trim().length()) {
                        ObjHoraFin[0].focus();
                    } else {
                        ObjHoraInicio[0].focus();
                    }

                    return false;
                }
            }

        }

        function checkTime(str, field) {
            var pattern = /^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$/i;
            return pattern.test(str);
        }

        function addHorarioRow(tableId, horarioJson) {
            var ObjTable = $('#' + tableId);
            //var ObjMedicoId = $('#MedicoDesgravamenIdHiddenField');

            var lastRowIndex = ObjTable.find('tbody:last').find('tr:last').index();
            var index = lastRowIndex + 1;
            var html = "<tr>";
            html += "<td>";
            html += "<input type='image' src='../Images/Neutral/delete.png' id='delRow" + index + "' onclick='eliminarFila(this)' class='inputDelete' />";
            html += "</td>";
            html += "<td>";
            html += "<input type='hidden' id='medicoHorarioId" + index + "' value='" + horarioJson.MedicoHorariosId + "' />";
            html += "<input type='hidden' id='medicoDesgravamenId" + index + "' value='" + horarioJson.MedicoDesgravamenId + "' />";
            html += "<input type='hidden' id='clienteId' value='" + index + "' />";
            html += "<span id=clienteNombre" + index + ">" + horarioJson.ClienteNombre + "</span>";
            html += "</td>";
            html += "<td>";
            html += "<span id=horaInicio" + index + ">" + horarioJson.HoraInicio + "</span>";
            html += "</td>";
            html += "<td>";
            html += "<span id=horaFin" + index + ">" + horarioJson.HoraFin + "</span";
            html += "</td>";

            $('#' + tableId).find('tbody:last').append(html);            
        }

    });
</script>
</asp:Content>

