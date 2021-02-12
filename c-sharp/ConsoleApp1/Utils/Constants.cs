namespace JokeGenerator
{
    /// <summary>
    /// The class contains constant strings which can be localized in future
    /// </summary>
    public class Constants
    {
        // This is a list of prompts during user interaction
        public const string HelpPrompt = "Press ? to get instructions.";
        public const string CategoriesPrompt = "Press c to get categories";
        public const string RandomJokesPrompt = "Press r to get random jokes";
        public const string QuitPrompt = "Press q to close the application";
        public const string RandomNamesPrompt = "Want to use a random name? y/n";
        public const string SpecifyCategoryPrompt = "Want to specify a category? y/n";
        public const string JokesCountPrompt = "How many jokes do you want? (1-9)";
        public const string EnterCategoryPrompt = "Enter a category.";
        public const string WelcomePrompt = "Welcome to the joke generator application";
        public const string InstructionPrompt = "Please follow the instructions to generate a joke: ";
        public const string CategoriesTitle = "Available categories are: ";
        
        // This is a list of error messages
        public const string ErrorInvalidJokeCount = "Number of jokes must be a number. (1-9)";
        public const string ErrorInvalidCategories = "Category is not valid";
        public const string ErrorNoCategoriesFound = "No categories found";
        public const string ErrorSystemFault = "There was an error generating jokes. Please contact system administator";
        public const string ErrorDetails = "Error details:";
        public const string ErrorNoJokeFound = "No joke found";

        public const string ScreenSeparator = "--------------------------------------------------------";
        public const string ChuckNorris = "Chuck Norris";

        public const string CategoryQueryString = "category";

        public const string CategoriesCacheKey = "CategoryCacheKey";

        // List of gender specific key words in the jokes
        public const string Male = "male";
        public const string Female = "female";
        public const string He = "He";
        public const string Him = "him";
        public const string His = "his";
        public const string She = "She";
        public const string Her = "her";
        public const string he = "he";
        public const string she = "she";
        public const string himeself = "himself";
        public const string herself = "herself";

        // List of api urls
        public const string RandomJokesApiURL = "jokes/random";
        public const string JokesCategoriesApiURL = "jokes/categories";
    }
}
