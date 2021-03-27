
using System.Collections.Generic;


namespace RedeNeural
{
    public class Camadas
    {

        public CamadaRetorno Calcular(List<float> entradas, CamadasInfo info, List<CamadasPeso> pesos)
        {
            #region Se não houver pesos
            if (pesos == null)
            {
                pesos = new List<CamadasPeso>();
                for (int a = 0; a < info.QuantidadeDeNeuronio; a++) pesos.Add(new CamadasPeso());

            }
            for (int a = 0; a < pesos.Count; a++)
            {
                if (pesos[a].pesos.Count == 0)
                {
                    pesos[a].pesos = CriadorDePesos(entradas.Count);
                    pesos[a].bias = 1;
                }
            }
            #endregion

            return Calculando(info, entradas, pesos, info.funcaoDeMutacao);

        }
        public CamadaRetorno Calcular(List<float> entradas, CamadaRetorno pai, FuncaoDeMutacao.Funcao tipoMutacao)
        {
            return Calculando(pai.info, entradas, pai.peso, tipoMutacao);
        }
        public CamadaRetorno Simular(List<float> entradas, CamadasInfo pai, List<CamadasPeso> pesos)
        {
            return Calculando(pai, entradas, pesos, FuncaoDeMutacao.Funcao.Nenhuma);
        }
        private CamadaRetorno Calculando(CamadasInfo info, List<float> entradas, List<CamadasPeso> pesos, FuncaoDeMutacao.Funcao tipoMutacao)
        {
            List<float> retornos = new List<float>();
            for (int a = 0; a < info.QuantidadeDeNeuronio; a++)
            {
                RealizarMutacao(info, pesos[a], entradas.Count, tipoMutacao);
                RealizarMutacaoNoBias(pesos[a], info);
                retornos.Add(new Neuronio().Calcular(entradas, pesos[a]));
            }
            retornos = RealizarAtivacao(info, retornos);

            CamadaRetorno r = new CamadaRetorno();
            r.info = info;
            r.peso = pesos;
            r.Saidas = retornos;
            return r;

        }
        private CamadaRetorno Calculando(CamadasInfo info, List<float> entradas, List<CamadasPeso> pesos)
        {
            List<float> retornos = new List<float>();
            for (int a = 0; a < info.QuantidadeDeNeuronio; a++)
            {
                //RealizarMutacao(info, pesos[a], entradas.Count);
                // RealizarMutacaoNoBias(pesos[a],info);
                retornos.Add(new Neuronio().Calcular(entradas, pesos[a]));
            }
            retornos = RealizarAtivacao(info, retornos);

            CamadaRetorno r = new CamadaRetorno();
            r.info = info;
            r.peso = pesos;
            r.Saidas = retornos;
            return r;

        }
        private void RealizarMutacaoNoBias(CamadasPeso pesos, CamadasInfo info)
        {

            //pesos.bias = Aleatorio.RandomNumber();
            pesos.bias = FuncaoDeMutacao.MutacaSuave(pesos.bias);

        }
        private List<float> RealizarAtivacao(CamadasInfo info, List<float> retornos)
        {
            if (info.funcaoDeAtivacao == FuncaoDeAtivacao.NomeFuncao.Nenhuma)
                return retornos;
            if (info.funcaoDeAtivacao == FuncaoDeAtivacao.NomeFuncao.Func)
                retornos = FuncaoDeAtivacao.func(retornos);
            if (info.funcaoDeAtivacao == FuncaoDeAtivacao.NomeFuncao.Relu)
                retornos = FuncaoDeAtivacao.relu(retornos);
            if (info.funcaoDeAtivacao == FuncaoDeAtivacao.NomeFuncao.ReluDx)
                retornos = FuncaoDeAtivacao.reluDx(retornos);
            if (info.funcaoDeAtivacao == FuncaoDeAtivacao.NomeFuncao.Sigmoid)
                retornos = FuncaoDeAtivacao.Sigmoid(retornos);
            if (info.funcaoDeAtivacao == FuncaoDeAtivacao.NomeFuncao.SoftMax)
                retornos = FuncaoDeAtivacao.SoftMax(retornos);
            if (info.funcaoDeAtivacao == FuncaoDeAtivacao.NomeFuncao.ReluNegativo)
                retornos = FuncaoDeAtivacao.reluNegativo(retornos);
            if (info.funcaoDeAtivacao == FuncaoDeAtivacao.NomeFuncao.Inteiro)
                retornos = FuncaoDeAtivacao.Inteiro(retornos);
            return retornos;
        }
        private CamadasPeso RealizarMutacao(CamadasInfo infoC, CamadasPeso peso, int quantidadeDeentrada, FuncaoDeMutacao.Funcao tipoMutacao)
        {
            FuncaoDeMutacao.Funcao info = tipoMutacao;
            if (info == FuncaoDeMutacao.Funcao.MutacaoAdicionada)
                peso.pesos = FuncaoDeMutacao.MutacaoAdicionada(peso.pesos);
            if (info == FuncaoDeMutacao.Funcao.MutacaoAdicionada2)
                peso.pesos = FuncaoDeMutacao.MutacaoAdicionada2(peso.pesos);
            if (info == FuncaoDeMutacao.Funcao.MutacaoCrossOverAdicionado)
                peso.pesos = FuncaoDeMutacao.MutacaoCrossOverAdicionado(peso.pesos);
            if (info == FuncaoDeMutacao.Funcao.MutacaoCrossOverAleatorio)
                peso.pesos = FuncaoDeMutacao.MutacaoCrossOverAleatorio(peso.pesos);
            if (info == FuncaoDeMutacao.Funcao.MutacaoAleatoria)
                peso.pesos = FuncaoDeMutacao.MutacaoAleatoria(quantidadeDeentrada);
            if (info == FuncaoDeMutacao.Funcao.MutacaoSuave)
                peso.pesos = FuncaoDeMutacao.MutacaSuave(peso.pesos);
            return peso;
        }
        private List<float> CriadorDePesos(int quantidade)
        {
            List<float> d = new List<float>();
            for (int a = 0; a < quantidade; a++)
            {
                d.Add(Aleatorio.Obter());
            }
            return d;
        }
    }
}
