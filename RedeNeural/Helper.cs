using System.IO;
using System.Xml.Serialization;

namespace RedeNeural
{
    public class Helper
    {
        static public string NomeDoArquivo = "consciencia.xml";

        public static void Gravar(Consciencia p)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Consciencia));
            TextWriter tw = new StreamWriter(@"c:\ai\" + NomeDoArquivo + "");
            xs.Serialize(tw, p);
            tw.Close();
        }

        public static Consciencia Carregar()
        {
            Consciencia pe = null;
            if (File.Exists(@"c:\ai\" + NomeDoArquivo + ""))
            {
                using(var sr=new StreamReader(@"c:\ai\" + NomeDoArquivo))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Consciencia));
                    pe = (Consciencia)xs.Deserialize(sr);
                }
            }
            return pe;
        }

        static MemoryStream tm;

        public static void Gravar(Consciencia p,bool m)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Consciencia));
            tm = new MemoryStream();
            xs.Serialize(tm, p);
        }

        public static Consciencia Carregar(bool m)
        {
            if (tm == null) return null;
            tm.Seek(0, SeekOrigin.Begin);
            Consciencia pe = null;
            XmlSerializer xs = new XmlSerializer(typeof(Consciencia));
            pe = (Consciencia)xs.Deserialize(tm);
            return pe;
        }
    }
}
