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
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(Contact request)
        {
           var findRequest =  (await _unitOfWork.Users.FindAsync(c => c.Email == request.UserEmail)).FirstOrDefault();
            if (findRequest != null)
            {
                request.UserId = findRequest.Id;
                await _unitOfWork.Contacts.CreateAsync(_mapper.Map<Contact, ContactEntity>(request));
                return true;
            }
            else { return false; }
        }

        public void DeleteAsync(Guid id)
        {
            _unitOfWork.Contacts.DeleteAsync(id);
        }

        public async Task<IEnumerable<Contact>> FindAsync(string UserId)
        {
            var requst = _mapper.Map<IEnumerable<ContactEntity>, IEnumerable<Contact>>(await _unitOfWork.Contacts.FindAsync(a => a.OwnerId == UserId)) ;

            foreach (var r in requst)
            {
                var req = (await _unitOfWork.Users.FindAsync(user => user.Id == r.UserId)).FirstOrDefault();

                if (req != null)
                {
                    r.UserEmail = req.Email;
                    r.UserName = req.UserName;
                }
                else
                {
                    DeleteAsync(r.ContactId);
                }
            }

            return requst;
        }

        public async Task<Contact> GetAsync(Guid id)
        {
            var requst = await _unitOfWork.Contacts.GetAsync(id);
            return _mapper.Map<ContactEntity, Contact>(requst);
        }

        public async Task<int> GetCountAsync()
        {
            return await _unitOfWork.Contacts.GetCountAsync();
        }

        public async Task<IEnumerable<Contact>> GetPagedAsync(string UserId,  int page = 1, int pageSize = 20)
        {
            var requst = await FindAsync(UserId);

            var amountToSkip = (page - 1) * pageSize;
            return requst.Skip(amountToSkip).Take(pageSize);
        }

        public void UpdateAsync(Contact request)
        {
             _unitOfWork.Contacts.UpdateAsync(_mapper.Map< Contact,ContactEntity>(request));
        }
    }
}
