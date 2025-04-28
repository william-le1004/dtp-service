using Application.Contracts;
using Application.Messaging.Wallet;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Consumers.Wallet;

public class TransactionRecordedConsumer(
    IEmailService service,
    ILogger<TransactionRecordedConsumer> logger) : IConsumer<TransactionRecordedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<TransactionRecordedIntegrationEvent> context)
    {
        await service.SendEmailAsync(context.Message.Email,
            $"DTP-Giao dịch",
            CreateBody(
                context.Message.TransactionType,
                context.Message.Amount,
                context.Message.Description,
                context.Message.UserName,
                context.Message.CreatedDate,
                context.Message.TransactionCode,
                context.Message.AvailableBalance
            )
        );
        logger.LogInformation($"Consumed Listened: Transaction {context.Message.TransactionCode}");
    }

    private string CreateBody(int type,
        decimal amount,
        string description,
        string userName,
        DateTime date,
        string transactionCode,
        decimal balance)
    {
        var transactionType = ParseTransactionType(type);
        var mark = transactionType switch
        {
            TransactionType.Deposit
                or TransactionType.Receive
                or TransactionType.Refund => "+",
            TransactionType.Withdraw
                or TransactionType.Payment
                or TransactionType.Transfer
                or TransactionType.ThirdPartyPayment => "-",
            TransactionType.Undefined => "",
            _ => ""
        };

        return $@"(DTP): {date:dd/MM/yyyy, HH:mm}<br/>
                 TK: {userName}<br/>
                 PS: {mark}{amount}<br/>
                 SD: {balance}<br/>
                 ND: {description}<br/>
                 SO GD: {transactionCode}";
    }


    private TransactionType ParseTransactionType(int type)
    {
        if (Enum.IsDefined(typeof(TransactionType), type))
        {
            return (TransactionType)type;
        }

        return TransactionType.Undefined;
    }

    private enum TransactionType
    {
        Deposit,
        Withdraw,
        Transfer,
        ThirdPartyPayment,
        Payment,
        Receive,
        Refund,
        Undefined
    }
}