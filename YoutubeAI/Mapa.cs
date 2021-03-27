using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace YoutubeAI
{
    public class Mapa
    {
        static Bitmap mp = new Bitmap(ProjetoPista.Program.localDaImagem);
        static int[,,] mps = new int[mp.Width, mp.Height, 2];
        static Mapas mapr = Carregar();

        static int Width = mp.Width;
        static int Height = mp.Height;

        static public int finalDaPista = 0;

        static private Mapas Carregar()
        {
            Mapas pe = null;
            if (File.Exists(ProjetoPista.Program.localDoXML))
            {
                using (var sr = new StreamReader(ProjetoPista.Program.localDoXML))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Mapas));
                    pe = (Mapas)xs.Deserialize(sr);
                }
            }

            return pe;
        }

        static private void Mapear()
        {
            for (int a = 0; a < mapr.map.Count; a++)
            {
                mps[mapr.map[a].x, mapr.map[a].y, 1] = mapr.map[a].distancia;
            }

            finalDaPista = mapr.map[mapr.map.Count - 1].distancia - 50;
            mapr = null;
        }

        static public void Iniciarmapa()
        {
            for (int w = 0; w < mp.Width; w++)
            {
                for (int h = 0; h < mp.Height; h++)
                {
                    Color c = mp.GetPixel((int)w, (int)h);
                    if (c.R <= 15 && c.G <= 15 && c.B <= 15)
                    {
                        mps[w, h, 0] = 1;
                    }
                    else
                    {
                        mps[w, h, 0] = 0;
                    }
                    mps[w, h, 1] = 0;
                }
            }
            mp.Dispose();
            Mapear();
        }
        static public int Local(double x, double y)
        {
            if (double.IsNaN(x)) x = 0;
            if (double.IsNaN(y)) y = 0;
            if (y < 0) y = 0;
            if (y >= Height) y = Height - 1;
            if (x >= Width) x = Width - 1;
            if (x < 0) x = 0;
            return mps[(int)x, (int)y, 0];
        }
        static public int Distancia(double x, double y)
        {
            if (double.IsNaN(x)) x = 0;
            if (double.IsNaN(y)) y = 0;
            if (y < 0) y = 0;
            if (y >= Height) y = Height-1;
            if (x >= Width) x = Width-1;
            if (x < 0) x = 0;
            return mps[(int)x, (int)y, 1];
        }
    }
    public class Mapas
    {
        public List<Rastro> map { get; set; }
    }
    public class Rastro
    {
        public int x { get; set; }
        public int y { get; set; }
        public int distancia { get; set; }
    }
}
