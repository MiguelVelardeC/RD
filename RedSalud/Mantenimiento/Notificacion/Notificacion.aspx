<%@ Page Title="Notificaciones" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Notificacion.aspx.cs" Inherits="Mantenimiento_Notificacion_Notificacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .normalField {}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
   
       <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Lista de Notificaciones</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <%--<span class="label">Titulo</span>--%>
                </div>

                <asp:TextBox id="IsAccordionActive" Text="0" runat="server" Style="display:none;" />
                <asp:Panel runat="server" Visible="false">
                </asp:Panel>

                 <div class="clear" style="margin-bottom: 10px;"></div>
                <asp:Panel ID="PanelAD" runat="server"></asp:Panel>



                  <asp:Panel id="AdminPanel" runat="server" CssClass="PanelAdmin" style="font-size: 12px;"> 
                    <a id="PanelButton" style="text-decoration: none;">
                        <h3 class="sectionTitle" style="background: #e1dddd;">
                           <span style="margin-left:30px;">CONFIGURAR NOTIFICACIONES</span>
                        </h3>
                    </a>
                    <div id="Contents" style="padding:1em 0.5em;" visible="False">
                        TIPO NOTIFICACIÓN
                         <%--<span class="label">Tipo Notificacion</span>--%>
                <asp:DropDownList ID="Tiponotificaciondp" runat="server" Height="22px" Width="140px" style="margin-left:3px">
                </asp:DropDownList>
                        &nbsp;
                        <div class="validators">
                            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                Display="Dynamic" 
                                ErrorMessage="Debe seleccionar el tipo notificación" 
                                MaximumValue="3" 
                                MinimumValue="2"
                                ControlToValidate="Tiponotificaciondp"
                                ValidationGroup="Ciudad">
                            </asp:RangeValidator>
                </div>
                        <br />
                        TÍTULO
                           <%--<span class="label">Titulo</span>--%>
                <asp:TextBox ID="Titulotxt" runat="server"
                    MaxLength="250" Width="370px"></asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="TituloRFV" runat="server"
                        ControlToValidate="Titulotxt"
                        ValidationGroup="Ciudad"
                        Display="Dynamic"
                        ErrorMessage="Debe ingresar el título"></asp:RequiredFieldValidator>
                </div>
                        <br />
                        
                        GRUPO NOTIFICACIÓN
                 <%--<span class="label">Grupo Notificacion</span>--%>
                <asp:DropDownList ID="gruponotificaciondp" runat="server" Height="22px" Width="140px" style="margin-left:3px">
                </asp:DropDownList>

                &nbsp;
                        &nbsp;
                    PRIORIDAD
                <%--<span class="label">Prioridad</span>--%>
                <asp:TextBox ID="Prioridadtxt" runat="server"
                    MaxLength="1" Width="50px" ToolTip="Ingresar del 0 al 3"></asp:TextBox>

                        <div class="validators">
                   
                             <asp:RangeValidator ID="GrupoNotificacionRFV" runat="server" 
                                Display="Dynamic" 
                                ErrorMessage="Debe seleccionar el grupo notificacion" 
                                MaximumValue="3" 
                                MinimumValue="2"
                                ControlToValidate="gruponotificaciondp"
                                ValidationGroup="Ciudad">
                            </asp:RangeValidator>
                            
                            
                </div>
    
                        <div class="validators">
                    <asp:RequiredFieldValidator ID="PrioridadRFV" runat="server"
                        ControlToValidate="Prioridadtxt"
                        ValidationGroup="Ciudad"
                        Display="Dynamic"
                        ErrorMessage="Debe ingresar la prioridad"></asp:RequiredFieldValidator>
                </div>
                        <div class="validators">
                            <asp:RegularExpressionValidator ID="PrioridadnumRFV" runat="server" 
                                ErrorMessage="Solo números del 0 al 3" ControlToValidate="Prioridadtxt" 
                                ValidationExpression="[0-3]*" 
                                ValidationGroup="Ciudad" 
                                Display="Dynamic">
                            </asp:RegularExpressionValidator>
                         </div>
                             <br />
                DESCRIPCIÓN
                        <br />
               <%-- <span class="label">Descripcion </span>--%>
                <asp:TextBox ID="Descripciontxt" runat="server"
                    MaxLength="500" Height="52px" TextMode="MultiLine" Width="430px"></asp:TextBox>
                <div class="validators">
                    <asp:RequiredFieldValidator ID="DescripcionRFV" runat="server"
                        ControlToValidate="Descripciontxt"
                        ValidationGroup="Ciudad"
                        Display="Dynamic"
                        ErrorMessage="Debe ingresar la descripción"></asp:RequiredFieldValidator>
                </div>

               &nbsp;<br />
                        ADJUNTAR IMAGEN
                     <br />
                     <asp:FileUpload ID="fotoFUP" runat="server" />
                     <br />
                     <asp:Label ID="lblfoto" runat="server" ForeColor="Red"></asp:Label>
                     <br />
   <br />
                    FECHA INICIO
               <%-- <span class="label">Fecha Inicio</span>--%>
                <asp:TextBox ID="Fechainiciotxt" runat="server"
                    MaxLength="20" ToolTip="dd/mm/yyyy HH:mm:ss" Width="145px"></asp:TextBox>
                        &nbsp;
                    FECHA FIN
                <%--<span class="label">Fecha Fin</span>--%>
                <asp:TextBox ID="Fechafintxt" runat="server"
                    MaxLength="20" Width="145px" ToolTip="dd/mm/yyyy HH:mm:ss"></asp:TextBox>
                        


                <div class="validators">
                    <asp:RequiredFieldValidator ID="FechainicioRFV" runat="server"
                        ControlToValidate="Fechainiciotxt"
                        ValidationGroup="Ciudad"
                        Display="Dynamic"
                        ErrorMessage="Debe ingresar la fecha de inicio"></asp:RequiredFieldValidator>
                </div>
                        <div class="validators">
                            <asp:RegularExpressionValidator ID="FEFechainicioRFV" runat="server" 
                                ErrorMessage="Debe respetar el formato dd/mm/yyyy HH:mm:ss" 
                                ControlToValidate="Fechainiciotxt" 
                                ValidationExpression="^([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})(\s)([0-1][0-9]|2[0-3])(:)([0-5][0-9])(:)([0-5][0-9])$" 
                                ValidationGroup="Ciudad" 
                                Display="Dynamic">
                            </asp:RegularExpressionValidator>
                         </div>

                <div class="validators">
                    <asp:RequiredFieldValidator ID="FechafinRFV" runat="server"
                        ControlToValidate="Fechafintxt"
                        ValidationGroup="Ciudad"
                        Display="Dynamic"
                        ErrorMessage="Debe ingresar la fecha fin"></asp:RequiredFieldValidator>
                </div>
                        <div class="validators">
                            <asp:RegularExpressionValidator ID="FEFechafinRFV" runat="server" 
                                ErrorMessage="Debe respetar el formato dd/mm/yyyy HH:mm:ss" 
                                ControlToValidate="Fechafintxt" 
                                ValidationExpression="^([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})(\s)([0-1][0-9]|2[0-3])(:)([0-5][0-9])(:)([0-5][0-9])$" 
                                ValidationGroup="Ciudad" 
                                Display="Dynamic">
                            </asp:RegularExpressionValidator>
                         </div>                
                        <br />
                        <div class="buttonsPanel">
                    <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                        OnClick="SaveButton_Click"
                        ValidationGroup="Ciudad">
                        <span>Guardar</span>
                    </asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="btnlimpiar" runat="server" 
                                CssClass="buttone" OnClick="btnlimpiar_Click">
                        <span>Limpiar</span>
                    </asp:LinkButton>
                  
                </div>
                    </div>
                </asp:Panel>
                <div class="gridContainer">
                    <asp:GridView ID="NotificacionGridView" runat="server"
                        DataSourceID="NotificacionDataSource"
                        OnRowCommand="NotificacionGridView_RowCommand"
                        CssClass="grid" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="50px" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteButton" runat="server" CssClass="deleteRow"
                                        CommandName="DeleteRecord" CommandArgument='<%# Bind("Notificacionid") %>'>
                                        <asp:Image runat="server" ImageUrl="~/Images/Neutral/delete.png"
                                            ToolTip="Inactivar" />
                                    </asp:LinkButton>
                                </ItemTemplate>

<HeaderStyle Width="50px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="TipoNotificacion" DataField="TipoNotificacion" SortExpression="TipoNotificacion" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                            <asp:BoundField DataField="GrupoNotificacion" HeaderText="GrupoNotificacion" SortExpression="GrupoNotificacion" />
                            <asp:BoundField DataField="Fecha_Creacion" HeaderText="Fecha_Creacion" SortExpression="Fecha_Creacion" />
                            <asp:BoundField DataField="FechaStart" HeaderText="FechaStart" SortExpression="FechaStart" />
                            <asp:BoundField DataField="FechaEnd" HeaderText="FechaEnd" SortExpression="FechaEnd" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                            <asp:BoundField DataField="PrioridadElemento" HeaderText="PrioridadElemento" SortExpression="PrioridadElemento" />
                        </Columns>
                        <EmptyDataTemplate>
                            <p style="background-color:white">
                                No hay Notificaciones registrados en el sistema.
                            </p>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="NotificacionDataSource" runat="server"
                        TypeName="Artexacta.App.Notificacion.BLL.NotificacionBLL"
                        SelectMethod="getNotificacionListar" OldValuesParameterFormatString="original_{0}" OnSelected="NotificacionDataSource_Selected">
                    </asp:ObjectDataSource>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <asp:HiddenField ID="HiddenButton" runat="server" />
    <div id="dialog-confirm" title="Eliminar" style="display:none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            ¿Está seguro que quiere inactivar la notificación?
         </p>
    </div>

    <script type="text/javascript">
        $(function () {
            $(".deleteRow").click(function () {
                $("#<%= HiddenButton.ClientID %>").val($(this).attr("href"));
                $("#dialog-confirm").dialog("open");
                return false;
            });

            $("#dialog-confirm").dialog({
                resizable: false,
                height: 200,
                modal: true,
                autoOpen: false,
                buttons: {
                    "Aceptar": function () {
                        eval($("#<%= HiddenButton.ClientID %>").val());
                        $(this).dialog("close");
                    },
                    "Cancelar": function () {
                        $(this).dialog("close");
                    }
                }
            });
        });



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


            $('.button').click(function () {
                return confirm('¿Está seguro en grabar los datos?');
            });
        });

    </script>



</asp:Content>

