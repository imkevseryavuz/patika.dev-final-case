

using AutoMapper;
using SiteManagementPanel.Business.Generic;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Data.Uow;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Business;

public class MessageService:GenericService<Message,MessageRequest,MessageResponse>, IMessageService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public MessageService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
}
