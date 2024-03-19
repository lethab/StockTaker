using UserManagementAPI.Models.Invoice;
using UserManagementAPI.Models;
using UserManagementAPI.Models.Product;

namespace UserManagementAPI.Services.Product
{
    public class ProductService : IProduct
    {
        private readonly ApplicationDBContext _context;

        public ProductService(ApplicationDBContext context)
        {
            _context = context;
        }

        public bool AddProduct(ProductModel product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
        }

        public bool EditProduct(ProductModel product)
        {
            try
            {
                _context.Products.Update(product);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
        }

        public ProductModel GetProduct(int id)
        {
            try
            {
                return _context.Products.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProductModel> GetProducts()
        {
            try
            {
                return _context.Products.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
