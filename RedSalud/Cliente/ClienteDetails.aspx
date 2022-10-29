
<%@ Page Title="Cliente" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ClienteDetails.aspx.cs" Inherits="Cliente_ClienteDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="left" style="max-width: 100%; width: 100%; padding: 10px 10px 10px 0px;">
        <telerik:RadTabStrip ID="ClienteTab" runat="server" MultiPageID="ClienteMP" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="Cliente" PageViewID="ClienteRPV"></telerik:RadTab>
                <telerik:RadTab Text="Prestaciones" ID="PrestacionesRT" runat="server"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>

        <telerik:RadMultiPage ID="ClienteMP" runat="server"
            CssClass="RadMultiPage"
            SelectedIndex="0">
            <telerik:RadPageView ID="ClienteRPV" runat="server">
                <div>
                    <div class="frame">
                        <div class="columnHead">
                            <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                                Text="Nuevo Cliente">
                            </asp:Label>
                        </div>
                        <div class="columnContent">
                            <div class="contentMenu">
                                <asp:HyperLink ID="HyperLink1" runat="server" Text="Volver a la Lista de Clientes"
                                    NavigateUrl="~/Cliente/ClienteList.aspx">
                                </asp:HyperLink>
                            </div>
                            <asp:HiddenField ID="CodigoRedMedicaHF" runat="server" />
                            <asp:FormView ID="ClienteFV" runat="server"
                                DataSourceID="ClienteODS">
                                <ItemTemplate>
                                    <asp:HiddenField ID="ClienteIdFVHF" runat="server" Value='<%# Bind("ClienteId") %>' />
                                    <asp:Panel runat="server"
                                        GroupingText="Agregar Red Medica al Cliente"
                                        DefaultButton="addRedMedicaRedCliente">
                                        <div>
                                            <span class="label">Asignar Red Medica</span>
                                            <asp:DropDownList ID="RedMedicaDDL" runat="server"
                                                DataSourceID="RedMedicaODS"
                                                DataTextField="Nombre"
                                                DataValueField="RedMedicaId">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="RedMedicaODS" runat="server"
                                                TypeName="Artexacta.App.RedMedica.BLL.RedMedicaBLL"
                                                SelectMethod="GetRedMedicaListxCodigo"
                                                OnSelected="RedMedicaODS_Selected">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="CodigoRedMedicaHF" Name="Codigo" PropertyName="Value" DbType="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>

                                            <div class="buttonsPanel">
                                                <asp:LinkButton ID="addRedMedicaRedCliente" runat="server" CssClass="button"
                                                    OnClick="addRedMedicaRedCliente_Click">
                                    <span>Agregar Red Medica</span>
                                                </asp:LinkButton>
                                            </div>
                                            <asp:GridView ID="RedClienteRedMedicaGV" runat="server"
                                                AutoGenerateColumns="false"
                                                DataSourceID="RedClienteRedMedicaODS"
                                                OnRowCommand="RedClienteRedMedicaGV_RowCommand">
                                                <EmptyDataTemplate>
                                                    <span>El Cliente no contiene Redes Medicas asignadas.</span>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-Height="26px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="DeleteButton" runat="server" Visible="false"
                                                                CssClass="deleteRow"
                                                                CommandName="DeleteRecord"
                                                                CommandArgument='<%# Bind("RedMedicaId") %>'>
                                                                <asp:Image ID="Image1" runat="server"
                                                                    ImageUrl="~/Images/Neutral/delete.png"
                                                                    ToolTip="Eliminar" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Codigo" HeaderText="Código" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre de la Red Medica" />
                                                </Columns>
                                            </asp:GridView>
                                    </asp:Panel>

                                    <asp:ObjectDataSource ID="RedClienteRedMedicaODS" runat="server"
                                        TypeName="Artexacta.App.RedMedica.BLL.RedMedicaBLL"
                                        SelectMethod="getRedMedicaListByClienteId"
                                        OnSelected="RedClienteRedMedicaODS_Selected">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    </div>

                        <div>
                            <span class="label">Código Cliente</span>
                            <asp:Label ID="CodigoTxt" runat="server"
                                Text='<%# Bind("CodigoCliente") %>'>
                            </asp:Label>

                            <span class="label">Nombre Jurídico</span>
                            <asp:Label ID="NombreTxt" runat="server"
                                Text='<%# Bind("NombreJuridico") %>'>
                            </asp:Label>

                            <span class="label">Nit</span>
                            <asp:Label ID="NitTxt" runat="server"
                                Text='<%# Bind("Nit") %>'>
                            </asp:Label>

                            <span class="label">Dirección</span>
                            <asp:Label ID="DireccionTxt" runat="server"
                                Text='<%# Bind("Direccion") %>'>
                            </asp:Label>

                            <span class="label">Teléfono 1</span>
                            <asp:Label ID="Telefono1Txt" runat="server"
                                Text='<%# Bind("Telefono1") %>'>
                            </asp:Label>

                            <span class="label">Teléfono 2</span>
                            <asp:Label ID="Telefono2Txt" runat="server"
                                Text='<%# Bind("Telefono2") %>'>
                            </asp:Label>

                            <span class="label">Nombre Contacto</span>
                            <asp:Label ID="NombrecontactoTxt" runat="server"
                                Text='<%# Bind("NombreContacto") %>'>
                            </asp:Label>

                            <span class="label">Email</span>
                            <asp:Label ID="EmailTxt" runat="server"
                                Text='<%# Bind("Email") %>'>
                            </asp:Label>

                            <span class="label">Costo de la consulta del internista</span>
                            <asp:Label ID="CostoConsultaInternistaTxt" runat="server"
                                Text='<%# Bind("CostoConsultaInternista") %>'>
                            </asp:Label>

                            <span class="label">Número de días hábiles para Reconsulta</span>
                            <asp:Label ID="NumeroDiasReconsultaTxt" runat="server"
                                Text='<%# Bind("NumeroDiasReconsulta") %>'>
                            </asp:Label>
                            <div style="width: 100%; height: 5px;"></div>
                            <asp:CheckBox ID="IsSOATCheckBox" Text="Cliente de SOAT" runat="server"
                                Checked='<%# Bind("IsSOAT") %>' Enabled="false" />
                            <div style="width: 100%; height: 5px;"></div>
                            <asp:CheckBox ID="IsGestionMedicaCheckBox" Text="Cliente de Gestión Médica" runat="server"
                                Checked='<%# Bind("IsGestionMedica") %>' Enabled="false" />
                            <div style="width: 100%; height: 5px;"></div>
                            <asp:CheckBox ID="IsDesgravamenCheckBox" Text="Cliente de Desgravamen" runat="server"
                                Checked='<%# Bind("IsDesgravamen") %>' Enabled="false" />
                            <div style="width: 100%; height: 5px;"></div>
                            <asp:CheckBox ID="CheckBox3" Text="Solo aceptar medicamentos del LINAME" runat="server"
                                Checked='<%# Bind("SoloLiname") %>' Enabled="false" />
                        </div>

                                    <div class="buttonsPanel">
                                        <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                                            CommandName="EDIT"
                                            ValidationGroup="RedMedica">
                                <span>Modificar</span>
                                        </asp:LinkButton>

                                        <asp:HyperLink ID="ReturnLB" runat="server"
                                            NavigateUrl="~/Cliente/ClienteList.aspx"
                                            Text="Volver a la lista de Clientes" />
                                    </div>
                                </ItemTemplate>
                                <InsertItemTemplate>
                                    <div>
                                        <span class="label">Código Cliente</span>
                                        <asp:TextBox ID="CodigoTxt" runat="server" CssClass="normalField"
                                            MaxLength="20"
                                            Text='<%# Bind("CodigoCliente") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RequiredFieldValidator ID="CodigoRFV" runat="server"
                                                ControlToValidate="CodigoTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Debe ingregar el Código del Cliente" />
                                        </div>

                                        <span class="label">Nombre Jurídico</span>
                                        <asp:TextBox ID="NombreTxt" runat="server" CssClass="normalField"
                                            MaxLength="250"
                                            Text='<%# Bind("NombreJuridico") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="NombreTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Debe ingregar el Nombre Jurídico del Cliente" />
                                        </div>

                                        <span class="label">Nit</span>
                                        <asp:TextBox ID="NitTxt" runat="server" CssClass="normalField"
                                            MaxLength="100"
                                            Text='<%# Bind("Nit") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RequiredFieldValidator ID="MontoRFV" runat="server"
                                                ControlToValidate="NitTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Debe ingregar el Nit del Cliente" />
                                            <asp:RegularExpressionValidator ID="MontoREV" runat="server"
                                                ControlToValidate="NitTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Solo permite números el Nit del Cliente y no puede ser cero."
                                                ValidationExpression="<% $ Resources:Validations, NitFormat %>" />
                                            <asp:CustomValidator ID="CVNit" runat="server"
                                                ErrorMessage="El Nit no puede ser menor o igual a cero."
                                                ControlToValidate="NitTxt"
                                                Display="Dynamic"
                                                ValidationGroup="RedMedica"
                                                ClientValidationFunction="CVNit_Validate" />
                                        </div>

                                        <span class="label">Dirección</span>
                                        <asp:TextBox ID="DireccionTxt" runat="server" CssClass="normalField"
                                            MaxLength="100"
                                            Text='<%# Bind("Direccion") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                                ControlToValidate="DireccionTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="No se permiten caracteres especiales para la dirección"
                                                ValidationExpression="<% $ Resources:Validations, DescriptionFormat %>" />
                                        </div>

                                        <span class="label">Telefono 1</span>
                                        <asp:TextBox ID="Telefono1Txt" runat="server" CssClass="normalField"
                                            MaxLength="20"
                                            Text='<%# Bind("Telefono1") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RegularExpressionValidator ID="Telefono1REV" runat="server"
                                                ControlToValidate="Telefono1Txt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Solo permite números para el telefono 1 del Cliente"
                                                ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                                        </div>

                                        <span class="label">Telefono 2</span>
                                        <asp:TextBox ID="Telefono2Txt" runat="server" CssClass="normalField"
                                            MaxLength="20"
                                            Text='<%# Bind("Telefono2") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                ControlToValidate="Telefono2Txt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Solo permite números el telefono 2 del Cliente"
                                                ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                                        </div>

                                        <span class="label">Nombre Contacto</span>
                                        <asp:TextBox ID="NombrecontactoTxt" runat="server" CssClass="normalField"
                                            MaxLength="250"
                                            Text='<%# Bind("NombreContacto") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                ControlToValidate="NombrecontactoTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="No se permiten caracteres especiales para el nombre del contacto del Cliente"
                                                ValidationExpression="<% $ Resources:Validations, DescriptionFormat %>" />
                                        </div>

                                        <span class="label">Email</span>
                                        <asp:TextBox ID="EmailTxt" runat="server" CssClass="normalField"
                                            MaxLength="250"
                                            Text='<%# Bind("Email") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                ControlToValidate="EmailTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="El Email no tiene el formato correcto"
                                                ValidationExpression="<% $ Resources:Validations, EMailFormat %>" />
                                        </div>

                                        <span class="label">Costo de la consulta del internista</span>
                                        <asp:TextBox ID="CostoConsultaInternistaTxt" runat="server" CssClass="normalField"
                                            MaxLength="5"
                                            Text='<%# Bind("CostoConsultaInternista") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="CostoConsultaInternistaTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Debe ingregar el Costo de la consulta del internista" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                                ControlToValidate="CostoConsultaInternistaTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Solo permite números el Costo de la consulta del internista"
                                                ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                                        </div>

                                        <span class="label">Número de Días hábiles para Reconsulta</span>
                                        <asp:TextBox ID="NumeroDiasReconsultaTxt" runat="server" CssClass="normalField"
                                            MaxLength="5"
                                            Text='<%# Bind("NumeroDiasReconsulta") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="NumeroDiasReconsultaTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Debe ingregar los números de días hábiles para Reconsulta" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                                ControlToValidate="NumeroDiasReconsultaTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Solo permite números en los números de días hábiles para Reconsulta"
                                                ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                                        </div>
                                        <div style="width: 100%; height: 5px;"></div>
                                        <asp:CheckBox ID="IsSOATCheckBox" Text="Cliente de SOAT" runat="server"
                                            Checked='<%# Bind("IsSOAT") %>' />
                                        <div style="width: 100%; height: 5px;"></div>
                                        <asp:CheckBox ID="IsGestionMedicaCheckBox" Text="Cliente de Gestión Médica" runat="server"
                                            Checked='<%# Bind("IsGestionMedica") %>' />
                                        <div style="width: 100%; height: 5px;"></div>
                                        <asp:CheckBox ID="IsDesgravamenCheckBox" Text="Cliente de Desgravamen" runat="server"
                                            Checked='<%# Bind("IsDesgravamen") %>' />
                                        <div style="width: 100%; height: 5px;"></div>
                                        <asp:CheckBox ID="NumeroDiasReconsultaCheckBox" Text="Solo aceptar medicamentos del LINAME" runat="server"
                                            Checked='<%# Bind("SoloLiname") %>' />
                                    </div>

                                    <div class="buttonsPanel">
                                        <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                                            CommandName="INSERT"
                                            ValidationGroup="RedMedica">
                        <span>Guardar</span>
                                        </asp:LinkButton>
                                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="secondaryButton"
                                            Text="Cancelar"
                                            NavigateUrl="~/Cliente/ClienteList.aspx">
                                        </asp:HyperLink>
                                    </div>
                                </InsertItemTemplate>

                                <EditItemTemplate>
                                    <asp:HiddenField ID="ClienteIdFVHF" runat="server" Value='<%# Bind("ClienteId") %>' />
                                    <div>
                                        <span class="label">Código Cliente</span>
                                        <asp:TextBox ID="CodigoTxt" runat="server" CssClass="normalField"
                                            MaxLength="20"
                                            Text='<%# Bind("CodigoCliente") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RequiredFieldValidator ID="CodigoRFV" runat="server"
                                                ControlToValidate="CodigoTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Debe ingregar el Código del Cliente" />
                                        </div>

                                        <span class="label">Nombre Jurídico</span>
                                        <asp:TextBox ID="NombreTxt" runat="server" CssClass="normalField"
                                            MaxLength="250"
                                            Text='<%# Bind("NombreJuridico") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="NombreTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Debe ingregar el Nombre Jurídico del Cliente" />
                                        </div>

                                        <span class="label">Nit</span>
                                        <asp:TextBox ID="NitTxt" runat="server" CssClass="normalField"
                                            MaxLength="100"
                                            Text='<%# Bind("Nit") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RequiredFieldValidator ID="MontoRFV" runat="server"
                                                ControlToValidate="NitTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Debe ingregar el Nit del Cliente" />
                                            <asp:RegularExpressionValidator ID="MontoREV" runat="server"
                                                ControlToValidate="NitTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Solo permite números el Nit del Cliente"
                                                ValidationExpression="<% $ Resources:Validations, NitFormat %>" />
                                            <asp:CustomValidator ID="CVNit" runat="server"
                                                ErrorMessage="El Nit no puede ser menor o igual a cero."
                                                ControlToValidate="NitTxt"
                                                Display="Dynamic"
                                                ValidationGroup="RedMedica"
                                                ClientValidationFunction="CVNit_Validate" />
                                        </div>

                                        <span class="label">Dirección</span>
                                        <asp:TextBox ID="DireccionTxt" runat="server" CssClass="normalField"
                                            MaxLength="100"
                                            Text='<%# Bind("Direccion") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                                ControlToValidate="DireccionTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="No se permiten caracteres especiales para la dirección"
                                                ValidationExpression="<% $ Resources:Validations, DescriptionFormat %>" />
                                        </div>

                                        <span class="label">Telefono 1</span>
                                        <asp:TextBox ID="Telefono1Txt" runat="server" CssClass="normalField"
                                            MaxLength="20"
                                            Text='<%# Bind("Telefono1") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RegularExpressionValidator ID="Telefono1REV" runat="server"
                                                ControlToValidate="Telefono1Txt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Solo permite números para el telefono 1 del Cliente"
                                                ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                                        </div>

                                        <span class="label">Telefono 2</span>
                                        <asp:TextBox ID="Telefono2Txt" runat="server" CssClass="normalField"
                                            MaxLength="20"
                                            Text='<%# Bind("Telefono2") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                ControlToValidate="Telefono2Txt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Solo permite números el telefono 2 del Cliente"
                                                ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                                        </div>

                                        <span class="label">Nombre Contacto</span>
                                        <asp:TextBox ID="NombrecontactoTxt" runat="server" CssClass="normalField"
                                            MaxLength="250"
                                            Text='<%# Bind("NombreContacto") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                ControlToValidate="NombrecontactoTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="No se permiten caracteres especiales para el nombre del contacto del Cliente."
                                                ValidationExpression="<% $ Resources:Validations, DescriptionFormat %>" />
                                        </div>

                                        <span class="label">Email</span>
                                        <asp:TextBox ID="EmailTxt" runat="server" CssClass="normalField"
                                            MaxLength="250"
                                            Text='<%# Bind("Email") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                ControlToValidate="EmailTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="El Email no tiene el formato correcto"
                                                ValidationExpression="<% $ Resources:Validations, EMailFormat %>" />
                                        </div>

                                        <span class="label">Costo de la consulta del internista</span>
                                        <asp:TextBox ID="CostoConsultaInternistaTxt" runat="server" CssClass="normalField"
                                            MaxLength="5"
                                            Text='<%# Bind("CostoConsultaInternista") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="CostoConsultaInternistaTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Debe ingregar el Costo de la consulta del internista" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                                ControlToValidate="CostoConsultaInternistaTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Solo permite números el Costo de la consulta del internista"
                                                ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                                        </div>

                                        <span class="label">Número de Días hábiles para Reconsulta</span>
                                        <asp:TextBox ID="NumeroDiasReconsultaTxt" runat="server" CssClass="normalField"
                                            MaxLength="5"
                                            Text='<%# Bind("NumeroDiasReconsulta") %>'>
                                        </asp:TextBox>
                                        <div class="validators">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="NumeroDiasReconsultaTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Debe ingregar los números de días hábiles para Reconsulta" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                                ControlToValidate="NumeroDiasReconsultaTxt"
                                                ValidationGroup="RedMedica"
                                                Display="Dynamic"
                                                ErrorMessage="Solo permite números en los números de días hábiles para Reconsulta"
                                                ValidationExpression="<% $ Resources:Validations, NumberFormat %>" />
                                        </div>
                                        <div style="width: 100%; height: 5px;"></div>
                                        <asp:CheckBox ID="IsSOATCheckBox" Text="Cliente de SOAT" runat="server"
                                            Checked='<%# Bind("IsSOAT") %>' />
                                        <div style="width: 100%; height: 5px;"></div>
                                        <asp:CheckBox ID="IsGestionMedicaCheckBox" Text="Cliente de Gestión Médica" runat="server"
                                            Checked='<%# Bind("IsGestionMedica") %>' />
                                        <div style="width: 100%; height: 5px;"></div>
                                        <asp:CheckBox ID="IsDesgravamenCheckBox" Text="Cliente de Desgravamen" runat="server"
                                            Checked='<%# Bind("IsDesgravamen") %>' />
                                        <div style="width: 100%; height: 5px;"></div>
                                        <asp:CheckBox ID="NumeroDiasReconsultaCheckBox" Text="Solo aceptar medicamentos del LINAME" runat="server"
                                            Checked='<%# Bind("SoloLiname") %>' />
                                    </div>

                                    <div class="buttonsPanel">
                                        <asp:LinkButton ID="SaveButton" runat="server" CssClass="button"
                                            CommandName="UPDATE"
                                            ValidationGroup="RedMedica">
                        <span>Guardar</span>
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="CancelLB" Text="Cancelar" runat="server"
                                            CssClass="secondaryButton"
                                            CommandName="CANCEL" />
                                    </div>
                                </EditItemTemplate>
                            </asp:FormView>
                            <asp:ObjectDataSource ID="ClienteODS" runat="server"
                                TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                                SelectMethod="GetRedClienteByClienteId"
                                InsertMethod="InsertRedCliente"
                                UpdateMethod="UpdateRedCliente"
                                OnSelected="ClienteODS_Selected"
                                OnInserted="ClienteODS_Inserted"
                                OnUpdated="ClienteODS_Updated">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>

                        <asp:HiddenField ID="ClienteIdHF" runat="server" />

                        <asp:HiddenField ID="HiddenButton" runat="server" />
                        <div id="dialog-confirm" title="Eliminar" style="display: none">
                            <p>
                                <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
                                Esta seguro que desea eliminar la Red Medica seleccionada?
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
                             height: 140,
                             modal: true,
                             autoOpen: false,
                             buttons: {
                                 "Eliminar": function () {
                                     eval($("#<%= HiddenButton.ClientID %>").val());
                                  $(this).dialog("close");
                              },
                               "Cancelar": function () {
                                   $(this).dialog("close");
                               }
                           }
                        });
                     });
                        </script>
                        <script type="text/javascript">
                            function CVNit_Validate(sender, args) {
                                args.IsValid = true;
                                var Nit = $('#<%= ClienteFV.FindControl("NitTxt").ClientID %>').val();
                        if (Nit <= 0) {
                            args.IsValid = false;
                        }
                    }
                        </script>
                    </div>
                </div>
            </telerik:RadPageView>

            <telerik:RadPageView ID="PrestacionesRPV" runat="server">
                <div>
                    <asp:Label 
                        ID="PrestacionesAlert" 
                        Visible="false" 
                        runat="server" 
                        ForeColor="Red"
                        Text="No se puede ingresar el monto y el porcentaje para el CoPago.">
                    </asp:Label>
                    <telerik:RadGrid ID="ClientePrestacionesRadGrid" runat="server"
                        AutoGenerateColumns="false"
                        DataSourceID="ClientePrestacionesODS"
                        AllowPaging="true"
                        PageSize="20"
                        OnPreRender="ClientePrestacionesRadGrid_PreRender"
                        OnItemCreated="ClientePrestacionesRadGrid_ItemCreated"
                        OnItemCommand="ClientePrestacionesRadGrid_ItemCommand"
                        OnUpdateCommand="ClientePrestacionesRadGrid_UpdateCommand"
                        Visible="true">
                        <MasterTableView DataKeyNames="preId, TipoPrestacion"
                            EditMode="InPlace"
                            EditFormSettings-EditColumn-CancelText="Cancelar"
                            EditFormSettings-EditColumn-UpdateText="Actualizar">
                            <NoRecordsTemplate>
                                <asp:Label runat="server" Text="No existen prestaciones configuradas para el cliente seleccionado"></asp:Label>
                            </NoRecordsTemplate>
                            <Columns>
                                <telerik:GridEditCommandColumn EditText="Seleccionar" CancelText="Cancelar" UpdateText="Guardar" />
                                <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                    HeaderText="Eliminar"
                                    CommandName="Delete"
                                    ButtonType="ImageButton"
                                    ItemStyle-Width="40px"
                                    ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="40px"
                                    HeaderStyle-HorizontalAlign="Center"
                                    ImageUrl="~/Images/neutral/delete.png"
                                    ConfirmText="¿Está seguro que desea Eliminar esta configuración de prestación para el Cliente seleccionado?" />

                                <telerik:GridBoundColumn DataField="Prestacion" UniqueName="Prestacion" HeaderText="Prestacion" ReadOnly="True" />
                                <telerik:GridNumericColumn DataField="Precio" UniqueName="Precio" HeaderText="Precio" DataType="System.Decimal" DataFormatString="{0:#,##0.00}"/>
                                <telerik:GridNumericColumn DataField="CoPagoMonto" UniqueName="CoPagoMonto" HeaderText="Monto CoPago" DataType="System.Decimal" DataFormatString="{0:#,##0.00}"/>
                                <telerik:GridNumericColumn DataField="CoPagoPorcentaje" UniqueName="CoPagoPorcentaje" HeaderText="% CoPago" DataType="System.Decimal" DataFormatString="{0:#,##0.00}"/>
                                <telerik:GridNumericColumn DataField="CantidadConsultasAno" UniqueName="CantidadConsultasAno" HeaderText="Cant.Consultas Año" DataType="System.Int32" />
                                <telerik:GridNumericColumn DataField="DiasCarencia" UniqueName="DiasCarencia" HeaderText="Días de Carencia" DataType="System.Int32" />
                                <telerik:GridNumericColumn DataField="MontoTope" UniqueName="MontoTope" HeaderText="Monto Tope" DataType="System.Decimal" DataFormatString="{0:#,##0.00}"/>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ClientePrestacionesODS" runat="server"
                        TypeName="Artexacta.App.RedClientePrestaciones.BLL.RedClientePrestacionesBLL"
                        OldValuesParameterFormatString="original_{0}"
                        UpdateMethod="UpdateClientePrestaciones"
                        SelectMethod="GetAllClientePrestaciones">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <div style="width: 100%; border-bottom: 1px solid black; margin: 20px 0px 20px 0px;">
                        <span style="font-weight: bold;">Lista de Ítems Habilitados para el Cliente por Prestación</span>
                    </div>

                    <div style="width: 100%; margin-bottom: 5px;">
                        <span style="font-weight: bold;"></span>
                    </div>

                    <%--ESPECIALIDADES--%>
                    <asp:Panel runat="server" ID="EspecialidadesPNL" Visible="false">
                        <div style="width: 100%; margin-bottom: 7px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" CssClass="label">Ítem de Prestación</asp:Label>
                                        <asp:DropDownList ID="EspecialidadesDDL" runat="server"
                                            DataSourceID="EspecialidadesNotSavedODS"
                                            DataTextField="Especialidad"
                                            DataValueField="EspecialidadId">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" CssClass="label">Frecuencia</asp:Label>
                                        <asp:DropDownList ID="FrecuenciaDDL" runat="server">
                                            <asp:ListItem Text="Mes" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Año" Value="A"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" CssClass="label">Cant. video llamadas</asp:Label>
                                        <asp:TextBox runat="server" ID="txtCantVideoLLamadas" MaxLength="3"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="AgregarEspecialidadBTN" Text="Agregar" OnClick="AgregarEspecialidadBTN_Click" />
                                    </td>
                                </tr>
                            </table>

                            <asp:ObjectDataSource ID="EspecialidadesNotSavedODS" runat="server"
                                TypeName="Artexacta.App.RedClientePrestaciones.BLL.RedEspecialidadPrestacionesBLL"
                                SelectMethod="GetEspecialidadPrestacionesNotSaved">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                                    <asp:Parameter Name="NotSaved" Type="Boolean" DefaultValue="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            
                        </div>

                        <telerik:RadGrid ID="EspecialidadPrestacionesRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            DataSourceID="EspecialidadesSavedODS"
                            AllowPaging="true"
                            PageSize="20"
                            OnItemCommand="EspecialidadPrestacionesRadGrid_ItemCommand"
                            Visible="true">
                            <MasterTableView DataKeyNames="detId, EspecialidadId">
                                <NoRecordsTemplate>
                                    <asp:Label runat="server" Text="No existen especialidades configuradas para la prestacion seleccionada"></asp:Label>
                                </NoRecordsTemplate>
                                <Columns>
                                    <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                        HeaderText="Eliminar"
                                        CommandName="Delete"
                                        ButtonType="ImageButton"
                                        ItemStyle-Width="40px"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="40px"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ImageUrl="~/Images/neutral/delete.png"
                                        ConfirmText="¿Está seguro que desea Eliminar esta configuración de especialidad para la prestacion seleccionada?" />

                                    <telerik:GridBoundColumn DataField="Especialidad" UniqueName="Especialidad" HeaderText="Especialidad" ReadOnly="True" />
                                    <telerik:GridBoundColumn DataField="DescripcionCantFrecuencia" UniqueName="DescripcionCantFrecuencia" HeaderText="Video LLamadas" ReadOnly="True" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>

                        <asp:ObjectDataSource ID="EspecialidadesSavedODS" runat="server"
                            TypeName="Artexacta.App.RedClientePrestaciones.BLL.RedEspecialidadPrestacionesBLL"
                            SelectMethod="GetEspecialidadPrestacionesNotSaved">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                                <asp:Parameter Name="NotSaved" Type="Boolean" DefaultValue="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </asp:Panel>

                    <%--GRUPOS LABORATORIOS--%>
                    <asp:Panel runat="server" ID="GruposLabPNL" Visible="false">
                        <div style="width: 100%; margin-bottom: 7px">
                            <asp:DropDownList ID="GruposLabDDL" runat="server"
                                DataSourceID="GruposLabNotSavedODS"
                                DataTextField="Estudio"
                                DataValueField="EstudioId">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="GruposLabNotSavedODS" runat="server"
                                TypeName="Artexacta.App.RedClientePrestaciones.BLL.RedGruposLabPrestacionesBLL"
                                SelectMethod="GetGruposLabPrestacionesNotSaved">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                                    <asp:Parameter Name="NotSaved" Type="Boolean" DefaultValue="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <asp:Button runat="server" ID="AgregarGruposLabBTN" Text="Agregar" OnClick="AgregarGruposLabBTN_Click" />
                        </div>

                        <telerik:RadGrid ID="GruposLabPrestacionesRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            DataSourceID="GruposLabSavedODS"
                            AllowPaging="true"
                            PageSize="20"
                            OnItemCommand="GruposLabPrestacionesRadGrid_ItemCommand"
                            Visible="true">
                            <MasterTableView DataKeyNames="detId, EstudioId">
                                <NoRecordsTemplate>
                                    <asp:Label runat="server" Text="No existen grupos de laboratorio configuradas para la prestacion seleccionada"></asp:Label>
                                </NoRecordsTemplate>
                                <Columns>
                                    <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                        HeaderText="Eliminar"
                                        CommandName="Delete"
                                        ButtonType="ImageButton"
                                        ItemStyle-Width="40px"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="40px"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ImageUrl="~/Images/neutral/delete.png"
                                        ConfirmText="¿Está seguro que desea Eliminar esta configuración de grupo de laboratorio para la prestacion seleccionada?" />

                                    <telerik:GridBoundColumn DataField="Estudio" UniqueName="Estudio" HeaderText="Estudio" ReadOnly="True" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>

                        <asp:ObjectDataSource ID="GruposLabSavedODS" runat="server"
                            TypeName="Artexacta.App.RedClientePrestaciones.BLL.RedGruposLabPrestacionesBLL"
                            SelectMethod="GetGruposLabPrestacionesNotSaved">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                                <asp:Parameter Name="NotSaved" Type="Boolean" DefaultValue="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </asp:Panel>

                    <%--IMAGENOLOGIA--%>
                    <asp:Panel runat="server" ID="ImagenPNL" Visible="false">
                        <div style="width: 100%; margin-bottom: 7px">
                            <asp:DropDownList ID="ImagenDDL" runat="server"
                                DataSourceID="ImagenNotSavedODS"
                                DataTextField="Estudio"
                                DataValueField="EstudioId">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ImagenNotSavedODS" runat="server"
                                TypeName="Artexacta.App.RedClientePrestaciones.BLL.RedImagenPrestacionesBLL"
                                SelectMethod="GetImagenPrestacionesNotSaved">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                                    <asp:Parameter Name="NotSaved" Type="Boolean" DefaultValue="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <asp:Button runat="server" ID="AgregarImagenBTN" Text="Agregar" OnClick="AgregarImagenBTN_Click" />
                        </div>

                        <telerik:RadGrid ID="ImagenPrestacionesRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            DataSourceID="ImagenSavedODS"
                            AllowPaging="true"
                            PageSize="20"
                            OnItemCommand="ImagenPrestacionesRadGrid_ItemCommand"
                            Visible="true">
                            <MasterTableView DataKeyNames="detId, EstudioId">
                                <NoRecordsTemplate>
                                    <asp:Label runat="server" Text="No existen imagenologias configuradas para la prestacion seleccionada"></asp:Label>
                                </NoRecordsTemplate>
                                <Columns>
                                    <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                        HeaderText="Eliminar"
                                        CommandName="Delete"
                                        ButtonType="ImageButton"
                                        ItemStyle-Width="40px"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="40px"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ImageUrl="~/Images/neutral/delete.png"
                                        ConfirmText="¿Está seguro que desea Eliminar esta configuración de imagenologia para la prestacion seleccionada?" />

                                    <telerik:GridBoundColumn DataField="Estudio" UniqueName="Estudio" HeaderText="Estudio" ReadOnly="True" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>

                        <asp:ObjectDataSource ID="ImagenSavedODS" runat="server"
                            TypeName="Artexacta.App.RedClientePrestaciones.BLL.RedImagenPrestacionesBLL"
                            SelectMethod="GetImagenPrestacionesNotSaved">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                                <asp:Parameter Name="NotSaved" Type="Boolean" DefaultValue="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </asp:Panel>

                    <%--CIRUGIA--%>
                    <asp:Panel runat="server" ID="CirugiasPNL" Visible="false">
                        <div style="width: 100%; margin-bottom: 7px">
                            <telerik:RadComboBox ID="CirugiasDDL" runat="server"
                                DataSourceID="CirugiasNotSavedODS"
                                Filter="StartsWith"
                                EnableLoadOnDemand="true"
                                DataTextField="CodigoArancelario"
                                DataValueField="CodigoArancelarioId">
                            </telerik:RadComboBox>
                            <asp:ObjectDataSource ID="CirugiasNotSavedODS" runat="server"
                                TypeName="Artexacta.App.RedClientePrestaciones.BLL.RedCirugiasPrestacionesBLL"
                                SelectMethod="GetCirugiasPrestacionesNotSaved">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                                    <asp:Parameter Name="NotSaved" Type="Boolean" DefaultValue="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <asp:Button runat="server" ID="AgregarCirugiasBTN" Text="Agregar" OnClick="AgregarCirugiasBTN_Click" />
                        </div>

                        <telerik:RadGrid ID="CirugiasPrestacionesRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            DataSourceID="CirugiasSavedODS"
                            AllowPaging="true"
                            PageSize="20"
                            OnItemCommand="CirugiasPrestacionesRadGrid_ItemCommand"
                            OnUpdateCommand="CirugiasPrestacionesRadGrid_UpdateCommand"
                            OnItemCreated="CirugiasPrestacionesRadGrid_ItemCreated"
                            Visible="true">
                            <MasterTableView DataKeyNames="detId, CodigoArancelarioId"
                                EditMode="InPlace">
                                <NoRecordsTemplate>
                                    <asp:Label runat="server" Text="No existen cirugias configuradas para la prestacion seleccionada"></asp:Label>
                                </NoRecordsTemplate>
                                <Columns>
                                    <telerik:GridEditCommandColumn EditText="Seleccionar" CancelText="Cancelar" UpdateText="Guardar" />
                                    <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                        HeaderText="Eliminar"
                                        CommandName="Delete"
                                        ButtonType="ImageButton"
                                        ItemStyle-Width="40px"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="40px"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ImageUrl="~/Images/neutral/delete.png"
                                        ConfirmText="¿Está seguro que desea Eliminar esta configuración de cirugias para la prestacion seleccionada?" />

                                    <telerik:GridBoundColumn DataField="CodigoArancelarioId" UniqueName="CodigoArancelarioId" HeaderText="Codigo Arancelario" ReadOnly="True" />
                                    <telerik:GridBoundColumn DataField="CodigoArancelario" UniqueName="CodigoArancelario" HeaderText="Cirugía" ReadOnly="True" />
                                    <telerik:GridNumericColumn DataField="detMontoTope" UniqueName="detMontoTope" HeaderText="Monto Tope" DataType="System.Decimal" />
                                    <telerik:GridNumericColumn DataField="detCantidadTope" UniqueName="detCantidadTope" HeaderText="Cant. Tope" DataType="System.Int32" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>

                        <asp:ObjectDataSource ID="CirugiasSavedODS" runat="server"
                            TypeName="Artexacta.App.RedClientePrestaciones.BLL.RedCirugiasPrestacionesBLL"
                            UpdateMethod="UpdateCirugiasPrestaciones"
                            SelectMethod="GetCirugiasPrestacionesNotSaved">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                                <asp:Parameter Name="NotSaved" Type="Boolean" DefaultValue="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </asp:Panel>

                    <%--ODONTOLOGIA--%>
                    <asp:Panel runat="server" ID="OdontoPNL" Visible="false">
                        <div style="width: 100%; margin-bottom: 7px">
                            <asp:DropDownList ID="OdontoDDL" runat="server"
                                DataSourceID="OdontoNotSavedODS"
                                DataTextField="PrestacionOdontologica"
                                DataValueField="PrestacionOdontologicaId">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="OdontoNotSavedODS" runat="server"
                                TypeName="Artexacta.App.RedClientePrestaciones.BLL.RedOdontoPrestacionesBLL"
                                SelectMethod="GetOdontoPrestacionesNotSaved">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                                    <asp:Parameter Name="NotSaved" Type="Boolean" DefaultValue="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <asp:Button runat="server" ID="AgregarOdontoBTN" Text="Agregar" OnClick="AgregarOdontoBTN_Click" />
                        </div>

                        <telerik:RadGrid ID="OdontoPrestacionesRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            DataSourceID="OdontoSavedODS"
                            AllowPaging="true"
                            PageSize="20"
                            OnItemCommand="OdontoPrestacionesRadGrid_ItemCommand"
                            OnUpdateCommand="OdontoPrestacionesRadGrid_UpdateCommand"
                            OnItemCreated="OdontoPrestacionesRadGrid_ItemCreated"
                            Visible="true">
                            <MasterTableView DataKeyNames="detId, PrestacionOdontologicaId"
                                EditMode="InPlace">
                                <NoRecordsTemplate>
                                    <asp:Label runat="server" Text="No existen odontologias configuradas para la prestacion seleccionada"></asp:Label>
                                </NoRecordsTemplate>
                                <Columns>
                                    <telerik:GridEditCommandColumn EditText="Seleccionar" CancelText="Cancelar" UpdateText="Guardar" />
                                    <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                        HeaderText="Eliminar"
                                        CommandName="Delete"
                                        ButtonType="ImageButton"
                                        ItemStyle-Width="40px"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="40px"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ImageUrl="~/Images/neutral/delete.png"
                                        ConfirmText="¿Está seguro que desea Eliminar esta configuración de odontologia para la prestacion seleccionada?" />

                                    <telerik:GridBoundColumn DataField="PrestacionOdontologica" UniqueName="PrestacionOdontologica" HeaderText="Prestacion Odontologica" ReadOnly="True" />
                                    <telerik:GridNumericColumn DataField="detCantidadConsultasAno" UniqueName="detCantidadConsultasAno" HeaderText="Cant.Consultas Año" DataType="System.Int32" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <asp:ObjectDataSource ID="OdontoSavedODS" runat="server"
                            TypeName="Artexacta.App.RedClientePrestaciones.BLL.RedOdontoPrestacionesBLL"
                            UpdateMethod="UpdateOdontoPrestaciones"
                            SelectMethod="GetOdontoPrestacionesNotSaved">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                                <asp:Parameter Name="NotSaved" Type="Boolean" DefaultValue="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </asp:Panel>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
</asp:Content>

