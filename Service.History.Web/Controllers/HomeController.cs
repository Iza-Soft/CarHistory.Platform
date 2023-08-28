using AutoMapper;

namespace Service.History.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
        }
    }
}