using System;
// https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/how-to-create-a-basic-rss-feed
// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/how-to-load-xml-from-a-file

namespace NewsAn
{
    class MainClass
    {
        public static void Main(string[] args)
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