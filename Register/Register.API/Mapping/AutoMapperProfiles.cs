using AutoMapper;
using Register.API.Models.Domain;
using Register.API.Models.DTO;

namespace Register.API.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Course, CourseDto>()
            .ForMember(s => s.Id, d => d.MapFrom(x => x.Id))
            .ForMember(s => s.Title, d => d.MapFrom(x => x.Title));
            //.ForMember(s => s.TeacherId, d => d.MapFrom(x => x.Teachers.)
            //.ForMember(s => s.TeacherFirstName, d => d.MapFrom(x => x.FirstName))
            //.ForMember(s => s.TeacherLastName, d => d.MapFrom(x => x.FirstName));
        }
    }
}
