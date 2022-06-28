using System.Text.RegularExpressions;

namespace DTP_Assessment_2022
{
    public class ContinuousQuestion : Question
    {
        float answer;
        public ContinuousQuestion(string questionString, float answer) : base(questionString)
        {
            this.answer = answer;
        }
        public override (float, string) GetMultiplier(string guess)
        {
            //try to cast to desired type
            string trimmed = guess.Trim();
            // find index of letters to remove units from answer
            Regex floatCheck = new Regex(@"[0-9]|\."); //all characters which belong in a float eg 17.2743
            List<int> deadChars = new List<int>();
            for (int i = trimmed.Length - 1; i >= 0; i--)
            {
                if (!floatCheck.IsMatch(trimmed[i].ToString()))
                {
                    deadChars.Add(i);
                }
                else break;
            }
            string unitless;
            if (deadChars.Count != 0)
            {
                int lastchar = deadChars[deadChars.Count - 1];
                unitless = trimmed.Substring(0, lastchar);
            }
            else unitless = trimmed;
            float userGuess = float.Parse(unitless);
            /*
            Ratio = userAnswer/answer
            Multiplier = 3 * min(1/Ratio,Ratio)
            Prevent really bad muls
            Multiplier = max(0.5,multiplier)
            */
            // this throws an error which should be caught by the main question loop if the users answer was of the incorrect form.
            float ratio = userGuess / answer;
            float multiplier = (3 * Math.Min(1 / ratio, ratio));
            string quote;
            if (multiplier > 2.99)
            {
                quote = "Dead On!";
            }
            else if(userGuess < answer)
            {
                quote = "Too low.";
            }
            else
            {
                quote = "Too high.";
            }
            return (((float)Math.Max(0.5, multiplier),quote));
        }
        public override string GetQuestion() => questionString + $" (Answers should be formatted like {decimal.Round((decimal)(new Random().NextDouble() * 50), 1)} or {new Random().NextInt64(1, 10)})";
        //show a hint to make it clear to the user the format to answer in
    }
}