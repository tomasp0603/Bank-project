public interface IOperations
{
    void Withdraw(int ammount, Customer customer);
    void Deposit (int ammount, Customer customer);
    void Transfer (int id, int ammount, Customer customer);
}
