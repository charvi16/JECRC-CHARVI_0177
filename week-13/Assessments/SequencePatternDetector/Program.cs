using System;
using System.Collections.Generic;
using System.Linq;

class SequencePatternDetector
{
    private List<int> numbers;

    private Dictionary<int, int> frequencyMap;

    public SequencePatternDetector(List<int> numbers)
    {
        this.numbers = numbers;

        frequencyMap = new Dictionary<int, int>();

        BuildFrequencyMap();
    }

    private void BuildFrequencyMap()
    {
        foreach (int num in numbers)
        {
            if (frequencyMap.ContainsKey(num))
            {
                frequencyMap[num]++;
            }
            else
            {
                frequencyMap[num] = 1;
            }
        }
    }

    // 1. Longest Consecutive Sequence
    public void FindLongestConsecutiveSequence()
    {
        HashSet<int> set = new HashSet<int>(numbers);

        int bestLength = 0;
        int bestStart = 0;

        foreach (int num in set)
        {
            // Start of sequence
            if (!set.Contains(num - 1))
            {
                int current = num;
                int length = 1;

                while (set.Contains(current + 1))
                {
                    current++;
                    length++;
                }

                if (length > bestLength)
                {
                    bestLength = length;
                    bestStart = num;
                }
            }
        }

        List<int> sequence = new List<int>();

        for (int i = 0; i < bestLength; i++)
        {
            sequence.Add(bestStart + i);
        }

        Console.WriteLine(
            $"Longest Consecutive Sequence: " +
            $"{string.Join(",", sequence)} " +
            $"(Length: {bestLength})\n"
        );
    }

    // 2. Most Frequent Element
    public void FindMostFrequentElement()
    {
        var mostFrequent = frequencyMap
            .OrderByDescending(x => x.Value)
            .First();

        Console.WriteLine(
            $"Most Frequent Element: " +
            $"{mostFrequent.Key} " +
            $"(appears {mostFrequent.Value} times)\n"
        );
    }

    // 3. First Non-Repeating Element
    public void FindFirstNonRepeating()
    {
        foreach (int num in numbers)
        {
            if (frequencyMap[num] == 1)
            {
                Console.WriteLine(
                    $"First Non-Repeating Element: {num}\n"
                );

                return;
            }
        }

        Console.WriteLine(
            "No non-repeating element found.\n"
        );
    }

    // 4. Pairs with Difference K
    public void FindPairsWithDifferenceK(int k)
    {
        HashSet<int> set = new HashSet<int>(numbers);

        List<string> pairs = new List<string>();

        foreach (int num in set)
        {
            if (set.Contains(num + k))
            {
                pairs.Add($"({num}, {num + k})");
            }
        }

        Console.WriteLine(
            $"Pairs with Difference {k}:\n"
        );

        Console.WriteLine(
            string.Join(", ", pairs)
        );

        Console.WriteLine();
    }

    // 5. Majority Element
    public void FindMajorityElement()
    {
        int n = numbers.Count;

        var mostFrequent = frequencyMap
            .OrderByDescending(x => x.Value)
            .First();

        double percentage =
            ((double)mostFrequent.Value / n) * 100;

        if (mostFrequent.Value > n / 2)
        {
            Console.WriteLine(
                $"Majority Element: {mostFrequent.Key} " +
                $"(appears {mostFrequent.Value} out of {n} times)"
            );
        }
        else
        {
            Console.WriteLine(
                $"Majority Element: {mostFrequent.Key} " +
                $"(appears {mostFrequent.Value} out of {n} times - " +
                $"{percentage:F1}% - No majority)"
            );
        }
    }
}

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();

        List<int> numbers = input
            .Split(',')
            .Select(int.Parse)
            .ToList();

        int k = int.Parse(Console.ReadLine());

        SequencePatternDetector detector =
            new SequencePatternDetector(numbers);

        Console.WriteLine(
            "--- Access Pattern Analysis ---\n"
        );

        detector.FindLongestConsecutiveSequence();

        detector.FindMostFrequentElement();

        detector.FindFirstNonRepeating();

        detector.FindPairsWithDifferenceK(k);

        detector.FindMajorityElement();
    }
}