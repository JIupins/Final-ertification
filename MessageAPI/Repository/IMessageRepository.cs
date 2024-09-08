using MessagingService.Models.DTO;

namespace MessagingService.Repository
{
    public interface IMessageRepository
    {
        public Guid AddMessage(string emailFrom, string emailTo, string text);

        public IEnumerable<MessageEmailsDto> GetMessages(string emailTo);

        public Task<string> GetUserEmail(Guid guid);
        public Task<Guid> GetUserGuid(string email);
    }
}
