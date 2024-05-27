using AutoMapper;
using StudentCore.Models;
using StudentWebAPI.Models;

namespace StudentWebAPI.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<StudentAddDto, Student>()
                .ForMember(dest => dest.CreatedAt, 
                    opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<Student, StudentGetDto>();

            CreateMap<Group, GroupDto>();

            CreateMap<StudentEditDto, Student>()
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => DateTime.Now));

            //CreateMap<FingerprintBase, FingerprintDto>()
            //    .ForMember(dest => dest.FingerprintId,
            //        opt => opt.MapFrom(src => src.FprtId));
        }
    }
}
