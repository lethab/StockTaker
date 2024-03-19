namespace UserManagementAPI.Models.Invoice
{
    public class InvoiceModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CreatedbyUserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime EditedDateTime { get; set; }
        public int Amount { get; set; }
        public string comment { get; set; }
        public Guid ProductId { get; set; }
    }
}
