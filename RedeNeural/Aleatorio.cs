using System;


namespace RedeNeural
{
    public class Aleatorio
    {
        private static readonly Random random = new Random();
        private static readonly object syncLook = new object();

        public static float Obter()
        {
            lock (syncLook)
            {
                return (float)random.NextDouble();
            }
        }

        public static int ObterEntre(int min,int max)
        {
            lock (syncLook)
            {
                return random.Next(min, max);
            }
        }
    }
}
