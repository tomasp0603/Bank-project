using ClassLibrary1;
using System.Net.NetworkInformation;

class Program
{
    static void Main()
    {
        //variables needed for the program to check the pin
        DatabaseRepository databaseRepository = new DatabaseRepository();
        string pin = GetPin();
        Customer customer = databaseRepository.RetrieveData(pin);
        //verification of the pin
        if (customer.Pin!=null)
        {
            bool verification = true;
            //loop everytime the user wants to choice an operation
            do
            {
                PrintMenu(customer);  
                CheckAndChoice(customer, ref verification);
            } while (verification == true);
        }
        else
        {
            IdError();
        }
    }

    static string GetPin()
    {
        string pin;
        System.Console.Write("Please enter your pin: ");
        pin = System.Console.ReadLine();
        Console.Clear();
        return pin;
    }

    //function to show the menu
    static void PrintMenu(Customer customer)
    {
        Console.WriteLine("Welcome user: " + customer.Id);
        Console.WriteLine("1. See your balance");
        Console.WriteLine("2. Withdraw");
        Console.WriteLine("3. Deposit");
        Console.WriteLine("4.Transfer to another account");
        Console.WriteLine("0. Exit the program");
        Console.Write("Enter your choice: ");
    }

    //if for the program not to break if an non-numeric input is read
    static void CheckAndChoice(Customer customer, ref bool verif)
    {
        OperationsRepository operationsRepository = new OperationsRepository();
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            switch (choice)
            {
                case 1:
                    //function to show the balance in another window
                    ShowBalance(customer);
                    break;

                case 2:
                    //function to withdraw money in another window
                    WithdrawMenu(customer, operationsRepository);
                    break;

                case 3:
                    //function to deposit money in another window
                    DepositMenu(customer, operationsRepository);
                    break;

                case 4:
                    //function to transfer money to another account in another window
                    TransferMenu(customer, operationsRepository);
                    break;

                case 0:
                    //changes verification so the loop doesn't continue if the user enters 0
                    verif = false;
                    Console.Clear();
                    break;

                default:
                    //function to show an error message which is the parameter
                    ErrorMsg("Enter a valid number");
                    break;
            }
        }
        else
        {
            ErrorMsg("Enter a number");
        }
    }

    static void ShowBalance(Customer customer)
    {
        Console.Clear();
        Console.WriteLine("Your balance is: " + customer.Balance);
        Console.ReadKey();
        Console.Clear();
    }

    static void WithdrawMenu(Customer customer, OperationsRepository operationsRepository)
    {
        Console.Clear();
        Console.Write("Enter the ammount you want to withdraw: ");
        if (int.TryParse(Console.ReadLine(), out int ammount))
        {
            operationsRepository.Withdraw(ammount, customer);
        }
        else
        {
            Console.WriteLine("Enter a number");
        }
        Console.ReadKey();
        Console.Clear();
    }

    static void DepositMenu(Customer customer, OperationsRepository operationsRepository)
    {
        Console.Clear();
        Console.Write("Enter the ammount you want to deposit: ");
        if (int.TryParse(Console.ReadLine(), out int ammount))
        {
            operationsRepository.Deposit(ammount,customer);
        }
        else
        {
            Console.WriteLine("Enter a number");
        }
        Console.ReadKey();
        Console.Clear();
    }

    static void TransferMenu(Customer customer, OperationsRepository operationsRepository)
    {
        Console.Clear();
        Console.Write("Enter the ammount you want to transfer: ");
        if (int.TryParse(Console.ReadLine(), out int ammount))
        {
            Console.Write("Enter the account id you want to transfer to: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                operationsRepository.Transfer(id,ammount,customer);
            }
            else
            {
                Console.WriteLine("Enter a number");
            }
        }
        else
        {
            Console.WriteLine("Enter a number");
        }
        Console.ReadKey();
        Console.Clear();
    }

    static void ErrorMsg(string msg)
    {
        Console.Clear();
        Console.WriteLine(msg);
        Console.ReadKey();
        Console.Clear();
    }

    static void IdError()
    {
        Console.WriteLine("Invalid Pin");
        Console.ReadKey();
        Console.Clear();
        Main();
    }
}