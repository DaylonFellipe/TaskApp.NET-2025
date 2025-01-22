namespace Daylon.TaskApp.Domain.Entities
{
    public class Task : EntityBase
    {
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;
    }
}
