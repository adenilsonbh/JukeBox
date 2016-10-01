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
            //https://pixabay.com/pt/música-clave-de-sol-clave-tonkunst-1521116/ imagem de fundo livre de direitos autorais
            
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
        private void Form1_Load(object sender, System.EventArgs e)
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
            botaoCarregar(this, null);
        }

        private void botaoCarregar(object sender, EventArgs e)
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

        private void CarregarCapas()
        {
            try
            {

                for (int i = 0; i < 9; i++)
                {
                    if (principal.Cds[i].caminhoCapa != null)
                    { pictures[i].ImageLocation = principal.Cds[i].caminhoCapa; }
                    else { pictures[i].ImageLocation = @"img\imgcap.png"; }
                    Application.DoEvents();
                }
                
                //somente para formatar texto na label
                if (principal.Cds[navegar.Image2].tituloCdDvd.Length >= 29)
                { lbTituloCdAtual.Text = (principal.Cds[navegar.Image2].tituloCdDvd).Substring(0, 29) + "..."; }
                else
                { lbTituloCdAtual.Text = principal.Cds[navegar.Image2].tituloCdDvd; }
                //

                Application.DoEvents();


                foreach (Musica elemento in principal.Cds[navegar.Image2].musicas)
                {
                    lstbCdSelecionado.Items.Add(elemento.get_tituloMusica());
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
            if (lstPlayList.Items.Count > 0 && contTimer==0)
            {
                Player2.URL = musicasSelecionadas[0].get_caminhoMusica();
                musicasSelecionadas.Remove(musicasSelecionadas[0]);
                lstPlayList.Items.Remove(lstPlayList.Items[0]);
                timer1.Start();
            }
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
                if (!lstPlayList.Items.Contains(principal.Cds[navegar.Image2].musicas[lstbCdSelecionado.SelectedIndex].get_tituloMusica())&creditos>=1)//verifica se ja escolheu a mesma música
                {

                    creditos --;
                    lbCreditos.Text = creditos.ToString();
                    musicasSelecionadas.Add(principal.Cds[navegar.Image2].musicas[lstbCdSelecionado.SelectedIndex]);
                    lstPlayList.Items.Add(principal.Cds[navegar.Image2].musicas[lstbCdSelecionado.SelectedIndex].get_tituloMusica());
                  
                    
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

        private void timer1_Tick(object sender, EventArgs e)
        {

            if(contTimer<50)
            {
                contTimer++;
            }
            else
            {
                contTimer = 0;
             
            }

            if (lstPlayList.Items.Count > 0 && lbTime.Text != "" && Convert.ToInt32(lbTime.Text.Replace(":", "")) == Convert.ToInt32(lbDuracao.Text.Replace(":", "")) - 1)
            {

                Player2.URL = musicasSelecionadas[0].get_caminhoMusica();
                musicasSelecionadas.Remove(musicasSelecionadas[0]);
                lstPlayList.Items.Remove(lstPlayList.Items[0]);

            }
            lbDuracao.Text = Player2.currentMedia.durationString;
            lbTime.Text = Player2.Ctlcontrols.currentPositionString;
            //lbTocando.Text += Player2.Ctlcontrols.currentItem.name; EXIBIR MUSICA QUE ESTÁ TOCANDO
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
            if (e.KeyCode == Keys.F5)
            {
                botaoCarregar(this,null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
