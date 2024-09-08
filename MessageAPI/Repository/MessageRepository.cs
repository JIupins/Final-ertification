using AutoMapper;
using Newtonsoft.Json;
using MessagingService.Models.DTO;
using MessagingService.Models.Context;
using MessagingService.Models;

namespace MessagingService.Repository
{
    public class MessageRepository(IMapper mapper, MessageDbContext context) : IMessageRepository
    {
        //private readonly IMapper _mapper = mapper;
        private readonly MessageDbContext _context = context;
        private readonly HttpClient _client = new();

        public Guid AddMessage(string emailFrom, string emailTo, string text)
        {
            var result = new Message()
            {
                Guid = Guid.NewGuid(),
                DateTime = DateTime.Now,
                IsReaded = false,
                Text = text,
                UserFromGuid = GetUserGuid(emailFrom).Result,
                UserToGuid = GetUserGuid(emailTo).Result
            };
            _context.Messages.Add(result);
            _context.SaveChanges();
            return result.Guid;
        }

        public IEnumerable<MessageEmailsDto> GetMessages(string emailTo)
        {
            var users = GetUsers().Result;
            var guid2email = users.ToDictionary(x => x.Guid, x => x.Email);
            var email2guid = users.ToDictionary(x => x.Email, x => x.Guid);

            var userToGuid = email2guid[emailTo];

            var dbresult = _context.Messages.Where(x => x.IsReaded == false && x.UserToGuid == userToGuid);

            var result = dbresult
                .Select(x => new MessageEmailsDto()
                {
                    Guid = x.Guid,
                    DateTime = x.DateTime,
                    EmailTo = emailTo,
                    Text = x.Text,
                    EmailFrom = guid2email[x.UserFromGuid]
                }).ToList();

            foreach (var msg in dbresult)
            {
                msg.IsReaded = true;
            }

            _context.SaveChanges();

            return result;
        }

        public async Task<string> GetUserEmail(Guid guid)
        {
            using var response = await _client.GetAsync($"https://localhost:7262/User/GetUserEmail?guid={guid}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            if (responseBody == null)
            {
                return "";
            }
            else
            {
                return "";
            }

            throw new Exception("Unknow response");

        }

        public async Task<Guid> GetUserGuid(string email)
        {
            using var response = await _client.GetAsync($"https://localhost:7262/User/GetUserGuid?email={email.Replace("@", "%40")}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var result = Guid.Parse(responseBody.TrimEnd('\"').TrimStart('\"'));
            return result;
        }

        private async Task<IEnumerable<UserDto>> GetUsers()
        {
            using var response = await _client.GetAsync("https://localhost:7262/User/GetUsers");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<UserDto>>(responseBody);
        }
    }
}
