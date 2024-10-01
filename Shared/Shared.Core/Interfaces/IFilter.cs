namespace Shared.Core.Interfaces;

public interface IFilter<T> where T : class
{
    IQueryable<T> FilterExecute(IQueryable<T> query, string lang);
}