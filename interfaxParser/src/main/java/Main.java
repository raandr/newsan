import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.phantomjs.PhantomJSDriver;

import java.io.IOException;

public class Main {

    public static void main(String[] args) throws IOException {
        /*
        WebDriver webDriver = new PhantomJSDriver();
        webDriver.get("http://e-disclosure.ru/poisk-po-soobshheniyam");
        Document doc = Jsoup.parse(webDriver.getPageSource());
        Element newsHeadlines = doc.selectFirst("#searchResults");
        */
        //System.setProperty("phantomjs.binary.path", "libs/phantomjs"); // path to bin file. NOTE: platform dependent
        WebDriver ghostDriver = new PhantomJSDriver();
        try {
            ghostDriver.get("http://e-disclosure.ru/");
            Document doc = Jsoup.parse(ghostDriver.getPageSource());
            Elements newsHeadlines = doc.select("td:contains(09.10.2019)");

            for (Element item : newsHeadlines) {
                String  data = item.data();
            }
            newsHeadlines = doc.select("td:contains(\"08.10.2019\")");
        } finally {
            ghostDriver.quit();
        }

    }

    private static
}
