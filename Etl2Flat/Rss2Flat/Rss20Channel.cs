using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
namespace Rss2Flat
{


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
}