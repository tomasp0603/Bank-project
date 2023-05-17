using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Movement : IMovement
    {
        public Movement() { }
        public Movement(string type, int ammountOfMoney, int balance, int customerId)
        {
            Type = type;
            AmmountOfMoney = ammountOfMoney;
            Balance = balance;
            CustomerId = customerId;
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public int AmmountOfMoney { get; set; }
        public int Balance { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
