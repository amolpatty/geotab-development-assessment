namespace JokeGenerator.Models
{
    /// <summary>
    /// This class stores the details of a Person
    /// </summary>
    public class Person : IPerson
    {
        public Person(string firstName, string lastName, string gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
    }
}
