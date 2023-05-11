public interface IDatabase
{
    Customer RetrieveData(string pin);
    Customer RetrieveData(int id);
    void UpdateData(Customer customer);
    string GenerateConnectionString();
}
