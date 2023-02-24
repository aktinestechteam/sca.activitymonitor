using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmUserActivityTracker
{
    public partial class frmHome : Form
    {
     
        public frmHome()
        {
            InitializeComponent();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            Initialize();
        }
        private void Initialize()
        {
            imgIcon.Image = Properties.Resources.activity_tracker_icon;


            PrincipalContext c = new PrincipalContext(ContextType.Machine, Environment.MachineName);
            UserPrincipal uc = UserPrincipal.FindByIdentity(c, System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            lblloggedIn.Text = uc.LastLogon?.ToString("dd MMM yyyy hh:mm:ss tt");

            EventLog eventLogItem = new EventLog("Security");
            var sev = eventLogItem.Entries.Cast<EventLogEntry>().Where(ev => (ev.InstanceId == 4624));


            //var diffInSeconds = (DateTime.Now- uc.LastLogon)?.TotalSeconds;
            //lblduration.Text = Utilities.ConvertSecondsToDateTime(diffInSeconds);





        } 



    }
}
