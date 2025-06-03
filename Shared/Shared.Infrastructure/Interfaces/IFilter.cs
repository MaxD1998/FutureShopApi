namespace Shared.Infrastructure.Interfaces;

public interface IFilter<T> where T : class
{
    IQueryable<T> FilterExecute(IQueryable<T> query, string lang);
}