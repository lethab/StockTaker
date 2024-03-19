namespace UserManagementAPI.Models.Product
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductType { get; set; }
        public bool isSold { get; set; }
    }
}
