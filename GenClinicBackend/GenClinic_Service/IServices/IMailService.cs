using GenClinic_Entities.DTOs.Request;

namespace GenClinic_Service.IServices
{
    public interface IMailService
    {
        Task SendMailAsync(MailDto mailData, CancellationToken cancellationToken = default);
    }
}