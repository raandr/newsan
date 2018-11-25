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
	    rss20PostContents.
        }

        public Rss20Post()
        {
            rss20PostContents = new Dictionary<Rss20PostEnum, string>();

        }

        public void Set(Rss20PostEnum e, string s)
        {
            rss20PostContents.Add(e, s);
	    rss20PostContents.
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

        public const string rss20PostTag = "item";

        public string PostTag => rss20PostTag;

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

        struct Rss20Image
        {
            public string title;
            public string url;
            public string link;
        }

        struct Rss20AtomLinkAttr
        {
            public string rel;
            public string type;
            public string href;
        }

        struct Rss20ChannelParams
        {
            public string title;
            public string link;
            public Rss20AtomLinkAttr rss20AtomLinkAttr;
            public string description;
            public string language;
            public string copyright;
            public string lastBuildDate;
            public Rss20Image image;
        }
        Rss20ChannelParams rss20ChannelParams;

        public string Rss20ChannelTitle
        {
            get => rss20ChannelParams.title;
            set => rss20ChannelParams.title = value;
        }

        public string Rss20ChannelLink
        {
            get => rss20ChannelParams.link;
            set => rss20ChannelParams.link = value;
        }

        public string Rss20ChannelDescription
        {
            get => rss20ChannelParams.description;
            set => rss20ChannelParams.description = value;
        }

        public string Rss20ChannelLanguage
        {
            get => rss20ChannelParams.language;
            set => rss20ChannelParams.language = value;
        }

        public string Rss20ChannelCopyright
        {
            get => rss20ChannelParams.copyright;
            set => rss20ChannelParams.copyright = value;
        }

        public string Rss20ChannelLastBuildDate
        {
            get => rss20ChannelParams.lastBuildDate;
            set => rss20ChannelParams.lastBuildDate = value;
        }

        public string Rss20ChannelAtomLinkAttrRel
        {
            get
            {


            }

            set
            {


            }
        }

        public string Rss20ChannelAtomLinkAttrType
        {
            get
            {
                return rss20ChannelParams.rss20AtomLinkAttr.type;
            }

            set
            {
                rss20ChannelParams.rss20AtomLinkAttr.type = value;
            }
        }


        public string Rss20ChannelAtomLinkAttrHref
        {
            get
            {


            }

            set
            {


            }
        }

        public string Rss20ChannelImageTitle
        {
            get
            {
                return rss20ChannelParams.image.title;
            }
            set
            {
                rss20ChannelParams.image.title = value;
            }
        }

        public string Rss20ChannelImageLink
        {
            get
            {
                return rss20ChannelParams.image.link;
            }
            set
            {
                rss20ChannelParams.image.link = value;
            }
        }

        public string Rss20ChannelImageUrl
        {
            get
            {
                return rss20ChannelParams.image.url;
            }
            set
            {
                rss20ChannelParams.image.url = value;
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
        // private const string rss20PostXmlElementName = "item";
        // see PostTag

        // need to add rss20Attributes

        public Rss20File(string inputRssFileName) : base(inputRssFileName)
        {
            
            // Temporary placeholders for data
            Rss20Channel r20C;
            Rss20Post r20P;
            IEnumerable<XAttribute> channelAttr;
            IEnumerable<XElement> iChannelEl;
            

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

            // start of big foreach for each channel in XML
            foreach (XElement iChannel in xmlIE.DescendantsAndSelf(rss20ChannelXmlElementName))
            {
                channelAttr = iChannel.Attributes();
                foreach (XAttribute iCA in channelAttr)
                {
                    switch (iCA.Name)
                    {
                        //case ""
                        default:
                        break;
                    }

                }

                // need to rewrite
                // looking for elements before the posts
                iChannelEl = iChannel.Descendants();

                /*
                foreach (XElement xChAn in iChannel.Ancestors())
                {
                    switch (xChAn.Name.LocalName.ToString())
                    {
                        case "":
                        break;
                    }
                }
                */

                //XElement iCh = iChannelEl.First();
                int i = 0;



                bool exitFlag = false;

                // process data about channel
                foreach (XElement iCh in iChannel.Ancestors())
                {
                    switch (iCh.Name.ToString())
                    {
                        case Rss20Channel.rss20PostTag: // <item>
                        exitFlag = true;
                                               
                        break;


                        case "title":
                        r20C.Rss20ChannelTitle = iCh.Value;
                            break;

                        case "link":
                        r20C.Rss20ChannelLink = iCh.Value;

                            break;
                        case "description":
                        r20C.Rss20ChannelDescription = iCh.Value;

                            break;
                        case "language":
                        r20C.Rss20ChannelLanguage = iCh.Value;

                            break;
                        case "copyright":
                        r20C.Rss20ChannelCopyright = iCh.Value;

                            break;
                        case "lastBuildDate":
                        r20C.Rss20ChannelLastBuildDate = iCh.Value;

                            break;
                        case "image":
                        // <image> has subtags
                        IEnumerable<XElement> xIm;
                        xIm = iChannelEl.Ancestors();
                        foreach (XElement xImX in xIm)
                        {
                            switch (xImX.Name.ToString())
                            {
                                case "tite":
                                r20C.Rss20ChannelImageTitle = xImX.Document.ToString();

                                break;
                                case "link":

                                break;
                                case "url":

                                break;
                            }
                        }
                        // end of <image>
                            break;



                    }
                    // end of switch

                    if (exitFlag)
                        break;

                }
                // end of channel params processing


                // Need to add channel attributes

                // start of post-by-post channel processing
                foreach (XElement iPost in iChannel.Descendants())
                {
                    //r20P.Wipe();
                    r20P.Clear();

                    // start of the post's attributes processing
                    // // post attribute is not an atrubute in terms of XML
                    foreach (XElement iAttr in iPost.Descendants(r20C.PostTag))
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

                    r20C.Add(r20P); // Data is copied into the object
                    // end of post's attributes processing

                }

                rss20Channels.Add(r20C); // Post is added to the Channel

            }
            // end of big foreach for each channel in XML


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

