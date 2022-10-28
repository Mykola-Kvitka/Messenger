using AutoMapper;
using Messenger.BLL.Interfaces;
using Messenger.BLL.Models;
using Messenger.DAL.Interfaces;
using Messenger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Services
{
    public class ChatServise : IChatServise
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessegeService _messegeService;
        private readonly IMapper _mapper;

        public ChatServise(IUnitOfWork unitOfWork, IMapper mapper, IMessegeService messegeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _messegeService = messegeService;
        }

        public async Task CreateAsync(CreateChat chats)
        {
            bool IsGroup = chats.Users.Count() >= 2;
            var chatEntity = new ChatEntity() { ChaiId = Guid.NewGuid(), ChatName = chats.ChatName, IsGroupChat = IsGroup };
            await _unitOfWork.Chats.CreateAsync(chatEntity);

            foreach (var u in chats.Users)
            {
                var user = (await _unitOfWork.Users.FindAsync(us => us.Id == u)).FirstOrDefault();
                chats.User.Add(user);
            }

            if (IsGroup)
            {
                foreach (var chat in chats.User)
                {
                    var entity = new UserChatsEntity() { UserChatsId = Guid.NewGuid(), ChatId = chatEntity.ChaiId, UserId = chat.Id };
                    await _unitOfWork.UserChats.CreateAsync(entity);
                }
            }
            else
            {
                var entity = new UserChatsEntity() { UserChatsId = Guid.NewGuid(), ChatId = chatEntity.ChaiId, UserId = chats.User.First().Id, ChatName = chats.User.Last().UserName };
               await _unitOfWork.UserChats.CreateAsync(entity);
                var entity1 = new UserChatsEntity() { UserChatsId = Guid.NewGuid(), ChatId = chatEntity.ChaiId, UserId = chats.User.Last().Id, ChatName = chats.User.First().UserName };
                await _unitOfWork.UserChats.CreateAsync(entity1);

            }

        }

        public async void DeleteAsync(DeleteChat del)
        {
            var requsts = (await _unitOfWork.UserChats.FindAsync(chat => chat.ChatId == del.ChaiId && chat.UserId == del.UserId)).FirstOrDefault();

            if (del.DeleteForOtherUser)
            {
                await _unitOfWork.UserChats.DeleteAsync(requsts.UserChatsId);

                requsts = (await _unitOfWork.UserChats.FindAsync(chat => chat.ChatId == del.ChaiId)).FirstOrDefault();
                await _unitOfWork.UserChats.DeleteAsync(requsts.UserChatsId);
            }
            else
            {
                //TODO: Implement deleting only for 1 user for message
                requsts.IsDeleted = true;
                await _unitOfWork.UserChats.UpdateAsync(requsts);
                //var requstsMessege = (await _unitOfWork.Messeges.FindAsync(Messege => Messege.ChatId == del.ChaiId && Messege.Ow == del.UserId)).FirstOrDefault();
            }


        }

        public Task<IEnumerable<Chat>> FindAsync(Expression<Func<Chat, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DisplayChats>> GetAllAsync(string Userid)
        {
            var displayChat = new List<DisplayChats>();
            var requsts = (await _unitOfWork.UserChats.FindAsync(chat => chat.UserId == Userid && chat.IsDeleted == false)).ToList();

            foreach (var r in requsts)
            {
                var display = new DisplayChats();
                var requstsChats = (await _unitOfWork.Chats.FindAsync(chat => chat.ChaiId == r.ChatId)).First();
                display.ChatId = r.ChatId;

                if (requstsChats.IsGroupChat)
                {
                    display.ChatName = requstsChats.ChatName;
                }
                else
                {
                    display.ChatName = r.ChatName;
                }
                displayChat.Add(display);

            }
            return displayChat;
        }

        public async Task<DisplayChat> GetAsync(Guid id, string userId)
        {
            var displayChat = new DisplayChat();

            displayChat.ChatId = id;
            displayChat.Messeges = (await _messegeService.GetPagedAsync(id, userId)).ToList();
            //TODO: ADD CHAT NAME;
            return displayChat;
        }
    }
}
