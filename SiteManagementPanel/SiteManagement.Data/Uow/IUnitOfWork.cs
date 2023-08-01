

using SiteManagamentPanel.Base;
using SiteManagement.Data.Domain;
using SiteManagement.Data.Repository;
using SiteManagementPanel.Data.Domain;

namespace SiteManagement.Data.Uow;

public interface IUnitOfWork
{
    void Complete();
    void CompleteWithTransaction();

    IGenericRepository<Entity> DynamicRepository<Entity>() where Entity : BaseModel;
    IGenericRepository<Apartment> ApartmentRepository { get; }
    IGenericRepository<ApartmentStatus> ApartmentStatusRepository { get; }
    IGenericRepository<ApartmentType> ApartmentTypeRepository { get; }
    IGenericRepository<Block> BlockRepository { get; }
    IGenericRepository<Bill> BillRepository { get; }
    IGenericRepository<Payment> PaymentRepository { get; }

    IGenericRepository<User> UserRepository { get; }
    IGenericRepository<UserLog> UserLogRepository { get; }



}
