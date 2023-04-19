using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
namespace HtmlParser;

public class WebScrapper
{
    private string _url;
    private readonly int _numberOfPages;
    private readonly string _pagesType;
    private readonly HttpClient _httpClient;

    public WebScrapper(string url, int numberOfPages, string pagesType)
    {
        _url = url;
        _numberOfPages = numberOfPages;
        _pagesType = pagesType;
        _httpClient = new();
        var baseAddress = new Uri(url).GetLeftPart(UriPartial.Authority);
        _httpClient.BaseAddress = new Uri(baseAddress);
    }
    public async Task Scrap()
    {
        for (int i = 0; i < _numberOfPages; i++)
        {
            var htmlNode = await GetHtmlDocument(_url);
            var directoryName = $"Page{i + 1}";
            Directory.CreateDirectory(directoryName);
            foreach (var j in htmlNode.SelectNodes("//article[@class='u-full-height c-card c-card--flush']"))
            {
                var type = j.SelectSingleNode(".//span[@class='c-meta__type']").InnerText;
                if (!_pagesType.StartsWith(type))
                    continue;
                var title = j.SelectSingleNode(".//a[@class='c-card__link u-link-inherit']").InnerText;
                var url = j.SelectSingleNode(".//a[@class='c-card__link u-link-inherit']").GetAttributeValue("href", "");
                await ParsePage(title, url, directoryName);
                _url = htmlNode.SelectSingleNode("//a[@class='c-pagination__link']").GetAttributeValue("href", "");
            }
        }
    }
    private async Task ParsePage(string title, string url, string directoryName)
    {
        var htmlNode = await GetHtmlDocument(url);
        var article = htmlNode.SelectSingleNode("//main[@class='c-article-main-column u-float-left']");
        var text = string.Join<string>("\n", article.InnerText.Split("\n")
        .Where(x => !string.IsNullOrWhiteSpace(x))
        .Select(x => x.Trim()));
        await File.WriteAllTextAsync($"./{directoryName}/{title}.txt", text);
    }
    private async Task<HtmlNode> GetHtmlDocument(string url)
    {
        var html = await _httpClient.GetStringAsync(url);
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);
        return htmlDocument.DocumentNode;
    }
}
