using AutoMapper;
using TinfWhoIs.Core.Database;
using TinfWhoIs.Models;

namespace TinfWhoIs.DiscordBot.Commands.Arguments
{
    public class ArgumentsMappingProfile : Profile
    {
        public ArgumentsMappingProfile()
        {
            CreateMap<AddTeacherArguments, Teacher>()
                .ForMember(x => x.Courses, x => x.Ignore());
        }
    }
}