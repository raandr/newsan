using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
namespace NewsAn
{
    class XmlParser
    {
        protected XElement fromFile;
        public IEnumerable<XElement> xmlEnum;



        public XmlParser(string filename)
        {
            fromFile = XElement.Load(filename);
            xmlEnum = fromFile.DescendantsAndSelf();
            /*
            XNamespace atom = "http://www.w3.org/2005/Atom";
            xmlEnum =
                from element in fromFile.Elements(atom + "rss")
                where (string)element.Attribute("rel") == "self"
                select element;
            */
        }

        public void PrintXml()
        {
            foreach (XElement ixE in xmlEnum)
            {
                if (!ixE.HasElements)
                {
                    Console.Write("Name: ");
                    Console.WriteLine(ixE.Name);
		    

                    Console.Write("Value: ");
                    Console.WriteLine(ixE.Value);

                    Console.Write("Parent name: ");
                    Console.WriteLine(ixE.Parent.Name);

                    Console.WriteLine("-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-");
                    Console.WriteLine("\r\n");

                }
            }


        }
    }

}
