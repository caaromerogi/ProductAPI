using AutoMapper;
using Dapper;
using FluentAssertions;
using Moq;
using ProductAPI.Application.Common.Interfaces;
using ProductAPI.Application.Configuration.Mapper;
using ProductAPI.Application.Queries.Product.GetPaginatedProduct;
using ProductAPI.Application.Queries.Product.GetProductById;

namespace test.Application.Queries.Product;

public class GetProductById
{
    private readonly Mock<IDapperService<ProductAPI.Domain.Models.Product>> _dapper;
    private readonly IMapper _mapper;
    public GetProductById()
    {
        var myProfile = new AutoMappingProfiles();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        _mapper = new Mapper(configuration);
        _dapper = new Mock<IDapperService<ProductAPI.Domain.Models.Product>>();
    }

    [Fact]
    public async Task GetProductById_Test(){
        var productDTO = new ProductDTO();
        productDTO.ProductId = 1;
        productDTO.InInventory = 100;
        productDTO.ProductName = "P1";

        var query = new GetProductByIdQuery();
        query.ProductId = 1;

        var productList = new List<ProductAPI.Domain.Models.Product>();
        var product = new ProductAPI.Domain.Models.Product();

        product.InInventory = 100;
        product.ProductName = "P1";
        product.ProductId = 1;

        productList.Add(product);

        _dapper.Setup(_ => _.Get(It.IsAny<string>(), It.IsAny<DynamicParameters>())).ReturnsAsync(productList);

        var handler = new GetProductByIdQueryHandler(_dapper.Object, _mapper);
        var result = await handler.Handle(query,It.IsAny<CancellationToken>());
        
        result.Should().BeEquivalentTo(productDTO);
    }
}