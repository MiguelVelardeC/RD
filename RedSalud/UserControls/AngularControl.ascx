
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AngularControl.ascx.cs" Inherits="UserControls_AngularControl" %>
<asp:Panel ID="Containter" runat="server">
    <asp:Literal ID="createControllerScript" runat="server" />
    <asp:Panel ID="ReadPanel" runat="server" ng-init="LoadJson()" CssClass="ControlPanel">
        <!-- ng-controller="Controller"-->
        <ul style="margin-left: 15px;">
            <li ng-repeat="obj in master" style="list-style-position: outside; margin-top: 10px;">{{obj.name}}<br />
                <ul ng-repeat="sub in obj.group" ng-if="obj.group">
                    <li style="font-size: 11px; list-style-position: outside; text-align: justify;">
                        <span style="font-weight: bold;">{{sub.name}}</span><br />
                        {{sub.value || "&nbsp;"}}
                    </li>
                </ul>
                <span ng-if="!obj.group" style="font-weight: normal;">{{obj.value || "&nbsp;"}}</span>                                
            </li>
        </ul>
    </asp:Panel>
    <asp:Panel ID="EditPanel" runat="server" ng-init="LoadJson()" Visible="false" CssClass="ControlPanel">
        <!-- ng-controller="Controller"-->
        <div ng-repeat="obj in master">
            <span class="label">{{obj.name}}</span>
            <div ng-switch on="obj.type">
                <div ng-switch-when="group">
                    <div ng-repeat="sub in obj.group" style="margin-left: 20px;">
                        <span class="label" style="font-size: 11px;">{{sub.name}}</span>
                        <input  type="text" ng-change="SaveChanges()" ng-model="sub.value" />
                        <div class="validation">
                            <span id="validation{{replaceSpace(sub.name)}}" class="angularValidation" style="display:none;color: Red;">Este campo no puede estar vacio y Max. {{getMaxlenght()}} caracteres</span>
                        </div>
                      
                    </div>
                </div>
                <span ng-switch-when="textarea"><textarea ng-change="SaveChanges()" ng-model="obj.value" rows="{{obj.rows}}" class="biggerField"></textarea></span>
                <span ng-switch-when="select"><select ng-options="group.value as group.name for group in obj.group" ng-change="SaveChanges()" ng-model="obj.value" multiple style="height: 100px;"></select></span>
                <span ng-switch-when="date"><div class="ui-datepicker-input"><input class="date" type="text" ng-change="SaveChanges()" ng-model="obj.value" /><div class="clear"></div></div></span>
                <span ng-switch-default><input type="text" ng-change="SaveChanges()" ng-model="obj.value" /></span>
            </div>
            <div class="validation">
                <span id="validation{{replaceSpace(obj.name)}}" class="angularValidation" style="display:none;color: Red;">Este campo no puede estar vacio y Max.   {{getMaxlenght()}} caracteres</span>
                 
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="JSonDataHF" runat="server" />
</asp:Panel>