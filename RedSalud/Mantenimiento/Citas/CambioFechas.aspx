<%@ Page Title="Cambio Fechas" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CambioFechas.aspx.cs" Inherits="Mantenimiento_Citas_CambioFechas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .sectionTitle:hover {
        background: #e1dddd;
        }
        .sectionTitle:focus {
            background: #e1dddd;
        }
        .PanelAdmin {
            border: 1px solid grey;
        }
        #PanelButton, #PanelButton:visited, #PanelButton:hover, #PanelButton:active {
        color: #333 !important;
        }        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
               <span class="title">Buscar cita para el cambio de fecha</span></div>
            <div class="columnContent">
                <div class="contentMenu">
                </div>
                <asp:TextBox id="IsAccordionActive" Text="0" runat="server" Style="display:none;" />
                <asp:Panel runat="server" Visible="false">
                </asp:Panel>

                 <div class="clear" style="margin-bottom: 10px;"></div>
                <asp:Panel ID="PanelAD" runat="server"></asp:Panel>
                  <asp:Panel id="AdminPanel" runat="server" CssClass="PanelAdmin" style="font-size: 12px;"> 
                    <a id="PanelButton" style="text-decoration: none;">
                        <h3 class="sectionTitle" style="background: #e1dddd;">
                           <span style="margin-left:30px;">BÚSQUEDA</span>
                        </h3>
                    </a>
                    <div id="Contents" style="padding:1em 0.5em;">
                    INGRESAR CÓDIGO CITA: 
                    <asp:TextBox ID="txtcitaid" runat="server" MaxLength="8"></asp:TextBox><asp:LinkButton ID="btnSearch" runat="server" CssClass="button" OnClick="btnSearch_Click" ValidationGroup="Cita">
                    <span>BUSCAR</span>
                    </asp:LinkButton>
                        <div class="validators">
                    <asp:RequiredFieldValidator ID="CitaRFV" runat="server"
                        ControlToValidate="txtcitaid"
                        ValidationGroup="Cita"
                        Display="Dynamic"
                        ErrorMessage="Debe ingregar el código de la cita"></asp:RequiredFieldValidator>
                </div>
                        <div class="validators">
                            <asp:RegularExpressionValidator ID="CitanumRFV" runat="server" ErrorMessage="Solo números" ControlToValidate="txtcitaid" ValidationExpression="[0-9]*" ValidationGroup="Cita" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>

                        <div style="float:right">
                            FECHA DE ACTUALIZACIÓN: 
                            <asp:Label ID="lblFecha" runat="server" Text="Label" Font-Bold="True"></asp:Label>

                        </div>
                                        
                    <br />
                    <div class="clear" style="margin-bottom: 5px;"></div>                                           
                       
                        <asp:LinkButton ID="btnlimpiar" runat="server" CssClass="button" OnClick="btnlimpiar_Click" >
                    <span>LIMPIAR</span>
                    </asp:LinkButton>   
                    </div>
                       
                </asp:Panel>
                    <div class="clear" style="margin-bottom: 5px;"></div>

                 <div class="gridContainer">
                      <%--  DataSourceID="CitasDataSource"--%>
                    <asp:GridView ID="CitasGridView" runat="server"
                         DataSourceID="CitasDataSource"
                        CssClass="grid" Width="100%" AutoGenerateColumns="False" OnRowCommand="CitasGridView_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="50px" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="UpdateButton" runat="server" CssClass="UpdateRecordc"
                                        CommandName="UpdateRecord" CommandArgument='<%# Bind("citaDesgravamenid") %>' Text="Actualizar">
                                        <asp:Image runat="server" ImageUrl="~/Images/Neutral/ActualizarRegistro.png"
                                            ToolTip="Actualizar" />
                                    </asp:LinkButton>
                                </ItemTemplate>

<HeaderStyle Width="50px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Estado" DataField="estado" SortExpression="estado" />
                            <asp:BoundField DataField="fechaCreacion" HeaderText="Fecha Creacion" SortExpression="fechaCreacion" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                            <asp:BoundField DataField="fechaHoraCita" HeaderText="Fecha de Cita" SortExpression="fechaHoraCita" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                            <asp:BoundField DataField="aprobado" HeaderText="Aprobado?" SortExpression="aprobado" />
                            <asp:BoundField DataField="observacionLabo" HeaderText="Observación" SortExpression="observacionLabo" />
                            <asp:BoundField DataField="nom" HeaderText="Especialista" SortExpression="nom" />
                        </Columns>
                        <EmptyDataTemplate>
                            <p style="background-color:white">
                                Debe ingresar un código para encontrar la cita.
                            </p>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="CitasDataSource" runat="server"
                        TypeName="Artexacta.App.CitaEstudio.BLL.CitaEstudioBLL"
                        SelectMethod="getCitasListar" OldValuesParameterFormatString="original_{0}" 
                        OnSelected="CitasDataSource_Selected">
                        <SelectParameters>
                            <asp:Parameter Name="citaDesgravamenId" Type="Int32" DefaultValue="0" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>

                 <div class="clear"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#PanelButton").click(function () {
                if (!$("#Contents").is(":visible")) {
                    $("#Contents").slideDown();
                } else {
                    $("#Contents").slideUp();
                }
                return false;
            });

        

            $("#<%= AdminPanel.ClientID %> h3").click(function () {
                if ($("#<%= IsAccordionActive.ClientID %>").val() != "0") {
                   $("#<%= IsAccordionActive.ClientID %>").val("0");
                } else {
                    $("#<%= IsAccordionActive.ClientID %>").val("1");
               }
           });
                   

            $('.UpdateRecordc').click(function () {
                return confirm('¿Está seguro en cambiar la fecha de la cita?');
            });

           

        });

    </script>
    
     
</asp:Content>

