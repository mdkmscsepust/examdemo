using backend.Application.Interfaces.Persistence;
using backend.Application.Models;
using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Services
{
    public class DropdownListRepository : IDropdownListRepository
    {
        private readonly AppDbContext _dbContext;

        public DropdownListRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DropdownOutDTO>> DoctorDropdownList()
        {
            try
            {
                return await _dbContext.Doctors.Select(x => new DropdownOutDTO(x.Id, x.FullName)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<DropdownOutDTO>> PatientDropdownList()
        {
            try
            {
                return await _dbContext.Patients.Select(x => new DropdownOutDTO(x.Id, x.FullName)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}