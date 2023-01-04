using MediatR;

namespace ProductAPI.Application.Queries.Purchase.GetPurchase;

public class GetPurchaseQuery : IRequest<IEnumerable<PurchaseDTO>>
{

}