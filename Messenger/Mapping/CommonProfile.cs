using AutoMapper;
using Messenger.BLL.Models;
using Messenger.DAL.Models;
using System.Linq.Expressions;
using System;
using Messenger.PL.ViewModels;

namespace Messenger.Mapping
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            AddBusinessMapping();
            AddWebMapping();
        }

        public void AddWebMapping()
        {
            CreateMap<MessegeEntity, Messege>().ReverseMap();
            CreateMap<ContactEntity, Contact>().ReverseMap();
            CreateMap<UserChatsEntity, UserChats>().ReverseMap();
            CreateMap<ChatEntity, Chat>().ReverseMap();
        }

        public void AddBusinessMapping()
        {
            CreateMap<Messege, MessegeViewModel>().ReverseMap();
            CreateMap<CreateChat, CreateChatViewModel>().ReverseMap();
            CreateMap<DeleteChat, DeleteChatViewModel>().ReverseMap();
            CreateMap<DisplayChat, DisplayChatViewModel>().ReverseMap();
            CreateMap<DisplayChats, DisplayChatsViewModel>().ReverseMap();
            CreateMap<Contact, ContactViewModel>().ReverseMap();
            CreateMap<UserChats, UserChatsViewModel>().ReverseMap();
            CreateMap<Chat, ChatViewModel>().ReverseMap();
        }
    }
}
