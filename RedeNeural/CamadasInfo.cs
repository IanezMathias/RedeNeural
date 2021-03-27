using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeNeural
{
    public class CamadasInfo
    {
        public int QuantidadeDeNeuronio { get; set; }
        public FuncaoDeAtivacao.NomeFuncao funcaoDeAtivacao { get; set; }
        public FuncaoDeMutacao.Funcao funcaoDeMutacao { get; set; }
    }
    public class CamadasPeso
    {
        public List<float> pesos = new List<float>();
        public float bias = 1;
    }

    public class CamadaRetorno
    {
        public CamadasInfo info { get; set; }
        public List<CamadasPeso> peso { get; set; }
        public List<float> Saidas { get; set; }
    }

    public class Saida
    {
        public List<CamadaRetorno> saidas = new List<CamadaRetorno>();
        public CamadaRetorno ultimaSaida { get; set; }
        public List<float> Erros { get; set; }
        public float ErroTotal { get; set; }
        public float Pontuacao { get; set; }
    }
}
