﻿using Serilog;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Data.Repository;

namespace SiteManagementPanel.Data.Uow;

public class UnitOfWork : IUnitOfWork
{
    private readonly SiteManagementDbContext dbContext;

    public UnitOfWork(SiteManagementDbContext dbContext)
    {
        this.dbContext = dbContext;
        ApartmentRepository= new GenericRepository<Apartment>(dbContext);
        ApartmentTypeRepository = new GenericRepository<ApartmentType>(dbContext);
        BlockRepository = new GenericRepository<Block>(dbContext);
        BillRepository= new GenericRepository<Bill>(dbContext);
        PaymentRepository= new GenericRepository<Payment>(dbContext);
        UserRepository= new GenericRepository<User>(dbContext);
        UserLogRepository= new GenericRepository<UserLog>(dbContext);
        MessageRepository=new GenericRepository<Message>(dbContext);
        ApartmentUserRepository= new GenericRepository<ApartmentUser>(dbContext);


    }


    public void Complete()
    {
        dbContext.SaveChanges();
    }

    public void CompleteWithTransaction()
    {
        using (var dbTransaction = dbContext.Database.BeginTransaction())
        {
            try
            {
                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                Log.Error(ex, "CompleteWithTransaction");
            }
        }
    }

    public IGenericRepository<Entity> DynamicRepository<Entity>() where Entity : BaseModel
    {
        return new GenericRepository<Entity>(dbContext);
    }


    public IGenericRepository<Apartment> ApartmentRepository { get; private set; }
    public IGenericRepository<ApartmentType> ApartmentTypeRepository { get; private set; }
    public IGenericRepository<Block> BlockRepository { get; private set; }
    public IGenericRepository<Bill> BillRepository { get; private set; }
    public IGenericRepository<Payment> PaymentRepository { get; private set; }
    public IGenericRepository<User> UserRepository { get; private set; }
    public IGenericRepository<UserLog> UserLogRepository { get; private set; }
    public IGenericRepository<Message> MessageRepository { get; private set; }
    public IGenericRepository<ApartmentUser> ApartmentUserRepository { get; private set; }

}
