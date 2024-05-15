using MambaASPNet.Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MambaASPNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITeamService _teamservice;
        public HomeController(ITeamService teamService)
        {
            _teamservice = teamService;
        }

        public IActionResult Index()
        {
            return View(_teamservice.GetAllTeams());
        }

        
    }
}
