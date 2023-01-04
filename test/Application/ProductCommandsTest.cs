using AutoMapper;
using Moq;
using ProductAPI.Application.Commands.Product.CreateProduct;
using ProductAPI.Application.Common.Interfaces;
using ProductAPI.Application.Configuration.Mapper;
using ProductAPI.Domain.Models;

namespace test.Application;

public class ProductCommandsTest
{
    public Mock<IUnitOfWork> unitOfWork;
    public IMapper mapper;

    public ProductCommandsTest()
    {
        this.unitOfWork = new Mock<IUnitOfWork>();
        var myProfile = new AutoMappingProfiles();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        mapper = new Mapper(configuration);

    }

    [Fact]
    public async Task CreateProductCommandHandlerTest()
    {
        var command = new CreateProductCommand();
        var commandHandler = new CreateProductCommandHandler(unitOfWork.Object, mapper);
        await commandHandler.Handle(command, It.IsAny<CancellationToken>());
        unitOfWork.Verify(_ => _.ProductRepository.AddAsync(It.IsAny<Product>()));
        unitOfWork.Verify(_ => _.SaveChangesAsync());
    }
}