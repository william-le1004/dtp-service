using Application.Contracts;
using Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Consumers;

public class UserAuthenticatedConsumer(ILogger<UserAuthenticatedConsumer> logger, IEmailService emailService)
    : IConsumer<UserAuthenticated>
{
    public Task Consume(ConsumeContext<UserAuthenticated> context)
    {
        var messageBody = MessageBody(context);
        var subject = "Cảm ơn vì đã chọn chúng tôi";
        emailService.SendEmailAsync(context.Message.Email, subject, messageBody);

        logger.LogInformation("User Authenticated Successfully at: {Email}", context.Message.Email);

        return Task.CompletedTask;
    }
    
    private static string MessageBody(ConsumeContext<UserAuthenticated> context)
    {
        var messageBody = $@"
        <body data-new-gr-c-s-loaded=""14.1182.0"" class=""body""
            style=""width:100%;height:100%;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0"">
            <div dir=""ltr"" class=""es-wrapper-color"" lang=""vi"" style=""background-color:#223748"">
                <table width=""100%"" cellspacing=""0"" cellpadding=""0""
                    background=""https://res.cloudinary.com/dtpfpt/image/upload/v1743155330/Logo/l9hnd2rp9vdrmgk6nthp.jpg""
                    class=""es-wrapper"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;
                    background-image:url(https://res.cloudinary.com/dtpfpt/image/upload/c_crop,g_auto,h_800,w_800/Logo/rbi92nln3ofxzrl1ujfh);
                    background-repeat:no-repeat;background-position:center top;background-color:#223748"">
                    <tr style=""border-collapse:collapse"">
                        <td valign=""top"" style=""padding:0;Margin:0"">
                            <table cellspacing=""0"" cellpadding=""0"" align=""center"" class=""es-content"" role=""none""
                                style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:100%;table-layout:fixed !important"">
                                <tr style=""border-collapse:collapse""></tr>
                                <tr style=""border-collapse:collapse"">
                                    <td align=""center"" class=""es-adaptive"" style=""padding:0;Margin:0"">
                                        <table cellspacing=""0"" cellpadding=""0"" align=""center"" class=""es-content-body""
                                            style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px""
                                            role=""none"">
                                            <tr style=""border-collapse:collapse"">
                                                <td align=""left"" style=""padding:10px;Margin:0"">
                                                    <table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""none""
                                                        style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                        <tr style=""border-collapse:collapse"">
                                                            <td align=""center"" valign=""top""
                                                                style=""padding:0;Margin:0;width:580px"">
                                                                <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""none""
                                                                    style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""center""
                                                                            style=""padding:0;Margin:0;display:none""></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table cellspacing=""0"" cellpadding=""0"" align=""center"" class=""es-header"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:100%;table-layout:fixed !important;background-color:transparent;
                                background-repeat:repeat;background-position:center top"">
                                <tr style=""border-collapse:collapse"">
                                    <td align=""center"" style=""padding:0;Margin:0"">
                                        <table cellspacing=""0"" cellpadding=""0"" align=""center"" class=""es-header-body""
                                            style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px""
                                            role=""none"">
                                            <tr style=""border-collapse:collapse"">
                                                <td align=""left""
                                                    style=""Margin:0;padding-top:20px;padding-right:20px;padding-bottom:40px;padding-left:20px;background-repeat:no-repeat"">
                                                    <table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""none""
                                                        style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                        <tr style=""border-collapse:collapse"">
                                                            <td valign=""top"" align=""center""
                                                                style=""padding:0;Margin:0;width:560px"">
                                                                <table width=""100%"" cellspacing=""0"" cellpadding=""0""
                                                                    role=""presentation""
                                                                    style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""center"" class=""es-m-txt-c""
                                                                            style=""padding:0;Margin:0;padding-bottom:40px;font-size:0"">
                                                                            <img src=""https://res.cloudinary.com/dtpfpt/image/upload/v1743151604/Logo/dtp-icon.ico""
                                                                                alt=""Photography logo"" title=""Photography logo""
                                                                                width=""139""
                                                                                style=""display:block;font-size:16px;border:0;outline:none;text-decoration:none"">
                                                                        </td>
                                                                    </tr>
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""center""
                                                                            style=""padding:0;Margin:0;padding-top:30px;padding-bottom:5px"">
                                                                            <h2
                                                                                style=""Margin:0;font-family:georgia, times, 'times new roman', serif;
                                                                                mso-line-height-rule:exactly;letter-spacing:0;
                                                                                font-size:57px;font-style:normal;font-weight:normal;line-height:57px;color:#ffffff"">
                                                                                Welcome<br></h2>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""center""
                                                                            style=""padding:0;Margin:0;font-size:0""><img
                                                                                src=""https://etkweev.stripocdn.email/content/guids/CABINET_6ebdc9f620b6c98ec92e579217982603/images/43981525778959712.png""
                                                                                alt=""to"" title=""to"" width=""42""
                                                                                style=""display:block;font-size:16px;border:0;outline:none;text-decoration:none"">
                                                                        </td>
                                                                    </tr>
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""center""
                                                                            style=""padding:0;Margin:0;padding-bottom:15px"">
                                                                            <h1
                                                                                style=""Margin:0;font-family:georgia, times, 'times new roman', serif;mso-line-height-rule:exactly;
                                                                                letter-spacing:0;font-size:69px;font-style:normal;
                                                                                font-weight:normal;line-height:69px;color:#ffffff"">
                                                                                Binh Dinh</h1>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""center""
                                                                            style=""padding:0;Margin:0;padding-bottom:25px"">
                                                                            <p
                                                                                style=""Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;
                                                                                line-height:24px;letter-spacing:0;color:#ffffff;font-size:16px"">
                                                                                Maldives thu nhỏ giữa lòng miền trung</p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table cellspacing=""0"" cellpadding=""0"" align=""center"" class=""es-content"" role=""none""
                                style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:100%;table-layout:fixed !important"">
                                <tr style=""border-collapse:collapse"">
                                    <td align=""center"" style=""padding:0;Margin:0"">
                                        <table cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center""
                                            class=""es-content-body"" role=""none""
                                            style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px"">
                                            <tr style=""border-collapse:collapse"">
                                                <td align=""left""
                                                    style=""padding:0;Margin:0;padding-top:30px;padding-right:40px;padding-left:40px;background-repeat:no-repeat"">
                                                    <table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""none""
                                                        style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                        <tr style=""border-collapse:collapse"">
                                                            <td valign=""top"" align=""center""
                                                                style=""padding:0;Margin:0;width:520px"">
                                                                <table width=""100%"" cellspacing=""0"" cellpadding=""0""
                                                                    role=""presentation""
                                                                    style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""left"" style=""padding:0;Margin:0"">
                                                                            <h2 class=""es-m-txt-l""
                                                                                style=""Margin:0;font-family:'times new roman', times, baskerville, georgia, serif;mso-line-height-rule:exactly;
                                                                                letter-spacing:0;font-size:28px;font-style:normal;font-weight:normal;line-height:33.6px;color:#333333"">
                                                                                Chúng tôi cảm ơn bạn vì đã chọn DTP.</h2>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""left"" class=""es-m-txt-l""
                                                                            style=""padding:0;Margin:0;font-size:0""><img
                                                                                src=""https://etkweev.stripocdn.email/content/guids/CABINET_6ebdc9f620b6c98ec92e579217982603/images/99301524564595313.png""
                                                                                alt="""" width=""75""
                                                                                style=""display:block;font-size:16px;border:0;outline:none;text-decoration:none"">
                                                                        </td>
                                                                    </tr>
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""left""
                                                                            style=""padding:0;Margin:0;padding-top:15px"">
                                                                            <p style=""Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:24px;
                                                                                letter-spacing:0;color:#666666;font-size:16px"">
                                                                                <span class=""product-description""> Chào mừng {context.Message.Name}
                                                                                    đến với vùng đất võ trời văn – Bình Định,
                                                                                    nơi biển xanh vẫy gọi, lịch sử hào hùng vang
                                                                                    vọng, và con người chân chất, nghĩa tình.
                                                                                </span>
                                                                            </p>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""left""
                                                                            style=""padding:0;Margin:0;padding-top:25px;padding-bottom:10px"">
                                                                            <p
                                                                                style=""Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;
                                                                                line-height:24px;letter-spacing:0;color:#666666;font-size:16px"">
                                                                                Tài Khoản: {context.Message.UserName}</p>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""left""
                                                                            style=""padding:0;Margin:0;padding-top:15px"">
                                                                            <p style=""Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;
                                                                                line-height:24px;
                                                                                letter-spacing:0;color:#666666;font-size:16px"">
                                                                                <span class=""product-description"">Hãy Tận Hưởng
                                                                                    Dịch Vụ Của Chúng Tôi.</span>
                                                                            </p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr style=""border-collapse:collapse"">
                                                <td align=""left""
                                                    style=""Margin:0;padding-top:20px;padding-bottom:40px;padding-right:40px;padding-left:40px"">
                                                    <table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""none""
                                                        style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                        <tr style=""border-collapse:collapse"">
                                                            <td valign=""top"" align=""center""
                                                                style=""padding:0;Margin:0;width:520px"">
                                                                <table width=""100%"" cellspacing=""0"" cellpadding=""0""
                                                                    role=""presentation""
                                                                    style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""left""
                                                                            style=""padding:0;Margin:0;padding-top:5px""><span
                                                                                class=""es-button-border""
                                                                                style=""border-style:solid;border-color:#333333;background:#333333;
                                                                                border-width:0px;display:inline-block;border-radius:5px;width:auto""><a
                                                                                    href=""https://dtp-frontend-three.vercel.app/""
                                                                                    target=""_blank"" class=""es-button""
                                                                                    style=""mso-style-priority:100 !important;text-decoration:none !important;
                                                                                    mso-line-height-rule:exactly;color:#FFFFFF;
                                                                                    font-size:16px;padding:8px 30px 8px 30px;
                                                                                    display:inline-block;background:#333333;
                                                                                    border-radius:5px;font-family:arial, 'helvetica neue', helvetica, sans-serif;
                                                                                    font-weight:normal;font-style:normal;line-height:19.2px;width:auto;
                                                                                    text-align:center;letter-spacing:0;mso-padding-alt:0;mso-border-alt:10px solid #333333"">
                                                                                    Bắt đầu</a></span></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table cellspacing=""0"" cellpadding=""0"" align=""center"" class=""es-content"" role=""none""
                                style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:100%;table-layout:fixed !important"">
                                <tr style=""border-collapse:collapse""></tr>
                                <tr style=""border-collapse:collapse"">
                                    <td align=""center"" style=""padding:0;Margin:0"">
                                        <table cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center""
                                            class=""es-content-body"" role=""none""
                                            style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px"">
                                            <tr style=""border-collapse:collapse"">
                                                <td bgcolor=""#223748"" align=""left""
                                                    style=""padding:0;Margin:0;background-color:#223748"">
                                                    <table cellspacing=""0"" cellpadding=""0"" align=""left"" class=""es-left""
                                                        role=""none""
                                                        style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                                        <tr style=""border-collapse:collapse"">
                                                            <td align=""center"" class=""es-m-p0r""
                                                                style=""padding:0;Margin:0;width:200px"">
                                                                <table width=""100%"" cellspacing=""0"" cellpadding=""0""
                                                                    role=""presentation""
                                                                    style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""right""
                                                                            style=""padding:0;Margin:0;font-size:0""><a
                                                                                target=""_blank""
                                                                                href=""https://res.cloudinary.com/dtpfpt/image/upload/v1743153931/Logo/x5ohj59sjxnajlznof1p.jpg""
                                                                                style=""mso-line-height-rule:exactly;text-decoration:underline;color:#999999;font-size:16px""><img
                                                                                    src=""https://etkweev.stripocdn.email/content/guids/CABINET_e80ba837a76d186bdfe6e7dbe9b74c72e98fd81d05f10e2531f02eb83e4d7db5/images/image_CwT.jpeg""
                                                                                    alt="""" width=""200"" class=""adapt-img""
                                                                                    style=""display:block;font-size:16px;border:0;outline:none;text-decoration:none;border-radius:0""></a>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table cellspacing=""0"" cellpadding=""0"" align=""left"" class=""es-left""
                                                        role=""none""
                                                        style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                                        <tr style=""border-collapse:collapse"">
                                                            <td align=""center"" style=""padding:0;Margin:0;width:200px"">
                                                                <table width=""100%"" cellspacing=""0"" cellpadding=""0""
                                                                    role=""presentation""
                                                                    style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""center""
                                                                            style=""padding:0;Margin:0;font-size:0""><a
                                                                                target=""_blank"" href=""""
                                                                                style=""mso-line-height-rule:exactly;text-decoration:underline;color:#999999;font-size:16px""><img
                                                                                    src=""https://etkweev.stripocdn.email/content/guids/CABINET_e80ba837a76d186bdfe6e7dbe9b74c72e98fd81d05f10e2531f02eb83e4d7db5/images/image_3WW.jpeg""
                                                                                    alt="""" width=""200"" class=""adapt-img""
                                                                                    style=""display:block;font-size:16px;border:0;outline:none;text-decoration:none""></a>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table cellspacing=""0"" cellpadding=""0"" align=""right"" class=""es-right""
                                                        role=""none""
                                                        style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
                                                        <tr style=""border-collapse:collapse"">
                                                            <td align=""center"" style=""padding:0;Margin:0;width:200px"">
                                                                <table width=""100%"" cellspacing=""0"" cellpadding=""0""
                                                                    role=""presentation""
                                                                    style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""left"" style=""padding:0;Margin:0;font-size:0"">
                                                                            <a target=""_blank"" href=""""
                                                                                style=""mso-line-height-rule:exactly;text-decoration:underline;color:#999999;font-size:16px""><img
                                                                                    src=""https://res.cloudinary.com/dtpfpt/image/upload/c_crop,g_auto,h_800,w_800/Logo/ry8qsavrvxven97gcbzx""
                                                                                    alt="""" width=""199"" class=""adapt-img""
                                                                                    style=""display:block;font-size:16px;border:0;outline:none;text-decoration:none""></a>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table cellspacing=""0"" cellpadding=""0"" align=""center"" class=""es-content"" role=""none""
                                style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:100%;table-layout:fixed !important"">
                                <tr style=""border-collapse:collapse"">
                                    <td align=""center"" style=""padding:0;Margin:0"">
                                        <table cellspacing=""0"" cellpadding=""0"" align=""center"" bgcolor=""#ffffff""
                                            class=""es-content-body""
                                            style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#ffffff;width:600px""
                                            role=""none"">
                                            <tr style=""border-collapse:collapse"">
                                                <td align=""left""
                                                    style=""Margin:0;padding-right:20px;padding-left:20px;padding-top:30px;padding-bottom:30px"">
                                                    <table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""none""
                                                        style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                        <tr style=""border-collapse:collapse"">
                                                            <td valign=""top"" align=""center""
                                                                style=""padding:0;Margin:0;width:560px"">
                                                                <table width=""100%"" cellspacing=""0"" cellpadding=""0""
                                                                    role=""presentation""
                                                                    style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                                    <tr style=""border-collapse:collapse"">
                                                                        <td align=""center"" class=""es-infoblock made_with""
                                                                            style=""padding:0;Margin:0;font-size:0""><a
                                                                                target=""_blank""
                                                                                href=""https://res.cloudinary.com/dtpfpt/image/upload/v1743150666/Logo/dtp%20small%20logo.png""
                                                                                style=""mso-line-height-rule:exactly;text-decoration:underline;color:#FFFFFF;font-size:12px""><img
                                                                                    src=""https://res.cloudinary.com/dtpfpt/image/upload/v1743150666/Logo/dtp%20small%20logo.png""
                                                                                    alt="""" width=""125""
                                                                                    style=""display:block;font-size:16px;border:0;outline:none;text-decoration:none""></a>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </body>
        ";
        return messageBody;
    }
}