
using System.Text.RegularExpressions;

public class Day7Part1
{
    public static void Main()
    {
        StreamReader streamReader = new StreamReader("Day7Test.txt");
        long winnerCount = 0;
        long ret = 0;
        bool done = false;
        int sumValue;

        //List of values in the seed line
        //string[] lines = streamReader.ReadToEnd().Split
        string[] hands = streamReader.ReadLine().Split(' ').ToArray();
        string[] bids = streamReader.ReadLine().Split(' ').ToArray();
        List<int[]> handRanks = new List<int[]>();

        for (int i = 0; i < hands.Length; i++)
        {
            int rank = '0';
            done = false;
            char[] chars = hands[i].ToCharArray();
            for (int j = 0; j < chars.Length; j++)
            {
                
                char[] x = { chars[j] };
                switch (chars.Intersect(x).Count())
                {
                    case 5:
                        rank = 6;
                        done = true;
                        break;
                    case 4:
                        rank = 5;
                        done = true;
                        break;
                    case 3:
                        //Check for full house
                        if (chars.Where(x => x != chars[j]).Distinct().Count() == 1)
                        {
                            rank = 4;
                            done = true;
                            break;
                        }

                        //Otherwise three of a kind
                        rank = 3;
                        done = true;
                        break;
                    case 2:
                        //Check for full house
                        if (chars.Where(x => x != chars[j]).Distinct().Count() == 1)
                        {
                            rank = 4;
                            done = true;
                            break;
                        }

                        //Check for 2 pair
                        if (chars.Where(x => x != chars[j]).Distinct().Count() == 2)
                        {
                            rank = 2;
                            done = true;
                            break;
                        }
                        //Otherwise 1 pair
                        rank = 1;
                        done = true;
                        break;                    
                    default:
                        rank = rank > 0 ? rank : 0;
                        break;
                }                

                if (done == true)
                {
                    int[] ints = new int[7];
                    ints[0] = rank;
                    for (int q  = 1; q <= 5; q++)
                    {
                        ints[q] = int.Parse(hands[i].ToCharArray()[q-1].ToString().Replace("J", "10").Replace("Q", "11").Replace("K", "12").Replace("A", "13"));
                    }
                    ints[6] = int.Parse(bids[i]);
                    handRanks.Add(ints);
                    break;
                }

            }
            
        }

        handRanks = handRanks.OrderBy(x => (x[0] * 6 + x[1] * 5 + x[2] * 4 + x[3] * 3 + x[4] * 2 + x[5] * 1)*-1).ToList();

        for (int i = 1; i <= handRanks.Count; i++)
        {
            int[] ints = handRanks[i - 1];
            ret +=  ints[6] * i;
        }

        //Return the answer
        Console.WriteLine(string.Format("Winners: {0}", ret));
        Console.ReadKey();
    }
}