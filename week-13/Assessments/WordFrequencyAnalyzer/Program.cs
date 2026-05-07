using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class WordFrequencyAnalyzer
{
    private Dictionary<string, int> wordCount;

    private int totalWords;

    public WordFrequencyAnalyzer(string text)
    {
        wordCount = new Dictionary<string, int>();

        ProcessText(text);
    }

    private void ProcessText(string text)
    {
        // Convert to lowercase
        text = text.ToLower();

        // Remove punctuation
        text = Regex.Replace(text, @"[^\w\s]", "");

        // Split into words
        string[] words = text.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries
        );

        totalWords = words.Length;

        // Count frequencies
        foreach (string word in words)
        {
            if (wordCount.ContainsKey(word))
            {
                wordCount[word]++;
            }
            else
            {
                wordCount[word] = 1;
            }
        }
    }

    public void DisplaySummary()
    {
        Console.WriteLine("--- Word Frequency Analysis ---\n");

        Console.WriteLine($"Total words: {totalWords}");

        Console.WriteLine($"Unique words: {wordCount.Count}\n");
    }

    public void DisplayTopNWords(int n)
    {
        Console.WriteLine($"Top {n} Frequent Words:\n");

        var topWords = wordCount
            .OrderByDescending(x => x.Value)
            .ThenBy(x => x.Key)
            .Take(n);

        foreach (var word in topWords)
        {
            Console.WriteLine(
                $"{word.Key}: {word.Value} times"
            );
        }

        Console.WriteLine();
    }

    public void DisplayWordsAppearingOnce()
    {
        Console.WriteLine(
            "Words appearing exactly once:\n"
        );

        var singleWords = wordCount
            .Where(x => x.Value == 1)
            .Select(x => x.Key)
            .OrderBy(x => x);

        Console.WriteLine(
            string.Join(", ", singleWords)
        );

        Console.WriteLine();
    }

    public void DisplayAverageFrequency()
    {
        double average =
            (double)totalWords / wordCount.Count;

        Console.WriteLine(
            $"Average frequency: {average:F2} times per unique word"
        );
    }
}

class Program
{
    static void Main()
    {
        string text = Console.ReadLine();

        int n = int.Parse(Console.ReadLine());

        WordFrequencyAnalyzer analyzer =
            new WordFrequencyAnalyzer(text);

        analyzer.DisplaySummary();

        analyzer.DisplayTopNWords(n);

        analyzer.DisplayWordsAppearingOnce();

        analyzer.DisplayAverageFrequency();
    }
}