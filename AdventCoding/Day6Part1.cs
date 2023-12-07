
using System.Text.RegularExpressions;

public class Day6Part1
{
    public static void Main()
    {
        StreamReader streamReader = new StreamReader("Day6Real.txt");
        int winnerCount = 0;
        int ret = 1;

        //List of values in the seed line
        string[] times = streamReader.ReadLine().Split(' ').Where(x => int.TryParse(x, out _)).ToArray();
        string[] distances = streamReader.ReadLine().Split(' ').Where(x => int.TryParse(x, out _)).ToArray();

        for (int i = 0; i < times.Length; i++)
        {
            winnerCount = 0;
            int time = int.Parse(times[i]);
            int distance = int.Parse(distances[i]);

            for (int j = 1; j <= time; j++)
            {
                winnerCount += (time - j) * j > distance ? 1 : 0;
            }
            ret = ret * winnerCount;
        }

        //Return the answer
        Console.WriteLine(string.Format("Location: {0}", ret));
        Console.ReadKey();
    }
}