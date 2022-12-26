namespace Questioncls;

///<summary>
///Class <c>Questions</c> contains a question and an answer.
///</summary>
public class Questions
{
    /// <value>Property <c>Question</c> is the question text.</value>
    public string Question {get; set; }
    /// <value>Property <c>Answer</c> is the question answer in double type.</value>
    public double Answer {get; set; }

    /// <summary>
    /// This contstructor assigns question and answer values.
    /// </summary>
    /// <param name="question">the new question.</param>
    /// <param name="answer">the new answer.</param>
    public Questions(string question, double answer)
    {
        Question = question;
        Answer = answer;
    }
    /// <summary>
    /// This method allows you to represent an object as a string
    /// </summary>
    public override string ToString()
    {
        return Question;
    }
    /// <summary>
    /// This method check user answer and true answer and return bool
    /// </summary>
    /// <param name="userAnswer">user answer for check.</param>
    public bool CheckAnswer(double userAnswer)
    {
        return Answer == userAnswer;
    }
}