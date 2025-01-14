using AutoMapper;
using Company.PL.ViewModels;
using Company.DAL.Model;

namespace Company.PL.MappingProfilesss
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();

        }
    }
}
