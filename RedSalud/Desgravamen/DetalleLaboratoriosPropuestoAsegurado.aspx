<%@ Page Title="Estudios para el Propuesto Asegurado" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DetalleLaboratoriosPropuestoAsegurado.aspx.cs" Inherits="Desgravamen_DetalleLaboratoriosPropuestoAsegurado" %>

<%@ Register TagPrefix="RedSalud" TagName="FileManager" Src="~/UserControls/FileManager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .PanelCobro
        {
            float: right;
            width: 250px;
            padding: 10px;
            background-color: #FFFF99;
            border: 1px solid #FFFF00;
            margin-right: 200px;
            text-align: center;
            margin-top: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Estudios para el Propuesto Asegurado</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server"
                        NavigateUrl="~/Desgravamen/LaboratorioPropuestoAsegurado.aspx" Text="Volver">
                    </asp:HyperLink>

                    <asp:LinkButton ID="OrdenServicioBtn" runat="server" Text="Orden de Servicio"
                        OnClick="OrdenServicioBtn_Click" ></asp:LinkButton>

                    <asp:Panel ID="PanelCobro" runat="server" Visible="false" CssClass="PanelCobro">
                        <h2>
                            Tome en cuenta que los estudios registrados <u>deben</u> ser cobrados al Propuesto asegurado
                        </h2>
                    </asp:Panel>


                    <span class="label">Nombre</span>
                    <asp:TextBox ID="NombreTextBox" runat="server" CssClass="bigField" ReadOnly="true"></asp:TextBox>

                    <span class="label">Cédula de Identidad</span>
                    <asp:TextBox ID="CedulaTextBox" runat="server" CssClass="normalField" ReadOnly="true"></asp:TextBox>

                    <span class="label">Fecha de Nacimiento</span>
                    <asp:TextBox ID="FechaNacimientoTextBox" runat="server" CssClass="normalField" ReadOnly="true"></asp:TextBox>

                    <span class="label">Cobrar al Propuesto Asegurado</span>
                    <asp:TextBox ID="CobrarAseguradoTextBox" runat="server" CssClass="smalField" ReadOnly="true"></asp:TextBox>
                    
                    <br /><br />

                    <span class="label">Estudios Solicitados</span>
                    <div id="alertaEstudios" class="alert warning" style="display:none;">
                      <span class="closebtn">&times;</span>  
                      <strong>No olvide</strong> marcar como <strong>realizado</strong> todos los estudios que esta subiendo.
                    </div>
                    <asp:LinkButton ID="MarcarRealizadoButton" runat="server" OnClick="MarcarRealizadoButton_Click"
                        ValidationGroup="Marcar">
                        Marcar como realizado
                    </asp:LinkButton>
                    <asp:LinkButton ID="MarcarNoRealizadoButton" runat="server" OnClick="MarcarNoRealizadoButton_Click"
                        ValidationGroup="Marcar">
                        Marcar como  NO realizado
                    </asp:LinkButton>
                    <div class="validation">
                        <asp:CustomValidator ID="ctvValidarEstudiosAMarcar" runat="server" Display="Dynamic" ValidationGroup="Marcar"
                            ErrorMessage="Debe seleccionar al menos un Estudio" 
                            ClientValidationFunction="ValidarEstudiosAMarcar">
                        </asp:CustomValidator>
                    </div>
                    <telerik:RadGrid ID="EstudiosGridView" runat="server"
                        AutoGenerateColumns="false" DataSourceID="EstudiosDataSource"
                        AllowPaging="false"
                        AllowMultiRowSelection="true">
                        <MasterTableView DataKeyNames="EstudioId" ClientDataKeyNames="EstudioId" ExpandCollapseColumn-Display="false">
                            <NoRecordsTemplate>
                                <div style="text-align: center;">No hay estudios a realizar para esta Cita</div>
                            </NoRecordsTemplate>
                            <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                            <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                            <HeaderStyle  Font-Size="11px" />
                            <Columns>
                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" >
                                </telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn UniqueName="Estudio" DataField="NombreEstudio"
                                    HeaderText="Estudio" />
                                <telerik:GridBoundColumn UniqueName="Realizado" DataField="RealizadoForDisplay"
                                    HeaderText="Realizado" />
                            </Columns>            
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="true" ClientEvents-OnRowSelected="SelectRow" ClientEvents-OnRowDeselected="UnselectRow">
                            <Selecting AllowRowSelect="true"></Selecting>
                        </ClientSettings>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="EstudiosDataSource" runat="server"
                        TypeName="Artexacta.App.Desgravamen.BLL.EstudioBLL"
                        SelectMethod="GetEstudiosByCitaDesgravamenId"
                        OnSelected="EstudiosDataSource_Selected">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="CitaDesgravamenIdHiddenField" PropertyName="Value"
                                Name="citaDesgravamenId" Type="Int32" />
                            <asp:ControlParameter ControlID="ProveedorMedicoIdHiddenField" PropertyName="Value"
                                Name="proveedorMedicoId" Type="Int32" />
                            <asp:ControlParameter ControlID="EstudioIdCitaLaboHiddenField" PropertyName="Value"
                                Name="estudioId" Type="Int32" />                                 
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    
                    <span class="label">Subir Archivo para Estudio</span>

                    <RedSalud:FileManager ID="EstudiosFileManager" runat="server" ShowMode="Normal" ObjectName="CITADESGRAVAMEN"  />

                    <asp:HiddenField ID="ProveedorMedicoIdHiddenField" runat="server" Value="0" />
                    <asp:HiddenField ID="CitaDesgravamenIdHiddenField" runat="server" Value="0" />
                    <asp:HiddenField ID="EstudioIdCitaLaboHiddenField" runat="server" Value="0" />
                    <asp:HiddenField ID="EstudiosSeleccionadosHiddenField" runat="server" Value="[]" />
                    <script type="text/javascript">
                        function SelectRow(sender, eventArgs) {
                            var id = eventArgs.getDataKeyValue("EstudioId");
                            console.log("Selected: " + id);
                            var selected = JSON.parse($("#<%= EstudiosSeleccionadosHiddenField.ClientID %>").val());
                            if (selected.indexOf(id) < 0)
                                selected.push(id);
                            $("#<%= EstudiosSeleccionadosHiddenField.ClientID %>").val(JSON.stringify(selected));
                            mostrarAlertaEstudios();
                        }

                        function UnselectRow(sender, eventArgs) {
                            var id = eventArgs.getDataKeyValue("EstudioId");
                            console.log("Unelected: " + id);
                            var selected = JSON.parse($("#<%= EstudiosSeleccionadosHiddenField.ClientID %>").val());
                            var index = selected.indexOf(id);
                            if (index >= 0)
                                selected.splice(index, 1);
                            $("#<%= EstudiosSeleccionadosHiddenField.ClientID %>").val(JSON.stringify(selected));
                        }

                        function ValidarEstudiosAMarcar(sender, args) {
                            var selected = JSON.parse($("#<%= EstudiosSeleccionadosHiddenField.ClientID %>").val());
                            console.log(selected.length);
                            if (selected.length == 0)
                                args.IsValid = false;
                        }

                        function mostrarAlertaEstudios() {
                            var selected = $("#<%= EstudiosSeleccionadosHiddenField.ClientID %>").val();
                            var result = (selected == "[]") ? 0 : selected.split(",").length;
                            if (result == 1) {
                                $("#alertaEstudios").css("opacity", "1");
                                $("#alertaEstudios").css('display', 'block');
                            }
                        }
                    </script>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

