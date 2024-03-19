using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models.Authentication.SignUp;
using UserManagementAPI.Models.Invoice;
using UserManagementAPI.Models.Product;
using UserManagementAPI.Services.Invoice;
using UserManagementAPI.Services.Product;

namespace UserManagementAPI.Controllers
{
    [Authorize(Roles = "Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IInvoice _invoiceService;
        private readonly IProduct _productService;
        public ManagerController(IInvoice invoiceService,IProduct productService)
        {

            _invoiceService = invoiceService;
            _productService = productService;

        }

        [HttpPost("CreateInvoice")]
        public IActionResult CreateInvoice([FromBody]InvoiceModel invoice)
        {
            //Check Model State
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addInvoice = _invoiceService.AddInvoice(invoice);
            if (addInvoice)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("EditInvoice")]
        public async Task<IActionResult> EditInvoice([FromBody]InvoiceModel invoice)
        {
            //Check Model State
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addInvoice = _invoiceService.EditInvoice(invoice);
            if (addInvoice)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[HttpGet("GetItemsSoldPerProduct")]
        //public  List<ProductModel> GetItemsSoldPerProduct()
        //{
        //    var getProducts = _productService.GetProducts();

        //    var products = getProducts.Where(_ => _.isSold).GroupBy(_ => _.ProductType).ToList();     
        //    return products.Select(p => new ProductModel { })
        //}

        [HttpGet("GetInProductsInStock")]
        public string GetInProductsInStock()
        {
            var getProducts = _productService.GetProducts();

            int prodInStock = getProducts.Where(_ => !_.isSold).Count();

            return "Number of products in stock is : "+ prodInStock;
        }

        [HttpGet("GetSoldProducts")]
        public string GetSoldProducts()
        {
            var getProducts = _productService.GetProducts();

            int prodNotInStock = getProducts.Where(_ => _.isSold).Count();

            return "Number of Sold products : " + prodNotInStock;
        }


        [HttpGet("GetSoldVSStockProducts")]
        public string GetSoldProductsInStock()
        {
            var getProducts = _productService.GetProducts();

            int prodNotInStock = getProducts.Where(_ => _.isSold).Count();
            int prodInStock = getProducts.Where(_ => !_.isSold).Count();

            return "Number of Sold products : " + prodNotInStock + " Number of Products in STock is : "+prodInStock;
        }

        [HttpGet("GetNumberOfProducts")]
        public string GetNumberOfProducts()
        {
            var getProducts = _productService.GetProducts().Count();

            return "Number of Sold products : " + getProducts;
        }

    }
}
