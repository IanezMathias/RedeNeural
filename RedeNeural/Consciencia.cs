
using System.Collections.Generic;


namespace RedeNeural
{
    public class Consciencia
    {
        public List<CamadaRetorno> camadas = new List<CamadaRetorno>();
        public string id { get; set; }
        public CamadaRetorno saida { get; set; }
        public float pontuacao { get; set; }
    }
}
