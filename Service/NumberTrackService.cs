using cargosiklink.Models;
using cargosiklink.Models.Const;
using cargosiklink.Models.ViewModel.NumberTrack;
using cargosiklink.Repository.Interfaces;
using cargosiklink.Service.Interfaces;
using System.Xml;

namespace cargosiklink.Service
{
    public class NumberTrackService : INumberTrackService
    {
        private readonly IBaseRepository<NumberTrack> _baseRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<State> _stateRepository;
        public NumberTrackService(IBaseRepository<NumberTrack> baseRepository, IBaseRepository<User> userRepository, IBaseRepository<State> stateRepository)
        {
            _baseRepository = baseRepository;
            _userRepository = userRepository;
            _stateRepository = stateRepository;
        }
        public async Task<bool> CreateTrack(NumberTrackViewModel model, string name)
        {
            var user = _userRepository.GetAll().FirstOrDefault(usr => usr.Name == name);
            if(user == null)
            {
                return false;
            }
            
            var numberTrack = new NumberTrack(model.NumberTrackCode, model.Description, StateConst.StateAddByUser, user.Id);
            await _baseRepository.Create(numberTrack);
            return true;
        }

        public async Task<bool> DeleteTrack(Guid id)
        {
            var numberTrack = _baseRepository.GetAll().FirstOrDefault(trck => trck.Id == id);
            if (numberTrack == null) 
                return false;

            await _baseRepository.Delete(numberTrack);
            return true;
        }

        public async Task<IEnumerable<NumberTrackViewModel>> GetAllTracksByUserId(Guid userId)
        {
            var numberTracks = _baseRepository.GetAll().Where(trck => trck.UserId == userId).ToList();
            if(numberTracks.Count == 0)
            {
                return new List<NumberTrackViewModel>();
            }
            else
            {
                var states = _stateRepository.GetAll().ToList();
                var models = numberTracks.Select(track => new NumberTrackViewModel
                {
                    Date = track.Date.ToShortDateString(),
                    Description = track.Description,
                    NumberTrackCode = track.NumberTrackCode,
                    StateName = track.State.Name,
                    Color = track.State.Color,
                    Id = track.Id.ToString()
                });
                return models;
            }
        }

        public async Task<IEnumerable<NumberTrackViewModel>> GetAllTracks()
        {
            var numberTracks = _baseRepository.GetAll().ToList();
            if (numberTracks.Count == 0)
            {
                return new List<NumberTrackViewModel>();
            }
            else
            {
                var states = _stateRepository.GetAll().ToList();
                var models = numberTracks.Select(track => new NumberTrackViewModel
                {
                    Date = track.Date.ToShortDateString(),
                    Description = track.Description,
                    NumberTrackCode = track.NumberTrackCode,
                    StateName = track.State.Name,
                    Color = track.State.Color,
                    Id = track.Id.ToString()
                });
                return models;
            }
        }

        public async Task<bool> UpdateTrack(NumberTrackViewModel model, string name)
        {
            var numberTrack = _baseRepository.GetAll().FirstOrDefault(trck => trck.Id == Guid.Parse(model.Id));
            if (numberTrack == null)
            {
                return false;
            }
            numberTrack.Description = model.Description;
            numberTrack.StateId = Guid.Parse(model.StateName);
            numberTrack.NumberTrackCode = model.NumberTrackCode;
            await _baseRepository.Update(numberTrack);
            return true;
        }
    }
}
