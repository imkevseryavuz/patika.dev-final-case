using Microsoft.EntityFrameworkCore;
using SiteManagamentPanel.Base;
using System.Linq.Expressions;
using static Dapper.SqlMapper;

namespace SiteManagementPanel.Data.Repository;

public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseModel
{
    private readonly SiteManagementDbContext dbContext;
    public GenericRepository(SiteManagementDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Delete(Entity entity)
    {
        dbContext.Set<Entity>().Remove(entity);
    }

    public void DeleteById(int id)
    {
        var entity = dbContext.Set<Entity>().Find(id);
        Delete(entity);
    }

    public List<Entity> GetAll()
    {
        return dbContext.Set<Entity>().AsNoTracking().ToList();
    }

    public IQueryable<Entity> GetAllAsQueryable()
    {
        return dbContext.Set<Entity>().AsQueryable();
    }


    public List<Entity> GetAllWithInclude(params string[] includes)
    {
        var query = dbContext.Set<Entity>().AsQueryable();
        query = includes.Aggregate(query, (currenct, inc) => currenct.Include(inc));
        return query.ToList();
    }

    public Entity GetById(int id)
    {
        var entity = dbContext.Set<Entity>().Find(id);
        return entity;
    }

    public Entity GetByIdWithInclude(int id, params string[] includes)
    {
        var query = dbContext.Set<Entity>().AsQueryable();
        query = includes.Aggregate(query, (currenct, inc) => currenct.Include(inc));
        return query.FirstOrDefault();// todo
    }

    public void Insert(Entity entity)
    {
        entity.InsertDate = DateTime.UtcNow;
        entity.InsertUser = "admin@pay.com";
        dbContext.Set<Entity>().Add(entity);
    }

    public void Save()
    {
        dbContext.SaveChanges();
    }

    public void Update(Entity entity)
    {
        dbContext.Set<Entity>().Update(entity);
    }

    public IEnumerable<Entity> Where(Expression<Func<Entity, bool>> expression)
    {
        return dbContext.Set<Entity>().Where(expression).AsQueryable();
    }

    public IEnumerable<Entity> WhereWithInclude(Expression<Func<Entity, bool>> expression, params string[] includes)
    {
        var query = dbContext.Set<Entity>().AsQueryable();
        query.Where(expression);
        query = includes.Aggregate(query, (currenct, inc) => currenct.Include(inc));
        return query.ToList();
    }
}

