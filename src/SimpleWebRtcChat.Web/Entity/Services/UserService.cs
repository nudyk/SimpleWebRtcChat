using SimpleWebRtcChat.Web.Entity.Entityes;
using SimpleWebRtcChat.Web.Entity.Repository;

namespace SimpleWebRtcChat.Web.Entity.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IRepository<User> typeRepository) : base(unitOfWork, typeRepository)
        {
        }

        public override void Update(User dbEntity, User newEntity)
        {
            dbEntity.Name = newEntity.Name;
            dbEntity.PeerId = newEntity.PeerId;
        }
    }
}