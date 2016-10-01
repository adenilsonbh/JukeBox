using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Jukebox_V1._000
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Musica> musicasSelecionadas = new List<Musica>();
        int contaArquivos;
        int contTimer = 0;
        int creditos = 5000;
        Conexao principal;
        Navegacao navegar;
        PictureBox[] pictures = new PictureBox[9];
        string caminhoPastaPrincipal = @"C:\MUSICA";


        private void button1_Click(object sender, EventArgs e)
        {
            lstbCdSelecionado.Items.Clear();
            if (principal != null)
            {
                principal.Cds.Clear();
            }
            musicasSelecionadas.Clear();
            lstPlayList.Items.Clear();

            if (!Directory.Exists(caminhoPastaPrincipal))
            {
                Directory.CreateDirectory(caminhoPastaPrincipal);
                MessageBox.Show("*** Favor colocar todas as pastas de músicas dentro de:\n " + caminhoPastaPrincipal, "Atenção!!!");
            }

            contaArquivos = Directory.GetFiles(caminhoPastaPrincipal, "*.*", SearchOption.AllDirectories).Length;
            principal = new Conexao(caminhoPastaPrincipal);
            principal.Carregar();
            navegar = new Navegacao();
            lbTotalCds.Text = principal.Cds.Count.ToString();
            Application.DoEvents();
            CarregarCapas();
            if (principal.Cds.Count == 0)// tem poucas pastas de músicas, emitir alerta
            {
                lbAtencaoPastaVazia.Text = "***Pasta sem músicas, \nFavor colocar todas as pastas de músicas dentro de:\n" + caminhoPastaPrincipal;

            }
            else
            {
                lbAtencaoPastaVazia.Text = "";
                lbCdAtual.Text = "1";
            }
        }

        public void ProximoCd()
        {
            if (principal != null)
            {
                if (principal.Cds != null)
                { navegar.ProximoCd(lbCdAtual, lbTituloCdAtual, principal.Cds, pictures, lstbCdSelecionado); }
            }
        }
        public void AnteriorCd()
        {
            if (principal != null)
            {
                if (principal.Cds != null)
                { navegar.AnteriorCd(lbCdAtual, lbTituloCdAtual, principal.Cds, pictures, lstbCdSelecionado); }
            }

        }

        private void btCarregarCapas_Click(object sender, EventArgs e)
        {

        }

        private void CarregarCapas()
        {
            try
            {

                if (principal.Cds[0].caminhoCapa != null)
                { pictures[0].ImageLocation = principal.Cds[0].caminhoCapa; }
                else { pictures[0].ImageLocation = @"img\imgcap.jpg3"; }
                Application.DoEvents();

                if (principal.Cds[1].caminhoCapa != null)
                { pictures[1].ImageLocation = principal.Cds[1].caminhoCapa; }
                else { pictures[1].ImageLocation = @"img\imgcap.jpg3"; }
                Application.DoEvents();

                if (principal.Cds[2].caminhoCapa != null)
                { pictures[2].ImageLocation = principal.Cds[2].caminhoCapa; }
                else { pictures[2].ImageLocation = @"img\imgcap.jpg3"; }
                Application.DoEvents();

                if (principal.Cds[3].caminhoCapa != null)
                { pictures[3].ImageLocation = principal.Cds[3].caminhoCapa; }
                else { pictures[3].ImageLocation = @"img\imgcap.jpg3"; }
                Application.DoEvents();

                if (principal.Cds[4].caminhoCapa != null)
                { pictures[4].ImageLocation = principal.Cds[4].caminhoCapa; }
                else { pictures[4].ImageLocation = @"img\imgcap.jpg3"; }
                Application.DoEvents();

                if (principal.Cds[5].caminhoCapa != null)
                { pictures[5].ImageLocation = principal.Cds[5].caminhoCapa; }
                else { pictures[5].ImageLocation = @"img\imgcap.jpg3"; }
                Application.DoEvents();

                if (principal.Cds[6].caminhoCapa != null)
                { pictures[6].ImageLocation = principal.Cds[6].caminhoCapa; }
                else { pictures[6].ImageLocation = @"img\imgcap.jpg3"; }
                Application.DoEvents();

                if (principal.Cds[7].caminhoCapa != null)
                { pictures[7].ImageLocation = principal.Cds[7].caminhoCapa; }
                else { pictures[7].ImageLocation = @"img\imgcap.jpg3"; }
                Application.DoEvents();

                if (principal.Cds[8].caminhoCapa != null)
                { pictures[8].ImageLocation = principal.Cds[8].caminhoCapa; }
                else { pictures[8].ImageLocation = @"img\imgcap.jpg3"; }
                Application.DoEvents();





                //somente para formatar texto na label
                if (principal.Cds[navegar.Image2].tituloCdDvd.Length >= 29)
                { lbTituloCdAtual.Text = (principal.Cds[navegar.Image2].tituloCdDvd).Substring(0, 29) + "..."; }
                else
                { lbTituloCdAtual.Text = principal.Cds[navegar.Image2].tituloCdDvd; }
                //

                Application.DoEvents();


                foreach (Musica elemento in principal.Cds[navegar.Image2].musicas)
                {
                    lstbCdSelecionado.Items.Add(elemento.tituloMusica);
                    Application.DoEvents();
                }
            }
            catch { }
            lstbCdSelecionado.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProximoCd();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnteriorCd();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IniciarPlayer();

        }

        private void IniciarPlayer()
        {
            if (lstPlayList.Items.Count > 0)
            {
                Player2.URL = musicasSelecionadas[0].caminhoMusica;
                musicasSelecionadas.Remove(musicasSelecionadas[0]);
                lstPlayList.Items.Remove(lstPlayList.Items[0]);
                timer1.Start();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void listBox2_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {

        }

        private void listBox2_DisplayMemberChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                AdcionarPlayList();
            }
        }

        private void AdcionarPlayList()
        {
            if (lstbCdSelecionado.SelectedIndex >= 0)
            {
                if (!lstPlayList.Items.Contains(principal.Cds[navegar.Image2].musicas[lstbCdSelecionado.SelectedIndex].tituloMusica)&creditos>=1)//verifica se ja escolheu a mesma música
                {

                    creditos --;
                    lbCreditos.Text = creditos.ToString();
                    musicasSelecionadas.Add(principal.Cds[navegar.Image2].musicas[lstbCdSelecionado.SelectedIndex]);
                    lstPlayList.Items.Add(principal.Cds[navegar.Image2].musicas[lstbCdSelecionado.SelectedIndex].tituloMusica);
                   // lstbCdSelecionado.MouseDown;
                    
                }

               
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 115)
            {
                ProximoCd();
            }
            if (e.KeyChar == 97)
            {
                AnteriorCd();
            }
        }

        private void axWindowsMediaPlayer1_EndOfStream(object sender, AxWMPLib._WMPOCXEvents_EndOfStreamEvent e)
        {

        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {



        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if(contTimer<50)
            {
                contTimer++;
            }
            else
            {
                contTimer = 0;
               // Player2.fullScreen = true;//melhorar
                
            }

            if (lstPlayList.Items.Count > 0 && lbTime.Text != "" && Convert.ToInt32(lbTime.Text.Replace(":", "")) == Convert.ToInt32(lbDuracao.Text.Replace(":", "")) - 1)
            {

                Player2.URL = musicasSelecionadas[0].caminhoMusica;
                musicasSelecionadas.Remove(musicasSelecionadas[0]);
                lstPlayList.Items.Remove(lstPlayList.Items[0]);

            }
            lbDuracao.Text = Player2.currentMedia.durationString;
            lbTime.Text = Player2.Ctlcontrols.currentPositionString;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictures[0] = pictureBox1;
            pictures[1] = pictureBox2;
            pictures[2] = pictureBox3;
            pictures[3] = pictureBox4;
            pictures[4] = pictureBox5;
            pictures[5] = pictureBox6;
            pictures[6] = pictureBox7;
            pictures[7] = pictureBox8;
            pictures[8] = pictureBox9;
            lbCreditos.Text = creditos.ToString();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            { 
                AnteriorCd();
            }
            if (e.KeyCode == Keys.Right)
            { 
                ProximoCd(); 
            }
            if (e.KeyCode == Keys.F1)
            {
                Player2.fullScreen = false;
                IniciarPlayer();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btSelecionar_Click(object sender, EventArgs e)
        {
            AdcionarPlayList();

        }
    }
}
