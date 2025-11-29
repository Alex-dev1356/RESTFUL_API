namespace Restful_API.Models
{
    //Models serves as a blueprint or FORMAT for the data we are working with in our API
    //This is the Format on how we are going to structure our data
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; } = null!; //We gave ut a Null value to avoid warnings   
        public string Author { get; set; } = null!; //We gave ut a Null value to avoid warnings   
        public int YearPubished { get; set; }
    }
}
