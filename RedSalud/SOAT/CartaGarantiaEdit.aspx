<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CartaGarantiaEdit.aspx.cs" Inherits="SOAT_CartaGarantiaEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    .Page {
            -moz-box-shadow: 0 !important;
            -webkit-box-shadow: 0 !important;
            box-shadow: 0 !important;
        }
    @media hiqpdf {
        .noPrintcss {
                display: none;
        }

        .noPrint, .PrintIcon, .ExpandCollapseIcon {
            display: none !important;
        }

        .Page {
            -moz-box-shadow: 0 !important;
            -webkit-box-shadow: 0 !important;
            box-shadow: 0 !important;
        }
        /** rest of settings **/
       
    }

@media print {
    .noPrint {
        display: none !important;
    }

    html * {
        background-color:#ffffff;
        border-width:0px;
    }

    body {
        font-size: 14px !important;
    }
    .row {
        background-color:#C9DFFC;
        font-size:14px;
    }
    .altRow {
        background-color:#E8F1FF;
        font-size:14px; 
    }
    .headerRow {
        font-size: 14px;
    }
}
    html * {
        text-transform: uppercase;
    }
    html {
        text-transform: uppercase;
    }
    .pregunta {
        float:left;
        margin-right: 20px;
        width: 45%;
    }
    .observacionesPregunta {
        float: left;
        width: 45%;
    }
    .cellTitle {
        padding: 5px;
        background-color: #e4e4e4;
    }
    .cellFirstCol {
        width: 100px;
    }
    .centerText {
        text-align: center;
    }
    .facturarPA {
        width:60%; 
        margin-left:auto; 
        margin-right:auto;
        padding: 10px;
        border: solid 1px;
        text-align:center;
        font-weight: bold;
        background-color: #EAEAEA;
    }
    .row {
        background-color:#C9DFFC;
        font-size:11px;
    }
    .altRow {
        background-color:#E8F1FF;
        font-size:11px; 
    }
    .headerRow {
        font-size: 11px;
    }
    h1 {
        text-align:center;
    }
    .UpdatedCenterWidth {
        width: 32%;
    }

    .addressContainer {
        /*margin-right: 20px;
        width: 150px;*/
        text-align: center;
    }

    .addressTitle {
        font-weight: bold;
        font-size: 8px;
    }

    .addressBody {
        font-size: 8px;
        margin: 0 !important;
    }

    .borderB {
        border-bottom: 1px solid #828282;
    }
    .borderL {
        border-left: 1px solid #828282;
    }
    .borderR {
        border-right: 1px solid #828282;
    }
    .borderT {
        border-top: 1px solid #828282;
    }

    tr.trC {
        border-collapse: collapse !important;
        padding: 5px 10px !important;
        border: 1px solid #828282 !important;
    }

    td.tdC {
        padding: 2px 10px;
    }

    .tHeader {
        padding: 0 15px !important; 
        margin-right: 15px; 
        background: linear-gradient(white, #EAEAEA);
    }

    .siniestro-details-box {
        padding: 5px;
        float: left;
        padding: 5px;
        height: 100%;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead noPrint">
                <span class="title">Carta de Garantia</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu noPrint">
                     
                    <asp:LinkButton ID="cmdVolver" runat="server" OnClick="cmdVolver_Click" Text="Volver" Visible="false"></asp:LinkButton>
                    
                    <asp:Image ID="printIcon" runat="server" Style="display:none;" ImageUrl="~/Images/Neutral/ExportPrint.gif" />
                </div>

                
                <br /><br />
                
            </div>
        </div>
    </div>

    <div class="threeColsLeft">
        <div class="frame">
            <div class="columnContent" style="margin-top:-5px; ">
                <img src="../Images/LogoPrint.jpg" alt="Red Salud" style="height:95px;" />
            </div>
        </div>
    </div>
    <div class="threeColsCenter" style="width:32%;">
        <div class="frame">
            <div class="columnContent">
                <h1 Id="ordendeservicioh1title" runat="server" >RESUMEN SINIESTRO</h1>
                <h2 id="ordenServicioCliente" runat="server" style="text-align:center; font-size: 17px;"></h2>
            </div>
        </div>
    </div>
    <div class="threeColsRight">
        <div class="frame">
            <div class="columnContent">
                
            </div>
        </div>
    </div>
    <div style="clear:both;"></div>
    <div class="oneColumn">
        <div class="frame">
            <div class="columnContent">                                
                <div style="background-color: #EAEAEA; padding: 5px 5px; margin: 0; font-weight: bold; border: 1px solid #828282">
                    DATOS DEL SINIESTRO
                </div>
                <div class="oneColumn" style="margin: 0 !important;"> <!--twoColsLeft-->
                    <div class="frame" style="padding: 0 !important;">
                        <div class="columnContent" style="border: 1px solid #828282; padding: 5px !important; border-top: 0 !important; line-height: 17px;">                            
                            <div style="display:inline-block !important; padding: 0 auto !important;">                                                                    
                                <div class="siniestro-details-box" style="width: 277px;">
                                    <b>Fecha De Siniestro: </b><asp:Literal ID="FechaSiniestro" runat="server"></asp:Literal>
                                    <br />
                                    <b>Fecha De Denuncia: </b><asp:Literal ID="FechaDenuncia" runat="server"></asp:Literal>                                    
                                </div>
                                <div style="display:inline-block !important; padding: 0 auto !important;">
                                    <div class="siniestro-details-box" style="width: 225px;">
                                        <b>Nro. Siniestro: </b><asp:Literal ID="NroSiniestro" runat="server"></asp:Literal>
                                        <br />
                                        <b>Nro. Roseta: </b><asp:Literal ID="NroRoseta" runat="server"></asp:Literal>
                                        <br />
                                        <b>Placa: </b><asp:Literal ID="Placa" runat="server"></asp:Literal>                                    
                                    </div>
                                    <div class="siniestro-details-box" style="width: 226px;">                                    
                                        <b>Titular Poliza: </b><asp:Literal ID="TitularPoliza" runat="server"></asp:Literal>
                                        <br />
                                        <b>CI/NIT Poliza: </b><asp:Literal ID="CI_NIT_Poliza" runat="server"></asp:Literal>
                                        <br />
                                        <b>Lugar Venta: </b><asp:Literal ID="LugarVentaPoliza" runat="server"></asp:Literal>                                    
                                    </div>
                                    <div class="siniestro-details-box" style="width: 220px;">                                    
                                        <b>Tipo Vehiculo: </b><asp:Literal ID="TipoVehiculo" runat="server"></asp:Literal>
                                        <br />
                                        <b>Nro. Chasis: </b><asp:Literal ID="NroChasis" runat="server"></asp:Literal>
                                        <br />   
                                        <b>Lugar Siniestro: </b><asp:Literal ID="LugarSiniestro" runat="server"></asp:Literal>                                                                        
                                    </div>
                                    <div class="siniestro-details-box" style="width: 182px;">
                                        <b>Area: </b><asp:Literal ID="Area" runat="server"></asp:Literal>
                                        <br />
                                        <b>Sector: </b><asp:Literal ID="SectorVehiculo" runat="server"></asp:Literal>
                                        <br />
                                        <b>Sindicato: </b><asp:Literal ID="Sindicato" runat="server"></asp:Literal>
                                    </div>                                    
                                    <div class="siniestro-details-box" style="width: 160px;">
                                        <b>Cant. Ilesos: </b><asp:Literal ID="CantIlesos" runat="server"></asp:Literal>
                                        <br />
                                        <b>Cant. Heridos: </b><asp:Literal ID="CantHeridos" runat="server"></asp:Literal>
                                        <br />
                                        <b>Cant. Fallecidos: </b><asp:Literal ID="CantFallecidos" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div style="display:inline-block !important; padding: 0 auto !important;">                                                                    
                                    <div class="siniestro-details-box" style="width: 277px;">
                                        <b>Causa: </b><asp:Literal ID="CausaSiniestro" runat="server"></asp:Literal>
                                        <br />
                                        <b>Observaciones: </b><asp:Literal ID="Observaciones" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
   
    <div class="oneColumn">
        <div class="frame">
            <div class="columnContent">                
                <asp:Panel ID="LaboSolicitadoPanel" runat="server">
                                                    
                    <div style="background-color: #EAEAEA; padding: 5px 5px; margin: 0; font-weight: bold; border: 1px solid #828282; border-bottom: 0;">
                        ACCIDENTADOS
                    </div>
                    <telerik:RadGrid 
                        ID="EstudiosGridView" 
                        runat="server"
                        AutoGenerateColumns="false" 
                        DataSourceID="AccidentadoODS"
                        AllowPaging="false"
                        AllowMultiRowSelection="true">
                        <MasterTableView DataKeyNames="AccidentadoId" ClientDataKeyNames="AccidentadoId" ExpandCollapseColumn-Display="false">
                            <NoRecordsTemplate>
                                <div style="text-align: center;">El Siniestro no tiene ningun accidentado</div>
                            </NoRecordsTemplate>
                            <AlternatingItemStyle CssClass="altRow" />
                            <ItemStyle CssClass="row" />
                            <HeaderStyle CssClass="headerRow" />
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="NoSortNombre" DataField="NombreForDisplay" HeaderText="Nombre" />
                                <telerik:GridBoundColumn UniqueName="NoSortCarnetIdentidad" DataField="CarnetIdentidad" HeaderText="CI" />
                                <telerik:GridBoundColumn UniqueName="NoSortGenero" DataField="GeneroForDisplay" HeaderText="Sexo" Visible="false" />
                                <telerik:GridBoundColumn UniqueName="NoSortFechaNacimiento" DataField="FechaNacimientoForDisplay" HeaderText="Fecha de Nacimiento" Visible="false" />
                                <telerik:GridBoundColumn UniqueName="NoSortEstadoCivil" DataField="EstadoCivil" HeaderText="Estado Civil" />
                                <telerik:GridBoundColumn UniqueName="NoSortLicenciaConducir" DataField="LicenciaConducirForDisplay" HeaderText="Tiene Licencia" />
                                <telerik:GridBoundColumn UniqueName="NoSortTipo" DataField="TipoForDisplay" HeaderText="Tipo" />
                                <telerik:GridBoundColumn UniqueName="NoSortEstado" DataField="EstadoForDisplay" HeaderText="Accidentado / Fallecido" />
                                <telerik:GridBoundColumn UniqueName="NoSortSiniestroPagado" DataField="SiniestroPagadoForDisplay" HeaderText="Total Gastos" Visible="false" />
                                <telerik:GridBoundColumn UniqueName="NoSortEstadoSeguimiento" DataField="EstadoSeguimiento" HeaderText="Estado" Visible="false" />
                                <telerik:GridBoundColumn UniqueName="NoSortReserva" DataField="Reserva" HeaderText="Reserva" />
                            </Columns>            
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="AccidentadoODS" runat="server"
                        TypeName="Artexacta.App.Accidentado.BLL.AccidentadoBLL"
                        OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetAccidentadosBySiniestroId"
                        OnSelected="AccidentadoODS_Selected">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="SearchAccidentadoTexbox" Name="search" PropertyName="Value" Type="String" />
                            <asp:ControlParameter ControlID="SiniestroIdHiddenfield" Name="SiniestroId" PropertyName="Value" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>

            </div>
        </div>
    </div>
    <div style="clear:both;">
    </div>
    <div class="oneColumn">
        <div class="frame">
            <div class="columnContent">                
                <asp:Panel ID="Panel1" runat="server">
                                                    
                    <div style="background-color: #EAEAEA; padding: 5px 5px; margin: 0; font-weight: bold; border: 1px solid #828282; border-bottom: 0;">
                        Gastos Medicos
                    </div>
                    <telerik:RadGrid 
                        ID="RadGrid1" 
                        runat="server"
                        AutoGenerateColumns="true" 
                        DataSourceID="AccidentadoGastosODS"
                        AllowPaging="false"
                        AllowMultiRowSelection="true">
                        <MasterTableView ExpandCollapseColumn-Display="false">
                            <NoRecordsTemplate>
                                <div style="text-align: center;">No Existen Gastos</div>
                            </NoRecordsTemplate>
                            <AlternatingItemStyle CssClass="altRow" />
                            <ItemStyle CssClass="row" />
                            <HeaderStyle CssClass="headerRow" />
                            
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="AccidentadoGastosODS" runat="server"
                        TypeName="Artexacta.App.Accidentado.BLL.AccidentadoBLL"
                        OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetGastosByAccidentadoPivotTipo"
                        OnSelected="AccidentadoODS_Selected">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="SiniestroIdHiddenfield" Name="SiniestroId" PropertyName="Value" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>

            </div>
        </div>
    </div>
    <div style="clear:both;">
    </div>
    <div class="oneColumn">
        <div class="frame">
            <asp:Repeater                  
                ID="accidentadosHistorialMedicoRepeater"
                runat="server"                
                DataSourceID="AccidentadoODS">
                <ItemTemplate>
                    <div class="columnContent">                
                        <asp:Panel ID="Panel2" runat="server">
                            <asp:HiddenField ID="SiniestroIDHF" runat="server" Value='<%# Eval("SiniestroId") %>' />
                            <asp:HiddenField ID="AccidentadoIDHF" runat="server" Value='<%# Eval("AccidentadoId") %>' />                   
                            <div style="background-color: #EAEAEA; padding: 5px 5px; margin: 0; font-weight: bold; border: 1px solid #828282; border-bottom: 0;">
                                Visitas Medicas - <asp:Literal ID="lbl_Title" runat="server" Text='<%# Eval("Nombre") %>' />
                            </div>
                            <telerik:RadGrid ID="GestionMedicaRadGrid" runat="server"
                                AutoGenerateColumns="false"
                                DataSourceID="GestionMedicaODS"
                                AllowPaging="false"
                                PageSize="100"
                                MasterTableView-DataKeyNames="AccidentadoId">
                                <MasterTableView>
                                    <NoRecordsTemplate>
                                        <div style="text-align: center;">No se encuentran visitas medicas registradas</div>
                                    </NoRecordsTemplate>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Nombre" HeaderText="Establecimiento de Salud" />
                                        <telerik:GridBoundColumn DataField="FechaVisitaForDisplay" HeaderText="Fecha de Visita" />
                                        <telerik:GridBoundColumn DataField="Grado" HeaderText="Grado" />
                                        <telerik:GridBoundColumn DataField="DiagnosticoPreliminar" HeaderText="Observaciones" Visible="true" />
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>

                    <asp:ObjectDataSource ID="GestionMedicaODS" runat="server"
                        TypeName="Artexacta.App.GestionMedica.BLL.GestionMedicaBLL"
                        OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetAllGestionMedicaBySiniestroID">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="SiniestroIDHF" Name="SiniestroId" PropertyName="Value" Type="Int32" />
                            <asp:ControlParameter ControlID="AccidentadoIDHF" Name="AccidentadoId" PropertyName="Value" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>                      
                        </asp:Panel>

                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <%-- 
            <asp:ObjectDataSource ID="AccidentadosHistorialODS" runat="server"
                TypeName="Artexacta.App.Accidentado.BLL.AccidentadoBLL"
                OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetAccidentadosBySiniestroId"
                OnSelected="AccidentadoODS_Selected">
                <SelectParameters>
                    <asp:ControlParameter ControlID="SearchAccidentadoTexbox" Name="search" PropertyName="Value" Type="String" />
                    <asp:ControlParameter ControlID="SiniestroIdHiddenfield" Name="SiniestroId" PropertyName="Value" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            --%>
        </div>
    </div>
    <!--
    <div style="margin: 0 auto; width:655px;">
        <div style="display: inline-flex;">
            <div class="addressContainer">
                <span class="addressTitle">Red Salud LPZ</span>
                <p class="addressBody" >Avenida 6 de agosto, Edificio el Carmen #2549 entre Pinilla y Pedro Salazar</p>
            </div>
            <div class="addressContainer">
                <span class="addressTitle">Conkardio</span>
                <p class="addressBody" >Calle Capitan Ravelo esquina Montevidoe, Edificio Capitan Ravelo #1201 Piso 10</p>
            </div>
            <div class="addressContainer">
                <span class="addressTitle">Cedimagen</span>
                <p class="addressBody" >Avenida heroes del pacifico esquina guerrilleros lanza #1285, Zona Miraflores</p>
            </div>
            <div class="addressContainer">
                <span class="addressTitle">Red Salud CBBA</span>
                <p class="addressBody" >Avenida Salamanca edificio Cibeles, Piso 6 Oficina 6C</p>
            </div>        
        </div>
    </div> 
    -->           
    <div style="clear:both;">
    </div>
    <%--
    <div style="background-color: #EAEAEA; padding: 5px 5px; margin: 10px 0; font-weight: bold;">
        <asp:Literal ID="AddressSectionTitle" runat="server"></asp:Literal>
    </div>
    --%>
    <asp:HiddenField ID="SiniestroIdHiddenfield" runat="server" Value="0" />
    <asp:HiddenField ID="SearchAccidentadoTexbox" runat="server" Value="" />
    <asp:HiddenField ID="CitaDesgravamenIdHiddenField" runat="server" Value="0" />
    <asp:HiddenField ID="PropuestoAseguradoIdHiddenField" runat="server" Value="0" />
    <asp:HiddenField ID="PaginaBackHiddenField" runat="server" />
    <asp:HiddenField ID="ProveedorMedicoIdHiddenField" runat="server" />

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= printIcon.ClientID %>').click(function () {
                window.print();
                return false;
            });
        });
    </script>
</asp:Content>

