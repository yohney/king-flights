<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="King.FlightSearch.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" ID="updMessage" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divError" runat="server" visible="false" class="alert alert-danger">
                <%= this.ErrorMessage %>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress id="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/content/images/loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" ID="updSearch" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="search-wrapper">
                <div class="row">
                    <div class="col-md-5">
                        <div class="form-group">
                            <label for="txtDeparture">Departure airport</label>
                            <asp:TextBox ID="txtDeparture" runat="server" CssClass="form-control" placeholder="Type airport name" />
                            <asp:HiddenField ID="hdnDepartureId" runat="server" />
                            <asp:RequiredFieldValidator ID="vldDeparture" runat="server" Text="Departure airport is obligatory!" ControlToValidate="txtDeparture" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="form-group">
                            <label for="txtDestination">Destination airport</label>
                            <asp:TextBox ID="txtDestination" runat="server" CssClass="form-control" placeholder="Type airport name" />
                            <asp:HiddenField ID="hdnDestinationId" runat="server" />
                            <asp:RequiredFieldValidator ID="vldDestination" runat="server" Text="Destination airport is obligatory!" ControlToValidate="txtDestination" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label for="txtCurrency">Currency</label>
                            <asp:DropDownList ID="ddCurrency" runat="server" CssClass="form-control">
                                <asp:ListItem Text="USD" Value="1" />
                                <asp:ListItem Text="EUR" Value="2" />
                                <asp:ListItem Text="HRK" Value="3" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtDepartureDate">Departure date</label>
                            <asp:TextBox ID="txtDepartureDate" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="vldDepartureDate" runat="server" Text="Required." ControlToValidate="txtDepartureDate" ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="vldDepartureDateFormat" runat="server" Text="Wrong format!" 
                                ControlToValidate="txtDepartureDate" ValidationExpression="[0-9]{1,2}\.[0-9]{1,2}\.[0-9]{4}" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtReturnDate">Return date</label>
                            <asp:TextBox ID="txtReturnDate" runat="server" CssClass="form-control" />
                            <asp:RegularExpressionValidator ID="vldArrivalDateFormat" runat="server" Text="Wrong format!" 
                                ControlToValidate="txtReturnDate" ValidationExpression="([0-9]{1,2}\.[0-9]{1,2}\.[0-9]{4}){0,1}" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label for="txtAdults">Adults</label>
                            <asp:TextBox ID="txtAdults" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="vldAdultsRequired" runat="server" Text="Required." ControlToValidate="txtAdults" ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="vldAdultsRegex" runat="server" Text="Must be number." 
                                ControlToValidate="txtAdults" ValidationExpression="[0-9]+" ForeColor="Red" />
                            
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label for="txtChildren">Children</label>
                            <asp:TextBox ID="txtChildren" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="vldChildrenreq" runat="server" Text="Required." ControlToValidate="txtChildren" ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="vldChildrenRegex" runat="server" Text="Must be number." 
                                ControlToValidate="txtChildren" ValidationExpression="[0-9]+" ForeColor="Red" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10 text-right">
                        
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:LinkButton ID="btnSearch" runat="server" OnClick="OnSearchClick" CssClass="btn btn-lg btn-success" Text="Search" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" ID="updResults" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Repeater ID="rptItineraries" runat="server">
                <ItemTemplate>
                    
                    <div class="row">
                        <div class="col-md-12">
                            <h3><%# Eval("DepartureAirport") %> -> <%# Eval("ArrivalAirport") %></h3>
                        </div>
                    </div>

                    <h4>Outbound</h4>
                    <asp:GridView ID="gridOutbound" runat="server" AutoGenerateColumns="false" CssClass="table table-condensed" DataSource='<%# Eval("OutboundFlights") %>'>
                        <Columns>
                            <asp:BoundField HeaderText="Departure time" DataField="DepartureTime" DataFormatString="{0:dd.MM.yyyy HH:mm}" />
                            <asp:BoundField HeaderText="Arrival time" DataField="ArrivalTime" DataFormatString="{0:dd.MM.yyyy HH:mm}" />
                            <asp:BoundField HeaderText="Stops" DataField="StopsCount" />
                        </Columns>
                    </asp:GridView>

                    <h4>Inbound</h4>
                    <asp:GridView ID="gridInbound" runat="server" AutoGenerateColumns="false" CssClass="table table-condensed" DataSource='<%# Eval("InboundFlights") %>'>
                        <Columns>
                            <asp:BoundField HeaderText="Departure time" DataField="DepartureTime" DataFormatString="{0:dd.MM.yyyy HH:mm}" />
                            <asp:BoundField HeaderText="Arrival time" DataField="ArrivalTime" DataFormatString="{0:dd.MM.yyyy HH:mm}" />
                            <asp:BoundField HeaderText="Stops" DataField="StopsCount" />
                        </Columns>
                    </asp:GridView>

                    <div class="row">
                        <div class="col-md-12 text-right">
                            <h3>Price for <%# Eval("Adults") %> adults and <%# Eval("Children") %> children: <%# Eval("Price") %> <%# Eval("Currency") %></h3>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function pageLoad(s,e) {
            registerAutocomplete(
                $("#<%= txtDeparture.ClientID %>"),
                $("#<%= hdnDepartureId.ClientID %>"),
                "/api/airports/suggest/");

            registerAutocomplete(
                $("#<%= txtDestination.ClientID %>"),
                $("#<%= hdnDestinationId.ClientID %>"),
                "/api/airports/suggest/");

            registerDatepicker($("#<%= txtDepartureDate.ClientID %>"));
            registerDatepicker($("#<%= txtReturnDate.ClientID %>"));
        }

        function registerDatepicker(txt) {
            txt.datepicker(
            {
                dateFormat: 'd.m.yy',
                changeMonth: true,
                changeYear: true,
                yearRange: '<%= DateTime.Now.Year %>:<%= DateTime.Now.Year + 1 %>'
            });
        }

        function registerAutocomplete(txt, hdn, url) {
            txt.autocomplete({
                minLength: 2,
                focus: function (event, ui) {
                    txt.val(ui.item.label);
                    return false;
                },
                select: function (event, ui) {
                    txt.val(ui.item.label);
                    hdn.val(ui.item.value);
                    return false;
                },
                source: function (req, resp) {
                    $.getJSON(url + "/" + req.term, {}, resp);
                }
            }).click(function () {
                $(this).select();
            });
        }
    </script>

</asp:Content>
