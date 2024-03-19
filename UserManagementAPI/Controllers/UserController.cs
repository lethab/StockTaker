using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models.Authentication.SignUp;
using UserManagementAPI.Models.Invoice;
using UserManagementAPI.Services.Invoice;

namespace UserManagementAPI.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IInvoice invoiceService;
        public UserController(IInvoice invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        [HttpPost("CreateInvoice")]
        public IActionResult CreateInvoice([FromBody]InvoiceModel invoice)
        {
            //Check Model State
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addInvoice = invoiceService.AddInvoice(invoice);
            if (addInvoice)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("api/User/EditInvoice")]
        public async Task<IActionResult> EditInvoice(InvoiceModel invoice)
        {
            //Check Model State
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addInvoice = invoiceService.EditInvoice(invoice);
            if (addInvoice)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
