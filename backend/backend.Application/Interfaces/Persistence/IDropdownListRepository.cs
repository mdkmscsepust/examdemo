using backend.Application.Models;

namespace backend.Application.Interfaces.Persistence
{
    public interface IDropdownListRepository
    {
        Task<IEnumerable<DropdownOutDTO>> DoctorDropdownList();
        Task<IEnumerable<DropdownOutDTO>> PatientDropdownList();
    }
}