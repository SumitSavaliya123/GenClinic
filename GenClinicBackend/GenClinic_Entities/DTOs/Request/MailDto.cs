using Microsoft.AspNetCore.Http;

namespace GenClinic_Entities.DTOs.Request
{
    public class MailDto
    {
        public string ToEmail { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public List<IFormFile> Attachments { get; set; } = new List<IFormFile>();
    }
    public class MailSettingDto
    {
        public string? Mail { get; set; }

        public string? DisplayName { get; set; }

        public string? Password { get; set; }

        public string? Host { get; set; }

        public int Port { get; set; }
    }

}