using AutoMapper;
using MediatR;
using ProductAPI.Application.Common.Interfaces;
using ProductAPI.Application.Common.Models;


namespace ProductAPI.Application.Commands.Product.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResponseModel>   
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var  productEntity = _mapper.Map<CreateProductCommand, Domain.Models.Product>(request);
        productEntity.IsEnabled = true;
        await _unitOfWork.ProductRepository.AddAsync(productEntity);
        await _unitOfWork.SaveChangesAsync();
        return new ResponseModel("Product created succesfully");
    }
}