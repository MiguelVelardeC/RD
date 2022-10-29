<%@ Page Title="Video llamadas" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CitasVideoLLamadasList.aspx.cs"
    Inherits="CitasVideoLLamadasList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label1" Text="Lista de Video LLamadas" runat="server" CssClass="title" />
            </div>
            <div class="contentMenu">
            </div>
            <asp:Panel runat="server" DefaultButton="SearchLB">
                <table>
                    <tr>
                        <td>
                            <asp:Label Text="Cliente" runat="server"
                                CssClass="label left" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ClienteDDL" runat="server"
                                DataSourceID="ClienteODS"
                                Style="width: 350px; height: 20px;"
                                DataValueField="ClienteId"
                                DataTextField="NombreJuridico"
                                AutoPostBack="false">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ClienteODS" runat="server"
                                TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                                OldValuesParameterFormatString="original_{0}"
                                SelectMethod="getRedClienteList" />
                        </td>
                        <td style="width: 40px;"></td>
                        <td>
                            <asp:Label Text="Paciente" runat="server" Width="130px" Height="20px"
                                CssClass="label left" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtBusPacienteId" Width="250px" Height="20px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" CssClass="label left" Text="Médico"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="medicoId" runat="server"
                                AutoPostBack="false"
                                Width="350px"
                                Height="20px">
                                <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td>
                            <asp:Label Text="Nro Póliza" runat="server" Height="20px"
                                CssClass="label left" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtBusNroPoliza" Width="250px" Height="20px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                            <asp:Label runat="server" CssClass="label left" Text="Ciudad"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="CiudadDDL" runat="server"
                                ClientIDMode="Static"
                                DataSourceID="CiudadODS"
                                DataValueField="CiudadId"
                                DataTextField="Nombre"
                                EmptyMessage="[Todas]"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="CiudadDDL_SelectedIndexChanged"
                                CssClass="bigField left">
                            </telerik:RadComboBox>
                            <asp:ObjectDataSource ID="CiudadODS" runat="server"
                                TypeName="Artexacta.App.Ciudad.BLL.CiudadBLL"
                                OldValuesParameterFormatString="{0}"
                                SelectMethod="getCiudadList"
                                OnSelected="CiudadODS_Selected"></asp:ObjectDataSource>
                        </td>
                        <td></td>
                        <td>
                            <asp:Label runat="server" CssClass="label left" Text="Rango de fechas"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="FechaInicio" Value="" runat="server" DateInput-EmptyMessage="Fecha Inicial" Width="250px" Height="20px"></telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <telerik:RadDatePicker ID="FechaFin" Value="" runat="server" DateInput-EmptyMessage="Fecha Final" Width="250px" Height="20px"></telerik:RadDatePicker>
                        </td>
                    </tr>
                </table>
                
                <div class="buttonsPanel">
                    <asp:LinkButton ID="SearchLB" Text="" runat="server"
                        CssClass="button"
                        ValidationGroup="SearchCaso">
                        <asp:Label ID="Label7" Text="Buscar" runat="server" />
                    </asp:LinkButton>
                </div>
            </asp:Panel>
            <asp:GridView ID="ClienteGV" runat="server"
                        DataSourceID="CitasVideoLLamadaODS"
                        OnRowCommand="ClienteGV_RowCommand"
                        CssClass="grid" Width="100%" AutoGenerateColumns="false">
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" Text="No existen citas de video llamadas" runat="server" />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="15px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton" runat="server"
                                        ImageUrl="~/Images/Neutral/select.png"
                                        CommandName="VerDetalle"
                                        CommandArgument='<%# Bind("citId") %>'
                                        ToolTip="Editar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="Medico" HeaderText="Médico" />
                            <asp:BoundField DataField="NroPoliza" HeaderText="Nro de poliza" />
                            <asp:BoundField DataField="Asegurado" HeaderText="Asegurado" />
                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de registro" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                            <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
                        </Columns>
                    </asp:GridView>
            <%--(int clienteId, int medicoId, string nroPoliza, string paciente, string ciudad, DateTime fechaInicial, DateTime fechaFinal)--%>
            <asp:ObjectDataSource ID="CitasVideoLLamadaODS" runat="server"
                TypeName="Artexacta.App.CitasVideoLLamada.BLL.CitaVideoLLamadaBLL"
                OldValuesParameterFormatString="original_{0}"
                SelectMethod="SearchCitasVideoLLamada"
                OnSelected="CitasVideoLLamadaODS_Selected">
                <SelectParameters>
                    
                    <asp:ControlParameter ControlID="ClienteDDL" Name="clienteId" DefaultValue="0" />
                    <asp:ControlParameter ControlID="medicoId" Name="medicoId" DefaultValue="0"/>
                    <asp:ControlParameter ControlID="txtBusNroPoliza" Name="nroPoliza" DefaultValue="%"/>
                    <asp:ControlParameter ControlID="txtBusPacienteId" Name="paciente" DefaultValue="%"/>
                    <asp:ControlParameter ControlID="CiudadDDL" Name="ciudad" DefaultValue="%"/>
                    <asp:ControlParameter ControlID="FechaInicio" Name="fechaInicial" DefaultValue="1900-01-01"/>
                    <asp:ControlParameter ControlID="FechaFin" Name="fechaFinal" DefaultValue="2099-01-01"/>
                    <%--<asp:ControlParameter ControlID="SearchTexbox" Name="Search" PropertyName="Text" Type="String" />--%>
                </SelectParameters>
            </asp:ObjectDataSource>

        </div>
    </div>
</asp:Content>

