using Hermes.APP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HermesForms
{
    public partial class frmMonitorEnvioDARF : Form
    {
        public frmMonitorEnvioDARF()
        {
            InitializeComponent();
        }

        private void btnIniciarServico_Click(object sender, EventArgs e)
        {
            if (btnIniciarServico.Text == "Iniciar serviço")
            {
                btnIniciarServico.BackColor = Color.Green;
                btnIniciarServico.Text = "Parar serviço";
                RecordLog a = new RecordLog();
                a.HermesLogService(string.Empty,"20211111","teste");
            }
        }
    }
}
