using System;
using System.Collections.Generic;
using System.Linq;

class Query
{
    protected List<int> dataSource;
    protected bool isExecuted;

    public Query(List<int> data)
    {
        dataSource = data;
        isExecuted = false;
    }

    public virtual IEnumerable<int> Apply()
    {
        return dataSource.AsEnumerable();
    }

    public virtual List<int> Execute()
    {
        isExecuted = true;
        return Apply().ToList();
    }

    public virtual string GetQueryType()
    {
        return "Base Query";
    }
}

class FilterQuery : Query
{
    private string predicate;
    private int filteredCount;

    public FilterQuery(List<int> data, string predicate)
        : base(data)
    {
        this.predicate = predicate;
    }

    public override IEnumerable<int> Apply()
    {
        IEnumerable<int> query = dataSource;

        if (predicate.StartsWith(">"))
        {
            int value = int.Parse(predicate.Substring(1));
            query = query.Where(x => x > value);
        }
        else if (predicate.StartsWith("<"))
        {
            int value = int.Parse(predicate.Substring(1));
            query = query.Where(x => x < value);
        }
        else if (predicate.ToLower() == "even")
        {
            query = query.Where(x => x % 2 == 0);
        }
        else if (predicate.ToLower() == "odd")
        {
            query = query.Where(x => x % 2 != 0);
        }

        return query;
    }

    public override List<int> Execute()
    {
        List<int> result = Apply().ToList();

        filteredCount = result.Count;
        isExecuted = true;

        Console.WriteLine(
            $"Filter Executed,Predicate:{predicate},Result Count:{filteredCount}"
        );

        return result;
    }

    public override string GetQueryType()
    {
        return "Filter Query";
    }
}

class AggregateQuery : Query
{
    private string operation;
    private double result;

    public AggregateQuery(List<int> data, string operation)
        : base(data)
    {
        this.operation = operation;
    }

    public override IEnumerable<int> Apply()
    {
        return dataSource.AsEnumerable();
    }

    public override List<int> Execute()
    {
        IEnumerable<int> query = Apply();

        switch (operation.ToLower())
        {
            case "sum":
                result = query.Sum();
                break;

            case "average":
                result = query.Average();
                break;

            case "max":
                result = query.Max();
                break;

            case "min":
                result = query.Min();
                break;
        }

        isExecuted = true;

        Console.WriteLine(
            $"Aggregation Executed,Operation:{operation},Result:{result}"
        );

        return query.ToList();
    }

    public override string GetQueryType()
    {
        return "Aggregate Query";
    }
}

class Program
{
    static void Main()
    {
        string queryType = Console.ReadLine().Trim();

        List<int> data = Console.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        string operationOrPredicate = Console.ReadLine().Trim();

        Query query;

        if (queryType.Equals("Filter", StringComparison.OrdinalIgnoreCase))
        {
            query = new FilterQuery(data, operationOrPredicate);
        }
        else
        {
            query = new AggregateQuery(data, operationOrPredicate);
        }

        query.Execute();
    }
}