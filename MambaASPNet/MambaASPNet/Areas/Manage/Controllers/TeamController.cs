using MambaASPNet.Business.Exceptions;
using MambaASPNet.Business.Services.Abstracts;
using MambaASPNet.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MambaASPNet.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        public IActionResult Index()
        {
            return View(_teamService.GetAllTeams());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Team team) 
        {
            if (!ModelState.IsValid) 
            {
                return View();
            }
            try
            {
                _teamService.CreateTeam(team);
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileNullReferenceException ex)
            {
                ModelState.AddModelError(ex.PropName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var existTeam = _teamService.GetTeam(x=>x.Id== id);

            if (existTeam == null) 
                return NotFound();

            return View(existTeam);
        }

        [HttpPost]
        public IActionResult DeleteTeam(int id)
        {
            var existTeam = _teamService.GetTeam(x => x.Id == id);

            if (existTeam == null)
                return NotFound();
            try
            {
                _teamService.DeleteTeam(id);
            }
            catch(EntityNotFoundException ex) 
            { 
                ModelState.AddModelError(ex.EntityName, ex.Message);
                return View();
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var existTeam = _teamService.GetTeam(x => x.Id == id);

            if (existTeam == null)
                return NotFound();

            return View(existTeam);
        }

        [HttpPost]
        public IActionResult Update(Team team)
        {
            if (!ModelState.IsValid) return View();

            _teamService.UpdateTeam(team.Id, team);
            return RedirectToAction("index");
        }

    }
}
