using AutoMapper;
using ProductAPI.Application.Commands.Product.CreateProduct;
using ProductAPI.Application.Commands.Purchase.CreatePurchase;
using ProductAPI.Application.Queries.Product.GetPaginatedProduct;
using ProductAPI.Domain.Models;

namespace ProductAPI.Application.Configuration.Mapper;

public class AutoMappingProfiles : Profile
{
    public AutoMappingProfiles()
    {
        CreateMap<CreateProductCommand, Product>().ReverseMap();
        CreateMap<ProductDTO, Product>().ReverseMap();
        CreateMap<CreatePurchaseCommand, Purchase>().ReverseMap();
    }
}