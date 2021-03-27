using System;
using System.Collections.Generic;


namespace RedeNeural
{

        public class FuncaoDeAtivacao
        {
            public enum NomeFuncao
            {
                Relu,
                ReluDx,
                Func,
                SoftMax,
                Sigmoid,
                Inteiro,
                Nenhuma,
                ReluNegativo
            }

            public static List<float> reluNegativo(List<float> X)
            {
                for (int a = 0; a < X.Count; a++)
                {
                    if (X[a] < 0)
                    {
                        X[a] = -1;
                    }
                    else
                    {
                        X[a] = X[a];
                    }
                }
                return X;
            }

            public static List<float> Inteiro(List<float> X)
            {
                for (int a = 0; a < X.Count; a++)
                {
                    X[a] = Math.Abs(X[a]);
                }
                return X;
            }

            public static List<float> func(List<float> X)
            {
                for (int a = 0; a < X.Count; a++)
                {
                    if (X[a] == 0)
                    {
                        X[a] = 0;
                    }
                    else
                    {
                        if (X[a] < 0)
                        {
                            X[a] = -1;
                        }
                        else
                        {
                            X[a] = 1;
                        }
                    }
                }
                return X;
            }

            public static List<float> relu(List<float> X)
            {
                for (int a = 0; a < X.Count; a++)
                {
                    if (X[a] <= 0)
                    {
                        X[a] = 0;
                    }
                    else
                    {
                        X[a] = X[a];
                    }
                }
                return X;
            }

            public static List<float> reluDx(List<float> X)
            {
                for (int a = 0; a < X.Count; a++)
                {
                    if (X[a] < 0)
                    {
                        X[a] = 0;
                    }
                    else
                    {
                        X[a] = 1;
                    }
                }
                return X;
            }

            public static List<float> SoftMax(List<float> X)
            {
                float total = 0;
                for (int a = 0; a < X.Count; a++)
                    total += X[a];
                for (int a = 0; a < X.Count; a++)
                    X[a] = X[a] / total;
                return X;
            }

            public static List<float> Sigmoid(List<float> X)
            {
                for (int a = 0; a < X.Count; a++)
                {
                    X[a] = (float)(2 / (1 + Math.Exp(-2 * X[a])) - 1);
                }
                return X;
            }

            public static List<float> Nada(List<float> x)
            {
                return x;
            }
        }
    }

