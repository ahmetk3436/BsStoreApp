namespace Entities.RequestParameters
{
    public class BookParameters : RequestParameters
    {
        public uint MinPrice { get; set; }

        public uint MaxPrice { get; set; }

        public bool ValidPrice => MaxPrice > MinPrice;

        public String? SearchTerm { get; set; }

        public BookParameters()
        {
            OrderBy = "id";
        }
    }
}
