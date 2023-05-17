using System.Data.SqlClient;

public class Customer : ICustomer
{ 
    //properties
    public int Id { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public int Balance { get; set; }
}
