using System;
using System.Xml.Linq;
using System.Collections.Generic;
namespace NewsAn
{
    class RssParser : XmlParser
    {
        protected enum Rss20ChannelElement
        {
            title,
            guid,
            pubDate,
            url,
            item,
            link,
            description,
            language,
            copyright,
            category,
            lastBuildDate,
            image
        };

        int i = -1; //See "title"
       

        //Rss20ChannelEnum rss20Element;title,

        //System.Collections.Generic.KeyValuePair<Rss20ChannelElement, string> rss20Element;
        protected Dictionary<Rss20ChannelElement, string> rss20Element;
        protected List<Dictionary<Rss20ChannelElement, string>> rss20Channel;


         // Needs to be rewritten
        public void NewRssParser(string filename) //: base(filename)
        {
            IEnumerable<XNode> xmlChannel;
            rss20Element = new Dictionary<Rss20ChannelElement, string>();
            rss20Channel = new List<Dictionary<Rss20ChannelElement, string>>();
            string xmlElementName;

            foreach (XElement xmlElement in xmlEnum)
            {
                try
                {
                    xmlElementName = xmlElement.Name.ToString();
                    if (xmlElementName == "channel")
                    {
                        xmlChannel = xmlElement.Nodes();

                    }
                }

                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.ToString());

                }

            }


        }
        

        public RssParser(string filename) : base(filename)
        {
            rss20Element = new Dictionary<Rss20ChannelElement, string>();
            rss20Channel = new List<Dictionary<Rss20ChannelElement, string>>();
            string xmlElementName;

            foreach (System.Xml.Linq.XElement xmlElement in xmlEnum)
            {
                try 
                {
                    xmlElementName = xmlElement.Name.ToString();
                    switch (xmlElementName)
                    {
                        // Every new title element defines a new news item 
                        case "title":
                            i++;
                            this.rss20Channel.Add(new Dictionary<Rss20ChannelElement, string>());
                            this.rss20Channel[i].Add(Rss20ChannelElement.title, xmlElement.Value);
                            break;

                            /* ****************************************************************************************** */

                        case "link":
                            this.rss20Channel[i].Add(Rss20ChannelElement.link, xmlElement.Value);
                            break;

                        case "description":
                            this.rss20Channel[i].Add(Rss20ChannelElement.description, xmlElement.Value);
                            break;

                        case "language":
                            this.rss20Channel[i].Add(Rss20ChannelElement.language, xmlElement.Value);
                            break;

                        case "copyright":
                            this.rss20Channel[i].Add(Rss20ChannelElement.copyright, xmlElement.Value);
                            break;

                        case "lastBuildDate":
                            this.rss20Channel[i].Add(Rss20ChannelElement.lastBuildDate, xmlElement.Value);
                            break;

                        case "image":
                            this.rss20Channel[i].Add(Rss20ChannelElement.image, xmlElement.Value);
                            break;

                        case "url":
                            // Do
                            break;

                        case "item":
                            // Need to do foreach item
                            break;

                        case "guid":
                            this.rss20Channel[i].Add(Rss20ChannelElement.guid, xmlElement.Value);
                            break;

                        case "pubDate":
                            this.rss20Channel[i].Add(Rss20ChannelElement.pubDate, xmlElement.Value);
                            break;

                        case "category":
                            // Do nothing
                            break;







                            /*
                             * {http://search.yahoo.com/mrss/}content
                             * {http://search.yahoo.com/mrss/}description
                             * {http://search.yahoo.com/mrss/}credit
                             * {http://purl.org/dc/elements/1.1/}creator
                             * 
                             *
                             */




                        case "{http://www.w3.org/2005/Atom}link":
                            // multiple additions
                            // this.rss20Channel[i].Add(Rss20ChannelElement.link, xmlElement.Value);
                            break;


                        case "rss":
                            // Do nothing
                            break;

                        case "channel":
                            // Do nothing
                            break;





                        default:
                            throw new ArgumentOutOfRangeException(xmlElementName);
                            //break;

                    }



                }


                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.ParamName);

                }

            }
        }


    }



}

