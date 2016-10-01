using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Jukebox_V1._000
{
    class Conexao
    {
        private List<CdDvd> cds = new List<CdDvd>();
        private CdDvd cdAux;
        private Musica musicAux;
        private string con;
        string capaAux;
        private bool temMusica = false;
        private int contCds=0;
        private StreamWriter writer = new StreamWriter(@"outros\log.txt");
       

        public int ContCds
        {
            get { return contCds; }
            
        }
        private int contMusicas=0;

        public int ContMusicas
        {
            get { return contMusicas; }
           
        }
        internal List<CdDvd> Cds
        {
            get { return cds; }
           
        }


        public Conexao(string conexao)
        {
            if(conexao!="")
            {
                this.con = conexao;
            }

        }
        public void Carregar()
        {
            DirectoryInfo diretorio = new DirectoryInfo(con);
           
            Load(diretorio);
            var regras = from query in cds orderby query.tituloCdDvd select query;// ordenar pelo titulo do cd
            cds = regras.ToList();
           
           
            writer.Close();
        }
        private void Load(DirectoryInfo dir)
        {
            cdAux = new CdDvd(dir.Name);
            bool flagNaoTemCapa = true;


            foreach (FileInfo file in dir.GetFiles("*.JPG"))
            {
                try
                {
                    capaAux = file.FullName;
                    cdAux.AddCapa(file.FullName);
                    flagNaoTemCapa = false;
                    Application.DoEvents();
                }
                catch
                {
                }
            }
           


            foreach (FileInfo file in dir.GetFiles("*.mp3"))
            {
                try
                {
                    musicAux = new Musica(file.Name, file.FullName, capaAux);
                    cdAux.AdcionarMusica(musicAux);
                    Application.DoEvents();
                    temMusica = true;
                }
                catch
                {

                }
            }
            if (temMusica)
            {

                if (flagNaoTemCapa)
                {
                    writer.WriteLine(dir.Name);//escrever log de pastas sem capa

                    try// ajustar 
                    {
                        string fileName = dir.Name + ".jpg";
                        string sourcePath = @"C:\Users\gleidson\Pictures\capas";//mudar a pasta para diretorio do programa
                        string targetPath = dir.FullName;
                        string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                        string destFile = System.IO.Path.Combine(targetPath, fileName);
                        System.IO.File.Copy(sourceFile, destFile, true);
                    }
                    catch
                    {

                    }



                }



                cds.Add(cdAux);
                temMusica = false;
            }



            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                Load(subDir);
                Application.DoEvents();
            }

        }

    }
}
