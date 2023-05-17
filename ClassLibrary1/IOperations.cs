public interface IOperations
{
    void Withdraw(int ammount, Customer customer);
    void Deposit (int ammount, Customer customer);
    void Transfer(string user, int ammount, Customer customer);
}
