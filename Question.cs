public abstract class Question
{
    public string questionString;
    public Question(string questionString)
    {
        this.questionString = questionString;
    }
    public abstract string getQuestion();
    public abstract (float,string) getMultiplier(string answer);
}