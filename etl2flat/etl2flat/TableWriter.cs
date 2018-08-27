using System;
using System.Text;


namespace NewsAn
{

    class TableWriter
    {
        char columnDelimiter;
        char[] rowDelimiter;
        byte[] rowDelimiterBytes;
        //System.Collections.Generic.List<string> row;

        string[] row;
        string line;
        StringBuilder lineBuilder;
        int lineLength;

        byte[] lineBytes;
        int index;
        int chunkSize;
        int chunk;
        int linesWritten;
        string fileName, chunkFile;
        System.Collections.Generic.List<string> fieldOrder;

        System.IO.FileStream fileStrim;
        int filePosition;

        void RowToLine()
        {

            lineLength = 0;

            if (row == null)
                return;
            foreach (string str in row)
            {
                if (str == null)
                    continue;
                lineLength += str.Length;
            }

            lineLength += row.Length - 1;

         
            lineBuilder.Clear();
            foreach (string str in row)
            {
                if (str == null)
                    continue;

                lineBuilder.Append(str);
                lineBuilder.Append(columnDelimiter);

            }

            line = lineBuilder.ToString();

        }

        void LineToBytes()
        {

            lineBytes = new UTF8Encoding(true).GetBytes(line);

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
            if (fileStrim == null)
                return;

            if (!fileStrim.CanWrite)
            {
                //fileStrim.Flush();
                fileStrim.Dispose();
            }

            if (linesWritten >= chunkSize)
            {
                CreateNextFlatFileChunk();
            }

            fileStrim.Write(lineBytes, 0, lineLength);
            fileStrim.Write(rowDelimiterBytes, 0, rowDelimiterBytes.Length);

            filePosition += lineLength;

            linesWritten++;
        }


        public TableWriter(XmlParser xmlP, System.Collections.Generic.List<string> fieldOrder, string fileName, int chunkSize, char columnDelimiter)
        {
            int length;
            int i = 0;
            index = -1;
            length = fieldOrder.Count;
            row = new string[length];
            lineLength = 0; // length of line
            lineBuilder = new StringBuilder();
            chunk = 0;
            linesWritten = 0;
            filePosition = 0;
            rowDelimiter = new char[2];
            rowDelimiter[0] = '\r';
            rowDelimiter[1] = '\n';

            rowDelimiterBytes = new UTF8Encoding(true).GetBytes(rowDelimiter, 0, rowDelimiter.Length);

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
                    i = 0;
                    continue;
                }

                i++;

            }


        }

    }

}
