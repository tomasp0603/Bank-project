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
            }
            else
            {
                Console.WriteLine("Enter a valid number");
            }
        }

        public void Transfer(int id, int ammount, Customer customer)
        {
            if (ammount > 0) 
            {
                if (ammount < customer.Balance)
                {
                    Customer customer2 = databaseRepository.RetrieveData(id);
                    if (customer2.Pin != null)
                    {
                        TransferAndUpdate(customer, customer2, ammount);
                        Console.WriteLine("Your new balance is: " + customer.Balance);
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID");
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
                databaseRepository.UpdateData(customer);
                databaseRepository.UpdateData(customer2);
            }
        }
    }
}