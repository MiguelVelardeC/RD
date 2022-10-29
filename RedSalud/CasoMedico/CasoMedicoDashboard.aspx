<%@ Page Title="Caso Medico" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CasoMedicoDashboard.aspx.cs" Inherits="CasoMedico_CasoMedicoDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .LeftBorder{
            border-right: 1px dashed #828282!important;
        }
        .BottomBorder{
            border-bottom: 1px dashed #828282!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Tablero de Control de Casos Médicos</span>
            </div>
        </div>
    </div>
    <asp:Label ID="Label10" Text="PACIENTE [CODIGO ASEGURADO]" runat="server" 
        style="width: 145px;"
        CssClass="label left" />
    <telerik:RadComboBox ID="PacienteRadComboBox" runat="server" 
        ShowMoreResultsBox="true" EnableVirtualScrolling="true" 
        AutoPostBack="false" EnableLoadOnDemand="true" CssClass="bigField"
        OnClientItemsRequesting="ClientItemsRequesting">
        <WebServiceSettings Method="GetPacientes" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
    </telerik:RadComboBox>
    <div class="clear" style="margin-bottom:5px;"></div>
    <asp:Panel runat="server" ID="FiltrosPanel" class="oneColumn" Style="text-align: left;">
            Fecha Inicial
            <telerik:RadDatePicker ID="FechaIni" runat="server"></telerik:RadDatePicker>
            &nbsp;&nbsp;&nbsp;&nbsp;
            Fecha Final
            <telerik:RadDatePicker ID="FechaFin" runat="server"></telerik:RadDatePicker>
            &nbsp;&nbsp;&nbsp;&nbsp;
            Ciudad
            <telerik:RadComboBox ID="CiudadCombo" runat="server" CssClass="mediumField"
                DataValueField="CiudadId" DataTextField="Nombre"
                AutoPostBack="false">
            </telerik:RadComboBox>
            &nbsp;&nbsp;&nbsp;&nbsp;
            Cliente
            <telerik:RadComboBox ID="ClienteCombo" runat="server"
                DataValueField="ClienteId"
                DataTextField="NombreJuridico"
                AutoPostBack="false">
            </telerik:RadComboBox>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <div class="buttonsPanel RadComboBox" >
                <asp:LinkButton ID="SearchLB" runat="server"
                    CssClass="button"
                    ValidationGroup="Internacion"
                    OnClick="SearchLB_Click">
                    <asp:Label Text="Recargar" runat="server" />
                </asp:LinkButton>
            </div>
        </asp:Panel>
        <div>
            <div>     
                <div class="columnHead">
                    <asp:Label ID="Label1" Text="TOTALES DE GASTOS MÉDICOS" runat="server" CssClass="title" />
                </div>

                <telerik:RadGrid ID="TotalesConsultasRadGrid" runat="server"
                    AutoGenerateColumns="false"
                    AllowPaging="false"
                    AllowMultiRowSelection="False"
                    OnItemDataBound="TotalesConsultasRadGrid_ItemDataBound">
                    <MasterTableView DataKeyNames="Descripcion" ShowFooter="true" ExpandCollapseColumn-Display="false"
                        CssClass="PorcentageContainer">
                        <NoRecordsTemplate>
                            <div style="text-align: center;">No hay totales a mostrar.</div>
                        </NoRecordsTemplate>
                        <AlternatingItemStyle BackColor="#E8F1FF" />
                        <ItemStyle BackColor="#C9DFFC" />
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="Descripcion" DataField="Descripcion"
                                HeaderText="DESCRIPCIÓN" Visible="true" FooterText="Total" />
                            <telerik:GridBoundColumn UniqueName="Numero" DataField="Numero"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-CssClass="LeftBorder" FooterStyle-CssClass="LeftBorder" HeaderStyle-CssClass="LeftBorder"
                                HeaderText="CANTIDAD" Visible="true" Aggregate="Sum" DataFormatString="{0:#,##0}" />
                            <telerik:GridBoundColumn UniqueName="TotalRecetas" DataField="TotalRecetas" DataFormatString="{0:#,##0.00}"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                HeaderText="RCTAS" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0.00}"
                                ItemStyle-CssClass="RPorMonto" FooterStyle-CssClass="RPorTotal"  />
                            <telerik:GridTemplateColumn HeaderText="%" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="RPorResultado"></telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="TotalDerivacion" DataField="TotalDerivacion" DataFormatString="{0:#,##0.00}"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                HeaderText="DERIV" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0.00}"
                                ItemStyle-CssClass="DPorMonto" FooterStyle-CssClass="DPorTotal" />
                            <telerik:GridTemplateColumn HeaderText="%" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="DPorResultado"></telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="TotalEstudio" DataField="TotalEstudio" DataFormatString="{0:#,##0.00}"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                HeaderText="LAB" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0.00}"
                                ItemStyle-CssClass="LPorMonto" FooterStyle-CssClass="LPorTotal" />
                            <telerik:GridTemplateColumn HeaderText="%" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="LPorResultado"></telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="TotalImagenologia" DataField="TotalImagenologia" DataFormatString="{0:#,##0.00}"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                HeaderText="IMG" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0.00}"
                                ItemStyle-CssClass="MPorMonto" FooterStyle-CssClass="MPorTotal" />
                            <telerik:GridTemplateColumn HeaderText="%" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="MPorResultado"></telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="TotalInternacion" DataField="TotalInternacion" DataFormatString="{0:#,##0.00}"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                HeaderText="INTER" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0.00}"
                                ItemStyle-CssClass="IPorMonto" FooterStyle-CssClass="IPorTotal" />
                            <telerik:GridTemplateColumn HeaderText="%" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="IPorResultado"></telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="TotalEmergencia" DataField="TotalEmergencia" DataFormatString="{0:#,##0.00}"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                HeaderText="EMERG" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0.00}" />
                            <telerik:GridBoundColumn UniqueName="TotalOdontologia" DataField="TotalOdontologia" DataFormatString="{0:#,##0.00}"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                HeaderText="ODONT" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0.00}" />
                            <telerik:GridBoundColumn UniqueName="Total" DataField="Total" DataFormatString="{0:#,##0.00}"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                HeaderText="TOTAL" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0.00}" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div>
                <div>     
                    <div class="columnHead">
                        <asp:Label Text="TOTALES DE CASOS MÉDICOS" runat="server" CssClass="title" />
                    </div>

                    <telerik:RadGrid ID="ConteoConsultasRadGrid" runat="server"
                        AutoGenerateColumns="false"
                        AllowPaging="false"
                        AllowMultiRowSelection="False">
                        <MasterTableView DataKeyNames="Nombre" ShowFooter="true" ExpandCollapseColumn-Display="false"
                            CssClass="PorcentageContainer">
                            <NoRecordsTemplate>
                                <div style="text-align: center;">No hay totales a mostrar.</div>
                            </NoRecordsTemplate>
                            <AlternatingItemStyle BackColor="#E8F1FF" />
                            <ItemStyle BackColor="#C9DFFC" />
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="Nombre" DataField="Nombre"
                                    HeaderText="Nombre" Visible="true" FooterText="Total" />
                                <telerik:GridBoundColumn UniqueName="Casos" DataField="Casos"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-CssClass="LeftBorder" FooterStyle-CssClass="LeftBorder" HeaderStyle-CssClass="LeftBorder"
                                    HeaderText="CANTIDAD" Visible="true" Aggregate="Sum" DataFormatString="{0:#,##0}" />
                                <telerik:GridBoundColumn UniqueName="TotalRecetas" DataField="TotalRecetas" DataFormatString="{0:#,##0}"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                    HeaderText="RCTAS" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    ItemStyle-CssClass="RPorMonto" FooterStyle-CssClass="RPorTotal"  />
                                <telerik:GridTemplateColumn HeaderText="%" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="RPorResultado"></telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="TotalDerivaciones" DataField="TotalDerivaciones" DataFormatString="{0:#,##0}"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                    HeaderText="DERIV" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    ItemStyle-CssClass="DPorMonto" FooterStyle-CssClass="DPorTotal" />
                                <telerik:GridTemplateColumn HeaderText="%" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="DPorResultado"></telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="TotalEstudios" DataField="TotalEstudios" DataFormatString="{0:#,##0}"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                    HeaderText="LAB" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    ItemStyle-CssClass="LPorMonto" FooterStyle-CssClass="LPorTotal" />
                                <telerik:GridTemplateColumn HeaderText="%" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="LPorResultado"></telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="TotalImagenologia" DataField="TotalImagenologia" DataFormatString="{0:#,##0}"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                    HeaderText="IMG" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    ItemStyle-CssClass="MPorMonto" FooterStyle-CssClass="MPorTotal" />
                                <telerik:GridTemplateColumn HeaderText="%" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="MPorResultado"></telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="TotalInternaciones" DataField="TotalInternaciones" DataFormatString="{0:#,##0}"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                    HeaderText="INTER" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    ItemStyle-CssClass="IPorMonto" FooterStyle-CssClass="IPorTotal" />
                                <telerik:GridTemplateColumn HeaderText="%" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="IPorResultado"></telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="TotalEnfermeria" DataField="TotalEnfermeria" DataFormatString="{0:#,##0}"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                    HeaderText="ENF" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    ItemStyle-CssClass="FPorMonto" FooterStyle-CssClass="FPorTotal" />
                                <telerik:GridTemplateColumn HeaderText="%" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="FPorResultado"></telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="TotalOdontologia" DataField="TotalOdontologia" DataFormatString="{0:#,##0}"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                    HeaderText="ODONT" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    ItemStyle-CssClass="OPorMonto" FooterStyle-CssClass="OPorTotal" />
                                <telerik:GridTemplateColumn HeaderText="%" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="OPorResultado"></telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div>
                    <div class="columnHead">
                        <asp:Label ID="Label3" Text="Totales Asegurados" runat="server" CssClass="title" />
                    </div>

                    <telerik:RadGrid ID="TotalesPacientesRadGrid" runat="server"
                        AutoGenerateColumns="false"
                        AllowPaging="false"
                        AllowMultiRowSelection="False"
                        OnItemDataBound="TotalesPacientesRadGrid_ItemDataBound">
                        <MasterTableView DataKeyNames="ID" ExpandCollapseColumn-Display="false">
                            <NoRecordsTemplate>
                                <div style="text-align: center;">NO HAY TOTALES A MOSTRAR.</div>
                            </NoRecordsTemplate>
                            <AlternatingItemStyle BackColor="#E8F1FF" />
                            <ItemStyle BackColor="#C9DFFC" />
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="Descripcion" DataField="Descripcion"
                                    HeaderText="Descripción" Visible="true" />
                                <telerik:GridBoundColumn UniqueName="Numero" DataField="Numero"
                                    HeaderText="Cantidad" Visible="true" />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div class="clear" style="margin-bottom: 5px;"></div>
            </div>
        </div>
        <telerik:RadTabStrip ID="RadTabStrip1" runat="server"
            MultiPageID="ReportesPages" Visible="false">
            <Tabs>
                <telerik:RadTab>
                    <TabTemplate>
                        REPORTE MÉDICO
                    </TabTemplate>
                </telerik:RadTab>
                <telerik:RadTab>
                    <TabTemplate>
                        REPORTE ECONÓMICO
                    </TabTemplate>
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="ReportesPages" runat="server" SelectedIndex="0">
            <telerik:RadPageView runat="server" ID="ReportesPagesCantidades" Selected="true">
                        <!-- twoColumnLeft -->
                <div class="">
                    <div class="threeColsLeft">

                        <div class="columnHead">
                            <asp:Label ID="Label14" Text="TOTAL DE ESTUDIOS MEDICOS" runat="server" CssClass="title" />
                        </div>
                        <telerik:RadGrid ID="GastosEstudiosRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            AllowPaging="false"
                            ShowGroupPanel="false"
                            AllowMultiRowSelection="False">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>
                            <MasterTableView TableLayout="Auto" GroupsDefaultExpanded="false" GroupLoadMode="Client">
                                <GroupByExpressions>
                                    <telerik:GridGroupByExpression>
                                        <SelectFields>
                                            <telerik:GridGroupByField FieldAlias="Grupo" FieldName="Grupo" HeaderText="<b></b>" HeaderValueSeparator=""></telerik:GridGroupByField>
                                            <telerik:GridGroupByField FieldAlias="GrupoTotal" FieldName="GrupoTotal" HeaderText="Total" />
                                        </SelectFields>
                                        <GroupByFields>
                                            <telerik:GridGroupByField FieldName="GrupoTotal" SortOrder="Descending"></telerik:GridGroupByField>
                                            <telerik:GridGroupByField FieldName="Grupo"></telerik:GridGroupByField>
                                        </GroupByFields>
                                    </telerik:GridGroupByExpression>
                                </GroupByExpressions>
                                <NoRecordsTemplate>
                                    <div style="text-align: center;">NO HAY LABORATORIOS A MOSTRAR.</div>
                                </NoRecordsTemplate>
                                <AlternatingItemStyle BackColor="#E8F1FF" />
                                <ItemStyle BackColor="#C9DFFC" />
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="GrupoTotal" DataField="GrupoTotal"
                                        HeaderText="Total" Visible="false" />
                                    <telerik:GridBoundColumn UniqueName="Grupo" DataField="Grupo"
                                        HeaderText="Grupo" Visible="false" />
                                    <telerik:GridBoundColumn UniqueName="Estudio" DataField="Estudio"
                                        HeaderText="Tipo de Laboratorio" Visible="true" />
                                    <telerik:GridBoundColumn UniqueName="Cantidad" DataField="Cantidad"
                                        HeaderText="Cantidad" Visible="true"   />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>

                        <div class="columnHead">
                            <asp:Label ID="Label5" Text="Top 10: PATOLOGIAS MAS DIANOSTICADAS" runat="server" CssClass="title" />
                        </div>
                        <telerik:RadGrid ID="EnfermedadesXConsultasRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            AllowPaging="false"
                            AllowMultiRowSelection="False">
                            <MasterTableView>
                                <NoRecordsTemplate>
                                    <div style="text-align: center;">No hay totales a mostrar.</div>
                                </NoRecordsTemplate>
                                <AlternatingItemStyle BackColor="#E8F1FF" />
                                <ItemStyle BackColor="#C9DFFC" />
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="Enfermedad" DataField="Enfermedad"
                                        HeaderText="Enfermedad" Visible="true" />
                                    <telerik:GridBoundColumn UniqueName="Cantidad" DataField="Cantidad"
                                        HeaderText="Casos" Visible="true" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>

                        <div class="columnHead">
                            <asp:Label ID="Label6" Text="Top 10: TIPO DE MEDICAMENTOS MAS RECETADOS" runat="server" CssClass="title" />
                        </div>
                        <telerik:RadGrid ID="GastosFarmaciaRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            AllowPaging="false"
                            AllowMultiRowSelection="False">
                            <MasterTableView>
                                <NoRecordsTemplate>
                                    <div style="text-align: center;">No hay Laboratorios a mostrar.</div>
                                </NoRecordsTemplate>
                                <AlternatingItemStyle BackColor="#E8F1FF" />
                                <ItemStyle BackColor="#C9DFFC" />
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="Medicamento" DataField="Medicamento"
                                        HeaderText="Medicamento" Visible="true" />
                                    <telerik:GridTemplateColumn HeaderText="Presentación / Concentración"
                                        Visible="true">
                                        <ItemTemplate>
                                            <asp:Literal Text='<%#Eval("Presentacion") %>' runat="server" />
                                            <asp:Literal Text='<%#Eval("Concentracion") %>' runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="Cantidad" DataField="Cantidad"
                                        HeaderText="Cantidad" Visible="true" />
                                    <telerik:GridTemplateColumn UniqueName="TOTAL" HeaderText="Total" >
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Literal ID="Literal2" Text='<%# FormatMoney(Eval("TOTAL")) %>' runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        
                        <div class="columnHead">
                            <asp:Label Text="Top 20: Prestaciones Odontológicas" runat="server" CssClass="title" />
                        </div>
                        <telerik:RadGrid ID="PrestacionesRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            AllowPaging="false"
                            AllowMultiRowSelection="False">
                            <MasterTableView ShowFooter="true">
                                <NoRecordsTemplate>
                                    <div style="text-align: center;">No hay Prestaciones a mostrar.</div>
                                </NoRecordsTemplate>
                                <AlternatingItemStyle BackColor="#E8F1FF" />
                                <ItemStyle BackColor="#C9DFFC" />
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="Nombre" DataField="Nombre" FooterText="Totales"
                                        HeaderText="Prestación" />
                                    <telerik:GridBoundColumn UniqueName="Total" DataField="Total" Aggregate="Sum"
                                        HeaderText="Cantidad" DataFormatString="{0:#,##0}" />
                                    <telerik:GridBoundColumn UniqueName="Gasto" DataField="Gasto" Aggregate="Sum"
                                        HeaderText="Gasto Total" DataFormatString="{0:##,0.00}" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="columnHead">
                            <asp:Label Text="TOP 10 Enfermedades Cronicas" runat="server" CssClass="title" />
                        </div>

                        <telerik:RadGrid ID="EnfermedadesCronicasRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            AllowPaging="false"
                            AllowMultiRowSelection="False">
                            <MasterTableView>
                                <NoRecordsTemplate>
                                    <div style="text-align: center;">No hay Enfermedades Cronicas a mostrar.</div>
                                </NoRecordsTemplate>
                                <AlternatingItemStyle BackColor="#E8F1FF" />
                                <ItemStyle BackColor="#C9DFFC" />
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="Nombre" DataField="Nombre"
                                        HeaderText="NOMBRE" />
                                    <telerik:GridBoundColumn UniqueName="Total" DataField="Total"
                                        HeaderText="CANTIDAD" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>

                    <div class="threeColsCenter">
                        <div class="columnHead">
                            <asp:Label ID="Label2" Text="Top 20: PACIENTES CON  MAYOR USO DEL SERVICIO MEDICO" runat="server" CssClass="title" />
                        </div>

                        <telerik:RadGrid ID="ConsultasXPacientesRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            AllowPaging="false"
                            AllowMultiRowSelection="False"
                            OnItemCommand="ConsultasXPacientesRadGrid_ItemCommand">
                            <MasterTableView DataKeyNames="PacienteId">
                                <NoRecordsTemplate>
                                    <div style="text-align: center;">No hay Pacientes a mostrar.</div>
                                </NoRecordsTemplate>
                                <AlternatingItemStyle BackColor="#E8F1FF" />
                                <ItemStyle BackColor="#C9DFFC" />
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="TemplateColumnHistorial" HeaderText="Historial">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="HistorialImageButton" runat="server"
                                                ImageUrl="~/Images/Neutral/historialMedico.png"
                                                CommandArgument='<%# Eval("PacienteId") %>'
                                                Width="24px" CommandName="Historial"
                                                ToolTip="Historial del Paciente"></asp:ImageButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="NombrePaciente" DataField="Nombre"
                                        HeaderText="Nombre del Paciente" Visible="true" />
                                    <telerik:GridBoundColumn UniqueName="Consultas" DataField="CONSULTAS"
                                        HeaderText="Consultas" Visible="true" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        
                        <div class="columnHead">
                            <asp:Label ID="Label9" Text="Top 20: PACIENTES CON ENFERMERDADES CRONICAS CON MAS CONSULTAS" runat="server" CssClass="title" />
                        </div>

                        <telerik:RadGrid ID="ConsultasXPacientesCriticosRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            AllowPaging="false"
                            AllowMultiRowSelection="False"
                            OnItemCommand="ConsultasXPacientesRadGrid_ItemCommand">
                            <MasterTableView DataKeyNames="PacienteId">
                                <NoRecordsTemplate>
                                    <div style="text-align: center;">No hay Pacientes a mostrar.</div>
                                </NoRecordsTemplate>
                                <AlternatingItemStyle BackColor="#E8F1FF" />
                                <ItemStyle BackColor="#C9DFFC" />
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="TemplateColumnHistorial" HeaderText="Historial">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="HistorialImageButton" runat="server"
                                                ImageUrl="~/Images/Neutral/historialMedico.png"
                                                CommandArgument='<%# Eval("PacienteId") %>'
                                                Width="24px" CommandName="Historial"
                                                ToolTip="Historial del Paciente"></asp:ImageButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="NombrePaciente" DataField="Nombre"
                                        HeaderText="Nombre del Paciente" Visible="true" />
                                    <telerik:GridBoundColumn UniqueName="Consultas" DataField="CASOS"
                                        HeaderText="Consultas" Visible="true" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>

                    <div class="threeColsRight">
                        <div class="columnHead">
                            <asp:Label ID="Label7" Text="ATENCIONES MEDICAS POR MES" runat="server" CssClass="title" />
                        </div>
                        <telerik:RadGrid ID="CasosXMesRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            AllowPaging="false"
                            AllowMultiRowSelection="False"
                            ShowGroupPanel="false"
                            ShowFooter="true" >
                            <MasterTableView GroupsDefaultExpanded="false" ShowGroupFooter="true" GroupLoadMode="Client">
                                <GroupByExpressions>
                                    <telerik:GridGroupByExpression>
                                        <SelectFields>
                                            <telerik:GridGroupByField FieldAlias="Year" FieldName="Year" HeaderText="AÑO"></telerik:GridGroupByField>
                                            <telerik:GridGroupByField FieldAlias="MES" FieldName="Mes"></telerik:GridGroupByField>
                                        </SelectFields>
                                        <GroupByFields>
                                            <telerik:GridGroupByField FieldName="Year"></telerik:GridGroupByField>
                                            <telerik:GridGroupByField FieldName="MesId" ></telerik:GridGroupByField>
                                        </GroupByFields>
                                    </telerik:GridGroupByExpression>
                                </GroupByExpressions>
                                <NoRecordsTemplate>
                                    <div style="text-align: center;">No hay Laboratorios a mostrar.</div>
                                </NoRecordsTemplate>
                                <AlternatingItemStyle BackColor="#E8F1FF" />
                                <ItemStyle BackColor="#C9DFFC" />
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="Year" DataField="Year"
                                        Visible="false" />
                                    <telerik:GridBoundColumn UniqueName="MesId" DataField="MesId"
                                        Visible="false" />
                                    <telerik:GridBoundColumn UniqueName="Mes" Visible="false"
                                        DataField="Mes">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="WEEK" DataField="WEEK"
                                        HeaderText="Semana" Visible="true" FooterText="Totales" />
                                    <telerik:GridBoundColumn UniqueName="Cantidad" DataField="Cantidad" DataFormatString="{0:#,##0}"
                                        HeaderText="Cantidad" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}" />
                                    <telerik:GridBoundColumn UniqueName="GASTOS" DataField="GASTOS" DataFormatString="{0:#,##0.00}"
                                        HeaderText="Total" Visible="true" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0.00}" />
                                </Columns>
                            </MasterTableView>
                            <GroupingSettings RetainGroupFootersVisibility="true" />
                        </telerik:RadGrid>

                        <div class="columnHead">
                            <asp:Label ID="Label8" Text="Top 20: DERIVACIONES MAS FRECUENTES POR ESPECIALISTA" runat="server" CssClass="title" />
                        </div>
                        <telerik:RadGrid ID="EspecialistasXDerivacionesRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            AllowPaging="false"
                            AllowMultiRowSelection="False">
                            <MasterTableView>
                                <NoRecordsTemplate>
                                    <div style="text-align: center;">No hay Laboratorios a mostrar.</div>
                                </NoRecordsTemplate>
                                <AlternatingItemStyle BackColor="#E8F1FF" />
                                <ItemStyle BackColor="#C9DFFC" />
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="Nombres" DataField="Nombres"
                                        HeaderText="Especialista" Visible="true" />
                                    <telerik:GridBoundColumn UniqueName="Especialidad" DataField="Especialidad"
                                        HeaderText="Especialidad" Visible="true" />
                                    <telerik:GridBoundColumn UniqueName="Cantidad" DataField="Cantidad"
                                        HeaderText="Cantidad" Visible="true" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>

                        <div class="columnHead">
                            <asp:Label ID="Label4" Text="ATENCIONES POR MEDICO" runat="server" CssClass="title" />
                        </div>
                        <telerik:RadGrid ID="DoctoresXCasosRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            AllowPaging="false"
                            AllowMultiRowSelection="False">
                            <MasterTableView>
                                <NoRecordsTemplate>
                                    <div style="text-align: center;">No hay Laboratorios a mostrar.</div>
                                </NoRecordsTemplate>
                                <AlternatingItemStyle BackColor="#E8F1FF" />
                                <ItemStyle BackColor="#C9DFFC" />
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="Medico" DataField="Medico"
                                        HeaderText="Medico" Visible="true" />
                                    <telerik:GridBoundColumn UniqueName="Especialidad" DataField="Especialidad"
                                        HeaderText="Especialidad" Visible="true" />
                                    <telerik:GridBoundColumn UniqueName="Cantidad" DataField="Cantidad"
                                        HeaderText="Cantidad" Visible="true" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="ReportesPagesEconomico">
            </telerik:RadPageView>
        </telerik:RadMultiPage>
        <script type="text/javascript">
            function ClientItemsRequesting(sender, eventArgs) {
                var context = eventArgs.get_context();
                context["ClientId"] = $find("<%= ClienteCombo.ClientID %>").get_value();
                context["UseAsegurado"] = 1;
            }
            
            $(document).ready(function () {
                makePorcentages();
            });
            function makeColumn(col, key) {
                var Total = parseFloat($(col).find('td.' + key + 'PorTotal').html().replace(/\./g, '').replace(',', '.'));
                var Porcentage = 0;
                $(col).find('tr').each(function () {
                    if ($(this).children('td.' + key + 'PorMonto').length > 0) {
                        if (Total > 0) {
                            var monto = parseFloat($(this).children('td.' + key + 'PorMonto').html().replace(/\./g, '').replace(',', '.'));
                            var _porcentage = (monto * 100) / Total;
                            Porcentage += _porcentage;
                            $(this).children('td.' + key + 'PorResultado').html((_porcentage).toFixed(2).replace('.', ',') + '&nbsp;%');
                        } else {
                            $(this).children('td.' + key + 'PorResultado').html('0&nbsp;%');
                        }
                    }
                });
                $(col).find('td.' + key + 'PorTotal').next().html(Porcentage > 100 ? '100&nbsp;%' : Porcentage.toFixed(2) + '&nbsp;%');
            }
            function makeColumnEsp(col, key) {
                //var Total = parseFloat($(col).find('td.' + key + 'PorTotal').html().replace(/\./g, '').replace(',', '.'));
                var Porcentage = 0;
                $(col).find('tr').each(function () {
                    if ($(this).children('td.' + key + 'PorMonto').length > 0) {
                        var Total = parseInt($($(this).find('td').get(1)).html().replace(/\./g, ''));

                        if (Total > 0) {
                            var monto = parseFloat($(this).children('td.' + key + 'PorMonto').html().replace(/\./g, '').replace(',', '.'));
                            var _porcentage = (monto / Total) * 100;
                            Porcentage += _porcentage;
                            $(this).children('td.' + key + 'PorResultado').html((_porcentage).toFixed(2).replace('.', ',') + '&nbsp;%');
                        } else {
                            $(this).children('td.' + key + 'PorResultado').html('0&nbsp;%');
                        }
                    }
                });
                $(col).find('td.' + key + 'PorTotal').next().html(Porcentage.toFixed(2) + '&nbsp;%');
            }
            function makePorcentages() {
                $('#<%= TotalesConsultasRadGrid.ClientID%> .PorcentageContainer').each(function () {
                    makeColumn($(this), 'R');
                    makeColumn($(this), 'D');
                    makeColumn($(this), 'L');
                    makeColumn($(this), 'M');
                    makeColumn($(this), 'I');
                });
                $('#<%= ConteoConsultasRadGrid.ClientID%> .PorcentageContainer').each(function () {
                    makeColumnEsp($(this), 'R');
                    makeColumnEsp($(this), 'D');
                    makeColumnEsp($(this), 'L');
                    makeColumnEsp($(this), 'M');
                    makeColumnEsp($(this), 'I');
                    makeColumnEsp($(this), 'F');
                    makeColumnEsp($(this), 'O');
                });
                $('.PorcentageGroupContainer').each(function () {
                    var list = new Array();
                    var Totales = new Array();
                    var start = true;
                    $(this).find('tbody tr').each(function (index) {
                        var td = $(this).find('td').get(1);
                        if ($(td).length <= 0) return;
                        if (start) {
                            if ($(td).attr('colspan') > 0) {
                                list.push(index);
                                start = false;
                            }
                        } else {
                            if ($(td).html().replace('&nbsp;', '').trim() == '') {
                                var monto = $(this).children('td.PorMonto');
                                monto.addClass('PorTotal');
                                monto.removeClass('PorMonto');
                                Totales.push($(monto).html().replace(/\./g, '').replace(',', '.'));
                                $(this).children('td.PorResultado').removeClass('PorResultado');
                                list.push(index);
                                start = true;
                            }
                        }
                    });
                    list.reverse();
                    Totales.reverse();
                    while (list.length > 0) {
                        var startIndex = list.pop();
                        var endIndex = list.pop() + 1;
                        var Total = Totales.pop();
                        var Porcentage = 0.0;
                        $(this).find('tbody tr').slice(startIndex, endIndex).each(function () {
                            if ($(this).children('td.PorMonto').length > 0) {
                                var porcentage = 0;
                                if (Total > 0) {
                                    var monto = parseFloat($(this).children('td.PorMonto').html().replace(/\./g, '').replace(',', '.'));
                                    porcentage = ((monto * 100) / Total);
                                }
                                Porcentage += porcentage;
                                $(this).children('td.PorResultado').html(porcentage.toFixed(2).replace('.', ',') + ' %');
                            } else if ($(this).children('td.PorTotal').length > 0) {
                                $(this).children('td.PorTotal').next().html((Porcentage > 100 ? 100 : Porcentage).toFixed(2) + '  %');
                            }
                        });
                        var total = $(this).find('tfoot tr td.PorTotal').next();
                        if ($(total).length > 0) {
                            $(total).html((Porcentage > 100 ? 100 : Porcentage).toFixed(2) + '  %');
                        }
                    }
                });
            }
        </script>
</asp:Content>