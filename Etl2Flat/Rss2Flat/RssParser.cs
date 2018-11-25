using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
namespace Rss2Flat
{







    class RssParser : XmlParser
    {

        int i = -1; //See "title"



        // To be completed
        public RssParser(string filename) : base(filename)
        {
            // Need to read not only RSS 2.0 version
            IEnumerable<XAttribute> rssAttributes; // holds rss attributes for the whole XML document
            IEnumerable<XElement> rssChannels;
            IEnumerable<XElement> rssChannel;


            // =================================
            // Main placeholder for data
            rss20Channels = new Rss20Channels();
            // =================================


            // Rss20Post = new Rss20Post();



        }


    }



}

