﻿using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
namespace Rss2Flat
{
    enum Rss20PostEnum
    {
        title,
        guid,
        pubDate,
        url,
        link,
        description,
        language,
        copyright,
        category,
        lastBuildDate,
        image
    }

    class Rss20Post
    {
        protected Dictionary<Rss20PostEnum, string> rss20ChannelElementContents; // One article (post)
        const int numberOfAttributes = sizeof(Rss20PostEnum);

        public Rss20Post(List<Rss20PostEnum> attributeNames, List<string> attributeValues)
        {
            int i;

            rss20ChannelElementContents = new Dictionary<Rss20PostEnum, string>(numberOfAttributes);

            for (i = 0; i < attributeNames.Count; i++)
            {
                rss20ChannelElementContents.Add(attributeNames[i], attributeValues[i]);

            }
        }

        public Rss20Post(Dictionary<Rss20PostEnum, string> values)
        {
            rss20ChannelElementContents = new Dictionary<Rss20PostEnum, string>(values);
        }

        public Rss20Post()
        {
            rss20ChannelElementContents = new Dictionary<Rss20PostEnum, string>();

        }

        public void Set(Rss20PostEnum e, string s)
        {
            rss20ChannelElementContents.Add(e, s);
        }




    }




    class Rss20Channel
    {
        //private List<Rss20Post> rss20ChannelElements;

        // I don't like lambda syntax
        //internal List<Rss20Post> Rss20ChannelElements { get => rss20ChannelElements; set => rss20ChannelElements = value; }

        public List<Rss20Post> rss20ChannelElements
        {
            get
            {
                return rss20ChannelElements;
            }

            set
            {
                rss20ChannelElements = new List<Rss20Post>(value);
            }

        }

        public void Add(Rss20Post Rss20Post)
        {
            rss20ChannelElements.Add(Rss20Post);
        }



    }

    class Rss20File : XmlFile
    {
        XElement rss20Tag;

        IEnumerable<XAttribute> rss20AttributesIE;


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

        List<Rss20XmlNamespace> rss20XmlNamespaces;


        // =========================
        // Main placeholder for data
        List<Rss20Channel> rss20Channels; // List of RSS 2.0 channels
        // =========================

        
        Rss20XmlNamespace rss20XmlNamespace;

        string s;
        XElement rssChannelXmlElement;
        private const string rss20ChannelXmlElementName = "channel";
        private const string rss20PostXmlElementName = "item";

        // need to add rss20Attributes

        public Rss20File(string inputRssFileName) : base(inputRssFileName)
        {
            Rss20Channel r20C;
            Rss20Post r20CE;

            rss20XmlNamespaces = new List<Rss20XmlNamespace>();
            // ======================================================
            // Start of rss attributes extraction
            rss20Tag = base.xmlIE.DescendantsAndSelf().First();
            rss20AttributesIE = rss20Tag.Attributes();

            foreach (XAttribute xA in rss20AttributesIE)
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

            r20C = new Rss20Channel();
            r20CE = new Rss20Post();
            string postElementName;
            
            foreach (XElement iChannel in xmlIE.Descendants(rss20ChannelXmlElementName))
            {
                foreach (XElement iPost in iChannel.Descendants(rss20PostXmlElementName))
                {
                    switch (iPost.Name.LocalName)
                    {
                        // Every new title element defines a new news item 
                        case "title":
                            r20CE.Set(Rss20PostEnum.title, iPost.Value);
                            break;

                        case "link":
                            r20CE.Set(Rss20PostEnum.link, iPost.Value);
                            break;

                        case "description":
                            r20CE.Set(Rss20PostEnum.description, iPost.Value);
                            break;

                        case "language":
                            r20CE.Set(Rss20PostEnum.language, iPost.Value);
                            break;

                        case "copyright":
                            r20CE.Set(Rss20PostEnum.copyright, iPost.Value);
                            break;

                        case "lastBuildDate":
                            r20CE.Set(Rss20PostEnum.lastBuildDate, iPost.Value);
                            break;

                        case "image":
                            r20CE.Set(Rss20PostEnum.image, iPost.Value);
                            break;

                        case "url":
                            r20CE.Set(Rss20PostEnum.url, iPost.Value);
                            break;

                        case "guid":
                            r20CE.Set(Rss20PostEnum.guid, iPost.Value);
                            break;

                        case "pubDate":
                            r20CE.Set(Rss20PostEnum.pubDate, iPost.Value);
                            break;

                        case "category":
                            // Do nothing
                            break;
                    }
                    
                }
                
                r20C.Add(r20CE);

            }



            rss20Channels = new List<Rss20Channel>(this.xmlIE.Descendants(rss20ChannelXmlElementName));
            // There may be several channels inside one XML




            foreach (XElement xE in rss20Channels)
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





    }

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


        // Bad logic, will be rewritten
        /*
        public RssParser(string filename) : base(filename)
        {
            rss20Element = new Dictionary<Rss20Post, string>();
            rss20Channel = new List<Dictionary<Rss20Post, string>>();
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
                            this.rss20Channel.Add(new Dictionary<Rss20Post, string>());
                            this.rss20Channel[i].Add(Rss20Post.title, xmlElement.Value);
                            break;



                        case "link":
                            this.rss20Channel[i].Add(Rss20Post.link, xmlElement.Value);
                            break;

                        case "description":
                            this.rss20Channel[i].Add(Rss20Post.description, xmlElement.Value);
                            break;

                        case "language":
                            this.rss20Channel[i].Add(Rss20Post.language, xmlElement.Value);
                            break;

                        case "copyright":
                            this.rss20Channel[i].Add(Rss20Post.copyright, xmlElement.Value);
                            break;

                        case "lastBuildDate":
                            this.rss20Channel[i].Add(Rss20Post.lastBuildDate, xmlElement.Value);
                            break;

                        case "image":
                            this.rss20Channel[i].Add(Rss20Post.image, xmlElement.Value);
                            break;

                        case "url":
                            // Do
                            break;

                        case "item":
                            // Need to do foreach item
                            break;

                        case "guid":
                            this.rss20Channel[i].Add(Rss20Post.guid, xmlElement.Value);
                            break;

                        case "pubDate":
                            this.rss20Channel[i].Add(Rss20Post.pubDate, xmlElement.Value);
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
                            // this.rss20Channel[i].Add(Rss20Post.link, xmlElement.Value);
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

