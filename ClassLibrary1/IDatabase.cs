using ClassLibrary1;

public interface IDatabase
{
    Customer RetrieveData(string user);
    bool CheckIfExists(string column, string valueToAsk);
    void ShowAllMovements(int id);
    Movement RetrieveLastMovement(int id);
    void UpdateData(Customer customer);
    void InsertMovement(Movement movement);
    string GenerateConnectionString();
}
