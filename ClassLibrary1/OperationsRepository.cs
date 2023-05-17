using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class OperationsRepository : IOperations
    {
        private DatabaseRepository databaseRepository = new DatabaseRepository();
        public void Withdraw(int ammount, Customer customer)
        {
            if (ammount > 0)
            {
                if (ammount < customer.Balance)
                {
                    customer.Balance -= ammount;
                    Console.WriteLine("Your new balance is: " + customer.Balance);
                    databaseRepository.UpdateData(customer);
                    Movement movement = new Movement("Withdraw", ammount, customer.Balance, customer.Id);
                    databaseRepository.InsertMovement(movement);
                }
                else
                {
                    Console.WriteLine("You don't have the sufficient founds to withdraw.");
                }
                
            }
            else
            {
                Console.WriteLine("Enter a valid number");
            }
        }

        public void Deposit(int ammount, Customer customer)
        {
            if (ammount > 0)
            {
                customer.Balance += ammount;
                Console.WriteLine("Your new balance is: " + customer.Balance);
                databaseRepository.UpdateData(customer);
                Movement movement = new Movement("Deposit", ammount, customer.Balance, customer.Id);
                databaseRepository.InsertMovement(movement);
            }
            else
            {
                Console.WriteLine("Enter a valid number");
            }
        }

        public void Transfer(string user, int ammount, Customer customer)
        {
            if (ammount > 0) 
            {
                if (ammount < customer.Balance)
                {
                    Customer customer2 = databaseRepository.RetrieveData(user);
                    if (customer2.User != null)
                    {
                        TransferAndUpdate(customer, customer2, ammount);
                        Console.WriteLine("Your new balance is: " + customer.Balance);
                    }
                    else
                    {
                        Console.WriteLine("User not found");
                    }
                }
                else
                {
                    Console.WriteLine("You don't have the sufficient founds to transfer.");
                }

            }
            else
            {
                Console.WriteLine("Enter a valid number");
            }

            void TransferAndUpdate(Customer customer1, Customer customer2, int ammount)
            {
                customer.Balance -= ammount;
                customer2.Balance += ammount;
                Movement movement = new Movement("Transfer out to " + customer2.User + " (id: " + customer2.Id + ")",ammount, customer.Balance, customer.Id);
                databaseRepository.InsertMovement(movement);
                Movement movement1 = new Movement("Transfer in from " + customer.User + " (id: " + customer.Id + ")", ammount, customer2.Balance, customer2.Id);
                databaseRepository.InsertMovement(movement1);
                databaseRepository.UpdateData(customer);
                databaseRepository.UpdateData(customer2);
            }
        }
    }
}