public abstract class Question
{
    public string questionString;
    public Question(string questionString)
    {
        this.questionString = questionString;
    }
    public abstract string GetQuestion();
    public abstract (float,string) GetMultiplier(string answer);
}