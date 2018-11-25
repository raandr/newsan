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

}