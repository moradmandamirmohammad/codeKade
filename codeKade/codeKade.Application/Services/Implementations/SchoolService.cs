using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.Entities.School;
using codeKade.DataLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace codeKade.Application.Services.Implementations
{
    public class SchoolService : ISchoolService
    {
        private readonly IGenericRepository<School> _schoolRepository;

        public SchoolService(IGenericRepository<School> schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public async ValueTask DisposeAsync()
        {
            if (_schoolRepository != null)
            {
                await _schoolRepository.DisposeAsync();
            }
        }

        public async Task<List<School>> GetAllSchools()
        {
            return await _schoolRepository.GetEntityQuery().ToListAsync();
        }
    }
}
