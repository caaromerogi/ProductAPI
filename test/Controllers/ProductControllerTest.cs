using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductAPI.Api.Controllers.Product;
using ProductAPI.Application.Commands.Product.CreateProduct;
using ProductAPI.Application.Commands.Product.DeleteProduct;
using ProductAPI.Application.Commands.Product.UpdateProduct;
using ProductAPI.Application.Common.Models;
using ProductAPI.Application.Queries.Product.GetPaginatedProduct;
using ProductAPI.Application.Queries.Product.GetProductById;
using test.MockData;

namespace test.Controllers;

public class ProductControllerTest
{
    public Mock<IMediator> mediator;

    public ProductControllerTest()
    {
        this.mediator = new Mock<IMediator>();
    }

    [Fact]
    public async Task GetProductsPaginated_OkObjectResult()
    {   
        //Arrange
        var products = new List<ProductDTO>().AsQueryable();
        var paginatedProducts = PaginationList<ProductDTO>.Create(products,1,1);
        mediator.Setup(_ => _.Send(It.IsAny<GetPaginatedProductQuery>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(paginatedProducts);
        var controller = new ProductController(mediator.Object);

        //Act
        var result = await controller.GetPaginatedResult(It.IsAny<GetPaginatedProductQuery>());

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetProductsPaginate_DataResult()
    {
        //Arrange
        var products = new ProductDTOBuilder().BuildList().AsQueryable();
        var paginatedProducts = PaginationList<ProductDTO>.Create(products,1,1);
        mediator.Setup(_ => _.Send(It.IsAny<GetPaginatedProductQuery>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(paginatedProducts);
        var controller = new ProductController(mediator.Object);

        //Act
        var result = await controller.GetPaginatedResult(It.IsAny<GetPaginatedProductQuery>());
        var objectResult = (OkObjectResult)result;
        var value = (PaginationList<ProductDTO>)objectResult.Value;

        //Assert
        Assert.Collection<ProductDTO>(value.Elements,
        o1 => Assert.Contains("Product1",o1.ProductName));
    }

    [Fact]
    public async Task GetById_Ok(){
        //Arrange
        var products = new ProductDTOBuilder().BuildList().AsQueryable();
        var productDTO = new ProductDTOBuilder().Build();
        mediator.Setup(_ => _.Send(It.IsAny<GetProductByIdQuery>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(productDTO);
        var controller = new ProductController(mediator.Object);

        //Act 
        var result = await controller.GetProductById(It.IsAny<GetProductByIdQuery>());

        //Assert 
        result.Should().BeOfType<OkObjectResult>();
    }
    [Fact]
    public async Task AddProduct_OkObjectResult()
    {
        //Arrange
        var products = new ProductDTOBuilder().BuildList().AsQueryable();
        var responseModel = new ResponseModel("Product added succesfully");
        mediator.Setup(_ => _.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(responseModel);
        var controller = new ProductController(mediator.Object);
        
        //Act
        var result = await controller.Add(It.IsAny<CreateProductCommand>());

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddProduct_Data()
    {
        //Arrange
        var products = new ProductDTOBuilder().BuildList().AsQueryable();
        var responseModel = new ResponseModel("Product added succesfully");
        mediator.Setup(_ => _.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(responseModel);
        var controller = new ProductController(mediator.Object);
        
        //Act
        var result = await controller.Add(It.IsAny<CreateProductCommand>());
        var objectResult = (OkObjectResult)result;
        var value = (ResponseModel)objectResult.Value;

        //Assert
        Assert.Equal("Product added succesfully", value.Message);
    }

    [Fact]
    public async Task UpdateProduct_OkObjectResult()
    {
        //Arrange
        var products = new ProductDTOBuilder().BuildList().AsQueryable();
        var responseModel = new ResponseModel("Product updated succesfully");
        mediator.Setup(_ => _.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(responseModel);
        var controller = new ProductController(mediator.Object);

        //Act
        var result = await controller.Update(It.IsAny<UpdateProductCommand>());
        
        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateProduct_Data()
    {
        //Arrange
        var products = new ProductDTOBuilder().BuildList().AsQueryable();
        var responseModel = new ResponseModel("Product updated succesfully");
        mediator.Setup(_ => _.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(responseModel);
        var controller = new ProductController(mediator.Object);

        //Act
        var result = await controller.Update(It.IsAny<UpdateProductCommand>());
        var objectResult = (OkObjectResult)result;
        var value = (ResponseModel)objectResult.Value;
        
        //Assert
        Assert.Equal("Product updated succesfully", value.Message);
    }

    [Fact]
    public async Task DeleteProduct_OkObjectResult()
    {
        //Arrange
        var products = new ProductDTOBuilder().BuildList().AsQueryable();
        var responseModel = new ResponseModel("Product deleted succesfully");
        mediator.Setup(_ => _.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(responseModel);
        var controller = new ProductController(mediator.Object);

        //Act
        var result = await controller.Delete(It.IsAny<DeleteProductCommand>());
        
        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteProduct_Data()
    {
        //Arrange
        var products = new ProductDTOBuilder().BuildList().AsQueryable();
        var responseModel = new ResponseModel("Product deleted succesfully");
        mediator.Setup(_ => _.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(responseModel);
        var controller = new ProductController(mediator.Object);

        //Act
        var result = await controller.Delete(It.IsAny<DeleteProductCommand>());
        var objectResult = (OkObjectResult)result;
        var value = (ResponseModel)objectResult.Value;
        
        //Assert
        Assert.Equal("Product deleted succesfully", value.Message);
    }

}