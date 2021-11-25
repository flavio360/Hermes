using Hermes;
using Hermes.APP;
using Hermes.BLL;
using Hermes.BLL.Utilidades;
using Hermes.DAO.SSManager;
using Hermes.DAO.SSManagerNew.Track;
using Hermes.DAO.Track.AirLink;
using Hermes.DAO.Track.Airlinkexpress;
using System;
using System.Collections;
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
            CheckpointSendedDAO c = new CheckpointSendedDAO();
            Checkpoint_airlinkDAO d = new Checkpoint_airlinkDAO();

            //1 recupera os itens para o primeiro envio, etapa que valida se já foi feito o primeiro envio
            var obj = a.LoadTrackingSS();

            //2 faz insert dos itens que dewvem ser enviados na pedidomsetre, só deve fazer insert uma vez . 
            b.InsertPedidomestre(obj);

            //3 recupera o que foi gravado
            List<Pedidomestre> pedidomestres = b.SelectPedidomestre(obj);

            //4 Registra na ssmanager tudo que foi gravado na pedidomestre da airlink
            c.InsertCheckpointSended(pedidomestres);

            //5 recupera os efetivamente os tracking disponiveis
            var objtrk = a.SelectTracking();

            //6 grava na base airlink os tracking dos pedidos
            d.InsertCheckpoint_airlink(objtrk);

            //7 grava os pedidos enviados 
            CheckpointTrackingSendedDAO.InsertCheckpointTrackingSended(objtrk);

            //recupera ultimo ID, função executada dentro do passo 6
            int dfdsf = d.SelectCheckpoint_airlink();  









        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
             var ftt = new ServiceControlExecutation();
            ftt.ValidadtionHourExec("1|2|9|4|5|9");
        }
    }
}
