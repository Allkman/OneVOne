using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OneVOne.Repository;

namespace PlayerDataService
{
    public class PersonsDBChromeDriver : IPersonsDBChromeDriver
    {
        private readonly ChromeDriver _driver;
        private readonly string _nbaPlayersUrl;
        private readonly string _connectionString;
        private readonly string _selectQuery;
        private readonly string _insertQuery;
        private readonly string _idParameter;
        private readonly string _firstNameParameter;
        private readonly string _lastNameParameter;
        private readonly string _paginationSelectTagTitle;
        private readonly string _optionTagName;
        private readonly string _playerNameDivClassName;
        private readonly string _playerFirstNameDivClassName;
        private readonly string _playerLastNameDivClassName;

        public PersonsDBChromeDriver(
            IOptions<UnitOfWorkOptions> options,
            IOptions<PersonsSqlOptions> personsSqlOptions,
            IOptions<PersonDbOptions> personDbOptions)
        {
            _connectionString = options.Value.ConnectionString;
            _selectQuery = personsSqlOptions.Value.SelectQuery;
            _insertQuery = personsSqlOptions.Value.InsertQuery;
            _idParameter = personsSqlOptions.Value.IdParameter;
            _firstNameParameter = personsSqlOptions.Value.FirstNameParameter;
            _lastNameParameter = personsSqlOptions.Value.LastNameParameter;
            _nbaPlayersUrl = personDbOptions.Value.NbaPlayersUrl;
            _paginationSelectTagTitle = personDbOptions.Value.PaginationSelectTagTitle;
            _optionTagName = personDbOptions.Value.OptionTagName;
            _playerNameDivClassName = personDbOptions.Value.PlayerNameDivClassName;
            _playerFirstNameDivClassName = personDbOptions.Value.PlayerFirstNameDivClassName;
            _playerLastNameDivClassName = personDbOptions.Value.PlayerLastNameDivClassName;
        }
        public PersonsDBChromeDriver()
        {

        }
        // Initialize the web driver
        public void ExecuteChromeDriverForPersonsDbTable()
        {
            // Navigate to the NBA players page
                    string connectionString = _connectionString;
            _driver.Navigate().GoToUrl(_nbaPlayersUrl);

            // Find the dropdown element that contains the number of pages
            IWebElement dropDownElement = _driver.FindElement(By.CssSelector("select[title='Page Number Selection Drown Down List']"));

            // Get the number of pages by counting the number of <option> elements in the dropdown element
            int numPages = dropDownElement.FindElements(By.TagName("option")).Count;

            // Iterate through each page and extract the first and last names of players
            for (int i = 1; i <= numPages - 1; i++)
            {
                // Select the current page number from the dropdown element
                new SelectElement(dropDownElement).SelectByIndex(i);

                // Wait for the page to load by checking for the presence of the first player name element
                var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
                var element = wait.Until(condition =>
                {
                    try
                    {
                        var elementToBeDisplayed = _driver.FindElement(By.CssSelector("div.RosterRow_playerName__G28lg"));
                        return elementToBeDisplayed.Displayed;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return false;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });

                // Find all the <div> elements with the class name "RosterRow_playerName__G28lg"
                IList<IWebElement> divElements = _driver.FindElements(By.CssSelector("div.RosterRow_playerName__G28lg"));

                // Iterate through each <div> element and extract the first and last names from its child <p> elements
                foreach (IWebElement divElement in divElements)
                {
                    // Find the first <p> element and extract its innerText as the first name
                    IWebElement firstNameElement = divElement.FindElement(By.CssSelector("p.RosterRow_playerFirstName__NYm50"));
                    string firstName = firstNameElement.Text;

                    // Find the second <p> element and extract its innerText as the last name
                    IWebElement lastNameElement = divElement.FindElement(By.CssSelector("p:nth-child(2)"));
                    string lastName = lastNameElement.Text;

                    // Check if a row with the same first and last name already exists in the database
                    string selectQuery = "SELECT COUNT(*) FROM dbo.Persons WHERE FirstName=@FirstName AND LastName=@LastName";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(selectQuery, connection);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);

                        connection.Open();

                        int count = (int)command.ExecuteScalar();

                        // If a row with the same first and last name does not exist, insert a new row
                        if (count == 0)
                        {
                            Guid id = Guid.NewGuid();
                            string insertQuery = "INSERT INTO dbo.Persons (Id, FirstName, LastName) VALUES (@Id, @FirstName, @LastName)";

                            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                            insertCommand.Parameters.AddWithValue("@Id", id);
                            insertCommand.Parameters.AddWithValue("@FirstName", firstName);
                            insertCommand.Parameters.AddWithValue("@LastName", lastName);

                            insertCommand.ExecuteNonQuery();
                            Console.WriteLine("Insert worked!");
                        }
                    }

                    // Do something with the extracted first and last names (e.g. print them to the console)
                    Console.WriteLine($"{firstName.ToLower()}-{lastName.ToLower()}");
                }
            }
            // Quit the web driver
            _driver.Quit();
        }
    }
}
