<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SeleccionHoraLibre.aspx.cs" Inherits="Desgravamen_SeleccionHoraLibre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function SeleccionarHoraFecha() {
            var oWnd = $find('<%=SeleccionHoraWindow.ClientID%>');
            oWnd.load();
            oWnd.show();
        }

        function SeleccionHoraWindow_OnClientShow(sender, args) {

        }
    </script>
    <style>
        .selectorFecha {
            width:100%;
        }
        .selectorDia {
            float:left; margin-right:10px;
            padding:4px 5px 4px 5px;
            background-color: rgb(74,123,166);
            color: rgb(240,240,240);
        }
        .panelMsg {
            padding: 10px;
            background-color: rgb(238, 203, 92);
        }
        .panelInfoMsg {
            padding: 10px;
            background-color: rgb(171, 197, 226);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">

    <telerik:RadWindowManager ID="MyWindowManager" runat="server" DestroyOnClose="true"></telerik:RadWindowManager>

    <asp:HiddenField ID="CitaDesgravamenIdHiddenField" runat="server" />
    <asp:HiddenField ID="NecesitaExamenHiddenField" runat="server" />
    <asp:HiddenField ID="FechaHoraHiddenField" runat="server" />
    <asp:HiddenField ID="CiudadIdHiddenField" runat="server" />
    <asp:HiddenField ID="ClienteIdHiddenField" runat="server" />
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label id="titulo" runat="server" CssClass="title">Seleccione la fecha y hora de su(s) cita</asp:Label> 
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" NavigateUrl="~/Desgravamen/PropuestoAseguradoLista.aspx" Text="Volver a la lista de Propuestos Asegurado"></asp:HyperLink>
                </div>
                <h2>
                    Nro: <b><asp:Literal ID="CitaLabel" runat="server"></asp:Literal></b>
                </h2>
                <p>
                    Propuesto Asegurado: <b><asp:Literal ID="PALabel" runat="server"></asp:Literal></b>
                </p>
                <p>
                    Aquí puede elegir la fecha y hora de su cita. Puede elegir la cita para revisión médica y para todos los laboratorios
                    que requieran citas.
                </p>
                <p>
                    Si no necesita examen médico y la cita es solamente para el laboratorio basta con definir la fecha.
                </p>

                <asp:Panel ID="MensajesPanel" runat="server" Visible="false" CssClass="panelMsg">
                    <asp:Literal ID="mensajePanelLabel" runat="server" ></asp:Literal>
                </asp:Panel>

                <br />
    <asp:Panel ID="SeleccionCitaRevisionMedicaPanel" runat="server">
        <h2>Fecha/Hora de la revisión médica</h2>
        <div class="panelInfoMsg">
        Médico: <asp:Label ID="MedicoLabel" runat="server"></asp:Label>
        <br />
        Fecha/Hora: <asp:Label ID="FechaHoraCita" runat="server"></asp:Label>
        </div>
        <div style="clear:both"></div>
        <div class="buttonsPanel">
            <asp:LinkButton ID="CambiarCitaButton" runat="server" 
                Text="" 
                CssClass="button" 
                OnClick="CambiarCitaButton_Click">
                <span>Programar cita</span>
            </asp:LinkButton>
            
        </div>
        
    </asp:Panel>

    <br />
    <asp:Panel ID="SeleccionCitaLaboPanel" runat="server">
    <h2>Proveedores Médicos encargados de los estudios</h2>
        <telerik:RadGrid ID="SeleccionHoraGridView" runat="server"
            AutoGenerateColumns="false" DataSourceID="SeleccionHoraDataSource"
            AllowPaging="false" 
            OnItemCommand="SeleccionHoraGridView_ItemCommand"
            OnItemDataBound="SeleccionHoraGridView_ItemDataBound"
            AllowMultiRowSelection="False">
            <MasterTableView DataKeyNames="ProveedorMedicoId" ExpandCollapseColumn-Display="false"
                CommandItemDisplay="None"
                AllowSorting="false" 
                OverrideDataSourceControlSorting="true">
                <NoRecordsTemplate>
                    <div style="text-align: center;">No están bien configurados los proveedores médicos para los estudios seleccionados</div>
                </NoRecordsTemplate>
                <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                <HeaderStyle  Font-Size="11px" />
                        
                <Columns>
                    <telerik:GridTemplateColumn HeaderText="Ver" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="40" HeaderStyle-Width="40">
                        <ItemTemplate>
                            <asp:ImageButton ID="SelectFechaHoraButton" runat="server"
                                ToolTip="Elegir o Cambiar Cita"
                                CommandName='<%# Eval("ProveedorMedicoId") %>'
                                ImageUrl="~/Images/Neutral/search32.png" Width="18px" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn UniqueName="EstudioId" DataField="EstudioId" Display="false"
                        HeaderText="Estudio" />
                    <telerik:GridBoundColumn UniqueName="EstudiosRealizados" DataField="EstudiosRealizados"
                        HeaderText="Estudios" />
                    <telerik:GridBoundColumn UniqueName="FechaCitaForDisplay" DataField="FechaCitaForDisplay"
                        HeaderText="Fecha" />
                </Columns>
            
            </MasterTableView>
            <ClientSettings>
                <Scrolling AllowScroll="true" />
                <Resizing AllowColumnResize="true" />
            </ClientSettings>
            <ExportSettings ExportOnlyData="true" IgnorePaging="true" HideStructureColumns="false" FileName="PropuestosAsegurados">
                <Pdf FontType="Subset" PaperSize="Letter" />
                <Excel Format="Html" />
                <Csv ColumnDelimiter="Colon" RowDelimiter="NewLine" />
            </ExportSettings>
        </telerik:RadGrid>
        <asp:ObjectDataSource ID="SeleccionHoraDataSource" runat="server"
            TypeName="Artexacta.App.Desgravamen.BLL.ProgramacionCitaLaboBLL"
            SelectMethod="GetProgramacionCitaLabo"
            OnSelected="SeleccionHoraDataSource_Selected">
            <SelectParameters>
                <asp:ControlParameter ControlID="CitaDesgravamenIdHiddenField" PropertyName="Value" Name="citaDesgravamenId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </asp:Panel>

                <br />
                <div class="buttonsPanel">
                    <asp:LinkButton ID="btnFinalizar" Text="" runat="server" 
                        CssClass="button" OnClick="btnFinalizar_Click">
                        <span>Finalizar y Ver Orden de Servicio</span>
                    </asp:LinkButton>

                    <asp:LinkButton ID="EliminarCitaButton" runat="server"
                        Text="Eliminar"
                        CssClass="secondaryButton" 
                        OnClick="EliminarCitaButton_Click"></asp:LinkButton>
                </div>

            </div>
        </div>
    </div>
    
    <div id="PopupSeleccionHora" style="display: none;">
        <telerik:RadWindow ID="SeleccionHoraWindow" runat="server" Width="650px" Height="550px"
            Title="Seleccionar Hora" DestroyOnClose="true"
            Modal="true" VisibleStatusbar="false" CssClass="radWin" Behaviors="Close,Move"
            KeepInScreenBounds="true" OnClientShow="SeleccionHoraWindow_OnClientShow" OnLoad="SeleccionHoraWindow_Load">
            <ContentTemplate>

                <asp:HiddenField ID="ProveedorMedicoIdHiddenField" runat="server" />
                <asp:HiddenField ID="CitaParaProveedorHiddenField" runat="server" Value="True" />
                <asp:HiddenField ID="FechaHoraSeleccionadaHiddenField" runat="server" />
                <asp:HiddenField ID="SelectorSoloFechaHiddenField" runat="server" />
                <asp:HiddenField ID="EstudioSelected" runat="server" />
                <asp:Panel ID="SolamenteFechaPanel" runat="server" Visible="false">
                    <span class="label">Fecha de cta laboratorio</span>
                    <telerik:RadDatePicker ID="FechaLaboratorio" runat="server"></telerik:RadDatePicker>

                    <div class="validation">
                        <asp:RequiredFieldValidator ID="rqrFechaLabo" runat="server" 
                            ControlToValidate="FechaLaboratorio"
                            ErrorMessage="Debe seleccionar una fecha" 
                            ValidationGroup="Cita"
                            Display="Dynamic">
                        </asp:RequiredFieldValidator>

                    </div>
                </asp:Panel>

                <asp:Panel ID="FechaYHoraPanel" runat="server" Visible="true">

                    <asp:Repeater ID="FechaSeleccionRepeater" runat="server">
                        <HeaderTemplate>
                            <div class="selectorFecha">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div id='dia<%# Eval("DiaFecha") %>' class="selectorDia"><%# Eval("DiaFecha") %></div>
                        </ItemTemplate>
                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>

                    <div style="clear:both;"></div>

                    <asp:Repeater ID="FechaHoraRepeater" runat="server">
                        <HeaderTemplate>
                            <table class="grid">
                                <thead class="head">
                                    <th>
                                        S
                                    </th>
                                    <th>
                                        Hora
                                    </th>
                                    <th>
                                        Proveedor
                                    </th>
                                </thead>
                        </HeaderTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                        <ItemTemplate>
                            <tr class='row dia<%# Eval("DiaFecha") %>'>
                                <td>
                                    <input type="radio" name="selHoraFecha" id='<%# Eval("ID") %>' />
                                </td>
                                <td>
                                    <%# Eval("HorarioForDisplay") %>
                                </td>
                                <td style="text-align:left; text-transform: uppercase;">
                                    <%# Eval("NombreProveedor") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class='altRow dia<%# Eval("DiaFecha") %>'>
                                <td>
                                    <input type="radio" name="selHoraFecha" id='<%# Eval("ID") %>' />
                                </td>
                                <td>
                                    <%# Eval("HorarioForDisplay") %>
                                </td>
                                <td style="text-align:left; text-transform: uppercase;">
                                    <%# Eval("NombreProveedor") %>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </asp:Panel>

                <div style="clear:both;"></div>
                
                <div class="buttonsPanel">
                    <asp:LinkButton ID="GuardarCitaButton" runat="server" CssClass="button" 
                        ValidationGroup="Cita" CommandName="FechaHora"
                        OnClick="GuardarCitaButton_Click">
                        <span>Guardar Cita</span>
                    </asp:LinkButton>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
    </div>

<script type="text/javascript">

    $(document).ready(function () {

        $("[class*='dia']").hide();
        var idFirst = $('.selectorDia').first();

        if ($('.' + idFirst.attr("id")).length == 0) {
            idFirst.hide();
            idFirst = $('.selectorDia').first().next();
        }
        $('.' + idFirst.attr("id")).show();

        $('.selectorDia').click(function () {

            $("[class*='dia']").hide();
            $('.' + this.id).show();

            return false;
        });

        $('#<%= GuardarCitaButton.ClientID %>').click(function () {
            $('#<%= FechaHoraSeleccionadaHiddenField.ClientID %>').val($("input[name='selHoraFecha']:checked").attr('id'));
        });


        // Confirmación de eliminación
        $('<%= EliminarCitaButton.ClientID %>').click(function () {
            return confirm("Se eliminarán todas las citas de revisión médica y de los laboratorios. Esta seguro?");
        });
    });
</script>
    
</asp:Content>

