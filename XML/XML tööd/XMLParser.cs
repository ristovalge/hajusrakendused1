using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XMLParser
{
    class Program
    {
        static void Main(string[] args)
        {
            XElement kontaktidFromFile = XElement.Load(@"kontaktid.xml");

            foreach (var isik in kontaktidFromFile.Elements())
            {
                Console.WriteLine(isik.Element("eesnimi").Value + " " + isik.Element("perenimi").Value);
                foreach (var kontakt in isik.Element("kontaktid").Elements())
                {
                    Console.WriteLine(kontakt.Attribute("type").Value + " " + kontakt.Value);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
