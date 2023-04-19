using System.Threading.Tasks;
namespace HtmlParser;

public class Program
{
    public async static Task Main()
    {
        var url = "https://www.nature.com/nature/articles?sort=PubD0ate&year=2022&page=1";
        Console.Write("Input number of pages: ");
        var numberOfPages = int.Parse(Console.ReadLine()!);
        Console.Write("Input type of pages: ");
        var pagesType = Console.ReadLine()!;
        var webScrapper = new WebScrapper(url, numberOfPages, pagesType);
        await webScrapper.Scrap();
    }
}

