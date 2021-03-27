using RedeNeural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YoutubeAI
{
    /// <summary>
    /// Interação lógica para Carro.xam
    /// </summary>
    public partial class Carro : UserControl
    {

        public List<SensorInfo> sensores = new List<SensorInfo>();
        public List<CamadasInfo> camadas;
        public Consciencia minhaConsciencia;
        public FuncaoDeMutacao.Funcao nomeDaMutacao;

        public int pontos = 0;
        public int pontosExtras = 0;
        public int ultimoPonto = -1;

        public bool primeiraRodada = true;
        public float angulo=180;
        public float minhaVelocidade = 2;
        public Posicao direcao = new Posicao();
        public Posicao minhaPosicao = new Posicao();

        int contagem = 0;
        int ultimoRegistro = 0;
        public List<float> saida = new List<float>();

        int tempo = 0;

        public Carro()
        {
            InitializeComponent();
        }

        public void Start()
        {
            Restart();
        }

        private void Restart()
        {
            camadas = Centralizador.ObterModeloDeCamadasDaRedeNeural(nomeDaMutacao);
            contagem = 0;
            ultimoRegistro = 0;
            pontos = 0;
            pontosExtras = 0;
            ultimoPonto = 0;
            angulo = 180;
            minhaVelocidade = 2;
            primeiraRodada = true;
            minhaConsciencia = null;
            tempo = 0;
            IrParaPosicaoInicial();
        }
        public void Update()
        {
            Pensar();
        }

        private void Pensar()
        {
            tempo++;
            AplicarSensores();

            if(minhaConsciencia==null && Centralizador.Simulado == false)
            {
                minhaConsciencia = new Cerebro().Calcular(ObterValoresDeSensoresPuro(), camadas);
                AplicarMovimento(minhaConsciencia);
                return;
            }
            else
            {
                if (primeiraRodada)
                {
                    primeiraRodada = false;
                    if (!Centralizador.Simulado)
                    {
                        minhaConsciencia = new Cerebro().ReCalcular(ObterValoresDeSensoresPuro(), camadas, minhaConsciencia);
                        AplicarMovimento(minhaConsciencia);
                        return;
                    }
                    else
                    {
                        AplicarMovimento(new Cerebro().Simular(ObterValoresDeSensoresPuro(), camadas, minhaConsciencia));
                        return;
                    }
                }
                else
                {
                    AplicarMovimento(new Cerebro().Simular(ObterValoresDeSensoresPuro(), camadas, minhaConsciencia));
                    return;
                }
            }
        }

        private void AplicarMovimento(Consciencia consciencia)
        {
            if (pontos >= Mapa.finalDaPista)
            {
                if (tempo < Centralizador.melhorTempo)
                {
                    pontosExtras = 50000 / tempo;
                    Centralizador.melhorTempo = tempo;
                }
                else
                {
                    pontosExtras = 0;
                }
                Morreu();
            }

            float rotacao = Math.Abs(consciencia.saida.Saidas[2]);
            if (rotacao >= 5) rotacao = 5;
            if (rotacao <= 0.1f) rotacao = 0.1f;

            float vel = Math.Abs(consciencia.saida.Saidas[3]);
            if (vel >= 5) vel = 5;
            if (vel <= 0.1f) vel = 0.1f;

            minhaVelocidade = vel;

            if(consciencia.saida.Saidas[0]>=10 && consciencia.saida.Saidas[0] <= 20)
            {
                angulo = angulo - rotacao;
                MoverAngulo(angulo);
            }

            if (consciencia.saida.Saidas[0] >= 0 && consciencia.saida.Saidas[0] <= 9)
            {
                angulo = angulo + rotacao;
                MoverAngulo(angulo);
            }

            if (consciencia.saida.Saidas[1] >= 10)
            {
                MoverPosicao(minhaPosicao.x + direcao.y, minhaPosicao.y + direcao.x);
            }

            saida = new List<float>();
            saida.Add(consciencia.saida.Saidas[0]);
            saida.Add(consciencia.saida.Saidas[1]);
            saida.Add(consciencia.saida.Saidas[2]);
            saida.Add(consciencia.saida.Saidas[3]);

            VerificarSeEstaParado();
        }

        public void MoverPosicao(float x,float y)
        {
            Canvas.SetTop(this,y);
            Canvas.SetLeft(this,x);
            minhaPosicao.x = x;
            minhaPosicao.y = y;
        }

        public void MoverAngulo(float ang)
        {
            direcao = Centralizador.CalcularAngulo(minhaVelocidade, angulo);
            meuAngulo.Angle = 90 - ang;
            angulo = ang;
        }
        public void IrParaPosicaoInicial()
        {
            MoverPosicao(Centralizador.posicaoInicial.x, Centralizador.posicaoInicial.y);
            MoverAngulo(180);
        }

        public List<float> ObterValoresDeSensoresPuro()
        {
            List<float> retorno = new List<float>();
            foreach(var i in sensores)
            {
                retorno.Add(i.valor);
            }
            return retorno;
        }

        public void AplicarSensores()
        {
            float X1 = minhaPosicao.x + 5;
            float Y1 = minhaPosicao.y + 5;

            sensores = new List<SensorInfo>();

            for(int a = 0; a < Centralizador.QuantidadeDeSensores; a++)
            {
                sensores.Add(new SensorInfo());
                sensores[a].valor = 0;
                sensores[a].x = X1;
                sensores[a].y = Y1;
                sensores[a].p = 0;

                float dispercao = a * (180 / (Centralizador.QuantidadeDeSensores - 1));
                dispercao = dispercao - 360 - angulo;

                for(int b = Centralizador.Distancia_Min_Sensor; b < Centralizador.Distancia_Max_Sensor; b++)
                {
                    float Adjacente = b * (float)(Math.Cos(dispercao * Math.PI / 180));
                    float Oposto = b * (float)(Math.Sin(dispercao * Math.PI / 180));
                    float y = Y1 + Oposto;
                    float x = X1 + Adjacente;
                    int pista = Mapa.Local(x, y);

                    if (pista > 0 )
                    {
                        if (sensores[a].p == 0)
                        {
                            sensores[a].x = x;
                            sensores[a].y = y;
                            sensores[a].valor += Centralizador.PontuacaoDeSensor;
                        }
                    }
                    else
                    {
                        sensores[a].p = 1;
                        break;
                    }
                }

            }

            if (Mapa.Local(X1, Y1) == 0) Morreu();
            
            

            pontos = (int)Mapa.Distancia(X1, Y1);

            if (ultimoPonto == -1)
            {
                ultimoPonto = pontos;
            }
            if (pontos > ultimoPonto)
            {
                ultimoPonto = pontos;
            }
            if (pontos < ultimoPonto)
            {
                pontos = ultimoPonto;
            }
            if (pontos >= Centralizador.MelhorCarroAtual.pontos)
            {
                Centralizador.MelhorCarroAtual = this;
            }
        }

        public void VerificarSeEstaParado()
        {
            contagem++;
            if (contagem == 1)
            {
                ultimoRegistro = pontos;
            }
            if(contagem>=Centralizador.TempoMaximoParado)
            {
                if (ultimoRegistro >= pontos)
                {
                    contagem = 0;
                    ultimoRegistro = -1;
                    Restart();
                    Morreu();
                }
                else
                {
                    contagem = 0;
                    ultimoRegistro = pontos;
                }
            }
        }

        public void Morreu()
        {
            pontos = pontos + pontosExtras;
            if (Centralizador.MelhorCarroMundial != null)
            {
                if(pontos>Centralizador.MelhorCarroMundial.pontuacao && !Centralizador.Simulado)
                {
                    minhaConsciencia.pontuacao = pontos;
                    Centralizador.MelhorCarroMundial = minhaConsciencia;
                }
            }
            else
            {
                if (minhaConsciencia != null)
                {
                    minhaConsciencia.pontuacao = pontos;
                    Centralizador.MelhorCarroMundial = minhaConsciencia;
                }
            }

            Restart();

            if (Centralizador.MelhorCarroMundial != null)
            {
                minhaConsciencia = Centralizador.MelhorCarroMundial;
            }
        }
    }
}
