using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jukebox_V1._000
{
    class Musica
    {
        public string tituloMusica;
        public string caminhoMusica;
        public string caminhoCapa;

        public Musica(string tituloM, string caminhoM, string caminhoC)
        {
            this.caminhoCapa = caminhoC;
            this.caminhoMusica = caminhoM;
            this.tituloMusica = tituloM;
        
        }



    }
}
