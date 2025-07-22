namespace panitab_backend.Dtos.Administration.CustomerAssistant
{
    public class CustomerAssistantDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string AssistantName { get; set; }

        public bool IsActive { get; set; }

    }
}
