using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace ProjetoPista
{
    public class Program
    {
        static public string localDaImagem = "c:\\ai\\mapa.bmp";
        static public string localDoXML = "c:\\ai\\mapa.xml";
        static public int LocalInicial_X = 756;
        static public int LocalInicial_Y = 648;

        static List<Rastro> rastros = new List<Rastro>();
        static Bitmap mapaImg = new Bitmap(localDaImagem);
        static int width = 0;
        static int height = 0;
        static int e = 0;
        static void Main(string[] args)
        {
            Iniciar();
        }

        static void Iniciar()
        {
            width = mapaImg.Width;
            height = mapaImg.Height;

            Rastro primeiroRastro = new Rastro();
            primeiroRastro.x = LocalInicial_X;
            primeiroRastro.y = LocalInicial_Y;
            primeiroRastro.distancia = 0;

            rastrear(primeiroRastro);
            rastrearTudo();
        }

        private static void rastrear(Rastro e)
        {
            if (e.x > width) return;
            if (e.y > height) return;
            if (e.x < 0) return;
            if (e.y < 0) return;
            Adicionar(e.x + 0, e.y + 0, e.distancia);
            Adicionar(e.x + 1, e.y + 0, e.distancia);
            Adicionar(e.x + 1, e.y + 1, e.distancia);
            Adicionar(e.x + 0, e.y + 1, e.distancia);
            Adicionar(e.x - 1, e.y + 1, e.distancia);
            Adicionar(e.x - 1, e.y + 0, e.distancia);
            Adicionar(e.x - 1, e.y - 1, e.distancia);
            Adicionar(e.x + 0, e.y - 1, e.distancia);
            Adicionar(e.x + 1, e.y - 1, e.distancia);
        }

        private static void Adicionar(int x, int y, int pai)
        {
            if (y >= height) return;
            if (x >= width) return;
            if (x < 0) return;
            if (y < 0) return;

            Color c = mapaImg.GetPixel(x, y);
            if(c.R<=10 && c.G<=10 && c.B <= 10)
            {
                mapaImg.SetPixel(x, y, Color.Red);
                Rastro novoRastro = new Rastro();
                novoRastro.x = x;
                novoRastro.y = y;
                novoRastro.distancia = pai + 1;
                rastros.Add(novoRastro);
            }

        }

        private static void rastrearTudo()
        {
            while (true)
            {
                e++;
                if (e >= rastros.Count - 1) break;
                rastrear(rastros[e]);
            }

            Gravar(new Mapas() { map = rastros });
        }

        private static void Gravar(Mapas mapa)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Mapas));
            TextWriter tw = new StreamWriter(localDoXML);
            xs.Serialize(tw, mapa);
            tw.Close();
        }
    }
}
