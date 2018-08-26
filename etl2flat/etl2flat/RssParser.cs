using System;
namespace NewsAn
{
    class RssParser : XmlParser
    {
        protected enum Rss20ChannelElement
        {
            title,
            link,
            description,
            language,
            copyright,
            lastBuildDate,
            image

        };
        int i = -1;
        //Rss20ChannelEnum rss20Element;

        //System.Collections.Generic.KeyValuePair<Rss20ChannelElement, string> rss20Element;
        protected System.Collections.Generic.Dictionary<Rss20ChannelElement, string> rss20Element;
        protected System.Collections.Generic.List<System.Collections.Generic.Dictionary<Rss20ChannelElement, string>> rss20Channel;

        public RssParser(string filename) : base(filename)
        {
            rss20Element = new System.Collections.Generic.Dictionary<Rss20ChannelElement, string>();
            rss20Channel = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<Rss20ChannelElement, string>>();

            foreach (System.Xml.Linq.XElement xmlElement in xmlEnum)
            {
                switch (xmlElement.Name.ToString())
                {
                    case "title":
                        i++;
                        this.rss20Channel.Add(new System.Collections.Generic.Dictionary<Rss20ChannelElement, string>());
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

                    default:

                        break;

                }


            }
        }


    }



}

