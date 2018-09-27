using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
namespace Rss2Flat
{
    enum Rss20ChannelElementEnum
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
    }

    class Rss20ChannelElement
    {
        protected Dictionary<Rss20ChannelElementEnum, string> rss20ChannelElementContents; // One article (post)
        const int numberOfAttributes = sizeof(Rss20ChannelElementEnum);

        public Rss20ChannelElement(List<Rss20ChannelElementEnum> attributeNames, List<string> attributeValues)
        {
            int i;
            

            rss20ChannelElementContents = new Dictionary<Rss20ChannelElementEnum, string>(numberOfAttributes);

            

            for (i = 0; i < attributeNames.Count; i++)
            {
                rss20ChannelElementContents.Add(attributeNames[i], attributeValues[i]);
                
            }
        }




    }




    class Rss20Channel
    {
        List<Rss20ChannelElement> rss20ChannelElements;






    }

    class Rss20Channels
    {
        List<Rss20Channel> rss20ChannelList; // List of RSS 2.0 channels






    }

    class RssParser : XmlParser
    {
        XElement rssTag;



        protected enum Rss20XmlNamespace
        {
            dc,
            media,
            atom,
            insert
        }

        protected enum CustomXmlNamespace
        {
            nyt

        }

        int i = -1; //See "title"



        List<Rss20XmlNamespace> rss20XmlNamespaces;


        // =========================
        // Main placeholder for data
        Rss20Channels rss20Channels;
        // =========================


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


            // rss20ChannelElement = new Rss20ChannelElement();
            string xmlElementName;
            Rss20XmlNamespace rss20XmlNamespace;
            rss20XmlNamespaces = new List<Rss20XmlNamespace>();
            string s;
            XElement rssChannelXmlElement;
            string rssChannelXmlElementName;


            
            // ======================================================
            // Start of rss attributes extraction
            rssTag = base.fromFile.DescendantsAndSelf().First();
            rssAttributes = rssTag.Attributes();

            foreach (XAttribute xA in rssAttributes)
            {
                s = xA.Name.LocalName;

                switch (s)
                {
                    case "atom":
                        rss20XmlNamespace = Rss20XmlNamespace.atom;
                        rss20XmlNamespaces.Add(rss20XmlNamespace);
                        break;

                    case "dc":
                        rss20XmlNamespace = Rss20XmlNamespace.dc;
                        rss20XmlNamespaces.Add(rss20XmlNamespace);
                        break;

                    case "insert":
                        rss20XmlNamespace = Rss20XmlNamespace.insert;
                        rss20XmlNamespaces.Add(rss20XmlNamespace);
                        break;

                    case "media":
                        rss20XmlNamespace = Rss20XmlNamespace.media;
                        rss20XmlNamespaces.Add(rss20XmlNamespace);
                        break;

                    default:
                        break;


                }

            }
            // End of rss attributes extraction
            // ======================================================



            // Getting the channels
            rssChannelXmlElementName = "channel";
            rssChannels = fromFile.Descendants(rssChannelXmlElementName);
            // There may be several channels inside one XML




            foreach (XElement xE in rssChannels)
            {
                try
                {
                    Console.WriteLine(xE.HasElements);

                    // Parsing the channel as RSS 2.0 channel
                    rss20




                }

                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.ToString());

                }

            }


        }


        // Bad logic, will be rewritten
        /*
        public RssParser(string filename) : base(filename)
        {
            rss20Element = new Dictionary<Rss20ChannelElement, string>();
            rss20Channel = new List<Dictionary<Rss20ChannelElement, string>>();
            string xmlElementName;

            //xmlEnum = 

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







                            //
                             // {http://search.yahoo.com/mrss/}content
                             // {http://search.yahoo.com/mrss/}description
                             // {http://search.yahoo.com/mrss/}credit
                             // {http://purl.org/dc/elements/1.1/}creator





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
            
        }*/


    }



}

