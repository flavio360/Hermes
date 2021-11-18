using Hermes;
using Hermes.APP;
using Hermes.DAO.Track.AirLink;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teste
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTrackAirlink_Click(object sender, EventArgs e)
        {
            LoadTrackingDAO a = new LoadTrackingDAO();
            a.LoadTrackingSS();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadTrackingDAO a = new LoadTrackingDAO();
            PedidomestreDAO b = new PedidomestreDAO();

            var obj = a.LoadTrackingSS();
            b.RecordTracksAirlink(obj);
        }
    }
}
