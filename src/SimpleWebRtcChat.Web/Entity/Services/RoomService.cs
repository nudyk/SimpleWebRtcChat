using SimpleWebRtcChat.Web.Entity.Entityes;
using SimpleWebRtcChat.Web.Entity.Repository;

namespace SimpleWebRtcChat.Web.Entity.Services
{
    public class RoomService:BaseService<Room>, IRoomService
    {
        public RoomService(IUnitOfWork unitOfWork, IRepository<Room> typeRepository) : base(unitOfWork, typeRepository)
        {
        }

        public override void Update(Room dbEntity, Room newEntity)
        {
            dbEntity.Uid = newEntity.Uid;
        }
    }
}
