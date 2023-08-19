using cargosiklink.Models.ViewModel.NumberTrack;
using cargosiklink.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cargosiklink.Controllers
{
    public class NumberTracksController : Controller
    {
        private readonly INumberTrackService _numberTrackService;
        private readonly IAccountService _accountService;
        public NumberTracksController(INumberTrackService numberTrackService, IAccountService accountService)
        {
            _numberTrackService = numberTrackService;
            _accountService = accountService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = await _accountService.GetUserId(User.Identity.Name);
            var models = await _numberTrackService.GetAllTracksByUserId(userId);
            return View(models);
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return PartialView();
            }
            var response = await _numberTrackService.GetAllTracks();
            var numberTrack = response.FirstOrDefault(trck => trck.Id == id.ToString());
            if (numberTrack == null)
            {
                return PartialView();
            }
            else
            {
                return PartialView(numberTrack);
            }
        }
        public async Task<IActionResult> GetAll()
        {
            var models = await _numberTrackService.GetAllTracks();
            return View(models);
        }
        public async Task<IActionResult> Update(Guid id)
        {
            if(id == Guid.Empty)
            {
                return PartialView();
            }
            var response = await _numberTrackService.GetAllTracks();
            var numberTrack = response.FirstOrDefault(trck => trck.Id == id.ToString());
            if (numberTrack == null)
            {
                return PartialView();
            }
            else
            {
                return PartialView(numberTrack);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(NumberTrackViewModel numberTrackViewModel)
        {
            if (numberTrackViewModel == null)
            {
                ModelState.AddModelError("", "При обновлении номера произошла ошибка");
            }
            if (User.Identity.Name == null)
            {
                ModelState.AddModelError("", "При создании номера произошла ошибка, ввойдите в систему");
            }

            var response = await _numberTrackService.UpdateTrack(numberTrackViewModel, User.Identity.Name);
            return PartialView(numberTrackViewModel);
        }

        public async Task<IActionResult> Create(NumberTrackViewModel numberTrackViewModel)
        {
            if(numberTrackViewModel == null)
            {
                ModelState.AddModelError("", "При создании номера произошла ошибка");
            }
            if (User.Identity.Name == null)
            {
                ModelState.AddModelError("", "При создании номера произошла ошибка, ввойдите в систему");
            }
            var response = await _numberTrackService.CreateTrack(numberTrackViewModel, User.Identity.Name);
            return RedirectToAction("Index", "NumberTracks");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _numberTrackService.DeleteTrack(id);
            return RedirectToAction("Index", "NumberTracks");
        }
    }
}
