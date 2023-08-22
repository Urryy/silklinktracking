using cargosiklink.Models;
using cargosiklink.Models.Const;
using cargosiklink.Models.ViewModel.NumberTrack;
using cargosiklink.Repository.Interfaces;
using cargosiklink.Service.Interfaces;
using System;

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
            try
            {
                var user = _userRepository.GetAll().FirstOrDefault(usr => usr.Name == name);
                if (user == null)
                {
                    return false;
                }

                if (!double.TryParse(model.Weight.Replace(" ", ""), out double weight))
                {
                    weight = 0.0;
                }
                if (!double.TryParse(model.Volume.Replace(" ",""), out double volume))
                {
                    volume = 0.0;
                }

                var numberTrack = new NumberTrack(model.NumberTrackCode, model.Description, StateConst.StateAddByUser, user.Id, weight, volume, model.Link);

                if (model.Comment != null && model.Comment != string.Empty)
                {
                    numberTrack.Comment = model.Comment;
                }

                await _baseRepository.Create(numberTrack);

                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
            
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
            var numberTracks = _baseRepository.GetAll().OrderByDescending(item => item.Date).Where(trck => trck.UserId == userId).ToList();
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
            var numberTracks = _baseRepository.GetAll().OrderByDescending(item => item.Date).ToList();
            if (numberTracks.Count == 0)
            {
                return new List<NumberTrackViewModel>();
            }
            else
            {
                var states = _stateRepository.GetAll().ToList();
                var users = _userRepository.GetAll().ToList();
                var models = numberTracks.Select(track => new NumberTrackViewModel
                {
                    Date = track.Date.ToShortDateString(),
                    Description = track.Description,
                    NumberTrackCode = track.NumberTrackCode,
                    StateName = track.State.Name,
                    Color = track.State.Color,
                    Id = track.Id.ToString(),
                    Weight = track.Weight.ToString(),
                    Volume = track.Volume.ToString(),
                    Link = track.Link.ToString(),
                    StateId = track.StateId.ToString(),
                    Comment = track.Comment ?? string.Empty,
                    UserCode = track.User.UserCode
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
            if (!double.TryParse(model.Weight.Replace(".", ","), out double weight))
            {
                return false;
            }
            if (!double.TryParse(model.Volume.Replace(".", ","), out double volume))
            {
                return false;
            }

            numberTrack.Description = model.Description;
            numberTrack.StateId = Guid.Parse(model.StateName);
            numberTrack.NumberTrackCode = model.NumberTrackCode;
            numberTrack.Weight = weight;
            numberTrack.Volume = volume;
            numberTrack.Link = model.Link;

            if (model.Comment != null && model.Comment != string.Empty)
            {
                numberTrack.Comment = model.Comment;
            }

            await _baseRepository.Update(numberTrack);
            return true;
        }
    }
}
