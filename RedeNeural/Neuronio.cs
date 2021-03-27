
using System.Collections.Generic;


namespace RedeNeural
{
    public class Neuronio
    {
        public float Calcular(List<float> entradas, CamadasPeso pesos)
        {
            float SomatorioTotal = 0;
            for (int a = 0; a < entradas.Count; a++)
            {
                SomatorioTotal += entradas[a] * pesos.pesos[a];
            }
            SomatorioTotal += pesos.bias;
            return SomatorioTotal;
        }
    }
}
