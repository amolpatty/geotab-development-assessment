namespace JokeGenerator.Models
{
    /// <summary>
    /// This class stores the details of a Person
    /// </summary>
    public class Person : IPerson
    {
        public Person(string firstName, string lastName, string gender)
        {
            Name = firstName;
            SurName = lastName;
            Gender = gender;
        }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Gender { get; set; }
        public string Region { get; set; }
    }
}
