using System;
namespace NewsAn
{
    class XmlParser
    {
        protected System.Xml.Linq.XElement fromFile;
        public System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement> xmlEnum;



        public XmlParser(string filename)
        {
            fromFile = System.Xml.Linq.XElement.Load(filename);
            xmlEnum = fromFile.DescendantsAndSelf();
        }

        public void PrintXml()
        {
            foreach (System.Xml.Linq.XElement ixE in xmlEnum)
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
