using System;
// https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/how-to-create-a-basic-rss-feed
// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/how-to-load-xml-from-a-file

namespace my_console
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            XmlParser xmlParser = new XmlParser(@"World.xml");
            xmlParser.PrintXml();

            RssParser rssParser = new RssParser(@"World.xml");

            System.Collections.Generic.List<string> order = 
                new System.Collections.Generic.List<string>(new string[] 
            {   "title", 
                "link", 
                "description", 
                "laguage",
                "copyright",
                "lastBuildDate",
                "image"            
            });

            TableWriter tableWriter = new TableWriter(xmlParser, order, "flatfile", 25000, '\t');

            String s = Console.ReadLine();

        }
    }


    class XmlParser
    {
        protected System.Xml.Linq.XElement fromFile;
        public System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement> xmlEnum;



        public XmlParser(string filename)
        {
            fromFile = System.Xml.Linq.XElement.Load(filename);
            xmlEnum = fromFile.DescendantsAndSelf();
        }

        public void PrintXml()
        {
            foreach (System.Xml.Linq.XElement ixE in xmlEnum)
            {
                if (!ixE.HasElements)
                {
                    Console.Write("Name: ");
                    Console.WriteLine(ixE.Name);

                    Console.Write("Value: ");
                    Console.WriteLine(ixE.Value);

                    Console.Write("Parent name: ");
                    Console.WriteLine(ixE.Parent.Name);

                    Console.WriteLine("-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-");
                    Console.WriteLine("\r\n");

                }
            }


        }
    }


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


    class TableWriter
    {
        char columnDelimiter;
        //System.Collections.Generic.List<string> row;
        string[] row;
        string line;
        byte[] lineBytes;
        int index;
        int chunkSize;
        int chunk;
        int linesWritten;
        string fileName, chunkFile;
        System.Collections.Generic.List<string> fieldOrder;

        System.IO.FileStream fileStrim;
        int fileOffset;

        void RowToLine()
        {
            int lineLength = 0;

            if (row == null)
                return;
            foreach (string str in row)
            {
                if (str == null)
                    continue;
                lineLength += str.Length;
            }

            lineLength += row.Length - 1;

            //line = new string((char)0, lineLength);


            foreach (string str in row)
            {
                line += str;
                line += columnDelimiter;
            }

        }

        void LineToBytes()
        {
            byte b;
            int i = 0;
            lineBytes = new byte[line.Length];
            foreach (char c in line)
            {
                b = (byte) c;
                lineBytes[i] = b;

                i++;
            }

        }

        void WriteFlatFile()
        {


        }


        void CreateNextFlatFileChunk()
        {
            chunk++;
            if (fileStrim != null)
            {
                if (fileStrim.CanWrite)
                    fileStrim.Flush();
                fileStrim.Dispose();

            }


            chunkFile = fileName + "_" + chunk.ToString();
            if (System.IO.File.Exists(chunkFile))
                return;
            fileStrim = System.IO.File.Create(chunkFile);

        }

        void WriteLineToFlatFiles()
        {
            if (!fileStrim.CanWrite)
            {
                //fileStrim.Flush();
                fileStrim.Dispose();
            }

            if (linesWritten >= chunkSize)
            {
                CreateNextFlatFileChunk();
            }

            fileStrim.Write(lineBytes, fileOffset, lineBytes.Length);

            fileOffset += lineBytes.Length;
            linesWritten++;
        }


        public TableWriter(XmlParser xmlP, System.Collections.Generic.List<string> fieldOrder, string fileName, int chunkSize, char columnDelimiter)
        {
            int length;
            int i = 0;
            index = -1;
            length = fieldOrder.Count;
            row = new string[length];
            chunk = 0;
            linesWritten = 0;
            fileOffset = 0;

            this.fieldOrder = fieldOrder;
            this.fileName = fileName;
            this.chunkSize = chunkSize;
            this.columnDelimiter = columnDelimiter;

            CreateNextFlatFileChunk();

            foreach (System.Xml.Linq.XElement xE in xmlP.xmlEnum)
            {
                try
                {
                    index = fieldOrder.IndexOf(xE.Name.ToString());

                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e.Message);
                }

                if (index == -1)
                    continue;

                row[index] = xE.Value;

                if (i >= length)
                {
                    RowToLine();
                    LineToBytes();
                    WriteLineToFlatFiles();

                }

                i++;

            }


        }

    }

}
