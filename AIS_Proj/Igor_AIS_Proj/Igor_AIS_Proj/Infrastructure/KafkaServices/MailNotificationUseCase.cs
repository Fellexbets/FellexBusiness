﻿using Igor_AIS_Proj.MailServices;

namespace Igor_AIS_Proj.Infrastructure.KafkaServices
{
    

    
    public class MailNotificationUseCase : IMailNotificationUseCase
    {
        private IMailService _mailService;

        public MailNotificationUseCase(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task MailNotificaiton(TransferTopicInfo info)
        {
            var mailtoReceiver = new MailRequest();
            mailtoReceiver.Body = $" Dear {info.RecipientName}, you just received a transfer of {info.Amount} {info.Currency}, from {info.FromUserName}";
            mailtoReceiver.Subject = "Transfer Received";
            mailtoReceiver.ToEmail = info.RecipientMail;
            await _mailService.SendEmailAsync(mailtoReceiver);

            if (info.RecipientMail != info.FromUserMail)
            {
                var mailToSender = new MailRequest();
                mailToSender.Body = $" Dear {info.FromUserName}, you just executed a transfer of {info.Amount} {info.Currency} to {info.RecipientName}";
                mailToSender.Subject = "Transfer Executed";
                mailToSender.ToEmail = info.FromUserMail;
                await _mailService.SendEmailAsync(mailToSender);
            }
            else return;
        }
    }
}
