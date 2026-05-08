using System;

class BankAccount
{
    // Properties
    public string accountNumber { get; }

    private double balance;

    // Constructor
    public BankAccount(string accountNumber, double initialDeposit)
    {
        this.accountNumber = accountNumber;
        balance = initialDeposit;
    }

    // Protected helper for derived classes
    protected double Balance
    {
        get { return balance; }
        set { balance = value; }
    }

    // Deposit Method
    public virtual bool Deposit(double amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        balance += amount;
        return true;
    }

    // Withdraw Method
    public virtual bool Withdraw(double amount)
    {
        if (amount <= 0 || amount > balance)
        {
            return false;
        }

        balance -= amount;
        return true;
    }

    // Get Balance
    public double GetBalance()
    {
        return balance;
    }
}

class SavingsAccount : BankAccount
{
    // Additional Properties
    private double interestRate;
    private double minimumBalance = 1000;

    // Constructor
    public SavingsAccount(string accountNumber,
                          double initialDeposit)
        : base(accountNumber, initialDeposit)
    {
    }

    // Override Withdraw
    public override bool Withdraw(double amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        // Enforce minimum balance rule
        if ((GetBalance() - amount) < minimumBalance)
        {
            Console.WriteLine(
                $"Withdrawal Failed: Minimum balance requirement {minimumBalance}"
            );

            return false;
        }

        Balance -= amount;

        Console.WriteLine($"Withdrawal Successful:{amount}");

        return true;
    }

    // Apply Interest
    public void ApplyInterest(double rate)
    {
        interestRate = rate;

        double interest = GetBalance() * (interestRate / 100);

        Balance += interest;

        Console.WriteLine(
            $"Interest Applied,Rate:{interestRate},New Balance:{GetBalance()}"
        );
    }
}

class CurrentAccount : BankAccount
{
    // Additional Properties
    private double overdraftLimit;
    private double transactionFee;

    // Constructor
    public CurrentAccount(string accountNumber,
                          double initialDeposit,
                          double overdraftLimit = 2000,
                          double transactionFee = 100)
        : base(accountNumber, initialDeposit)
    {
        this.overdraftLimit = overdraftLimit;
        this.transactionFee = transactionFee;
    }

    // Override Withdraw
    public override bool Withdraw(double amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        // Allow overdraft
        if ((GetBalance() + overdraftLimit) < amount)
        {
            Console.WriteLine("Withdrawal Failed: Overdraft limit exceeded");
            return false;
        }

        Balance -= amount;

        Console.WriteLine($"Withdrawal Successful:{amount}");

        return true;
    }

    // Deduct Transaction Fee
    public void DeductTransactionFee()
    {
        Balance -= transactionFee;

        Console.WriteLine(
            $"Fee Deducted,Amount:{transactionFee},Remaining:{GetBalance()}"
        );
    }
}

class Program
{
    static void Main()
    {
        // Input
        string accountType = Console.ReadLine().Trim();

        string accountNumber = Console.ReadLine().Trim();

        double initialDeposit =
            double.Parse(Console.ReadLine().Trim());

        BankAccount account;

        // Create account object
        if (accountType.Equals("Savings",
            StringComparison.OrdinalIgnoreCase))
        {
            account = new SavingsAccount(
                accountNumber,
                initialDeposit
            );
        }
        else
        {
            account = new CurrentAccount(
                accountNumber,
                initialDeposit
            );
        }

        // Multiple operations
        while (true)
        {
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                break;
            }

            string[] parts = input.Split(' ');

            string operation = parts[0];

            // Withdraw
            if (operation.Equals("Withdraw",
                StringComparison.OrdinalIgnoreCase))
            {
                double amount = double.Parse(parts[1]);

                account.Withdraw(amount);
            }

            // Deposit
            else if (operation.Equals("Deposit",
                StringComparison.OrdinalIgnoreCase))
            {
                double amount = double.Parse(parts[1]);

                bool success = account.Deposit(amount);

                if (success)
                {
                    Console.WriteLine(
                        $"Deposit Successful:{amount}"
                    );
                }
            }

            // Get Balance
            else if (operation.Equals("GetBalance",
                StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(
                    $"Current Balance: {account.GetBalance()}"
                );
            }

            // Apply Interest
            else if (operation.Equals("ApplyInterest",
                StringComparison.OrdinalIgnoreCase))
            {
                if (account is SavingsAccount sa)
                {
                    double rate = double.Parse(parts[1]);

                    sa.ApplyInterest(rate);
                }
            }

            // Deduct Fee
            else if (operation.Equals("DeductTransactionFee",
                StringComparison.OrdinalIgnoreCase))
            {
                if (account is CurrentAccount ca)
                {
                    ca.DeductTransactionFee();
                }
            }
        }
    }
}