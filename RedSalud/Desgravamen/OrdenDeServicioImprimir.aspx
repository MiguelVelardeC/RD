<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OrdenDeServicioImprimir.aspx.cs" Inherits="Desgravamen_OrdenDeServicioImprimir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
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

    
    .tHeader {
        background: #EAEAEA !important;
    }
    /** rest of settings **/
       
}


@media print {
    .noPrint {
        display: none;
        visibility: hidden;
    }

    html * {
        background-color:#ffffff;
        border-width:0px;
    }

    body {
        font-size: 14px !important;
    }
    /*
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
    }*/
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
        text-align: left;
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
        width: 100px; 
        background: linear-gradient(white, #EAEAEA);
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead noPrint">
                <span class="title">Examen Médico</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu noPrint">
                    <asp:LinkButton ID="cmdVolver" runat="server" OnClick="cmdVolver_Click" Text="Volver"></asp:LinkButton>

                    <asp:Image ID="printIcon" runat="server" ImageUrl="~/Images/Neutral/ExportPrint.gif" />
                    <asp:ImageButton ID="pdfIcon" ImageUrl="~/Images/Neutral/pdf-icon-light.png" Width="25" Height="25" runat="server" OnClick="pdfIcon_Click" />
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
                <h1 Id="ordendeservicioh1title" runat="server" >Orden de Servicio</h1>
                <h2 id="ordenServicioCliente" runat="server" style="text-align:center; font-size: 17px;"></h2>
            </div>
        </div>
    </div>
    <div class="threeColsRight">
        <div class="frame">
            <div class="columnContent">
                <h3 style="margin-top:3px; text-align:center;">
                Nro: <asp:Literal ID="CitaIdLabel" runat="server"></asp:Literal>
                </h3>
            </div>
        </div>
    </div>
    <div style="clear:both;"></div>

    <div class="twoColsLeft">
        <div class="frame">
            <div class="columnContent">
                
                Ciudad: <b><asp:Literal ID="NombreCiudad" runat="server"></asp:Literal></b>
                <br />
                <br />
                Médico Examinador: <b><asp:Literal ID="MENombre" runat="server"></asp:Literal></b>
                <br />
                Fecha: <b><asp:Literal ID="FechaCita" runat="server"></asp:Literal></b>
                <br />
                <br />
                Propuesto Asegurado: <b><asp:Literal ID="PANombre" runat="server"></asp:Literal></b>
                <br />
                Fecha de Nacimiento: <b><asp:Literal ID="PAFechaNacimiento" runat="server"></asp:Literal></b>
                <br />
                Carnet de Identidad Nro: <b><asp:Literal ID="PACI" runat="server"></asp:Literal></b>
                <br />
                Celular: <b><asp:Literal ID="PATelefonoCelular" runat="server"></asp:Literal></b>
                <br />
                <br />
                <div style="background-color:#EAEAEA; padding: 5px 5px;">Referencia: <b><asp:Literal ID="CitaReferencia" runat="server"></asp:Literal></b></div>
            </div>
        </div>
    </div>
    <div class="twoColsRight">
        <div class="frame">
            <div class="columnContent">
            </div>
        </div>
    </div>

    <div class="oneColumn">
        <div class="frame">
            <div class="columnContent">
                
                <asp:Panel ID="FacturarPAPanel" runat="server" CssClass="facturarPA">
                    Facturar al Propuesto Asegurado
                </asp:Panel>

                <br />

                <asp:Panel ID="LaboSolicitadoPanel" runat="server">

                    <telerik:RadGrid ID="EstudiosGridView" runat="server"
                        AutoGenerateColumns="false" 
                        DataSourceID="EstudiosDataSource"
                        AllowPaging="false"
                        AllowMultiRowSelection="true">
                        <MasterTableView DataKeyNames="EstudioId" ClientDataKeyNames="EstudioId" ExpandCollapseColumn-Display="false">
                            <NoRecordsTemplate>
                                <div style="text-align: center;">No se han solicitado estudios</div>
                            </NoRecordsTemplate>
                            <AlternatingItemStyle CssClass="altRow" />
                            <ItemStyle CssClass="row" />
                            <HeaderStyle CssClass="headerRow" />
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="NombreProveedor" DataField="NombreProveedor"
                                    HeaderText="Proveedor" />
                                <telerik:GridBoundColumn UniqueName="Fecha" DataField="FechaCitaForDisplay"
                                    HeaderText="Fecha/Hora Cita" />
                                <telerik:GridBoundColumn UniqueName="Estudio" DataField="NombreEstudio"
                                    HeaderText="Estudio" />
                            </Columns>            
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="EstudiosDataSource" runat="server"
                        TypeName="Artexacta.App.Desgravamen.BLL.EstudioBLL"
                        SelectMethod="GetEstudiosByCitaDesgravamenIdForEjecutivo"
                        OnSelected="EstudiosDataSource_Selected">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="CitaDesgravamenIdHiddenField" PropertyName="Value"
                                Name="citaDesgravamenId" Type="Int32" />     
                            
                            <asp:ControlParameter ControlID="ProveedorMedicoIdHiddenField" PropertyName="Value"
                                Name="proveedorMedicoId" Type="Int32" /> 
                                                          
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>

            </div>
        </div>
    </div>
    <div style="clear:both;">
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
    <div style="background-color: #EAEAEA; padding: 5px 5px; margin: 10px 0; font-weight: bold;">
        <asp:Literal ID="AddressSectionTitle" runat="server"></asp:Literal>
    </div>
    <table class="facturaTable" style="margin: 0 auto; width: 99%; border-collapse: collapse;">
        <tr>
        <th></th>
        <th></th> 
        <th></th> 
        <th></th>
        </tr>
        <tr class="trC">
            <td class="borderR tHeader" style="font-size:10px;">
                La Paz
            </td>
            <td class="tdC">
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle1" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent1" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle2" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent2" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">            
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle3" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent3" runat="server"></asp:Literal></p>
                </div>
            </td>
                <!--
            <td>
                <div class="addressContainer">
                    <span class="addressTitle">Red Salud CBBA</span>
                    <p class="addressBody" >Avenida Salamanca edificio Cibeles, Piso 6 Oficina 6C</p>
                </div>
                <div class="addressContainer">
                    <span class="addressTitle">DARRAS</span>
                    <p class="addressBody" >Calle Baptista entre Arevalo y La Paz Edificio Medicorp #761</p>
                </div>
                <div class="addressContainer">
                    <span class="addressTitle">Dr. Salazar</span>
                    <p class="addressBody" >Av. Papa Paulo entre Ramon Rivero y plazuela quintanilla Edificio Colombo piso 7 Oficina 1</p>
                </div>
            </td> 
            <td>
                <div class="addressContainer">
                    <span class="addressTitle">Neomedic</span>
                    <p class="addressBody" >3er anillo externo frente al zoologico esquina calle los tucanes #97</p>
                </div>
            </td>
                -->
        </tr>
        <tr class="trC">
            <td class="borderR tHeader" style="font-size:10px;">
                Cochabamba
            </td>
            <td class="tdC">
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle4" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent4" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle5" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent5" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">            
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle6" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent6" runat="server"></asp:Literal></p>
                </div>
            </td>
        </tr>
        <tr class="trC">
            <td class="borderR tHeader" style="font-size:10px;">
                Santa Cruz
            </td>
            <td class="tdC">                                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle7" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent7" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle8" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent8" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC"></td>
        </tr>
        <tr class="trC">
            <td class="borderR tHeader" style="font-size:10px;">
                Trinidad
            </td>
            <td class="tdC">                                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle9" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent9" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle10" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent10" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">                                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle11" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent11" runat="server"></asp:Literal></p>
                </div>
            </td>
        </tr>
        <tr class="trC">
            <td class="borderR tHeader" style="font-size:10px;">
                Sucre
            </td>
            <td class="tdC">                                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle12" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent12" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle13" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent13" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">                                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle14" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent14" runat="server"></asp:Literal></p>
                </div>
            </td>
        </tr>
        <tr class="trC">
            <td class="borderR tHeader" style="font-size:10px;">
                Potosi
            </td>
            <td class="tdC">                                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle15" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent15" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle16" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent16" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">                                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle17" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent17" runat="server"></asp:Literal></p>
                </div>
            </td>
        </tr>
        <tr class="trC">
            <td class="borderR tHeader" style="font-size:10px;">
                Oruro
            </td>
            <td class="tdC">                                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle18" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent18" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle19" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent19" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">                                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle20" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent20" runat="server"></asp:Literal></p>
                </div>
            </td>
        </tr>
        <tr class="trC">
            <td class="borderR tHeader" style="font-size:10px;">
                El Alto (La Paz)
            </td>
            <td class="tdC">                                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle21" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent21" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle22" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent22" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">                                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle23" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent23" runat="server"></asp:Literal></p>
                </div>
            </td>
        </tr>
        <tr class="trC">
            <td class="borderR tHeader" style="font-size:10px;">
                Tarija
            </td>
            <td class="tdC">                                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle24" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent24" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle25" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent25" runat="server"></asp:Literal></p>
                </div>
            </td>
            <td class="tdC">                                
                <div class="addressContainer">
                    <span class="addressTitle"><asp:Literal ID="AddressTitle26" runat="server"></asp:Literal></span>
                    <p class="addressBody" ><asp:Literal ID="AddressContent26" runat="server"></asp:Literal></p>
                </div>
            </td>
        </tr>
    </table>
    <!--
    <div style="margin: 0 auto; width:500px;">
        <div style="display: inline-flex; text-align: center; margin:auto;">
            <div class="addressContainer">
                <span class="addressTitle">DARRAS</span>
                <p class="addressBody" >Calle Baptista entre Arevalo y La Paz Edificio Medicorp #761</p>
            </div>
            <div class="addressContainer">
                <span class="addressTitle">Dr. Salazar</span>
                <p class="addressBody" >Av. Papa Paulo entre Ramon Rivero y plazuela quintanilla Edificio Colombo piso 7 Oficina 1</p>
            </div>
            <div class="addressContainer">
                <span class="addressTitle">Neomedic</span>
                <p class="addressBody" >3er anillo externo frente al zoologico esquina calle los tucanes #97</p>
            </div>
        </div>
    </div>
    -->
    <br />
    <br />  
    <br />
    <div class="twoColsLeft">
        <div class="frame">
            <asp:panel runat="server" ID="digitalSignatureLeftPadding" Visible="false" class="columnContent centerText" Height="170px">                
                
            </asp:panel>
            <div class="columnContent centerText">
                -------------------------------------------------------
                <br />
                <asp:Literal ID="PANombreFirma" runat="server"></asp:Literal>
                <br />
                Firma Propuesto Asegurado
            </div>
        </div>
    </div>

    <div class="twoColsRight">
        <div class="frame">
            <asp:panel runat="server" ID="digitalSignature" class="columnContent centerText" Visible="false" Height="170px">                
                <asp:Image ID="digitalSignatureImage" runat="server" />
            </asp:panel>
            <div class="columnContent centerText">
                -------------------------------------------------------
                <br />
                <br />
                <asp:Literal ID="FirmaFooterLabel" runat="server"></asp:Literal>
            </div>
        </div>
    </div>

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

