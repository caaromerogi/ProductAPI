using AutoMapper;
using ProductAPI.Application.Commands.Product.CreateProduct;
using ProductAPI.Domain.Models;

namespace ProductAPI.Application.Configuration.Mapper;

public class AutoMappingProfiles : Profile
{
    protected AutoMappingProfiles()
    {
        CreateMap<CreateProductCommand, Product>().ReverseMap();
    }
}