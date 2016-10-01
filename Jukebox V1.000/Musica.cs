using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jukebox_V1._000
{
    class Musica
    {
        private string tituloMusica;
        private string caminhoMusica;
        private string caminhoCapa;

        public Musica(string tituloM, string caminhoM, string caminhoC)
        {
            this.caminhoCapa = caminhoC;
            this.caminhoMusica = caminhoM;
            this.tituloMusica = tituloM;
        
        }
        public string get_tituloMusica()
        {
            return this.tituloMusica;
        }
        public string get_caminhoMusica()
        {
            return this.caminhoMusica;
        }
        public string get_caminhoCapa()
        {
            return this.caminhoCapa;
        }

    }
}
