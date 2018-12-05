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


        RssVersion rssVersion;

        public RssVersion Version
        {
            get
            {
                return rssVersion;
            }
        }

        protected XName rssName;

        protected virtual RssVersion GetVersion()
        {
            // Checking the version
            // Example: <rss version="2.0">

            IEnumerable<XElement> rssXElement;
            IEnumerable<XAttribute> versionXAttribute;
            string rssVersionString;

            rssXElement = rssXml.DescendantsAndSelf((XName)"rss");
            versionXAttribute = rssXElement.Attributes((XName)"version");

            // need to check the behaviour of Current
            rssVersionString = versionXAttribute.GetEnumerator().Current.Value;
            switch (rssVersionString)
            {
                case "1.0":
                rssVersion = RssVersion.v10;
                break;

                case "2.0":
                rssVersion = RssVersion.v20;
                break;
            }

        }



        public RssFile(string inputFileName)
        {
            
            this.fileName = inputFileName;
            rssXml = System.Xml.Linq.XElement.Load(fileName);
            rssXmlElements = rssXml.DescendantsAndSelf();

            // Checking the version
            // Example: <rss version="2.0">
            GetVersion();
            
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
