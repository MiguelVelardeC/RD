<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgendaMedico.aspx.cs" Inherits="Desgravamen_AgendaMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .Atendida {
            background-color: #84E86F !important;
        }
        .NoVino {
            background-color: #ECB8A4 !important;
        }
        .FaltaFicha {
            background-color: #ebcf74 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%= btnSearchAgendaMedico.ClientID %>").click(function () {
                var propuestoAsegurado = $find("<%= PropuestoAseguradoTextBox.ClientID %>").get_value();
                var areDatesIgnored = $("#<%= ConsiderDatesCheckBox.ClientID %>").is(":checked");

                if (propuestoAsegurado.length < 3 && areDatesIgnored) {
                    alert("Se debe escribir un nombre si no se especificara una fecha");
                    return false;
                }

                return true;
            });

            $("#<%= medicosComboBox.ClientID %>").change(function () {
                var selectedValue = $(this).val();
                var valueText = $("#<%= medicosComboBox.ClientID %> option:selected").text();

                $("#<%= MedicoIdHiddenField.ClientID %>").val(selectedValue);
                $("#<%= lblMedicoNombre.ClientID %>").text(valueText);
            });
            
            $(".DateControls").click(function () {
                var propuestoAsegurado = $find("<%= PropuestoAseguradoTextBox.ClientID %>").get_value();
                var areDatesIgnored = $("#<%= ConsiderDatesCheckBox.ClientID %>").is(":checked");

                if (propuestoAsegurado.length < 3 && areDatesIgnored) {
                    alert("Se debe escribir un nombre si no se especificara una fecha");
                    return false;
                }

                return true;
            });
        });
    </script>
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Agenda de Médico: </span>
                <asp:Label ID="lblMedicoNombre" runat="server" Style="text-transform:uppercase;"></asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    
                </div>

                <asp:Label ID="lblfechaSeleccion" runat="server" Text="Elegir Fecha" CssClass="label"></asp:Label>

                <asp:Button ID="cmdUnDiaAntes" CssClass="DateControls" runat="server" Text="&lt;&lt;" OnClick="cmdUnDiaAntes_Click" />
                <telerik:RadDatePicker ID="dtFechaSeleccion" runat="server"
                    OnSelectedDateChanged="dtFechaSeleccion_SelectedDateChanged"
                    AutoPostBack="true"></telerik:RadDatePicker>
                <asp:Button ID="cmdUnDiaDespues" CssClass="DateControls" runat="server" Text="&gt;&gt;" OnClick="cmdUnDiaDespues_Click" />
                <asp:Panel ID="panelAdmin" runat="server" Style="display:inline;" DefaultButton="botonSearch" >
                    <span>No Incluir Fecha: </span>
                    <asp:CheckBox ID="ConsiderDatesCheckBox" runat="server" />  
                    <span>MEDICOS: </span>
                    <asp:DropDownList ID="medicosComboBox" runat="server" 
                            AutoPostBack="false"
                            OnSelectedIndexChanged="medicosComboBox_SelectedIndexChanged"
                            Visible="false"
                            Width="200px"
                            Height="22px"
                            style="margin-left:3px">
                            <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                    </asp:DropDownList>                  
                    <telerik:RadTextBox ID="PropuestoAseguradoTextBox" runat="server" EmptyMessage="Nombre del Propuesto Asegurado" Width="230"></telerik:RadTextBox>
                    <asp:LinkButton ID="btnSearchAgendaMedico" runat="server" CssClass="button" OnClick="btnSearchAgendaMedico_Click" >
                        <span>BUSCAR</span>
                    </asp:LinkButton>  
                    
                    <asp:Button id="botonSearch" Text="" Style="display:none;" runat="server" OnClick="btnSearchAgendaMedico_Click" />
                </asp:Panel>
                

                <asp:Label ID="lblListaCitasDia" runat="server" CssClass="label" ></asp:Label>
                <telerik:RadGrid ID="gridCitas" runat="server"
                    AutoGenerateColumns="false" DataSourceID="AgendaMedicoDataSource"
                    AllowPaging="false" 
                    OnItemCommand="gridCitas_ItemCommand"
                    OnItemDataBound="gridCitas_ItemDataBound"
                    AllowMultiRowSelection="False">
                    <MasterTableView DataKeyNames="CitaDesgravamen" ExpandCollapseColumn-Display="false">
                        <NoRecordsTemplate>
                            <div style="text-align: center;">No hay citas programadas para este día</div>
                        </NoRecordsTemplate>
                        <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                        <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                        <HeaderStyle  Font-Size="11px" />
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Ver" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="VerPA" runat="server"
                                        ToolTip="Atender la cita"
                                        CommandName="VerPA"
                                        CommandArgument='<%# Eval("CitaDesgravamen") %>'
                                        ImageUrl="~/Images/Neutral/search32.png" Width="18px" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="CitaDesgravamen" DataField="CitaDesgravamen"
                                HeaderText="Id Cita" />
                            <telerik:GridBoundColumn UniqueName="Estado" DataField="Estado"
                                HeaderText="Estado" />
                            <telerik:GridBoundColumn UniqueName="FechaHoraCitaForDisplay" DataField="FechaHoraCitaForDisplay"
                                HeaderText="Fecha y Hora de la Cita" />
                            <telerik:GridTemplateColumn HeaderText="Propuesto Asegurado" 
                                HeaderStyle-HorizontalAlign="Center" 
                                ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="nombrePA" runat="server"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>                            
                            <telerik:GridBoundColumn UniqueName="NombreMedico" DataField="NombreMedico"
                                HeaderText="Medico" Display="false"/>                              
                            <telerik:GridBoundColumn UniqueName="FinancieraNombre" DataField="FinancieraNombre"
                                HeaderText="Financiera" />                                                        
                            <telerik:GridBoundColumn UniqueName="EjecutivoNombre" DataField="EjecutivoNombre"
                                HeaderText="Ejecutivo" />                                                        
                            <telerik:GridBoundColumn UniqueName="ClienteNombre" DataField="ClienteNombre"
                                HeaderText="Cliente" />
                        </Columns>
            
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="AgendaMedicoDataSource" runat="server"
                    TypeName="Artexacta.App.Desgravamen.BLL.PropuestoAseguradoBLL"
                    SelectMethod="GetProgramacionCitaByMedicoFecha"
                    OnSelected="AgendaMedicoDataSource_Selected">
                    <SelectParameters>
                        <asp:ControlParameter Name="medicoId" Type="Int32" PropertyName="Value" ControlID="MedicoIdHiddenField" />
                        <asp:ControlParameter Name="fecha" PropertyName="SelectedDate" ControlID="dtFechaSeleccion" Type="DateTime" />
                        <asp:ControlParameter Name="ignoreDate" PropertyName="Checked" ControlID="ConsiderDatesCheckBox" Type="Boolean" />
                        <asp:ControlParameter Name="propuestoAsegurado" PropertyName="Text" ControlID="PropuestoAseguradoTextBox" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="MedicoIdHiddenField" runat="server" Value="0" />
</asp:Content>

