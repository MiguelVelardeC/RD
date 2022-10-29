<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvancedForm.ascx.cs" Inherits="TestCalendarioAdvancedForm" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="rsAdvancedEdit rsAdvancedModal" style="position: relative">
    <div class="rsModalBgTopLeft">
    </div>
    <div class="rsModalBgTopRight">
    </div>
    <div class="rsModalBgBottomLeft">
    </div>
    <div class="rsModalBgBottomRight">
    </div>
    <%-- Title bar. --%>
    <div class="rsAdvTitle">
        <%-- The rsAdvInnerTitle element is used as a drag handle when the form is modal. --%>
        <h1 class="rsAdvInnerTitle">
            <%= Owner.Localization.AdvancedEditAppointment %>
    </h1>
        <asp:LinkButton runat="server" ID="AdvancedEditCloseButton" CssClass="rsAdvEditClose"
            CommandName="Cancel" CausesValidation="false" ToolTip='<%# Owner.Localization.AdvancedClose %>'>
            <%= Owner.Localization.AdvancedClose %>
        </asp:LinkButton>
    </div>
    <div class="rsAdvContentWrapper">
        <%-- Scroll container - when the form height exceeds MaximumHeight scrollbars will appear on this element--%>
        <div class="rsAdvOptionsScroll">
            <asp:Panel runat="server" ID="AdvancedEditOptionsPanel" CssClass="rsAdvOptions">
                <asp:Panel runat="server" ID="BasicControlsPanel" CssClass="rsAdvBasicControls" OnDataBinding="BasicControlsPanel_DataBinding">
                    <telerik:RadTextBox runat="server" ID="SubjectText" Width="100%" Label='<%# Owner.Localization.AdvancedSubject + ":" %>' EnableSingleInputRendering="false"/>
                    <asp:RequiredFieldValidator runat="server" ID="SubjectValidator" ControlToValidate="SubjectText"
                        EnableClientScript="true" Display="None" CssClass="rsValidatorMsg" />
                </asp:Panel>

                <telerik:RadTextBox runat="server" ID="DescriptionText" TextMode="MultiLine" Columns="50"
                    Rows="5" Width="100%" Label='<%# Owner.Localization.AdvancedDescription + ":" %>'
                    Text='<%# Eval("Description") %>' EnableSingleInputRendering="false"/>
                
            </asp:Panel>
        </div>
        <asp:Panel runat="server" ID="ButtonsPanel" CssClass="rsAdvancedSubmitArea">
            <div class="rsAdvButtonWrapper">
                <asp:LinkButton runat="server" ID="UpdateButton" CssClass="rsAdvEditSave">
                    <span><%= Owner.Localization.Save %></span>
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="CancelButton" CssClass="rsAdvEditCancel" CommandName="Cancel"
                    CausesValidation="false">
                    <span><%= Owner.Localization.Cancel %></span>
                </asp:LinkButton>
            </div>
        </asp:Panel>
    </div>
</div>