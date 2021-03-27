using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeNeural
{
    public class Cerebro
    {

        public Consciencia Calcular(List<float> entradas,List<CamadasInfo> infos)
        {
            List<CamadaRetorno> camadas = new List<CamadaRetorno>();
            for(int a = 0; a < infos.Count; a++)
            {
                Camadas c = new Camadas();
                if (a == 0)
                {
                    camadas.Add(c.Calcular(entradas, infos[a], null));
                }
                else
                {
                    camadas.Add(c.Calcular(camadas[camadas.Count - 1].Saidas, infos[a], null));
                }
            }

            Consciencia s = new Consciencia();
            s.camadas = camadas;
            s.saida = camadas[camadas.Count - 1];
            s.id = Guid.NewGuid().ToString();
            return s;

        }

        public Consciencia ReCalcular(List<float> entradas, List<CamadasInfo> infos,Consciencia melhorConciencia)
        {
            List<CamadaRetorno> camadas = new List<CamadaRetorno>();
            for (int a = 0; a < infos.Count; a++)
            {
                Camadas c = new Camadas();
                if (a == 0)
                {
                    camadas.Add(c.Calcular(entradas, infos[a], melhorConciencia.camadas[a].peso));
                }
                else
                {
                    camadas.Add(c.Calcular(camadas[camadas.Count - 1].Saidas, infos[a], melhorConciencia.camadas[a].peso));
                }
            }

            Consciencia s = new Consciencia();
            s.camadas = camadas;
            s.saida = camadas[camadas.Count - 1];
            s.id = Guid.NewGuid().ToString();
            return s;

        }

        public Consciencia Simular(List<float> entradas, List<CamadasInfo> infos, Consciencia melhorConciencia)
        {
            List<CamadaRetorno> camadas = new List<CamadaRetorno>();
            for (int a = 0; a < infos.Count; a++)
            {
                Camadas c = new Camadas();
                if (a == 0)
                {
                    camadas.Add(c.Simular(entradas, infos[a], melhorConciencia.camadas[a].peso));
                }
                else
                {
                    camadas.Add(c.Simular(camadas[camadas.Count - 1].Saidas, infos[a], melhorConciencia.camadas[a].peso));
                }
            }

            Consciencia s = new Consciencia();
            s.camadas = camadas;
            s.saida = camadas[camadas.Count - 1];
            return s;
        }
    }
}
