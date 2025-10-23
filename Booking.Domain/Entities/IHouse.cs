
namespace Booking.Domain.Entities
{
    public interface IHouse
    {
        DateTime? CreatedDate { get; set; }
        string? Description { get; set; }
        int Id { get; set; }
        string? ImageUrl { get; set; }
        string Name { get; set; }
        int Occupancy { get; set; }
        double Price { get; set; }
        int SqMeters { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}