#!/bin/bash
mcs -debug -reference:System.Xml.Linq Rss2Flat/XmlParser.cs Rss2Flat/RssParser.cs Rss2Flat/TableWriter.cs Rss2Flat/Program.cs;