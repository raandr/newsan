using System;
using System.Xml.Linq;
using System.Collections.Generic;
using Rss2Flat;


namespace Rss2Flat
{

    public enum RssVersion
    {
        v10,
        v20

    }
    class RssFile
    {
        private string fileName;
        private System.Xml.Linq.XElement rssXml;
        protected IEnumerable<System.Xml.Linq.XElement> rssXmlElements;


        RssVersion rssVersionValue;

        public RssVersion rssVersion
        {
            get
            {
                return rssVersionValue;
            }
        }

        protected XName rssName;

        public RssVersion GetVersion()
        {

        }



        public RssFile(string inputFileName)
        {
            
            this.fileName = inputFileName;
            rssXml = System.Xml.Linq.XElement.Load(fileName);
            rssXmlElements = rssXml.DescendantsAndSelf();

            // Checking the version
            // Example: <rss version="2.0">
            rssXml.DescendantsAndSelf((XName)"version");
        }

        public void PrintXml()
        {
            foreach (System.Xml.Linq.XElement ixE in rssXmlElements)
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
