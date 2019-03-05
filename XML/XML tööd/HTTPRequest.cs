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
            ParsePage();
            Console.ReadKey();
        }

        static async void ParsePage()
        {
            try
            {
                HttpClient webClient = new HttpClient();
                string data = await webClient.GetStringAsync("https://www.yr.no/place/Estonia/Harjumaa/Tallinn/forecast.xml");

                XElement root = XElement.Parse(data);

                Console.WriteLine(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
