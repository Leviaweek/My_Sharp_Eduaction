namespace WordsCount;

///<summary>
///class <c>Word</c> contain word and counter number of words this type.
///</summary>
public class Word
{
    /// <value>
    /// Property <c>Text</c> contain word text.
    /// </value>
    public string Text {get; init; }
    
    /// <summary>
    /// Field <c>count</c> is counter of number used words.
    /// </summary>
    public int count = 1;

    /// <summary>
    /// Creating word object
    /// </summary>
    /// <param name="text">Word text.</param>
    public Word(string text)
    {
        Text = text;
    }
}