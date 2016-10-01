using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Jukebox_V1._000
{
    class Navegacao
    {
       
        private int[] posImage = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        private int contCdAtual = 1;

        public  int Image2
        {
            get { return posImage[2]; }
            
        }
        public Navegacao ()
        {

        }

        /// <summary>
        /// Método para passar os cds para frente nos pictures. Recebe uma lista de paremetros
        /// </summary>
        /// <param lbCdAtual="label cdAtual"></param>
        /// <param lbTituloCdAtual="label titulo do cd atual"></param>
        ///  <param cds="list com cds carregados"></param>
        ///  <param picture="vetor com pictureBox"></param>
        ///  <param lstbCdSelecionado="listbox musicas do cd atual"></param>
        
        public void ProximoCd(Label lbCdAtual, Label lbTituloCdAtual, List<CdDvd> cds, PictureBox[] picture, ListBox lstbCdSelecionado)
        {

            contCdAtual++;
            if (contCdAtual > cds.Count)
            {
                contCdAtual = 1;
            }
            
            lstbCdSelecionado.Items.Clear();
            for (int i = 0; i < 9; i++)
            {
                
                posImage[i]++;
                
                if (posImage[i] >= cds.Count || posImage[i] < 0)
                {
                    posImage[i] = 0;
                }
                lbCdAtual.Text = contCdAtual.ToString();//qual cd entre todos 
                try
                {
                    if (cds[posImage[i]].caminhoCapa != null)
                    { picture[i].ImageLocation = cds[posImage[i]].caminhoCapa; }
                    else
                    {

                        picture[i].ImageLocation = @"img\imgcap.png";
                    }
                }
                catch { }

                Application.DoEvents();

                if(i==2)
                {
                    if (cds[posImage[i]].tituloCdDvd.Length >= 29)
                    { lbTituloCdAtual.Text = (cds[posImage[i]].tituloCdDvd).Substring(0, 29) + "..."; }
                    else
                    { lbTituloCdAtual.Text = cds[posImage[i]].tituloCdDvd; }
                    //
                    Application.DoEvents();


                    foreach (Musica elemento in cds[posImage[i]].musicas)
                    {
                        lstbCdSelecionado.Items.Add(elemento.get_tituloMusica());
                        Application.DoEvents();
                    }

                }


            }

            

            lstbCdSelecionado.Focus();
        }
        /// <summary>
        /// Método para voltar os cds nos pictures. Recebe uma lista de paremetros
        /// </summary>
        /// <param lbCdAtual="label cdAtual"></param>
        /// <param lbTituloCdAtual="label titulo do cd atual"></param>
        ///  <param cds="list com cds carregados"></param>
        ///  <param picture="vetor com pictureBox"></param>
        ///  <param lstbCdSelecionado="listbox musicas do cd atual"></param>
        public void AnteriorCd(Label lbCdAtual, Label lbTituloCdAtual, List<CdDvd> cds, PictureBox[] picture, ListBox lstbCdSelecionado)
        {

            contCdAtual--;
            if (contCdAtual ==0)
            {
                contCdAtual = (cds.Count);
            }
           
            lstbCdSelecionado.Items.Clear();
            for (int i = 0; i < 9; i++)
            {

                posImage[i]--;
               

                if (posImage[i] < 0 || posImage[i] >= cds.Count)
                {
                    posImage[i] = (cds.Count - 1);
                }
                lbCdAtual.Text = contCdAtual.ToString();//qual cd entre todos  lb5
                try
                {
                    if (cds[posImage[i]].caminhoCapa != null)
                    { picture[i].ImageLocation = cds[posImage[i]].caminhoCapa; }
                    else
                    {

                        picture[i].ImageLocation = @"img\imgcap.png";
                    }
                }
                catch { }

                Application.DoEvents();

                if (i == 2)
                {
                    if (cds[posImage[i]].tituloCdDvd.Length >= 29)
                    { lbTituloCdAtual.Text = (cds[posImage[i]].tituloCdDvd).Substring(0, 29) + "..."; }
                    else
                    { lbTituloCdAtual.Text = cds[posImage[i]].tituloCdDvd; }
                    //
                    Application.DoEvents();


                    foreach (Musica elemento in cds[posImage[i]].musicas)
                    {
                        lstbCdSelecionado.Items.Add(elemento.get_tituloMusica());
                        Application.DoEvents();
                    }

                }


            }



            lstbCdSelecionado.Focus();
            

        }
    }
}
