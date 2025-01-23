namespace Daylon.TaskApp.Communication.Responses
{
    public class ResponseGetTaskJson
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
    }
}
