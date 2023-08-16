using cargosiklink.Models.ViewModel.NumberTrack;

namespace cargosiklink.Service.Interfaces
{
    public interface INumberTrackService
    {
        Task<IEnumerable<NumberTrackViewModel>> GetAllTracksByUserId(Guid userId);
        Task<IEnumerable<NumberTrackViewModel>> GetAllTracks();
        Task<bool> DeleteTrack(Guid id);
        Task<bool> UpdateTrack(NumberTrackViewModel model, string name);
        Task<bool> CreateTrack(NumberTrackViewModel model, string name);
    }
}
