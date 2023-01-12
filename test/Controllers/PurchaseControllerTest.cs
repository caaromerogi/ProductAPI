using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductAPI.Api.Controllers.Purchase;
using ProductAPI.Application.Commands.Purchase.CreatePurchase;
using ProductAPI.Application.Common.Models;
using ProductAPI.Application.Queries.Purchase;
using ProductAPI.Application.Queries.Purchase.GetPurchase;

namespace test.Controllers;

public class PurchaseControllerTest
{
    public Mock<IMediator> mediator;

    public PurchaseControllerTest()
    {
        this.mediator = new Mock<IMediator>();
    }

    [Fact]
    public async Task CreatePurchaseOrder_Ok(){
        //Arrange
        var command = new CreatePurchaseCommand();
        command.IdNumber = "1";
        command.IdType = "CC";
        command.ClientName = "Carlos";

        var expectedResult = new ResponseModel("Purchase successfully added");
        mediator.Setup(_ => _.Send(It.IsAny<CreatePurchaseCommand>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(expectedResult);
        var controller = new PurchaseController(mediator.Object);

        //Act
        var result = await controller.Create(command);

        //Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetPurchases_Ok(){
        //Arrange
        var query = new GetPurchaseQuery();
        var expectedPurchase = new List<PurchaseDTO>() 
        {
            new PurchaseDTO(){
                PurchaseId = 1,
                Date = new DateTime(),
                IdType = "CC",
                IdNumber = "010101",
                ClientName = "Carlos"
            }
        };
        mediator.Setup(_ => _.Send(It.IsAny<GetPurchaseQuery>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(expectedPurchase);
        
        var controller = new PurchaseController(mediator.Object);

        //Act
        var result = await controller.Get(query);

        //Assert
        result.Should().BeOfType<OkObjectResult>();
    }

}