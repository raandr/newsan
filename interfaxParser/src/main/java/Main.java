import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.nodes.Node;
import org.jsoup.nodes.TextNode;
import org.jsoup.select.Elements;

import java.io.IOException;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Locale;

public class Main {

    private static String _siteUrl = "http://e-disclosure.ru";

    public static void main(String[] args) throws IOException, ParseException {
        String dateStr = "15.10.2019";

        for (Element item : getLinks(dateStr)) {
            Node dataNode = item.parentNode().childNode(3);

            InterfaxNewsItem newsItem = new InterfaxNewsItem();
            DateFormat dateFormat = new SimpleDateFormat("dd.mm.yyyy", Locale.ENGLISH);
            newsItem.source = ((TextNode)dataNode.childNodes().get(1).childNode(0)).text();
            newsItem.date = dateFormat.parse(dateStr);

            String linkNode = _siteUrl + dataNode.childNode(5).attr("href");

            newsItem.text = getNewsText(linkNode);
            newsItem.text = getNewsText(linkNode);
        }
    }

    private static Elements getLinks(String dateString) throws IOException {
        Document doc = Jsoup.connect(_siteUrl).get();
        Elements newsHeadlines = doc.select("td:contains(" + dateString +")");
        return newsHeadlines;
    }

    private static String getNewsText(String url) throws IOException {
        Document doc = Jsoup.connect(url).get();
        return doc.text();
    }

}