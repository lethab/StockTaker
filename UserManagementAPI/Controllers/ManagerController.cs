using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models.Authentication.SignUp;
using UserManagementAPI.Models.Invoice;
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
        //public async Task<IActionResult> GetItemsSoldPerProduct()
        //{
        //    //Check Model State
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var getProducts = _invoiceService.EditInvoice(invoice);
        //    if (addInvoice)
        //    {
        //        return StatusCode(StatusCodes.Status200OK);
        //    }
        //    else
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}




    }
}
