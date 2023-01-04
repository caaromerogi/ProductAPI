using AutoMapper;
using MediatR;
using ProductAPI.Application.Common.Interfaces;
using ProductAPI.Application.Common.Models;
using ProductAPI.Domain.Exceptions.Implementation;

namespace ProductAPI.Application.Commands.Product.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ResponseModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseModel> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
        if (productEntity is null){
            throw new ProductNotFoundException($"The product with Id: {request.ProductId} wasn't found");
        }
        productEntity.IsEnabled = false;
        await _unitOfWork.SaveChangesAsync();

        return new ResponseModel("The product was removed");

    }
}