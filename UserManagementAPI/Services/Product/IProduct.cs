using UserManagementAPI.Models.Invoice;
using UserManagementAPI.Models.Product;

namespace UserManagementAPI.Services.Product
{
    public interface IProduct
    {
        List<ProductModel> GetProducts();
        ProductModel GetProduct(int id);
        bool EditProduct(ProductModel product);
        bool DeleteProduct(int id);
        bool AddProduct (ProductModel product);
    }
}
