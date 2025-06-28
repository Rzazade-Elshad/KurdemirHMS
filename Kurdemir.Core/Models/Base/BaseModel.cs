using Kurdemir.Core.Enums;

namespace Kurdemir.Core.Models.Base;
public abstract class BaseModel
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }=false;
    public DateTime CreateTime { get; set; }= DateTime.UtcNow.AddHours(4);
    public DateTime? DeleteTime {  get; set; } 
}
