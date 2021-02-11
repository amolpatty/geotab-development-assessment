namespace JokeGenerator.Models
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string Gender { get; set; }
        string LastName { get; set; }
    }
}