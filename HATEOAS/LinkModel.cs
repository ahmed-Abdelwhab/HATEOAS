namespace HATEOAS
{
    public class LinkModel
    {
        public string Href { get; set; }
        public string Rel { get; set; } // describe action: self, confirm, cancel
        public string Method { get; set; } // GET, POST, etc.
    }

}
