namespace MoneyKeeper.Budget.Core.Services.Transactions
{
    public interface ITransactionCreator
    {
        Task Create(DTO.TransactionDto dto);
    }
}