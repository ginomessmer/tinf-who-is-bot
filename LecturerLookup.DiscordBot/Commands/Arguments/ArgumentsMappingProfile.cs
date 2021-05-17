using AutoMapper;
using LecturerLookup.Models;

namespace LecturerLookup.DiscordBot.Commands.Arguments
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