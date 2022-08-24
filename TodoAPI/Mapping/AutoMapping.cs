using AutoMapper;
using TodoAPI.DTO;
using TodoAPI.Models;

namespace TodoAPI.Mapping
{
    
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Card, CardDTO>();
            CreateMap<CardDTO, Card>();
        }
    }
}
