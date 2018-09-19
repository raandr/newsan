using System;


namespace Rss2Flat
{
    class Program
    {
        static void Main(string[] args)
        {

            //XmlParser xmlParser = new XmlParser(@"World.xml");


            RssParser rssParser = new RssParser(@"World.xml");
            rssParser.PrintXml();

            System.Collections.Generic.List<string> order =
                new System.Collections.Generic.List<string>(new string[]
            {   "title",
                "link",
                "description",
                "laguage",
                "copyright",
                "lastBuildDate",
                "image"
            });

            TableWriter tableWriter = new TableWriter(rssParser, order, "flatfile", 25000, '\t');

            String s = Console.ReadLine();


        }
    }
}
