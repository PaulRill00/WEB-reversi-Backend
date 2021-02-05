namespace ReversiRestAPI.Models.API
{
    public class APIAction
    {
        public string Action { get; set; }
        public int ColMove { get; set; }
        public int RowMove { get; set; }
        public string Player { get; set; }
    }
}
