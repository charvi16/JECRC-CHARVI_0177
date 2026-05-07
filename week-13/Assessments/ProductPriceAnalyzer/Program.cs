using System;
using System.Collections.Generic;
using System.Linq;

class ProductPriceAnalyzer
{
    private int[] prices;

    public ProductPriceAnalyzer(int[] prices)
    {
        this.prices = prices;
    }

    // Display Original Prices
    public void DisplayOriginalPrices()
    {
        Console.WriteLine(
            "--- Product Price Analysis ---\n"
        );

        Console.WriteLine(
            "Original Prices: " +
            string.Join(", ", prices) + "\n"
        );
    }

    // Bubble Sort
    public int[] BubbleSort()
    {
        int[] arr = (int[])prices.Clone();

        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = 0; j < arr.Length - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }

        return arr;
    }

    // Selection Sort
    public int[] SelectionSort()
    {
        int[] arr = (int[])prices.Clone();

        for (int i = 0; i < arr.Length - 1; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }

            int temp = arr[i];
            arr[i] = arr[minIndex];
            arr[minIndex] = temp;
        }

        return arr;
    }

    // Binary Search
    public int BinarySearch(int[] sorted, int target)
    {
        int left = 0;
        int right = sorted.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;

            if (sorted[mid] == target)
            {
                return mid;
            }
            else if (sorted[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return -1;
    }

    // Find Pairs with Target Sum
    public void FindPairsWithSum(int target)
    {
        int[] sorted = BubbleSort();

        int left = 0;
        int right = sorted.Length - 1;

        Console.WriteLine(
            $"Pairs that sum to {target}:\n"
        );

        while (left < right)
        {
            int sum = sorted[left] + sorted[right];

            if (sum == target)
            {
                Console.WriteLine(
                    $"({sorted[left]}, {sorted[right]})"
                );

                left++;
                right--;
            }
            else if (sum < target)
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        Console.WriteLine();
    }

    // Longest Increasing Subsequence
    public void FindLongestIncreasingSubsequence()
    {
        int n = prices.Length;

        int[] dp = new int[n];
        int[] prev = new int[n];

        Array.Fill(dp, 1);
        Array.Fill(prev, -1);

        int maxLength = 1;
        int lastIndex = 0;

        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (prices[j] < prices[i] &&
                    dp[j] + 1 > dp[i])
                {
                    dp[i] = dp[j] + 1;
                    prev[i] = j;
                }
            }

            if (dp[i] > maxLength)
            {
                maxLength = dp[i];
                lastIndex = i;
            }
        }

        List<int> lis = new List<int>();

        while (lastIndex != -1)
        {
            lis.Add(prices[lastIndex]);
            lastIndex = prev[lastIndex];
        }

        lis.Reverse();

        Console.WriteLine(
            "Longest Increasing Subsequence:\n"
        );

        Console.WriteLine(
            $"{string.Join(", ", lis)} " +
            $"(Length: {maxLength})\n"
        );
    }

    // Statistics
    public void DisplayStatistics(int[] sorted)
    {
        double average = prices.Average();

        double median;

        int n = sorted.Length;

        if (n % 2 == 0)
        {
            median =
                (sorted[n / 2 - 1] + sorted[n / 2]) / 2.0;
        }
        else
        {
            median = sorted[n / 2];
        }

        Console.WriteLine("Statistics:\n");

        Console.WriteLine(
            $"Lowest Price: {sorted.First()}"
        );

        Console.WriteLine(
            $"Highest Price: {sorted.Last()}"
        );

        Console.WriteLine(
            $"Average Price: {average:F2}"
        );

        Console.WriteLine(
            $"Median Price: {median:F2}"
        );
    }
}

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();

        int[] prices = input
            .Split(',')
            .Select(x => int.Parse(x.Trim()))
            .ToArray();

        int targetSum = int.Parse(Console.ReadLine());

        ProductPriceAnalyzer analyzer =
            new ProductPriceAnalyzer(prices);

        analyzer.DisplayOriginalPrices();

        int[] sorted = analyzer.BubbleSort();

        Console.WriteLine(
            "Sorted Prices (Ascending): " +
            string.Join(", ", sorted) + "\n"
        );

        Console.WriteLine(
            "Binary Search Results:\n"
        );

        int index399 =
            analyzer.BinarySearch(sorted, 399);

        if (index399 != -1)
        {
            Console.WriteLine(
                $"Price 399 found at index {index399}"
            );
        }
        else
        {
            Console.WriteLine(
                "Price 399 not found"
            );
        }

        int index500 =
            analyzer.BinarySearch(sorted, 500);

        if (index500 != -1)
        {
            Console.WriteLine(
                $"Price 500 found at index {index500}"
            );
        }
        else
        {
            Console.WriteLine(
                "Price 500 not found"
            );
        }

        Console.WriteLine();

        analyzer.FindPairsWithSum(targetSum);

        analyzer.FindLongestIncreasingSubsequence();

        analyzer.DisplayStatistics(sorted);
    }
}