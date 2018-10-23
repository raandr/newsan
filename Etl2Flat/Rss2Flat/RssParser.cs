using System;
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
        protected Dictionary<Rss20PostEnum, string> rss20PostContents; // One article (post)
        const int numberOfAttributes = sizeof(Rss20PostEnum);

        public Rss20Post(List<Rss20PostEnum> attributeNames, List<string> attributeValues)
        {
            int i;

            rss20PostContents = new Dictionary<Rss20PostEnum, string>(numberOfAttributes);

            for (i = 0; i < attributeNames.Count; i++)
            {
                rss20PostContents.Add(attributeNames[i], attributeValues[i]);

            }
        }

        public Rss20Post(Dictionary<Rss20PostEnum, string> values)
        {
            rss20PostContents = new Dictionary<Rss20PostEnum, string>(values);
        }

        public Rss20Post()
        {
            rss20PostContents = new Dictionary<Rss20PostEnum, string>();

        }

        public void Set(Rss20PostEnum e, string s)
        {
            rss20PostContents.Add(e, s);
        }

        public void Clear()
        {
            rss20PostContents.Clear();
        }

        public void Wipe()
        {
            foreach (Rss20PostEnum iK in rss20PostContents.Keys)
            {
                //KeyValuePair cannot be changed
                //var newEntry = new KeyValuePair<Tkey, Tvalue>(oldEntry.Key, newValue);
                //var newEntry = new KeyValuePair<Rss20PostEnum, string>(iK, newValue);
                
                string newValue = "";                
                rss20PostContents[iK] = newValue;
            }
        }




    }




    class Rss20Channel
    {
        //private List<Rss20Post> rss20Posts;

        // I don't like lambda syntax
        //internal List<Rss20Post> rss20Posts { get => rss20Posts; set => rss20Posts = value; }

        public List<Rss20Post> rss20Posts
        {
            get
            {
                return rss20Posts;
            }

            set
            {
                rss20Posts = new List<Rss20Post>(value);
            }

        }

        public void Add(Rss20Post Rss20Post)
        {
            rss20Posts.Add(Rss20Post);
        }



    }

    class Rss20File : XmlFile
    {
        
        // Main placeholder for data
        List<Rss20Channel> rss20Channels; // List of RSS 2.0 channels
        // Channels > Channel > Post
        

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


        
        
        Rss20XmlNamespace rss20XmlNamespace;

        string s;
        XElement rssChannelXmlElement;
        private const string rss20ChannelXmlElementName = "channel";

        // Every post in the channel is inside the <item> tag
        private const string rss20PostXmlElementName = "item";

        // need to add rss20Attributes

        public Rss20File(string inputRssFileName) : base(inputRssFileName)
        {
            
            // Temporary placeholders for data
            Rss20Channel r20C;
            Rss20Post r20P;
            IEnumerable<XAttribute> channelAttr;
            XNode channelNode;
            

            // Initializing the data placeholder of the class
            rss20Channels = new List<Rss20Channel>();
            
            

            rss20XmlNamespaces = new List<Rss20XmlNamespace>();
            
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
            r20P = new Rss20Post();
            string postElementName;

            foreach (XElement iChannel in xmlIE.DescendantsAndSelf(rss20ChannelXmlElementName))
            {
                channelAttr = iChannel.Attributes();
                foreach (XAttribute iCA in channelAttr)
                {
                    switch(iCA.Name)
                    {
                        //case ""
                        default:
                        break;
                    }

                }

                channelNode = iChannel.FirstNode;



                while (channelNode.ToString() != rss20PostXmlElementName)
                {
                    switch (channelNode.ToString())
                    {
                        case "title":
                            break;

                        case "link":

                            break;
                        case "description":

                            break;
                        case "language":

                            break;
                        case "copyright":

                            break;
                        case "lastBuildDate":

                            break;
                        case "image":

                            break;



                    }
                    channelNode = channelNode.NextNode;
                }


                // Need to add channel attributes
                foreach (XElement iPost in iChannel.Descendants())
                {
                    //r20P.Wipe();
                    r20P.Clear();
                    foreach (XElement iAttr in iPost.Descendants(rss20PostXmlElementName))
                    {
                        switch (iAttr.Name.LocalName)
                        {
                            // Every new title element defines a new news item 
                            case "title":
                                r20P.Set(Rss20PostEnum.title, iAttr.Value);
                                break;

                            case "link":
                                r20P.Set(Rss20PostEnum.link, iAttr.Value);
                                break;

                            case "description":
                                r20P.Set(Rss20PostEnum.description, iAttr.Value);
                                break;

                            case "language":
                                r20P.Set(Rss20PostEnum.language, iAttr.Value);
                                break;

                            case "copyright":
                                r20P.Set(Rss20PostEnum.copyright, iAttr.Value);
                                break;

                            case "lastBuildDate":
                                r20P.Set(Rss20PostEnum.lastBuildDate, iAttr.Value);
                                break;

                            case "image":
                                r20P.Set(Rss20PostEnum.image, iAttr.Value);
                                break;

                            case "url":
                                r20P.Set(Rss20PostEnum.url, iAttr.Value);
                                break;

                            case "guid":
                                r20P.Set(Rss20PostEnum.guid, iAttr.Value);
                                break;

                            case "pubDate":
                                r20P.Set(Rss20PostEnum.pubDate, iAttr.Value);
                                break;

                            case "category":
                                // Do nothing
                                break;
                        }

                    }

                    r20C.Add(r20P);


                    // Data is copied into the object



                }

                rss20Channels.Add(r20C);





            }




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

