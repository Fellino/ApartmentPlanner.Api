namespace ApartmentPlanner.Api.Domain.Entities
{
    public class Apartment
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int CreatedByUserId { get; private set; }
        public DateTime DeliveredAt { get; private set; }
    }
}
