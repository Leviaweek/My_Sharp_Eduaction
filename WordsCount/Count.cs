using System.Text;
namespace WordsCount;

///<summary>
///class <c>Count</c> count number of words in text.
///</summary>
public class Count
{
    /// <summary>
    /// Field <c>words</c> contain all used words.
    /// </summary>
    public List<Word> words = new();

    private static readonly List<char> _separators = new() {',', '.', '!', '?', '"', ';', ':', '-', '%', '$', '#', '(', ')', ' ', '\''};

    /// <summary>
    /// Reading line in text
    /// </summary>
    /// <param name="fileName">File name.</param>
    public void ReadFile(string fileName)
    {
        using var file = new StreamReader(fileName);

        while (true)
        {
            var line = file.ReadLine();
            if (line == null)
                break;
            var splittedText = SplitText(line);
            CountWords(splittedText);
        }
        file.Close();
        SortWords();
        PrintAllWords();
    }

    /// <summary>
    /// Count number of words
    /// </summary>
    /// <param name="splittedText">Splitted Text.</param>
    public void CountWords(List<string> splittedText)
    {
        foreach (var text in splittedText)
        {
            var match = false;

            if (text == "")
                continue;

            foreach (var word in words)
            {
                if (text == word.Text)
                {
                    word.count++;
                    match = true;
                }
            }
            if (!match)
                words.Add(new Word(text));
        }
    }

    /// <summary>
    /// Saving sorted result to file
    /// </summary>
    /// <param name="fileName">File name.</param>
    public void SaveSortedResult(string fileName)
    {
        using var writer = new StreamWriter(fileName, false);
        foreach (var word in words)
            writer.WriteLine($"{word.Text}: {word.count}");
        writer.Close();
    }

    /// <summary>
    /// Words sorting
    /// </summary>
    public void SortWords()
    {
        for (var i = 1; i < words.Count; i++)
            for (var j = 0; j < words.Count - i; j++)
                if (words[j].count < words[j + 1].count)
                    (words[j], words[j + 1]) = (words[j + 1], words[j]);
    }

    /// <summary>
    /// Print all words
    /// </summary>
    public void PrintAllWords()
    {
        foreach (var word in words)
            Console.WriteLine($"{word.Text}: {word.count}");
    }

    /// <summary>
    /// Text split
    /// </summary>
    /// <param name="line">Read line.</param>
    public static List<string> SplitText(string line)
    {
        var text = string.Empty;
        var splittedText = new List<string>();
        foreach (var symbol in line)
        {
            if (_separators.Contains(symbol))
            {
                splittedText.Add(text.Trim().ToLower());
                text = string.Empty;
            }
            else
            {
                text += symbol;
            }
        }
        splittedText.Add(text);
        return splittedText;
    }
}
