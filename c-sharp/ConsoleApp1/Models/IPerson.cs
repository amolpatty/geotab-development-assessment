namespace JokeGenerator.Models
{
    public interface IPerson
    {
        string Name { get; set; }
        string Gender { get; set; }
        string SurName { get; set; }
        string Region { get; set; }
    }
}