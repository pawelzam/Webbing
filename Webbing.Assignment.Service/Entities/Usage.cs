namespace Webbing.Assignment.Service.Entities;

// Will be use to store the usages of the sim
public class Usage
{
    public int Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid SimId { get; set; }
    public DateTime Date { get; set; }
    public long Quota { get; set; }
}