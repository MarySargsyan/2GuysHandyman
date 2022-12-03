using _2GuysHandyman.ApiModels;
using _2GuysHandyman.models;
using AutoMapper;
using WebAPI.ApiModels;

namespace _2GuysHandyman.Mapping
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<Services, ServicesApiModel>();
            CreateMap<ServicesApiModel, Services>();

            CreateMap<UsersApiModel, Users>();
        }
    }
}
