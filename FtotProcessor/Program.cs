using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FtotProcessor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if (args.Count() != 1)
                {
                    throw new Exception("You must supply an RCS FTOT filename.");
                }

                var document = XDocument.Load(args[0]);
                var ns = document.Root.GetDefaultNamespace();
                var ftot2PrintFormat = document.Descendants(ns + "FTOT")
                    .ToDictionary(
                    x => x.Attribute("t").Value,
                    x => x.Element(ns + "Traditional")?.Element(ns + "CCSTFormat")?.Attribute("f")?.Value);

                foreach (var ticketType in ftot2PrintFormat)
                {
                    Console.WriteLine($"ticket type {ticketType.Key} print format {ticketType.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error: " + ex.Message);
            }

        }
    }
}
