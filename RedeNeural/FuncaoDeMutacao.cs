using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeNeural
{
    public class FuncaoDeMutacao
    {
        public enum Funcao
        {
            Nenhuma,
            Livre,
            MutacaoAleatoria,
            MutacaoAdicionada,
            MutacaoCrossOverAleatorio,
            MutacaoCrossOverAdicionado,
            MutacaoAdicionada2,
            MutacaoSuave
        }

        public static List<float> MutacaoAleatoria(int Quantidade)
        {
            List<float> NovoPeso = new List<float>();
            for (int a = 0; a < Quantidade; a++)
            {
                float ValorNovo = Aleatorio.Obter();
                float Sinal = Aleatorio.Obter();
                if (Sinal < 0.5)
                    ValorNovo = -ValorNovo;
                NovoPeso.Add(ValorNovo);
            }
            return NovoPeso;
        }
        public static List<float> MutacaoAdicionada(List<float> pesos)
        {
            List<float> NovoPeso = new List<float>();
            for (int a = 0; a < pesos.Count; a++)
            {
                float ValorNovo = Aleatorio.Obter();
                float Sinal = Aleatorio.Obter();
                if (Sinal < 0.5)
                    ValorNovo = -ValorNovo;
                NovoPeso.Add(ValorNovo + pesos[a]);
            }
            return NovoPeso;
        }
        public static List<float> MutacaoCrossOverAleatorio(List<float> pesos)
        {
            List<float> NovoPeso = pesos;
            int quantidade = (int)(pesos.Count / 2);
            float Sentido = Aleatorio.Obter();
            if (Sentido >= 0.5)
            {
                for (int a = 0; a < quantidade; a++)
                {
                    float ValorNovo = Aleatorio.Obter();
                    float Sinal = Aleatorio.Obter();
                    if (Sinal < 0.5)
                        ValorNovo = -ValorNovo;
                    NovoPeso[a] = ValorNovo;
                }
            }
            else
            {
                if (quantidade <= 0) quantidade = 1;
                for (int a = quantidade - 1; a > 0; a--)
                {
                    float ValorNovo = Aleatorio.Obter();
                    float Sinal = Aleatorio.Obter();
                    if (Sinal < 0.5)
                        ValorNovo = -ValorNovo;
                    NovoPeso[a] = ValorNovo;
                }
            }
            return NovoPeso;
        }



        public static List<float> MutacaoCrossOverAdicionado(List<float> pesos)
        {
            List<float> NovoPeso = pesos;
            int quantidade = (int)(pesos.Count / 2);
            float Sentido = Aleatorio.Obter();
            if (Sentido >= 0.5)
            {
                for (int a = 0; a < quantidade; a++)
                {
                    float ValorNovo = Aleatorio.Obter();
                    float Sinal = Aleatorio.Obter();
                    if (Sinal < 0.5)
                        ValorNovo = -ValorNovo;
                    NovoPeso[a] = ValorNovo + pesos[a];
                }
            }
            else
            {
                if (quantidade <= 0) quantidade = 1;
                for (int a = quantidade - 1; a > 0; a--)
                {
                    float ValorNovo = Aleatorio.Obter();
                    float Sinal = Aleatorio.Obter();
                    if (Sinal < 0.5)
                        ValorNovo = -ValorNovo;
                    NovoPeso[a] = ValorNovo + pesos[a];
                }
            }
            return NovoPeso;
        }
        public static List<float> MutacaoAdicionada2(List<float> pesos)
        {
            List<float> NovoPeso = new List<float>();
            for (int a = 0; a < pesos.Count; a++)
            {
                //float ValorNovo = 0.00001;
                float ValorNovo = Aleatorio.Obter() / 1000;
                float Sinal = Aleatorio.Obter();
                if (Sinal < 0.5)
                    ValorNovo = -ValorNovo;
                NovoPeso.Add(ValorNovo + pesos[a]);
            }
            return NovoPeso;
        }

        internal static float MutacaSuave(float bias)
        {
            List<float> b = new List<float>();
            b.Add(bias);
            float r = MutacaSuave(b)[0];
            return r;
        }

        public static List<float> MutacaSuave(List<float> pesos)
        {
            List<float> NovoPeso = new List<float>();
            for (int a = 0; a < pesos.Count; a++)
            {
                float ValorNovo = Aleatorio.Obter() / 4;
                float Sinal = Aleatorio.Obter();
                if (Sinal < 0.5)
                {
                    pesos[a] = pesos[a] - ValorNovo;
                }
                else
                {
                    pesos[a] = pesos[a] + ValorNovo;
                }
                if (pesos[a] >= 1) pesos[a] = 1;
                if (pesos[a] <= -1) pesos[a] = -1;
                NovoPeso.Add(pesos[a]);
            }
            return NovoPeso;
        }
    }
}
