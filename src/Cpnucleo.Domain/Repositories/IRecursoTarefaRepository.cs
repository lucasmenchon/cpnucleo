using Cpnucleo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cpnucleo.Domain.Repositories
{
    public interface IRecursoTarefaRepository : IGenericRepository<RecursoTarefa>
    {
        Task<IEnumerable<RecursoTarefa>> GetByTarefaAsync(Guid idTarefa);
    }
}