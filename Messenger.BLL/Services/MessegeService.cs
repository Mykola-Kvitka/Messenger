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
    public class MessegeService : IMessegeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessegeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(Messege request)
        {
            var requestEntity = _mapper.Map<Messege, MessegeEntity>(request);

            requestEntity.MessegeId = Guid.NewGuid();
            requestEntity.ISDeleted = false;
            requestEntity.CreateDate = DateTime.Now;

            await _unitOfWork.Messeges.CreateAsync(requestEntity);

        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Messeges.DeleteAsync(id);
        }

        public async Task<IEnumerable<Messege>> FindAsync(Expression<Func<MessegeEntity, bool>> predicate)
        {
            var replay = await _unitOfWork.Messeges.FindAsync(predicate);

            return _mapper.Map<List<MessegeEntity>, List<Messege>>(replay);
        }

        public async Task<Messege> GetAsync(Guid id)
        {
            var result = await _unitOfWork.Messeges.GetAsync(id);

            return _mapper.Map<MessegeEntity, Messege>(result);
        }

        public async Task<int> GetCountAsync()
        {
            return await _unitOfWork.Messeges.GetCountAsync();
        }


        public async Task<IEnumerable<Messege>> GetPagedAsync(Guid ChatId, string userId, int page = 1, int pageSize = 20)
        {
            var requst = (await FindAsync(a => a.ChatId == ChatId)).ToList();

            requst.RemoveAll(a => a.ISDeleted == true && a.OwnerId == userId);

            requst.OrderByDescending(a => a.CreateDate);

            var amountToSkip = (page - 1) * pageSize;
            return requst.Skip(amountToSkip).Take(pageSize);
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var requst = await _unitOfWork.Messeges.GetAsync(id);

            requst.ISDeleted = true;

           await _unitOfWork.Messeges.UpdateAsync(requst);
        }

        public async Task UpdateAsync(Messege request)
        {
           await _unitOfWork.Messeges.UpdateAsync(_mapper.Map<Messege,MessegeEntity>(request));
        }

    }
}
