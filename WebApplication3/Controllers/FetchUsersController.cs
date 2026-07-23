using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FetchUsersController : ControllerBase
    {
        private readonly ILogger<FetchUsersController> _logger;
        private readonly IDiagnosticContext _diagnosticContext;
        private List<FetchUsersModel> _users = [
                new Models.FetchUsersModel { UserId = 1, FirstName = "John", LastName = "Doe", Gender = "Male" },
                new Models.FetchUsersModel {UserId = 2, FirstName = "Jane", LastName = "Smith", Gender = "Male" }
            ];
        public FetchUsersController(ILogger<FetchUsersController> logger, IDiagnosticContext diagnosticContext)
        {
            _logger = logger;
            _diagnosticContext = diagnosticContext;
        }

        [HttpGet]
        // GET: FetchUsersController
        public ActionResult Index()
        {
            _logger.LogInformation("Fetching users... at {Time}", DateTime.UtcNow);
            _diagnosticContext.Set("UserCount", _users.Count);
            _diagnosticContext.Set("RequestPath", HttpContext.Request.Path);
            _diagnosticContext.Set("reqUser", "Toluwalase Oladokun");

            return Ok(_users);
        }

        // GET: FetchUsersController/Details/5
        public ActionResult Details(int id)
        {
            return Ok();
        }

        // GET: FetchUsersController/Create
        public ActionResult Create()
        {
            return Ok();
        }

      
        // GET: FetchUsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return Ok();
        }

        
        // GET: FetchUsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return Ok();
        }

    }
}
