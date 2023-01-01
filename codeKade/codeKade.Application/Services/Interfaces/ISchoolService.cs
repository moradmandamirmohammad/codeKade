using codeKade.DataLayer.Entities.School;

namespace codeKade.Application.Services.Interfaces;

public interface ISchoolService : IAsyncDisposable
{
    Task<List<School>> GetAllSchools();
}