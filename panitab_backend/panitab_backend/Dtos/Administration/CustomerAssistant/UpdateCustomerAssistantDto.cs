namespace panitab_backend.Dtos.Administration.CustomerAssistant
{
    public class UpdateCustomerAssistantDto
    {
        public string CustomerId { get; set; }

        public string AssistantName { get; set; }

        public bool IsActive { get; set; }
    }
}
