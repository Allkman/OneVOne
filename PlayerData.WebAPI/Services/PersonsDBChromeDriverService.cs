using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using OneVOne.GameService.Repository;
using PlayerData.WebAPI.Options;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using OneVOne.GameService.Infrastructure;
using System.Linq;
using OneVOne.GameService.Core.Entities;
using System.Net;
using System.Threading;
using System.Net.Http;
using System.Security.Authentication;
using Polly;

namespace PlayerData.WebAPI.Services
{
    public class PersonsDBChromeDriverService : IPersonsDBChromeDriverService
    {
        private ChromeDriver _driver;
        private IUnitOfWork _unitOfWork;
        private readonly string _nbaPlayersUrl;
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
        private List<string> _playerURLs;

        private const int MaxRetries = 10;

        public PersonsDBChromeDriverService(
            IUnitOfWork unitOfWork,
            IOptions<UnitOfWorkOptions> options,
            IOptions<PersonsSqlOptions> personsSqlOptions,
            IOptions<PersonDbOptions> personDbOptions)
        {
            _unitOfWork = unitOfWork;
            _driver = new ChromeDriver();
            _playerURLs = new List<string>();
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

        public async Task ExecuteChromeDriverForPersonsDbTable()
        {
            // Navigate to the NBA players page
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
                IList<IWebElement> divElements = _driver.FindElements(By.TagName("tr")).ToList();

                // Iterate through each <div> element and extract the first and last names from its child <p> elements
                foreach (IWebElement divElement in divElements.Skip(1))
                {

                    // Find the first <p> element and extract its innerText as the first name
                    IWebElement firstNameElement = divElement.FindElement(By.CssSelector("p.RosterRow_playerFirstName__NYm50"));

                    string firstName = firstNameElement.Text;

                    // Find the second <p> element and extract its innerText as the last name
                    IWebElement lastNameElement = divElement.FindElement(By.CssSelector("p:nth-child(2)"));
                    string lastName = lastNameElement.Text;


                    // Check if a row with the same first and last name already exists in the database
                    var person = await _unitOfWork.PersonRepository.FindAsync(person => person.FirstName == firstName && person.LastName == lastName);
                    if (person == null)
                    {
                        person = new Person { FirstName = firstName, LastName = lastName };
                        await _unitOfWork.PersonRepository.AddAsync(person);
                        await _unitOfWork.CommitAsync();
                    }

                    IWebElement playerLinkElement = divElement.FindElement(By.TagName("a"));
                    string href = playerLinkElement.GetAttribute("href");
                    _playerURLs.Add(href);
                    var player = await _unitOfWork.PlayerRepository.FindAsync(player => player.PersonId == person.Id);
                    if (player == null)
                    {

                        player = new Player();
                        player.Person = person;
                        player.PersonId = person.Id;
                        player.NbaPlayerPageUrl = href;
                        await _unitOfWork.PlayerRepository.AddAsync(player);
                        await _unitOfWork.CommitAsync();
                    }

                    if (player.PersonId == person.Id)
                    {
                        // If the player already exists, retrieve it from the database to ensure that it has the correct Person object
                        player.Person = await _unitOfWork.PersonRepository.FindAsync(p => p.Id == person.Id);
                        player.NbaPlayerPageUrl = href;
                        await _unitOfWork.CommitAsync();

                    }
                    else
                    {
                        player.Person = person;
                        player.PersonId = person.Id;
                    }

                    IWebElement teamAbbElement = null;
                    try
                    {
                        teamAbbElement = divElement.FindElement(By.CssSelector("a.Anchor_anchor__cSc3P.RosterRow_team__AunTP"));
                    }
                    catch (NoSuchElementException)
                    {
                        // Element not found, set teamAbbElement to null
                        teamAbbElement = null;
                    }
                    string teamABB = teamAbbElement?.Text;
                    if (teamABB != null)
                    {
                        var team = await _unitOfWork.TeamRepository.FindAsync(team => team.Abbreviation == teamABB);
                        player.TeamId = team.Id;
                        await _unitOfWork.CommitAsync();
                    }
                    else
                    {
                        player.TeamId = null;
                        await _unitOfWork.CommitAsync();
                    }
                }
            }
            Console.WriteLine(_playerURLs.Count);
            _driver.Quit();
        }

        public async Task GetPlayersImage()
        {
            var players = await _unitOfWork.PlayerRepository.GetAllAsync();
            var waitSecondPageToLoad = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));

            var skipWhile = players.SkipWhile(
                p => /*p.Person.FirstName.ToLower() != "gabe" && */p.Person.LastName.ToLower() != "achiuwa");
            foreach (var player in skipWhile)
            {
                _driver.Navigate().GoToUrl(player.NbaPlayerPageUrl);

                //playerUrl = player.NbaPlayerPageUrl;
                IWebElement imageElement = _driver.FindElement(By.CssSelector("img.PlayerImage_image__wH_YX.PlayerSummary_playerImage__sysif"));

                if (imageElement != null)
                {
                    string srcValue = imageElement.GetAttribute("src");
                    var cookieContainer = new CookieContainer();
                    _driver.Manage().Cookies.AllCookies.ToList().ForEach(c =>
                        cookieContainer.Add(new Uri(player.NbaPlayerPageUrl), new System.Net.Cookie(c.Name, c.Value))
                    );

                    // Check if a PlayerImage with the same PlayerId already exists in the database
                    var existingPlayerImages = await _unitOfWork.PlayerImageRepository.GetAllAsync();
                    var existingPlayerImage = existingPlayerImages.FirstOrDefault(i => i.PlayerId == player.Id);

                    var httpClientHandler = new HttpClientHandler { SslProtocols = SslProtocols.Tls12, CookieContainer = cookieContainer };
                    using (var httpClient = new HttpClient(httpClientHandler) { Timeout = TimeSpan.FromSeconds(30) })
                    {
                        try
                        {
                            byte[] imageBytes = await DownloadImageWithRetry(srcValue);
                            if (existingPlayerImage != null)
                            {
                                existingPlayerImage.Image = imageBytes;
                                await _unitOfWork.CommitAsync();
                            }
                            else
                            {
                                var playerImage = new PlayerImage { Image = imageBytes, PlayerId = player.Id };
                                await _unitOfWork.PlayerImageRepository.AddAsync(playerImage);
                                await _unitOfWork.CommitAsync();
                            }
                        }
                        catch (HttpRequestException)
                        {
                            throw new HttpRequestException();
                        }
                    }
                }
            }
            _driver.Quit();
        }
        public async Task<byte[]> DownloadImageWithRetry(string imageUrl)
        {
            var httpClientHandler = new HttpClientHandler { SslProtocols = SslProtocols.Tls12 };
            var httpClient = new HttpClient(httpClientHandler) { Timeout = TimeSpan.FromSeconds(30) };

            int retryCount = 0;
            while (retryCount < MaxRetries)
            {
                try
                {
                    byte[] imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                    return imageBytes;
                }
                catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.BadGateway)
                {
                    retryCount++;
                    var backoffDelay = TimeSpan.FromSeconds(Math.Pow(2, retryCount));
                    await Task.Delay(backoffDelay);
                }
            }

            throw new Exception($"Failed to download image after {MaxRetries} retries.");
        }
    }
}
