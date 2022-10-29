<%@ Page Title="Paciente" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PacienteList.aspx.cs" Inherits="Paciente_PacienteList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label1" Text="Lista de Pacientes" runat="server" CssClass="title" />
            </div>
            <div class="contentMenu">
                <asp:HyperLink ID="HyperLink1" runat="server"
                    Text="Agregar un nuevo Paciente"
                    NavigateUrl="~/Paciente/PacienteDetails.aspx" />
            </div>
            <asp:Panel runat="server" DefaultButton="SearchLB">
                <asp:Label ID="Label6" Text="Buscar Paciente por nombre, apellido o CI" runat="server" CssClass="label" />
                <asp:TextBox ID="SearchTexbox" runat="server" CssClass="biggerField" />

                <div class="buttonsPanel">
                    <asp:LinkButton ID="SearchLB" Text="" runat="server"
                        CssClass="button"
                        ValidationGroup="SearchCaso"
                        OnClick="SearchLB_Click">
                        <asp:Label ID="Label7" Text="Buscar" runat="server" />
                    </asp:LinkButton>
                </div>
            </asp:Panel>

            <telerik:RadGrid ID="PacienteRadGrid" runat="server"
                AutoGenerateColumns="false"
                DataSourceID="PacienteODS"
                AllowPaging="true"
                PageSize="20"
                OnItemCommand="PacienteRadGrid_ItemCommand"
                Visible="false"
                MasterTableView-DataKeyNames="PacienteId">
                <MasterTableView>
                    <NoRecordsTemplate>
                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    </NoRecordsTemplate>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Editar">
                            <ItemTemplate>
                                <asp:ImageButton ID="DetailsImageButton" runat="server"
                                    ImageUrl="~/Images/Neutral/select.png"
                                    CommandArgument='<%# Bind("PacienteId") %>'
                                    Width="24px" CommandName="Select"
                                    ToolTip="Editar"></asp:ImageButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                            HeaderText="Eliminar"
                            CommandName="Eliminar"
                            ButtonType="ImageButton"
                            ItemStyle-Width="40px"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="40px"
                            HeaderStyle-HorizontalAlign="Center"
                            ImageUrl="~/Images/neutral/delete.png"
                            ConfirmText="¿Está seguro que desea eliminar el Paciente?" />
                        
                        <telerik:GridBoundColumn DataField="NombreForDisplay" HeaderText="Nombres y Apellidos (ID)" />
                        <telerik:GridBoundColumn DataField="Nombre" HeaderText="Nombre" Visible="false" />
                        <%--<telerik:GridBoundColumn DataField="Apellido" HeaderText="Apellido" Visible="false" />--%>
                        <telerik:GridBoundColumn DataField="FechaNacimiento" HeaderText="Fecha Nacimiento" DataFormatString="{0:d}" />
                        <telerik:GridBoundColumn DataField="CarnetIdentidad" HeaderText="CI" />
                        <telerik:GridBoundColumn DataField="Direccion" HeaderText="Dirección" />
                        <telerik:GridBoundColumn DataField="Telefono" HeaderText="Telefono" />
                        <telerik:GridBoundColumn DataField="LugarTrabajo" HeaderText="Lugar de trabajo" />
                        <telerik:GridBoundColumn DataField="TelefonoTrabajo" HeaderText="Telefono de trabajo" />
                        <telerik:GridBoundColumn DataField="EstadoCivil" HeaderText="Estado Civil" />
                        <telerik:GridBoundColumn DataField="NroHijos" HeaderText="Nro. de Hijos" />
                        <telerik:GridBoundColumn DataField="Email" HeaderText="Email" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

            <asp:ObjectDataSource ID="PacienteODS" runat="server"
                TypeName="Artexacta.App.Paciente.BLL.PacienteBLL"
                OldValuesParameterFormatString="original_{0}"
                SelectMethod="SearchPaciente"
                OnSelected="PacienteODS_Selected">
                <SelectParameters>
                    <asp:ControlParameter ControlID="SearchTexbox" Name="Search" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>

