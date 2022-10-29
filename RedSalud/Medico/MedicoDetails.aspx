<%@ Page Title="Médico" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MedicoDetails.aspx.cs" Inherits="Medico_MedicoDetails" %>

<%@ Register Src="~/UserControls/FotoMedico.ascx" TagPrefix="RedSalud" TagName="FotoMedico" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <style>
        .TextoCheckBox {
            text-align:center;
        }
    </style>
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                    Text="Nuevo Medico">
                </asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Volver a la Lista de Medicos"
                        NavigateUrl="~/Medico/MedicoList.aspx">
                    </asp:HyperLink>
                </div>
                <div class="left" style="width: 30%;">
                    <asp:FormView ID="MedicoFV" runat="server" DataSourceID="MedicoODS" OnModeChanged="MedicoFV_ModeChanged">
                        <ItemTemplate>
                            <asp:Label Text="Nombre" runat="server" CssClass="label" />
                            <asp:Label ID="Nombre" Text='<%# Bind("Nombre") %>' runat="server" />

                            <asp:Label Text="Especialidad" runat="server" CssClass="label" />
                            <asp:Label ID="Especialidad" Text='<%# Bind("Especialidad") %>' runat="server" />

                            <asp:Label Text="Sedes" runat="server" CssClass="label" />
                            <asp:Label ID="Sedes" Text='<%# Bind("Sedes") %>' runat="server" />

                            <asp:Label Text="Colegio Médico" runat="server" CssClass="label" />
                            <asp:Label ID="ColegioMedico" Text='<%# Bind("ColegioMedico") %>' runat="server" />

                            <asp:Label Text="Estado" runat="server" CssClass="label" />
                            <asp:Label ID="Estado" Text='<%# Eval("Estado").ToString() == "ACTIVO" ? "Activo" : "Inactivo" %>' runat="server" />

                            <asp:Label Text="Observación" runat="server" CssClass="label" />
                            <asp:Label ID="Observacion" Text='<%# Bind("Observacion") %>' runat="server" />

                            <asp:Label Text="Ámbito" runat="server" CssClass="label" />
                            <asp:Label ID="IsExternal" Text='<%# Eval("IsExternal").ToString() == "False" ? "Interno": "Externo" %>' runat="server" />
                            
                            <asp:Label ID="IsCallCenterLabel" Text="Asignado a Call Center" runat="server" CssClass="label" />
                            <asp:Label ID="IsCallCenter" Text='<%# Eval("IsCallCenter").ToString() == "True" ? "Asignado": "No Asignado" %>' runat="server" />

                            <asp:Label Text="Permite video llamada" runat="server" CssClass="label" />
                            <asp:Label Text='<%# Eval("PermiteVideoLlamada").ToString() == "True" ? "Si": "No" %>' runat="server" />

                            <asp:Label Text="Fecha Actualización" runat="server" CssClass="label" />
                            <asp:Label ID="FechaActualizacion" Text='<%# Bind("FechaActualizacion") %>' runat="server" />
                            <RedSalud:FotoMedico runat="server" ID="FotoMedico" Editable="false" MedicoId='<%# Bind("MedicoId") %>' FotoId='<%# Bind("FotoId") %>'/>
                            <div class="buttonsPanel">
                                <asp:LinkButton runat="server"
                                    CommandName="Edit" CssClass="button">
                                    <asp:Label ID="Label4" text="Modificar" runat="server" />
                                </asp:LinkButton>
                                <asp:HyperLink NavigateUrl="~/Medico/MedicoList.aspx" runat="server"
                                    CssClass="secondaryButton" Text="Cancelar" />
                            </div>
                        </ItemTemplate>
                        <InsertItemTemplate>
                            <asp:Panel ID="InsertPanel" runat="server" DefaultButton="InsertMedicoLB">
                                <div>
                                    <asp:Label Text="Usuario" runat="server" CssClass="label" />
                                    <telerik:RadComboBox ID="UserRadComboBox" runat="server" 
                                        ShowMoreResultsBox="true" EnableVirtualScrolling="true" 
                                        AutoPostBack="false" EnableLoadOnDemand="true" CssClass="bigField"
                                        SelectedValue='<%# Bind("UserId") %>'
                                        OnDataBinding="InsertRadComboBox_DataBinding">
                                        <WebServiceSettings Method="GetUsuarios" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                    </telerik:RadComboBox>
                                    <div class="validation">
                                        <asp:RequiredFieldValidator ID="UserRFV" runat="server"
                                            Display="Dynamic"
                                            ControlToValidate="UserRadComboBox"
                                            ErrorMessage="Debe seleccionar un Usuario." />
                                    </div>

                                    <asp:Label Text="Especialidad" runat="server" CssClass="label" />
                                    <telerik:RadComboBox ID="EspecialidadRadComboBox" runat="server"
                                        ShowMoreResultsBox="true" EnableVirtualScrolling="true" 
                                        AutoPostBack="false" EnableLoadOnDemand="true" CssClass="bigField"
                                        SelectedValue='<%# Bind("EspecialidadId") %>'>
                                        <WebServiceSettings Method="GetEspecialidad" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                    </telerik:RadComboBox>
                                    <div class="validation">
                                        <asp:RequiredFieldValidator ID="EspecialidadRFV" runat="server"
                                            Display="Dynamic"
                                            ControlToValidate="EspecialidadRadComboBox"
                                            ErrorMessage="La Especialidad es requerida." />
                                    </div>

                                    <asp:Label Text="Sedes" runat="server" CssClass="label" />
                                    <asp:TextBox ID="SedesTextBox" runat="server"
                                        CssClass="normalField"
                                        Text='<%# Bind("Sedes") %>' />
                                    <div class="validation">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            Display="Dynamic"
                                            ControlToValidate="SedesTextBox"
                                            ErrorMessage="El Sedes es requerido." />
                                    </div>

                                    <asp:Label Text="Colegio Médico" runat="server" CssClass="label" />
                                    <asp:TextBox ID="TextBox1" runat="server"
                                        CssClass="normalField"
                                        Text='<%# Bind("ColegioMedico") %>' />
                                    <div class="validation">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                            Display="Dynamic"
                                            ControlToValidate="SedesTextBox"
                                            ErrorMessage="El Colegio Médico es requerido." />
                                    </div>
                                
                                    <asp:Label Text="Estado" runat="server" CssClass="label" />
                                    <telerik:RadComboBox ID="EstadoRadComboBox" runat="server"
                                        ClientIDMode="Static"
                                        EmptyMessage="Selecciones un estado"
                                        CssClass="normalField"
                                        SelectedValue='<%# Bind("Estado") %>'>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Activo" Value="ACTIVO" />
                                            <telerik:RadComboBoxItem Text="Inactivo" Value="INACTIVO" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <div class="validation">
                                        <asp:RequiredFieldValidator ID="CustomValidator2" runat="server"
                                            ErrorMessage="Debe seleccionar un estado"
                                            ControlToValidate="EstadoRadComboBox"
                                            Display="Dynamic" />
                                    </div>

                                    <asp:Label Text="Observación" runat="server" CssClass="label" />
                                    <asp:TextBox ID="DireccionTextBox" runat="server"
                                        CssClass="bigField" TextMode="MultiLine"
                                        Text='<%# Bind("Observacion") %>' />
                                    <div class="validation"></div>

                                    <asp:Label Text="Ámbito" runat="server" CssClass="label" />
                                    <telerik:RadComboBox ID="IsExternalCombo" runat="server"
                                        ClientIDMode="Static"
                                        EmptyMessage="Es interno o externo"
                                        CssClass="normalField"
                                        SelectedValue='<%# Bind("IsExternal") %>'>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Interno" Value="False" />
                                            <telerik:RadComboBoxItem Text="Externo" Value="True" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:Label ID="IsCallCenterLabel" Text="Asignado a Call Center" runat="server" CssClass="label" />
                                    <telerik:RadComboBox ID="IsCallCenterCombo" runat="server"
                                        ClientIDMode="Static"
                                        EmptyMessage="Sin Asignacion"
                                        CssClass="normalField"
                                        SelectedValue='<%# Bind("IsCallCenter") %>'>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="No Asignado" Value="False" />
                                            <telerik:RadComboBoxItem Text="Asignado" Value="True" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <div class="validation">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ErrorMessage="Debe seleccionar un estado"
                                            ControlToValidate="IsCallCenterCombo"
                                            Display="Dynamic" />
                                    </div>
                                </div>

                                <div class="buttonsPanel">
                                    <asp:LinkButton ID="InsertMedicoLB" runat="server"
                                        CssClass="button" CommandName="Insert">
                                        <asp:Label text="Guardar" runat="server" />
                                    </asp:LinkButton>
                                    <asp:HyperLink ID="ReturnHL" runat="server"
                                        Text="Retornar a la lista de Medicos"
                                        NavigateUrl="~/Medico/MedicoList.aspx"
                                        CssClass="secondaryButton" />
                                </div>
                            </asp:Panel>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:Panel ID="UpdatePanel" runat="server" DefaultButton="UpdateMedicoLB">
                                <asp:HiddenField ID="MedicoIDHiddenField" runat="server" Value='<%# Bind("MedicoId") %>' />
                                <div>
                                    <asp:Label Text="Usuario" runat="server" CssClass="label" />
                                    <telerik:RadComboBox ID="UserRadComboBox" runat="server" CssClass="bigField"
                                        ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                        AutoPostBack="false" EnableLoadOnDemand="true"
                                        SelectedValue='<%# Bind("UserId") %>' Text='<%# Eval("Nombre") %>'
                                        OnDataBinding="RadComboBox_DataBinding">
                                        <WebServiceSettings Method="GetUsuarios" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                    </telerik:RadComboBox>
                                    <div class="validation">
                                        <asp:RequiredFieldValidator ID="UserRFV" runat="server"
                                            Display="Dynamic" ValidationGroup="update"
                                            ControlToValidate="UserRadComboBox"
                                            ErrorMessage="Debe seleccionar un Usuario." />
                                    </div>

                                    <asp:Label Text="Especialidad" runat="server" CssClass="label" />
                                    <telerik:RadComboBox ID="EspecialidadRadComboBox" runat="server" CssClass="bigField"
                                        ShowMoreResultsBox="true" EnableVirtualScrolling="true" 
                                        AutoPostBack="false" EnableLoadOnDemand="true" 
                                        SelectedValue='<%# Bind("EspecialidadId") %>' Text='<%# Eval("Especialidad") %>'
                                        OnDataBinding="RadComboBox_DataBinding">
                                        <WebServiceSettings Method="GetEspecialidad" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                    </telerik:RadComboBox>
                                    <div class="validation">
                                        <asp:RequiredFieldValidator ID="EspecialidadRFV" runat="server"
                                            Display="Dynamic" ValidationGroup="update"
                                            ControlToValidate="EspecialidadRadComboBox"
                                            ErrorMessage="La Especialidad es requerida." />
                                    </div>

                                    <asp:Label ID="Label1" Text="Sedes" runat="server" CssClass="label" />
                                    <asp:TextBox ID="SedesTextBox" runat="server"
                                        CssClass="normalField"
                                        Text='<%# Bind("Sedes") %>' />
                                    <div class="validation">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            Display="Dynamic" ValidationGroup="update"
                                            ControlToValidate="SedesTextBox"
                                            ErrorMessage="El Sedes es requerido." />
                                    </div>

                                    <asp:Label Text="Colegio Médico" runat="server" CssClass="label" />
                                    <asp:TextBox ID="TextBox1" runat="server"
                                        CssClass="normalField"
                                        Text='<%# Bind("ColegioMedico") %>' />
                                    <div class="validation">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                            Display="Dynamic" ValidationGroup="update"
                                            ControlToValidate="SedesTextBox"
                                            ErrorMessage="El Colegio Médico es requerido." />
                                    </div>
                                
                                    <asp:Label Text="Estado" runat="server" CssClass="label" />
                                    <telerik:RadComboBox ID="EstadoRadComboBox" runat="server"
                                        ClientIDMode="Static" CssClass="normalField"
                                        EmptyMessage="Selecciones un Estado"
                                        SelectedValue='<%# Bind("Estado") %>'>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Activo" Value="ACTIVO" />
                                            <telerik:RadComboBoxItem Text="Inactivo" Value="INACTIVO" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <div class="validation">
                                        <asp:RequiredFieldValidator ID="CustomValidator2" runat="server"
                                            ErrorMessage="Debe seleccionar un estado"
                                            ControlToValidate="EstadoRadComboBox"
                                            Display="Dynamic" ValidationGroup="update" />
                                    </div>

                                    <asp:Label ID="Label3" Text="Observación" runat="server" CssClass="label" />
                                    <asp:TextBox ID="DireccionTextBox" runat="server"
                                        CssClass="bigField" TextMode="MultiLine"
                                        Text='<%# Bind("Observacion") %>' />
                                    <div class="validation"></div>

                                    
                                    <asp:Label ID="Label5" Text="Ámbito" runat="server" CssClass="label" />
                                    <telerik:RadComboBox ID="IsExternalCombo" runat="server"
                                        ClientIDMode="Static"
                                        EmptyMessage="Es interno o externo"
                                        CssClass="normalField"
                                        SelectedValue='<%# Bind("IsExternal") %>'>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Interno" Value="False" />
                                            <telerik:RadComboBoxItem Text="Externo" Value="True" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:Label ID="IsCallCenterLabel" Text="Asignado a Call Center" runat="server" CssClass="label" />
                                    <telerik:RadComboBox ID="IsCallCenterCombo" runat="server"
                                        ClientIDMode="Static"
                                        EmptyMessage="Sin Asignacion"
                                        CssClass="normalField"
                                        SelectedValue='<%# Bind("IsCallCenter") %>'>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="No Asignado" Value="False" />
                                            <telerik:RadComboBoxItem Text="Asignado" Value="True" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:Label Text="Permite video llamada" runat="server" CssClass="label" />
                                    <telerik:RadComboBox ID="PermiteVideoLLamadaRCB" runat="server"
                                        ClientIDMode="Static"
                                        CssClass="normalField"
                                        SelectedValue='<%# Bind("PermiteVideoLlamada") %>'>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="No" Value="False" />
                                            <telerik:RadComboBoxItem Text="Si" Value="True" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <RedSalud:FotoMedico runat="server" ID="FotoMedico" Editable="true" FotoId='<%# Bind("FotoId") %>' MedicoId='<%# Bind("MedicoId") %>' />
                                    <div class="validation">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ErrorMessage="Debe seleccionar un estado"
                                            ControlToValidate="IsExternalCombo"
                                            Display="Dynamic" />
                                    </div>
                                </div>
                                <div class="buttonsPanel">
                                    <asp:LinkButton ID="UpdateMedicoLB" runat="server"
                                        CssClass="button" ValidationGroup="update" CommandName="Update">
                                        <asp:Label ID="Label2" text="Guardar" runat="server" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="CancelUpdate" Text="Cancelar" runat="server"
                                        OnClick="CancelUpdate_Click" CssClass="secondaryButton" />
                                </div>
                            </asp:Panel>
                        </EditItemTemplate>
                    </asp:FormView>
                </div>
                <div class="left" style="width: 70%;">
                    <asp:Panel ID="MedicoClienteListPanel" runat="server" 
                        GroupingText="Clientes Asociados">
                        <asp:Label ID="ClienteLabel" Text="Cliente" runat="server" CssClass="label" />
                        <asp:DropDownList ID="ClienteDDL" runat="server"
                            DataSourceID="ClienteODS"
                            CssClass="bigField left"
                            DataValueField="ClienteId"
                            DataTextField="NombreJuridico"
                            style="margin-bottom: 10px;">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ClienteODS" runat="server"
                            TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                            OldValuesParameterFormatString="original_{0}"
                            SelectMethod="getAllRedClienteList"
                            OnSelected="ClienteODS_Selected" />

                        <asp:LinkButton ID="NewMedicoClienteLB" runat="server" 
                            CssClass="left" style="margin-left: 10px;"
                            OnClick="NewMedicoClienteLB_Click">
                            <span>Asociar Cliente</span>
                        </asp:LinkButton>
                        <div class="clear"></div>
                        <telerik:RadGrid ID="MedicoClienteRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            DataSourceID="MedicoClienteODS"
                            AllowPaging="true"
                            PageSize="5"
                            MasterTableView-DataKeyNames="ClienteId">
                            <MasterTableView>
                                <NoRecordsTemplate>
                                    <asp:Label runat="server" Text="Este medico no esta asignado a ningun Cliente"></asp:Label>
                                </NoRecordsTemplate>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="Eliminar" ItemStyle-Width="24px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="DeleteImageButton" runat="server"
                                                ImageUrl="~/Images/neutral/delete.png"
                                                OnCommand="MedicoClienteButton_Command"
                                                OnClientClick="return confirm('¿Está seguro que desea eliminar la Gestion Médica?');"
                                                CommandArgument='<%# Eval("ClienteId") %>'
                                                Width="24px" CommandName="Eliminar"
                                                ToolTip="Eliminar"></asp:ImageButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="CodigoCliente" HeaderText="Código del Cliente" />
                                    <telerik:GridBoundColumn DataField="NombreCliente" HeaderText="Nombre del Cliente" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>

                        <asp:ObjectDataSource ID="MedicoClienteODS" runat="server"
                            TypeName="Artexacta.App.Medico.BLL.MedicoBLL"
                            OldValuesParameterFormatString="original_{0}"
                            SelectMethod="getMedicoClienteByMedicoId"
                            OnSelected="MedicoClienteODS_Selected">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="MedicoIdHF" Name="MedicoId" PropertyName="Value" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </asp:Panel>

                    <%--panel definicion de horarios del medico--%>
                    <asp:Panel ID="MedicoHorarioPNL" runat="server" 
                        GroupingText="Horario de video llamadas">
                        <asp:Panel runat="server" ID="AgregarHorarioPNL">
                            <table style="width:100%;">
                            <tr>
                                <td colspan="4">
                                    <asp:Label Text="Dias" runat="server" CssClass="label"/>
                                    <asp:CheckBox runat="server" ID="chkLunes" Text="Lunes" TextAlign="Left" CssClass="TextoCheckBox"/>
                                    <asp:CheckBox runat="server" ID="chkMartes" Text="Martes" TextAlign="Left" CssClass="TextoCheckBox"/>
                                    <asp:CheckBox runat="server" ID="chkMiercoles" Text="Miércoles" TextAlign="Left" CssClass="TextoCheckBox"/>
                                    <asp:CheckBox runat="server" ID="chkJueves" Text="Jueves" TextAlign="Left" CssClass="TextoCheckBox"/>
                                    <asp:CheckBox runat="server" ID="chkViernes" Text="Viernes" TextAlign="Left" CssClass="TextoCheckBox"/>
                                    <asp:CheckBox runat="server" ID="chkSabado" Text="Sábado" TextAlign="Left" CssClass="TextoCheckBox"/>
                                    <asp:CheckBox runat="server" ID="chkDomingo" Text="Domingo" TextAlign="Left" CssClass="TextoCheckBox"/>
                                </td>
                                
                                <td>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    <asp:Label runat="server" CssClass="label">Hora Inicio</asp:Label>
                                    <asp:DropDownList runat="server" ID="HoraInicioHoraDDL" Width="50px"></asp:DropDownList>:
                                    <asp:DropDownList runat="server" ID="HoraInicioMinutoDDL" Width="50px"></asp:DropDownList>
                                </td>
                                <td style="width: 20%;">
                                    <asp:Label runat="server" CssClass="label">Hora Fin</asp:Label>
                                    <asp:DropDownList runat="server" ID="HoraFinHoraDDL" Width="50px"></asp:DropDownList>:
                                    <asp:DropDownList runat="server" ID="HoraFinMinutoDDL" Width="50px"></asp:DropDownList>
                                </td>
                                <td style="width: 20%; vertical-align:central;">
                                    <asp:LinkButton ID="lnkAgregarHorario" runat="server" 
                                        CssClass="left" style="margin-left: 10px;"
                                        OnClick="NewMedicoHorarioLB_Click">
                                        <span>Agregar</span>
                                    </asp:LinkButton>
                                </td>
                                <td style="width: 20%;"></td>
                                <td style="width: 20%;"></td>
                            </tr>
                        </table>
                        </asp:Panel>
                        
                        <div class="clear"></div>
                        <telerik:RadGrid ID="MedicoHorarioRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            DataSourceID="MedicoHorarioODS"
                            AllowPaging="true"
                            PageSize="5"
                            MasterTableView-DataKeyNames="horId">
                            <MasterTableView>
                                <NoRecordsTemplate>
                                    <asp:Label runat="server" Text="Este medico no esta asignado a ningun Cliente"></asp:Label>
                                </NoRecordsTemplate>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="Eliminar" ItemStyle-Width="24px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="DeleteImageButton" runat="server"
                                                ImageUrl="~/Images/neutral/delete.png"
                                                OnCommand="MedicoHorarioButton_Command"
                                                OnClientClick="return confirm('¿Está seguro que desea eliminar el horario?');"
                                                CommandArgument='<%# Eval("horId") %>'
                                                Width="24px" CommandName="Eliminar"
                                                ToolTip="Eliminar"></asp:ImageButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Dia" HeaderText="Día" />
                                    <telerik:GridBoundColumn DataField="horaInicio" HeaderText="Horario de inicio" />
                                    <telerik:GridBoundColumn DataField="horaFin" HeaderText="Horario fin" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>

                        <asp:ObjectDataSource ID="MedicoHorarioODS" runat="server"
                            TypeName="Artexacta.App.Medico.BLL.MedicoBLL"
                            OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetMedicoHorario"
                            OnSelected="MedicoHorarioODS_Selected">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="MedicoIdHF" Name="MedicoId" PropertyName="Value" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </asp:Panel>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>

    <asp:ObjectDataSource ID="MedicoODS" runat="server"
        TypeName="Artexacta.App.Medico.BLL.MedicoBLL"
        DataObjectTypeName="Artexacta.App.Medico.Medico"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMedicoByMedicoId"
        InsertMethod="InsertMedico"
        UpdateMethod="UpdateMedico"
        OnSelected="MedicoODS_Selected"
        OnInserted="MedicoODS_Inserted"
        OnUpdated="MedicoODS_Updated">
        <SelectParameters>
            <asp:ControlParameter ControlID="MedicoIdHF" Name="MedicoId" PropertyName="value" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <script type="text/javascript">
        function ClienteCV_Validate(sender, args) {
            args.IsValid = true;

            var value = $find('ClienteDDL').get_value();

            if (value <= "") {
                args.IsValid = false;
            }
        }
        function EstadoCivilDDLCV_Validate(sender, args) {
            args.IsValid = true;

            var value = $find('EstadoCivilDDL').get_value();

            if (value == "") {
                args.IsValid = false;
            }
        }
    </script>
    <asp:HiddenField ID="MedicoIdHF" runat="server" />
    <asp:HiddenField ID="UserIDHF" runat="server" />
    <asp:HiddenField ID="UserNameHF" runat="server" />
</asp:Content>

