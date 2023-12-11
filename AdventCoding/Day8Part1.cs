
using System.Text.RegularExpressions;

public class Day8Part1
{
    public static void Main()
    {
        StreamReader streamReader = new StreamReader("Day8Real.txt");
        int steps = 0, i = 0;
        string code;
        
        Dictionary<string, string> leftMap = new Dictionary<string, string>();
        Dictionary<string, string> rightMap = new Dictionary<string, string>();
        string[] codes = new string[3];

        //List of values in the seed line
        char[] directions = streamReader.ReadLine().ToCharArray();

        //Populate Maps
        while (streamReader.ReadLine() is { } fileLine)
        {
            codes = fileLine.Replace('=', ',').Replace(@"\s+", " ").Replace("(", "").Replace(")","").Split(',');
            if (fileLine.Length > 1)
            {
                leftMap.Add(codes[0].Trim(), codes[1].Trim());
                rightMap.Add(codes[0].Trim(), codes[2].Trim());
            }
        }
        code = "AAA"; // leftMap.First().Key;

        while (code != "ZZZ")
        {
            steps++;
            if (directions[i] == 'L')
            {
                code = leftMap[code];
            }
            if (directions[i] == 'R')
            {
                code = rightMap[code];
            }
            //Console.WriteLine(string.Format("Direction: {1}, Steps: {0}", code, directions[i]));

            i = i == (directions.Length - 1) ? 0 : i + 1;
        }

        //Return the answer
        Console.WriteLine(string.Format("Steps: {0}", steps));
        Console.ReadKey();
    }
}