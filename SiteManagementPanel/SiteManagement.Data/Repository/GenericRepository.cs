

using Microsoft.EntityFrameworkCore;
using SiteManagamentPanel.Base;
using System.Linq.Expressions;

namespace SiteManagement.Data.Repository;

public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseModel
{
    private readonly SiteManagementDbContext dbContext;
    public GenericRepository(SiteManagementDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Save()
    {
        dbContext.SaveChanges();
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

    public Entity GetById(int id)
    {
        var entity = dbContext.Set<Entity>().Find(id);
        return entity;
    }

    public void Insert(Entity entity)
    {
        entity.InsertDate = DateTime.UtcNow;
        entity.InsertUser = "kevser@email.com";
        dbContext.Set<Entity>().Add(entity);
    }

    public void Update(Entity entity)
    {
        dbContext.Set<Entity>().Update(entity);
    }






}
