using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserActivityMonitor.Services.Implementation;
using UserActivityMonitor.Watchers;

namespace UserActivityMonitor
{
    public partial class frmMain : Form
    {

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                ntfUserNotification.Visible = true;
            }
        }

        private void ntfUserNotification_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            ntfUserNotification.Visible = false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            
            new ManageDB().CreateSqlLiteDatabase();
            /* Get WebPage URL from IE */
            //WebPageWatcher.GetInstance().StartMonitoring();

            /* Monitors Apps/Programs */
            ProgramWatcher.GetInstance().StartMonitoring();

        }
    }
}
