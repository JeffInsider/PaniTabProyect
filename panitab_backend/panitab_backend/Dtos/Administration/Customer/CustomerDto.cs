namespace panitab_backend.Dtos.Administration.Customer
{
    public class CustomerDto
    {
        public Guid Id { get; set; }  // ID único del cliente

        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public decimal Balance { get; set; }  // Saldo del cliente
        public bool IsActive { get; set; }
    }
}
