using System.ComponentModel.DataAnnotations;

namespace panitab_backend.Dtos.Administration.CustomerAssistant
{
    public class CreateCustomerAssistantDto
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public string AssistantName { get; set; }
    }
}

//Pendiente hacer nueva migracion y c
