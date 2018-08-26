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


    class TableWriter
    {
        char columnDelimiter;
        //System.Collections.Generic.List<string> row;

        string[] row;
        char[] line;
        int lineLength;

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
            int i, j;
            int strLength;
            if (row == null)
                return;
            foreach (string str in row)
            {
                if (str == null)
                    continue;
                lineLength += str.Length;
            }

            lineLength += row.Length - 1;

            i = 0;
            foreach (string str in row)
            {
                strLength = str.Length;
                // Needs to be changed - too many new...
                for (j = 0; j < strLength; j++)
                {
                    line[i] = str[j];
                    i++;

                }

                line[i] = columnDelimiter;
            }

        }

        void LineToBytes()
        {
            byte b;
            int i = 0;
            lineBytes = new byte[line.Length];
            foreach (char c in line)
            {
                b = (byte)c;
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
            lineLength = 1024; // length of line
            line = new char[lineLength];
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

