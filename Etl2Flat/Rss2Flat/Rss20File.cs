using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
namespace Rss2Flat
{


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
}
