using ProductAPI.Application.Queries.Product.GetPaginatedProduct;

namespace test.MockData;

public class ProductDTOBuilder
{
public ProductDTO Build(){
        return new ProductDTO{
                ProductId = 1,
                ProductName = "Product1",
                InInventory = 100,
                MaxPurchase = 6,
                MinPurchase = 2
            };
    }

    public List<ProductDTO> BuildList(){
        return new List<ProductDTO>{
            new ProductDTO{
                ProductId = 1,
                ProductName = "Product1",
                InInventory = 100,
                MaxPurchase = 6,
                MinPurchase = 2
            },
            new ProductDTO{
                ProductId = 2,
                ProductName = "Product2",
                InInventory = 200,
                MaxPurchase = 55,
                MinPurchase = 10
            }
        };
    }

    public  List<ProductDTO> BuildEmptyList(){
        return new List<ProductDTO>();
    }  
}