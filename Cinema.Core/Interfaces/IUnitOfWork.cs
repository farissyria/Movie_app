using Cinema.Core.Entities;

namespace Cinema.Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<Movie> Movies { get; }
        IMovieRepository MovieRepo { get; }
       
        Task<int> CompleteAsync();
    }
}
