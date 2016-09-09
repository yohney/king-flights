using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using King.FlightSearch.Services;

namespace King.FlightSearch.Web
{
    public partial class SyncAirports : System.Web.UI.Page
    {
        protected string ErrorMessage { get; private set; }

        public FlightService FlightService { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.msgError.Visible = false;
            this.msgSuccess.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var result = this.FlightService.SyncAirports();
            if(result.IsSuccess)
            {
                this.msgSuccess.Visible = true;
            }
            else
            {
                this.msgError.Visible = true;
                this.ErrorMessage = string.Join("<br/>", result.Errors);
            }
        }
    }
}