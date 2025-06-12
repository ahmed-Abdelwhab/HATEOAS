namespace HATEOAS
{
    public class OrderResponseModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public List<LinkModel> Links { get; set; } = new List<LinkModel>();
    }

}
