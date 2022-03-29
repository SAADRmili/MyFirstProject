using AutoMapper;
using MyFirstProject.API.DTO;
using MyFirstProject.Shared.Entities;

namespace MyFirstProject.API.Helpers
{
    public class Profiles :Profile

    {
        public Profiles()
        {
            CreateMap<Brand, BrandDetails>();
        }
        
    }
}
