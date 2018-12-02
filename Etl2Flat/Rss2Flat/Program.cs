using System;


namespace Rss2Flat
{
    class Program
    {
        static void Main(string[] args)
        {

            //XmlParser xmlParser = new XmlParser(@"World.xml");
            Rss20 rss20File;


            RssFile rssFile = new RssFile(@"World.xml");

            if (rssFile.rssVersion != RssVersion.v20)
            {
                // Any RSS version that is not RSS 2.0
            }
            
            Rss20 rss20File = new Rss20(@"World.xml");
            rss20File.PrintXml();

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
