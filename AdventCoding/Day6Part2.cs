
using System.Text.RegularExpressions;

public class Day6Part2
{
    public static void Main()
    {
        StreamReader streamReader = new StreamReader("Day6Real.txt");
        long winnerCount = 0;
        long ret = 1;

        //List of values in the seed line
        string[] times = streamReader.ReadLine().Replace(" ", "").Split(':').Where(x => long.TryParse(x, out _)).ToArray();
        string[] distances = streamReader.ReadLine().Replace(" ", "").Split(':').Where(x => long.TryParse(x, out _)).ToArray();

        for (int i = 0; i < times.Length; i++)
        {
            winnerCount = 0;
            long time = long.Parse(times[i]);
            long distance = long.Parse(distances[i]);

            for (long j = 1; j <= time; j++)
            {
                winnerCount += (time - j) * j > distance ? 1 : 0;
            }
            ret = ret * winnerCount;
        }

        //Return the answer
        Console.WriteLine(string.Format("Winners: {0}", ret));
        Console.ReadKey();
    }
}