<%@ Page Title="Estudios Médicos" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EstudiosMedicos.aspx.cs" Inherits="Mantenimiento_Citas_EstudiosMedicos" %>

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
               <span class="title">Buscar estudios médicos</span></div>
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
                           <span style="margin-left:30px;">BÚSQUEDA DE CITA</span>
                        </h3>
                    </a>
                    <div id="Contents" style="padding:1em 0.5em;">
                        <div style="float:right">
                            
                            <asp:Label ID="lblFecha" runat="server" Text="Label" Font-Bold="True" Visible="False"></asp:Label>

                        </div>
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
                          <asp:ImageButton ID="imgeliminar" runat="server" ImageUrl="~/Images/Neutral/delete.png" 
                              CssClass="BotonDelete" Visible="False" OnClick="imgeliminar_Click" 
                              ToolTip="Eliminar estudios de la cita" />
                          <asp:ImageButton ID="imgupdate" runat="server" ImageUrl="~/Images/Neutral/update.jpg"
                              CssClass="BotonActualizar" Visible="False"  
                              ToolTip="Actualizar fecha estudios y su file" OnClick="imgupdate_Click" Width="22px" />

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
                    <asp:GridView ID="CitaEstudioGridView" runat="server"
                        DataSourceID="CitaEstudioDataSource"
                        CssClass="grid" Width="100%" AutoGenerateColumns="False" OnRowCommand="CitaEstudioGridView_RowCommand" OnSelectedIndexChanged="CitaEstudioGridView_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="50px" >
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkall" runat="server" Text="Todo" AutoPostBack="True" OnCheckedChanged="chkall_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkestudio" runat="server" OnCheckedChanged="chkestudio_CheckedChanged" />
                                    <asp:Label ID="lblcitaid" runat="server" Visible="False" Text='<%# Eval("citadesgravamenid") %>'></asp:Label>
                                    <asp:Label ID="lblestudioid" runat="server" Visible="False" Text='<%# Eval("estudioId") %>'></asp:Label>
                                    <asp:Label ID="lblproveedorid" runat="server" Visible="False" Text='<%# Eval("proveedorMedicoId") %>'></asp:Label>
                                    </ItemTemplate>

                            <HeaderStyle Width="50px"></HeaderStyle>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="citaDesgravamenId" HeaderText="citaid" SortExpression="citaDesgravamenId" Visible="False" />
                            <asp:BoundField DataField="estudioId" HeaderText="estudioid" SortExpression="estudioId" Visible="False" />--%>
                            <asp:BoundField HeaderText="Nombre Estudio" DataField="nom_estudio" SortExpression="nom_estudio" />
                            <asp:BoundField DataField="fechaRealizado" HeaderText="Fecha_Realizado" SortExpression="fechaRealizado" />
                            <asp:BoundField DataField="nom_proveedor" HeaderText="Proveedor" SortExpression="nom_proveedor" />
                            <asp:BoundField DataField="realizado" HeaderText="Realizado?" SortExpression="realizado" />
                            <asp:BoundField DataField="necesitoCita" HeaderText="Necesito Cita?" SortExpression="necesitoCita" />
                        </Columns>
                        <EmptyDataTemplate>
                            <p style="background-color:white">
                                No se ha encontrado estudios registrados en el sistema.
                            </p>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="CitaEstudioDataSource" runat="server"
                        TypeName="Artexacta.App.CitaEstudio.BLL.CitaEstudioBLL"
                        SelectMethod="getCitasEstudioListar" OldValuesParameterFormatString="original_{0}" 
                        OnSelected="CitaEstudioDataSource_Selected">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="citaDesgravamenId" Type="Int32" />
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
                   

            $('.BotonDelete').click(function () {
                return confirm('Está seguro que quiere eliminar los estudios seleccionados');
            });
            
            $('.BotonActualizar').click(function () {
                return confirm('Está seguro que quiere actualizar la fecha del estudio y sus files');
            });
        });

    </script>
    
     
</asp:Content>

