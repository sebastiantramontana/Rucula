using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations
{
    public class TitulosService : ITitulosService
    {
        public TitulosService()
        {
        }

        public IEnumerable<Titulo> GetAllTitulos()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TituloIsin> GetAllTitulosIsin()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TituloIsin> GetPrunedTitulosIsin()
        {
            throw new NotImplementedException();
        }
    }
}