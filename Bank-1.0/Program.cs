using ClassLibrary1;
using System.Net.NetworkInformation;

class Program
{
    static void Main()
    {
        //variables needed for the program to check the user
        DatabaseRepository databaseRepository = new DatabaseRepository();
        Customer customer = new Customer();
        string passAux = AskUserAndPassword(customer);
        customer = databaseRepository.RetrieveData(customer.User);
        //verification of the user and password
        if (passAux==customer.Password)
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
            Msg("Invalid username or password");
            Main();
        }
    }

    //function to show the menu
    static void PrintMenu(Customer customer)
    {
        Console.WriteLine("Welcome user: " + customer.User);
        Console.WriteLine("1. See your balance");
        Console.WriteLine("2. Withdraw");
        Console.WriteLine("3. Deposit");
        Console.WriteLine("4. Transfer to another account");
        Console.WriteLine("5. Change Username or Password");
        Console.WriteLine("6. View all movements");
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

                case 5:
                    ChangeUserAndPass(customer);
                    break;

                case 6:
                    ViewAllMovements(customer);
                    break;

                case 0:
                    //changes verification so the loop doesn't continue if the user enters 0
                    verif = false;
                    Console.Clear();
                    break;

                default:
                    //function to show an error message which is the parameter
                    Msg("Enter a valid number");
                    break;
            }
        }
        else
        {
            Msg("Enter a number");
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
        string user;
        Console.Write("Enter the ammount you want to transfer: ");
        if (int.TryParse(Console.ReadLine(), out int ammount))
        {
            Console.Write("Enter the user you want to transfer to: ");
            user = Console.ReadLine();
            if (user == customer.User)
            {
                Console.WriteLine("You can't transfer to yourself");
            }
            else
            {
                operationsRepository.Transfer(user, ammount, customer);
            } 
        }
        else
        {
            Console.WriteLine("Enter a number");
        }
        Console.ReadKey();
        Console.Clear();
    }

    static void ChangeUserAndPass(Customer customer)
    {
        Console.Clear();
        int choice;
        bool verif=true;
        DatabaseRepository databaseRepository = new DatabaseRepository();
        
        do
        {
            Console.WriteLine("1. Change username");
            Console.WriteLine("2. Change password");
            Console.WriteLine("0. Go back");
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        ChgUsername();
                        break;

                    case 2:
                        ChgPassword();
                        break;

                    case 0:
                        verif= false;
                        databaseRepository.UpdateData(customer);
                        break;

                    default:
                        Msg("Enter a valid option");
                        break;
                }
            }
            else
            {
                Msg("Enter a valid option");
            }
        } while (verif);
        Console.Clear();

        void ChgUsername()
        {
            Console.Clear();
            Console.WriteLine("Enter your new username: ");
            string userAux = Console.ReadLine();
            if (userAux == "")
            {
                Msg("Please enter a valid username");
            }
            else if (databaseRepository.CheckIfExists("Username", userAux))
            {
                customer.User = userAux;
                Msg("Your new username is " + customer.User);
            }
            else
            {
                Msg("This username is already in use.");
            }
        }

        void ChgPassword()
        {
            Console.Clear();
            Console.WriteLine("Enter your new password: ");
            string passAux = Console.ReadLine();
            if (passAux.Length < 8)
            {
                Msg("Your password must have a minimum of 8 characters.");
            }
            else if (databaseRepository.CheckIfExists("Password",passAux))
            {
                customer.Password = passAux;
                Msg("Your new password is: " + customer.Password);
            }
            else
            {
                Msg("This password is already in use.");
            }
        }
    }

    static void ViewAllMovements(Customer customer)
    {
        Console.Clear();
        DatabaseRepository databaseRepository = new DatabaseRepository();
        databaseRepository.ShowAllMovements(customer.Id);
        Console.ReadKey();
        Console.Clear();
    }

    static void Msg(string msg)
    {
        Console.Clear();
        Console.WriteLine(msg);
        Console.ReadKey();
        Console.Clear();
    }

    static string AskUserAndPassword(Customer customer)
    {
        string passAux;
        Console.Write("Enter your username: ");
        customer.User = Console.ReadLine();
        System.Console.Write("Enter your password: ");
        passAux = Console.ReadLine();
        Console.Clear();
        return passAux;
    }
}