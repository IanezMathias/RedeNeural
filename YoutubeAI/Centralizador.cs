using RedeNeural;
using System;
using System.Collections.Generic;
using System.Windows.Shapes;

namespace YoutubeAI
{
    public class Centralizador
    {
        #region Variaveis
        public static int melhorTempo = 999999999; // Indica o melhor tempo do melhor carro. Quanto menor o tempo, melhor o carro.
        public static bool Simulado = false;
        public static int QuantidadeDeCarros=25;//Quantidade de carros na pista.
        public static Posicao posicaoInicial = new Posicao() { x = ProjetoPista.Program.LocalInicial_X, y = ProjetoPista.Program.LocalInicial_Y };//Posição inicial do carro
        public static int QuantidadeDeSensores=20;//Quantidade de sensores do carro.
        public static int Distancia_Min_Sensor = 5;//O valor minimo que o carro pode chegar da parede.
        public static int Distancia_Max_Sensor = 100;//O valor maximo que o carro identifica a parede.
        public static float PontuacaoDeSensor = 20;//Peso para o tamanho do sensor.
        public static Carro MelhorCarroAtual = new Carro();//Indica o melhor carro atual.
        public static Consciencia melhorCarroMundial;//Indica a melhor consciencia mundial.
        public static int TempoMaximoParado = 20;//Tempo maximo parado
        public static List<int> PontuacaoDasMutacoes = new List<int>();//nao usado
        public static List<Line> linhas = new List<Line>();// lista de linhas que indicam o sensor
        internal static List<Carro> carros = new List<Carro>();// listas dos carros.
        #endregion

        #region Metodos
        public static void Iniciar()
        {
            for (int a = 0; a < 8; a++)
            {
                PontuacaoDasMutacoes.Add(0);
            }
        }
        static public Consciencia MelhorCarroMundial
        {
            get
            {
                RedeNeural.Helper.Gravar(melhorCarroMundial, true);
                return RedeNeural.Helper.Carregar(true);
            }
            set
            {
                RedeNeural.Helper.Gravar(value);
                melhorCarroMundial = value;
            }
        }
        public static void ObterMelhorConsciencia()
        {
            melhorCarroMundial = RedeNeural.Helper.Carregar();
        }
        public static Posicao CalcularAngulo(float velocidade,float angulo)
        {
            Posicao retorno = new Posicao();
            retorno.x = velocidade * (float)(Math.Cos(angulo * Math.PI / 180));
            retorno.y = velocidade * (float)(Math.Sin(angulo * Math.PI / 180));
            retorno.angulo = angulo;
            return retorno;
        }
        public static List<CamadasInfo> ObterModeloDeCamadasDaRedeNeural(FuncaoDeMutacao.Funcao funcaoDeMutacao)
        {
            List<CamadasInfo> redeNeural = new List<CamadasInfo>();

            CamadasInfo camada_entrada = new CamadasInfo();
            CamadasInfo camada_oculta1 = new CamadasInfo();
            CamadasInfo camada_saida = new CamadasInfo();

            camada_entrada.funcaoDeAtivacao = FuncaoDeAtivacao.NomeFuncao.Relu;
            camada_entrada.funcaoDeMutacao = funcaoDeMutacao;
            camada_entrada.QuantidadeDeNeuronio = QuantidadeDeSensores;

            camada_oculta1.funcaoDeAtivacao = FuncaoDeAtivacao.NomeFuncao.Func;
            camada_oculta1.funcaoDeMutacao = funcaoDeMutacao;
            camada_oculta1.QuantidadeDeNeuronio = 16;

            camada_saida.funcaoDeAtivacao = FuncaoDeAtivacao.NomeFuncao.Nenhuma;
            camada_saida.funcaoDeMutacao = funcaoDeMutacao;
            camada_saida.QuantidadeDeNeuronio = 4;

            redeNeural.Add(camada_entrada);
            redeNeural.Add(camada_oculta1);
            redeNeural.Add(camada_saida);

            return redeNeural;
        }
        #endregion
    }
}
