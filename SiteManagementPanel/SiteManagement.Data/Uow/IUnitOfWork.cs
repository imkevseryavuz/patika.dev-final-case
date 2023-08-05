using SiteManagamentPanel.Base;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Data.Repository;

namespace SiteManagementPanel.Data.Uow;

public interface IUnitOfWork
{
    void Complete();
    void CompleteWithTransaction();

    IGenericRepository<Entity> DynamicRepository<Entity>() where Entity : BaseModel;
    IGenericRepository<Apartment> ApartmentRepository { get; }
    IGenericRepository<ApartmentType> ApartmentTypeRepository { get; }
    IGenericRepository<Block> BlockRepository { get; }
    IGenericRepository<Bill> BillRepository { get; }
    IGenericRepository<Payment> PaymentRepository { get; }
    IGenericRepository<Message> MessageRepository { get; }

    IGenericRepository<User> UserRepository { get; }
    IGenericRepository<UserLog> UserLogRepository { get; }



}
