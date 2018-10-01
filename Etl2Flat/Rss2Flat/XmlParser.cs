using System;
using System.Xml.Linq;
using System.Collections.Generic;
using Rss2Flat;


namespace Rss2Flat
{
    class XmlFile
    {
        protected string fileName;
        protected System.Xml.Linq.XElement fromFile;
        public IEnumerable<System.Xml.Linq.XElement> xmlIE;



        public XmlFile(string inputFileName)
        {
            this.fileName = inputFileName;
            fromFile = System.Xml.Linq.XElement.Load(fileName);
            xmlIE = fromFile.DescendantsAndSelf();
        }

        public void PrintXml()
        {
            foreach (System.Xml.Linq.XElement ixE in xmlIE)
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
