using RedeNeural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Rectangle> apresentacaoSensores = new List<Rectangle>();
        List<Label> apresentacaoSaidas = new List<Label>();

        public MainWindow()
        {
            InitializeComponent();

            Mapa.Iniciarmapa();
            Centralizador.ObterMelhorConsciencia();
            Centralizador.Iniciar();

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 1;
            t.Tick += T_Tick;
            CarregarLinhas();
            CarregarRepresentacao();
            CarregarCarros();
            t.Start();
        }

        private void CarregarCarros()
        {
            int mutacao = 1;
            if (Centralizador.Simulado) Centralizador.QuantidadeDeCarros = 1;
            for (int a = 0; a < Centralizador.QuantidadeDeCarros; a++)
            {
                Carro n = new Carro();
                n.Width = 10;
                n.Height = 10;
                n.nomeDaMutacao = NomeDaFuncao(mutacao);
                n.Start();

                if (mutacao >= 6) mutacao = 0;
                mutacao++;
                Centralizador.carros.Add(n);
                mapa.Children.Add(n);
            }
        }

        private FuncaoDeMutacao.Funcao NomeDaFuncao(int mutacao)
        {
            if (mutacao == 0) return FuncaoDeMutacao.Funcao.Livre;
            if (mutacao == 1) return FuncaoDeMutacao.Funcao.MutacaoAdicionada;
            if (mutacao == 2) return FuncaoDeMutacao.Funcao.MutacaoAdicionada2;
            if (mutacao == 3) return FuncaoDeMutacao.Funcao.MutacaoAleatoria;
            if (mutacao == 4) return FuncaoDeMutacao.Funcao.MutacaoCrossOverAdicionado;
            if (mutacao == 5) return FuncaoDeMutacao.Funcao.MutacaoCrossOverAleatorio;
            if (mutacao == 6) return FuncaoDeMutacao.Funcao.MutacaoSuave;
            return FuncaoDeMutacao.Funcao.Nenhuma;
        }

        private void CarregarRepresentacao()
        {
            for (int a = 0; a < Centralizador.QuantidadeDeSensores; a++)
            {
                Rectangle rc = new Rectangle();
                rc.Height = 10;
                rc.Width = 1;
                rc.HorizontalAlignment = HorizontalAlignment.Left;
                rc.VerticalAlignment = VerticalAlignment.Top;
                rc.Margin = new Thickness(2, 2, 2, 0);
                rc.Fill = Brushes.Blue;
                apresentacaoSensores.Add(rc);
                spSensores.Children.Add(rc);
            }
            for (int a = 0; a < 20; a++)
            {
                Label rc = new Label();
                rc.Height = 30;
                rc.HorizontalAlignment = HorizontalAlignment.Left;
                rc.VerticalAlignment = VerticalAlignment.Top;
                rc.Margin = new Thickness(2, 2, 0, 0);
                apresentacaoSaidas.Add(rc);
                sps.Children.Add(rc);
            }
        }
        private void CarregarLinhas()
        {
            for (int a = 0; a < Centralizador.QuantidadeDeSensores; a++)
            {
                Line linha = new Line();
                linha.Stroke = System.Windows.Media.Brushes.Red;
                linha.StrokeThickness = 1;
                Centralizador.linhas.Add(linha);
                Overlay.Children.Add(linha);
            }
        }

        private void T_Tick(object sender, EventArgs e)
        {
            for (int a = 0; a < Centralizador.carros.Count; a++)
            {
                Centralizador.carros[a].Update();
            }
            CalcularIndicadores();
        }

        private void CalcularIndicadores()
        {
            double dif = (Centralizador.Distancia_Max_Sensor - Centralizador.Distancia_Min_Sensor) * Centralizador.PontuacaoDeSensor;
            double maxWidth = 296;

         

            if (Centralizador.MelhorCarroAtual.saida != null)
            {
                for (int a = 0; a < Centralizador.MelhorCarroAtual.saida.Count; a++)
                {
                    apresentacaoSaidas[a].Content = "Saida " + (a + 1) + ":" + Centralizador.MelhorCarroAtual.saida[a] + "";
                }
            }

            if (Centralizador.MelhorCarroMundial != null)
            {
                apresentacaoSaidas[Centralizador.MelhorCarroAtual.saida.Count].Content = "Melhor Pontuação:" + Centralizador.MelhorCarroMundial.pontuacao;
                apresentacaoSaidas[Centralizador.MelhorCarroAtual.saida.Count + 1].Content = "Melhor Tempo:" + Centralizador.melhorTempo;
            }

            for (int a = 0; a < Centralizador.linhas.Count; a++)
            {
                if (Centralizador.QuantidadeDeSensores > 0)
                {
                    Centralizador.linhas[a].X1 = Centralizador.MelhorCarroAtual.minhaPosicao.x + 5;
                    Centralizador.linhas[a].Y1 = Centralizador.MelhorCarroAtual.minhaPosicao.y + 5;

                    Centralizador.linhas[a].X2 = Centralizador.MelhorCarroAtual.sensores[a].x;
                    Centralizador.linhas[a].Y2 = Centralizador.MelhorCarroAtual.sensores[a].y;

                    double widthCorrent = (Centralizador.MelhorCarroAtual.sensores[a].valor * maxWidth) / dif;
                    apresentacaoSensores[a].Width = widthCorrent;
                }
            }
        }
    }
}
