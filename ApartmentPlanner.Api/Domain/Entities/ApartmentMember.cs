using ApartmentPlanner.Api.Domain.Enums;

namespace ApartmentPlanner.Api.Domain.Entities
{
    public class ApartmentMember
    {
        public int Id { get; private set; }
        public int ApartmentId { get; private set; }
        public int UserId { get; private set; }
        public MemberRole Role { get; private set; }
        public DateTime JoinedAt { get; private set; }
    }
}
