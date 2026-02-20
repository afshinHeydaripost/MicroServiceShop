using Products.DataModel.Context;
using Helper;
using Helper.Base;
using Microsoft.EntityFrameworkCore;


namespace Products.Services;

public class GeneralServices<T> : IGeneralServices<T> where T : BaseEntity
{
    protected readonly MicroServiceShopContext _Context;
    private readonly DbSet<T> entities;

    public GeneralServices(MicroServiceShopContext Context)
    {
        _Context = Context;
        entities = _Context.Set<T>();
    }

    public async Task<GeneralResponse> Add(T item)
    {
        try
        {
            await entities.AddAsync(item);
            await Save();
            return GeneralResponse.Success();
        }
        catch (Exception ex)
        {
            return GeneralResponse.Fail(ex);
        }
    }

    public async Task<GeneralResponse> Edit(T item)
    {
        try
        {
            _Context.DetachLocal(item, item.Id);
            await Save();
            return GeneralResponse.Success();
        }
        catch (Exception ex)
        {
            return GeneralResponse.Fail(ex);
        }
    }

    public async Task<GeneralResponse> Delete(int id)
    {
        try
        {
            var item = await GetById(id);
            if (item == null)
            {
                return GeneralResponse.NotFound();
            }
            entities.Remove(item);
            await Save();
            return GeneralResponse.SuccessDelete();
        }
        catch (Exception ex)
        {
            return GeneralResponse.Fail(ex);
        }
    }

    public async Task<GeneralResponse> Delete(T entity)
    {
        try
        {
            entities.Remove(entity);
            await Save();
            return GeneralResponse.Success();
        }
        catch (Exception ex)
        {
            return GeneralResponse.Fail(ex);
        }
    }

    public async Task<List<T>> GetAll()
    {
        return await entities.ToListAsync();
    }

    public async Task<T> GetById(int id)
    {
        try
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task Save()
    {
        await _Context.SaveChangesAsync();
    }

    public IQueryable<T> GetQuery()
    {
        return entities.AsQueryable();
    }
}

