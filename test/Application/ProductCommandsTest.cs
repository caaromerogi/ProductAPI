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
        var r = new Mock<IGenericRepository<Product>>();
        var command = new CreateProductCommand();
        var t = new CancellationTokenSource();
        command.InInventory = 100;
        command.ProductName = "P1";
        command.MaxPurchase = 5;
        command.MinPurchase = 1;

        unitOfWork.Setup(_ => _.ProductRepository).Returns(r.Object);
        r.Setup(_ => _.AddAsync(It.IsAny<Product>())).Returns(Task.FromResult(mapper.Map<CreateProductCommand, Product>(command)));

        var commandHandler = new CreateProductCommandHandler(unitOfWork.Object, mapper);
        var result = await commandHandler.Handle(command, t.Token);
        unitOfWork.Verify(_ => _.ProductRepository.AddAsync(It.IsAny<Product>()));
        unitOfWork.Verify(_ => _.SaveChangesAsync());

    }
}