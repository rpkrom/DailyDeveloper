namespace BlazorWebApp.Shared
{
    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty; 

        public string Description { get; set; } = string.Empty; 

        public string Url { get; set; } = string.Empty;

        public string ImageURL { get; set; } = string.Empty; // TODO: modify this to be a reference/pointer to a image url either in the app or blob storage

    }
}
