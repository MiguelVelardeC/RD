<%@ Page Title="Panel de Control de Desgravamen" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReportesDesgravamen.aspx.cs" Inherits="Desgravamen_ReportesDesgravamen" MaintainScrollPositionOnPostBack = "true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
.rgExpXLS
    {
        display:none;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Panel de Control de Desgravamen</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                        <div>
                        <asp:HyperLink ID="ListaPAButton" runat="server" Style="margin-right:20px;"
                            NavigateUrl="~/Desgravamen/PropuestoAseguradoLista.aspx">
                            Administrar Propuestos Asegurados
                        </asp:HyperLink>
                        <asp:HyperLink ID="LaboratorioDesgravamenButton" visible="false" runat="server" Style="margin-right:20px;"
                            NavigateUrl="~/Desgravamen/LaboratorioPropuestoAsegurado.aspx">
                            Administrar Laboratorio para Propuesto Asegurado
                        </asp:HyperLink>
                        <asp:HyperLink ID="ReporteEstudioXPAButton" runat="server" Style="margin-right:20px;"
                            NavigateUrl="~/Desgravamen/ReporteEstudioXPA.aspx">
                            Reporte de Estudios x Propuesto Asegurado
                        </asp:HyperLink>    
                                                         
                        <asp:HyperLink ID="ReporteEstudiosEstadosButton" runat="server"
                            NavigateUrl="~/Desgravamen/ReporteEstudiosEstados.aspx">
                            Reporte de Estados de Citas y Estudios
                        </asp:HyperLink> 
                             <br />
                            <asp:HyperLink ID="ReporteEstudioNew" runat="server" Style="margin-right:20px;"
                            NavigateUrl="~/Desgravamen/ReporteEstudioXPANew.aspx">
                            Reporte de Estudios x Propuesto Asegurado New
                        </asp:HyperLink>  
                        </div>
                        <div style="margin-top:10px;">                     
                        </div>                
                </div>
                <div>
                    
                    <span  class="label" style="display:none;">Gestión</span>
                    <telerik:RadComboBox ID="GestionCombo" runat="server" CssClass="smallField"
                        AutoPostBack="true" OnSelectedIndexChanged="GestionCombo_SelectedIndexChanged" Visible ="false">
                    </telerik:RadComboBox>
                                                        
                    <span class="label" style="display:inline;">Fecha Inicio</span> 
                    <telerik:RadDatePicker ID="DtInicialTop" runat="server" Width="100px"></telerik:RadDatePicker>                
                    <span class="label" style="display:inline;">Fecha Fin</span>
                    <telerik:RadDatePicker ID="DtFinalTop" runat="server" Width="100px"></telerik:RadDatePicker>
                    <span class="label" style="display:inline;">Ciudad</span>
                    <asp:DropDownList ID="ciudadComboBox" runat="server" 
                        AutoPostBack="false"
                        Width="150px">
                    <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <span class="label" style="display:inline;">Cliente</span>                    
                    <asp:DropDownList ID="clientesComboBox" runat="server" 
                            AutoPostBack="true"
                            OnSelectedIndexChanged="clientesComboBox_SelectedIndexChanged">
                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <span class="label" style="display:inline;">Financiera</span>
                    <asp:DropDownList ID="financieraTopComboBox" runat="server" 
                        AutoPostBack="false"
                        Width="177px">
                    <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                    </asp:DropDownList><br />
                    <asp:LinkButton ID="BtnTopReportes" runat="server" CssClass="button"
                    OnClick="BtnTopReportes_Click"
                    Style="margin-top: 7px; margin-bottom: 3px;">
                    <span>Generar Reporte</span>
                    </asp:LinkButton>
                </div>
                <br />
                <div class="float" style="width:570px; float:left">
                    <span class="label">Cantidad de Citas por Financiera por Ciudad</span>
                    <telerik:RadGrid ID="CantidadFiancieraCiudadGridView" runat="server"
                        AutoGenerateColumns="false" DataSourceID="CantidadFiancieraCiudadDataSource"
                        AllowPaging="false"
                        AllowMultiRowSelection="False"
                        ShowFooter="true"
                        OnColumnCreated="CantidadFiancieraCiudadGridView_ColumnCreated"
                        >
                        <MasterTableView ExpandCollapseColumn-Display="false">
                            <NoRecordsTemplate>
                                <div style="text-align: center;">No existen citas atendidas y aprobadas en este rango</div>
                            </NoRecordsTemplate>
                            <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                            <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                            <HeaderStyle  Font-Size="11px" />
                            <Columns>
                            
                                <telerik:GridBoundColumn UniqueName="Financiera" DataField="Financiera"
                                    HeaderText="Financiera" />
                                <telerik:GridBoundColumn UniqueName="Ciudad" DataField="Ciudad"
                                    HeaderText="Ciudad" />
                                <telerik:GridBoundColumn UniqueName="Cantidad" DataField="Cantidad"
                                    HeaderText="Cantidad" Aggregate="Sum" FooterText="TOTAL: " />
                            </Columns>
            
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="CantidadFiancieraCiudadDataSource" runat="server"
                        TypeName="Artexacta.App.Desgravamen.BLL.ReporteDesgravamenBLL"
                        SelectMethod="GetReporteCantidadCitasPorFinanceraCiudad"
                        OnSelected="CantidadFiancieraCiudadDataSource_Selected">
                        <SelectParameters>
                            <asp:ControlParameter Name="dtFechaInicial" PropertyName="SelectedDate" ControlID="DtInicialTop" Type="DateTime" />
                            <asp:ControlParameter Name="dtFechaFinal" PropertyName="SelectedDate" ControlID="DtFinalTop" Type="DateTime" />
                            <asp:ControlParameter ControlID="ciudadComboBox" PropertyName="SelectedValue" Type="String" Name="strCiudadId" />
                            <asp:ControlParameter ControlID="financieraTopComboBox" PropertyName="SelectedValue" Type="Int32" Name="intFinancieraId" />
                            <asp:ControlParameter ControlID="clientesComboBox" PropertyName="SelectedValue" Type="Int32" Name="intClienteId" />
                            <%-- <asp:ControlParameter ControlID="GestionCombo" PropertyName="SelectedValue" Type="Int32" Name="year" />--%>
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
                <div style="width: 570px;margin-left: 20px; float:left">
                    <span class="label">Cantidad de Citas por Médico</span>
                    <telerik:RadGrid ID="CantidadCitasPorMedicoGridView" runat="server"
                        AutoGenerateColumns="false" DataSourceID="CantidadCitasPorMedicoDataSource"
                        AllowPaging="false"
                        AllowMultiRowSelection="False"
                        ShowFooter="true">
                        <MasterTableView ExpandCollapseColumn-Display="false">
                            <NoRecordsTemplate>
                                <div style="text-align: center;">No hay propuestos asegurados registrados</div>
                            </NoRecordsTemplate>
                            <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                            <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                            <HeaderStyle  Font-Size="11px" />
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="Medico" DataField="Medico"
                                    HeaderText="Medico" />
                                <telerik:GridBoundColumn UniqueName="Cantidad" DataField="Cantidad"
                                    HeaderText="Cantidad"  Aggregate="Sum" FooterText="TOTAL: " />
                            </Columns>
            
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="CantidadCitasPorMedicoDataSource" runat="server"
                        TypeName="Artexacta.App.Desgravamen.BLL.ReporteDesgravamenBLL"
                        SelectMethod="GetReporteCantidadCitasPorMedico"
                        OnSelected="CantidadCitasPorMedicoDataSource_Selected">
                        <SelectParameters>
                            <asp:ControlParameter Name="dtFechaInicial" PropertyName="SelectedDate" ControlID="DtInicialTop" Type="DateTime" />
                            <asp:ControlParameter Name="dtFechaFinal" PropertyName="SelectedDate" ControlID="DtFinalTop" Type="DateTime" />
                            <asp:ControlParameter ControlID="ciudadComboBox" PropertyName="SelectedValue" Type="String" Name="strCiudadId" />
                            <asp:ControlParameter ControlID="financieraTopComboBox" PropertyName="SelectedValue" Type="Int32" Name="intFinancieraId" />
                            <asp:ControlParameter ControlID="clientesComboBox" PropertyName="SelectedValue" Type="Int32" Name="intClienteId" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
                <div style="clear:both"></div>
                <br />                                
                <div class="contentMenu">
                </div>
                <div style="width:100%; display:block;">
                    <div style="float:left;">
                        <span class="label" style="">Cobro al Cliente</span>
                        <asp:DropDownList ID="cobroClienteCombo" runat="server" 
                            AutoPostBack="false"
                            Width="155px">
                        <asp:ListItem Value="-1" Text="TODOS"></asp:ListItem>
                        <asp:ListItem Value="0" Text="SI"></asp:ListItem>
                        <asp:ListItem Value="1" Text="NO"></asp:ListItem>
                        </asp:DropDownList>
                        <span  class="label" style="">Cliente</span>
                        <asp:DropDownList ID="clientesComboBox2" runat="server" 
                            AutoPostBack="true"
                            Width ="155px"
                            OnSelectedIndexChanged="clientesComboBox2_SelectedIndexChanged">
                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <span  class="label" style="">Sin Financiera</span>
                        <asp:CheckBox ID="ProductoTipo" runat="server" AutoPostBack="true" OnCheckedChanged="ProductoTipo_CheckedChanged"/>                                    
                        <span  class="label" style="">Fecha Inicio</span> 
                        <telerik:RadDatePicker ID="FechaInicio" runat="server" Width="100px"></telerik:RadDatePicker>
                    </div>
                    
                    <div style="width:85%; float:right;">
                        <span  class="label" style="color:white;">_</span>
                        <span  class="label" style="color:white;">_</span>                                  
                        <span  class="label" style="">Estudios</span>                 
                        <asp:DropDownList ID="estudiosComboBox" runat="server" 
                            AutoPostBack="false"
                            Width="150px">
                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <span  class="label" style="">Financiera</span>             
                        <asp:DropDownList ID="financieraComboBox" runat="server" 
                            AutoPostBack="false"
                            Width="185px">
                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                        </asp:DropDownList>                
                        <span  class="label" style="">Fecha Fin</span>
                        <telerik:RadDatePicker ID="FechaFin" runat="server" Width="100px"></telerik:RadDatePicker>
                    </div>
                </div>  
                <br />              
                <div style="width:100%; display:inline-block;">
                    <asp:LinkButton ID="GenerateReportButton" runat="server" CssClass="button"
                    OnClick="GenerateReportButton_Click"
                    Style="margin-top: 7px; margin-bottom: 3px; margin-right:0px">
                    <span>Generar Reporte</span>
                    </asp:LinkButton>
                <asp:LinkButton ID="GenerateExcelReportButton" runat="server" CssClass="button"
                    OnClick="GenerateExcelReportButton_Click"
                    Style="margin-top: 7px; margin-bottom: 3px;">
                    <span>Exportar a Excel</span>
                </asp:LinkButton>  
                </div>
                
                              
                <span class="label">Cantidad de Estudios por Financiera</span>    
                <div class="validation">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="FechaInicio"
                        ErrorMessage="Debe seleccionar una Fecha de Inicio" Display="Dynamic"
                        ValidationGroup="EstudiosPorFinanciera">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="validation">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="FechaFin"
                        ErrorMessage="Debe seleccionar una Fecha de Fin" Display="Dynamic"
                        ValidationGroup="EstudiosPorFinanciera">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="validation">
                    <asp:CompareValidator runat="server" ControlToValidate="FechaInicio"
                        ControlToCompare="FechaFin" Operator="LessThanEqual"
                        ErrorMessage="La Fecha de Inicio no puede ser mayor que la Fecha Fin" Display="Dynamic"
                        ValidationGroup="EstudiosPorFinanciera">
                    </asp:CompareValidator>
                </div>

                <div style="overflow:scroll; width:100%">
                <telerik:RadGrid ID="EstudiosPorFinancieraGrid" runat="server" 
                    OnDataBound="EstudiosPorFinancieraGrid_DataBound"
                    OnDataBinding="EstudiosPorFinancieraGrid_DataBinding"
                    OnItemCommand="EstudiosPorFinancieraGrid_ItemCommand" 
                    GroupPanelPosition="Top" >
                    <ExportSettings 
                        Excel-Format="Xlsx" 
                        FileName="EstudiosPorFinanciera" 
                        ExportOnlyData="true"></ExportSettings>
                    <MasterTableView 
                        ExpandCollapseColumn-Display="false" 
                        CommandItemDisplay="Top" 
                        OverrideDataSourceControlSorting="true" >
                        <CommandItemSettings 
                            ShowExportToExcelButton="true"                             
                            ShowRefreshButton="false" 
                            ShowAddNewRecordButton="false" />
                        <NoRecordsTemplate>
                            <div style="text-align: center;">No hay Estudios Realizados</div>
                        </NoRecordsTemplate>
                        <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                        <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                        <HeaderStyle  Font-Size="11px" />
                            
                    </MasterTableView>
                </telerik:RadGrid>
                    </div>
            </div>
        </div>
    </div>
</asp:Content>

