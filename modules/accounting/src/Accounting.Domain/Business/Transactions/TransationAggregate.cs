using Accounting.Domain.Business.Transactions.Events;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;

namespace Accounting.Domain.Business.Transactions
{
    public class TransactionAggregate : AggregateRoot<TransactionAggregate, TransactionId>
    {
        private readonly TransactionAggregateState _transactionAggregateState = new TransactionAggregateState();
        public TransactionAggregate(TransactionId id) : base(id)
        {
            Register(_transactionAggregateState);
        }

        public IExecutionResult RegisterTransaction(string customerId, string accountId ,float amount)
        {
            Emit(new TransactionRegisteredEvent(new TransactionEntity(this.Id)
            {                
                AccountId = accountId ,
                CustomerId = customerId,
                Amount = amount
            }));

            return ExecutionResult.Success();
        }

        public void TransactionRegisterComplete(TransactionEntity entity)
        {
            Emit(new TransactionRegisterCompletedEvent(entity));
        }


    }
}