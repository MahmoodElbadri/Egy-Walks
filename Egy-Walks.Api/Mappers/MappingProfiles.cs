using AutoMapper;
using Egy_Walks.Api.Models.Domain;
using Egy_Walks.Api.Models.DTOs;
using Egy_Walks.Api.Models.DTOs.WalkDTOs;

namespace Egy_Walks.Api.Mappers;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<Region, RegionRequest>().ReverseMap();
        CreateMap<Region, RegionResponse>().ReverseMap();
        

        CreateMap<Walk, WalkRequest>().ReverseMap();
        CreateMap<Walk, WalkResponse>().ReverseMap();
    }
}