using Application.Contracts;
using Application.Messaging.Wallet;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Consumers.Wallet;

public class WithdrawRejectedConsumer(
    IEmailService service,
    ILogger<TransactionRecordedConsumer> logger) : IConsumer<WithdrawRejected>
{
    public async Task Consume(ConsumeContext<WithdrawRejected> context)
    {
        string body = $@"
<p>Xin chào <strong>{context.Message.UserName}</strong>,</p>

<p>Giao dịch rút tiền của bạn đã bị hủy:</p>

<ul>
  <li><strong>Lý do:</strong> {context.Message.Remark}</li>
  <li><strong>Mã yêu cầu:</strong> {context.Message.ExternalTransactionCode}</li>
  <li><strong>Mã giao dịch nội bộ:</strong> {context.Message.TransactionCode}</li>
  <li><strong>Số tiền:</strong> {context.Message.Amount:N0} VND</li>
  <li><strong>Mô tả:</strong> {context.Message.Description ?? "Không có mô tả"}</li>
  <li><strong>Ngày tạo:</strong> {context.Message.CreatedAt:dd/MM/yyyy HH:mm:ss} (UTC)</li>
</ul>

<p>Nếu bạn không thực hiện giao dịch này, vui lòng liên hệ ngay với chúng tôi.</p>

<p>Trân trọng,<br/>Đội ngũ hỗ trợ</p>
";

        await service.SendEmailAsync(context.Message.Email,
            $"DTP-Giao dịch",
            body
        );
        logger.LogInformation($"Consumed Listened: ExternalTransaction {context.Message.ExternalTransactionCode}");
    }
}