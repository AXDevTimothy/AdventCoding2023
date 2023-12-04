public class Day3Part2
{
    public static void Main()
    {
        StreamReader streamReader = new StreamReader("PuzzleInput.txt");
        List<String> partList = new List<string>();
        int maxColumn = 0; int maxRow = 0;
        string number, row;
        int sumValue = 0, numStart = 0, numEnd = 0;
        char value;
        List<Int32> numbers = new List<int>();  

        //Populate PartList
        while (streamReader.ReadLine() is { } fileLine)
        {
            maxColumn = fileLine.Length;
            partList.Add(fileLine);
        }
        maxRow = partList.Count;

        for (int x = 0; x < maxRow; x++)
        {
            row = partList[x];
            number = ""; numStart = 0; numEnd = 0;

            for (int i = 0; i < row.Length; i++)
            {
                value = row[i];

                if (value == '*')
                {
                    numbers.Clear();
                  
                    for (int r = (x == 0 ? 0 : x - 1); r <= (x == maxRow - 1 ? x : x + 1); r++)
                    {
                        numStart = 0; numEnd = 0;

                        for (int c = (i == 0 ? 0 : i - 1); c <= (i == maxColumn - 1 ? i : i + 1); c++)
                        {
                            //Skip this coordinate if it was already included in a previously found number
                            if (c <= numEnd + 1)
                                continue;

                            if (char.IsDigit(partList[r][c]))
                            {
                                numStart = findStart(r, c);
                                numEnd = findEnd(r, c);
                                numbers.Add(Int32.Parse(partList[r].Substring(numStart, numEnd - numStart + 1)));
                            }

                            if (numbers.Count > 2)
                                Console.WriteLine(string.Format("HIT Row {0}, Col {1}", r, c));
                        }
                    }

                    if (numbers.Count == 2)
                    {
                        sumValue += numbers[0] * numbers[1];
                        //Console.WriteLine(string.Format("HIT Row {0}, Col {1}", numbers[0], numbers[1]));
                    }
                }
            }
        }
        Console.WriteLine(sumValue.ToString());
        Console.ReadKey();

        int findStart(int row, int column)
        {                       
            char[] partListRow = partList[row].ToArray();

            if(column != 0 && char.IsDigit(partListRow[column - 1]))
            {
                return findStart(row, column - 1);
            }            

            return column;
        }
        int findEnd(int row, int column)
        {
            char[] partListRow = partList[row].ToArray();

            if (column != maxColumn - 1 && char.IsDigit(partListRow[column + 1]))
            {
                return findEnd(row, column + 1);
            }

            return column;
        }
    }


    

}