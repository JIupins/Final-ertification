using AutoMapper;
using MessagingService.Models;

namespace MessagingService.Models.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Message, MessageDto>(MemberList.Destination).ReverseMap();
        }
    }
}
