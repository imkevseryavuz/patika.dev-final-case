﻿using AutoMapper;
using Serilog;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Business.Generic;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Data.Uow;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Business;

public class MessageService : GenericService<Message,MessageRequest,MessageResponse>, IMessageService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public MessageService(IMapper mapper, IUnitOfWork unitOfWork) : base( mapper, unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public ApiResponse<MessageResponse> SendMessage(MessageRequest messageRequest)
    {
        try
        {
            var message = new Message
            {
                FromUserId = messageRequest.FromUserId,
                ToUserId = messageRequest.ToUserId,
                Content = messageRequest.Content,
                IsRead = false
            };

            _unitOfWork.MessageRepository.Insert(message);
            _unitOfWork.Complete();


            var response = _mapper.Map<Message, MessageResponse>(message);
            return new ApiResponse<MessageResponse>("Message Send");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "MessageService.SendMessage");

            if (ex.InnerException != null)
            {
                Log.Error(ex.InnerException, "Inner Exception Details:");
            }

            return new ApiResponse<MessageResponse>("An error occurred while saving the entity changes.");
        }
    }
    public ApiResponse<List<MessageResponse>> GetMessagesByUserId(int userId)
    {
        try
        {
            var messages = _unitOfWork.MessageRepository.Where(m => m.FromUserId == userId || m.ToUserId == userId).ToList();

            var response = _mapper.Map<List<Message>, List<MessageResponse>>(messages);
            return new ApiResponse<List<MessageResponse>>(response);
        }
        catch (Exception ex)
        {

            Log.Error(ex, "MessageService.GetMessagesByUserId");
            return new ApiResponse<List<MessageResponse>>(ex.Message);
        }
    }
    public ApiResponse<MessageResponse> GetMessageById(int messageId)
    {
        try
        {
            var message = _unitOfWork.MessageRepository.GetById(messageId);

            if (message == null)
            {
                return new ApiResponse<MessageResponse>("Message not found");
            }

            var response = _mapper.Map<Message, MessageResponse>(message);
            return new ApiResponse<MessageResponse>(response);
        }
        catch (Exception ex)
        {

            Log.Error(ex, "MessageService.GetMessageById");
            return new ApiResponse<MessageResponse>(ex.Message);
        }
    }
    public ApiResponse MarkMessageAsRead(int messageId)
    {
        try
        {
            var message = _unitOfWork.MessageRepository.GetById(messageId);

            if (message == null)
            {
                return new ApiResponse("Message not found");
            }

            message.IsRead = !message.IsRead;

            _unitOfWork.MessageRepository.Update(message);
            _unitOfWork.Complete();


            return new ApiResponse(message: "Message is read");
        }
        catch (Exception ex)
        {

            Log.Error(ex, "MessageService.Update");
            return new ApiResponse(ex.Message);
        }
    }
    public ApiResponse DeleteMessagesByUserId(int userId)
    {
        try
        {
            var messagesToDelete = _unitOfWork.MessageRepository.Where(m => m.FromUserId == userId || m.ToUserId == userId).ToList();

            if (messagesToDelete.Count == 0)
            {
                return new ApiResponse { Success = false, Message = "No messages found for the user." };
            }

            foreach (var message in messagesToDelete)
            {
                _unitOfWork.MessageRepository.Delete(message);
            }

            _unitOfWork.Complete();

            return new ApiResponse { Success = true, Message = "Messages deleted successfully." };
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while deleting messages.");

            return new ApiResponse { Success = false, Message = "An error occurred while deleting messages. Error Details: " + ex.Message };
        }
    }
}
