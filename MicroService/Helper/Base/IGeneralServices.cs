namespace Helper.Base;

public interface IGeneralServices<T>
{
    Task<GeneralResponse> Add(T item);
    Task<GeneralResponse> Edit(T item);
    Task<GeneralResponse> Delete(int id);
    Task<GeneralResponse> Delete(T entity);
    Task Save();
    Task<T> GetById(int id);
    Task<List<T>> GetAll();
    IQueryable<T> GetQuery();
}
