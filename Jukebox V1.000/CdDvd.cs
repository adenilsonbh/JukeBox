using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jukebox_V1._000
{
    class CdDvd
    {
        public string tituloCdDvd;
        public string caminhoCdDvd;
        public string caminhoCapa;

        public List<Musica> musicas = new List<Musica>();

        public CdDvd(string tituloCd)
        {

            this.tituloCdDvd = tituloCd;
           
           // this.caminhoCdDvd = caminhoCd;

        }
        public void AddCapa(string caminhoC)
        {
            this.caminhoCapa = caminhoC;
        }
        public void AdcionarMusica(Musica music)
        {
            this.musicas.Add(music);
        }


    }
}
