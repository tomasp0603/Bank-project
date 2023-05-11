using System.Data.SqlClient;

public class Customer : ICustomer
{ 
    //properties
    public int Id { get; set; }
    public string Pin { get; set; }
    public int Balance { get; set; }
}
