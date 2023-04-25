using System.Text.RegularExpressions;
using OneVOne.GameService.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;
using System;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;
using System.Linq;
using OpenQA.Selenium.Interactions;

namespace PlayerData.WebAPI.Services
{
    public class PlayerStatsToDBChromeDriverService : IPlayerStatsToDBChromeDriverService
    {
        ChromeOptions options = new ChromeOptions();

        private ChromeDriver _driver;
        private IUnitOfWork _unitOfWork;

        public PlayerStatsToDBChromeDriverService(IUnitOfWork unitOfWork)
        {
            options.AddArgument("--new-window");
            _driver = new ChromeDriver(options);
            _unitOfWork = unitOfWork;
        }
        public async Task ExecuteChromeDriverForPlayerStatsToDbTable()
        {
            var allPlayers = await _unitOfWork.PlayerRepository.GetAllAsync();
            foreach (var player in allPlayers.SkipWhile(p => p.Person.FirstName.ToLower() != "domantas" && p.Person.LastName.ToLower() != "sabonis"))
            {
                string url = UrlFormat(player.Person.FirstName, player.Person.LastName);

                if (url != null)
                {
                    // Navigate to webpage
                    _driver.Navigate().GoToUrl(url);
                    //_driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);

                    player.OutsideScoring = Convert.ToByte(GetWebElement("Outside Scoring").FindElement(By.TagName("span")).Text);
                    player.InsideScoring = Convert.ToByte(GetWebElement("Inside Scoring").FindElement(By.TagName("span")).Text);
                    player.Defending = Convert.ToByte(GetWebElement("Defending").FindElement(By.TagName("span")).Text);
                    player.Athleticism = Convert.ToByte(GetWebElement("Athleticism").FindElement(By.TagName("span")).Text);
                    player.Playmaking = Convert.ToByte(GetWebElement("Playmaking").FindElement(By.TagName("span")).Text);
                    player.Rebounding = Convert.ToByte(GetWebElement("Rebounding").FindElement(By.TagName("span")).Text);
                    if (player.OutsideScoring == null)
                    {
                        continue;
                    }

                }
                await _unitOfWork.CommitAsync();
            }
            _driver.Quit();
        }

        private IWebElement GetWebElement(string statType)
        {
            return _driver.FindElement((By.XPath($"//h5[contains(text(), '{statType}')]")));
        }
        private string UrlFormat(string firstName, string lastName)
        {
            switch ($"{firstName.ToLower()}-{lastName.ToLower()}")
            {
                case "mo-bamba":
                    return $"https://www.2kratings.com/mohamed-bamba";
                case "nic-claxton":
                    return $"https://www.2kratings.com/nicolas-claxton";
                case "juancho-hernangomez":
                    return $"https://www.2kratings.com/juan-hernangomez";
                case "tim-hardaway jr.":
                    return $"https://www.2kratings.com/tim-hardaway-jr";
                case "ron-harper jr.":
                    return $"https://www.2kratings.com/ron-harper-jr";
                case "bones-hyland":
                    return $"https://www.2kratings.com/nahshon-hyland";
                case "jaren-jackson jr.":
                    return $"https://www.2kratings.com/jaren-jackson-jr";
                case "kenyon-martin jr.":
                    return $"https://www.2kratings.com/kenyon-martin-jr";
                case "marcus-morris sr.":
                    return $"https://www.2kratings.com/marcus-morris-sr";
                case "svi-mykhailiuk":
                    return $"https://www.2kratings.com/sviatoslav-mykhailiuk";
                case "gary-payton ii":
                    return $"https://www.2kratings.com/gary-payton-ii";
                case "cam-reddish":
                    return $"https://www.2kratings.com/cameron-reddish";
                case "gary-trent jr.":
                    return $"https://www.2kratings.com/gary-trent-jr";
                case "ish-wainright":
                    return $"https://www.2kratings.com/ishmail-wainright";
                case "":
                    return default;
            }
            if (lastName.Contains(" Jr."))
            {
                return $"https://www.2kratings.com/{firstName.ToLower()}-{lastName.ToLower().Replace(" jr.", "")}";
            }
            if (firstName.Contains('.') || lastName.Contains('.'))
            {
                return $"https://www.2kratings.com/{firstName.ToLower().Replace(".", "")}-{lastName.ToLower().Replace(".", "")}";
            }
            string pattern = @"\b\w+\b"; // pattern to match any word in the input string
            Match match = Regex.Match(lastName, pattern);
            if (match.Success)
            {
                string targetString = match.Value;
                int index = lastName.IndexOf(targetString);
                lastName.Substring(0, index).Trim();
                return $"https://www.2kratings.com/{firstName.ToLower()}-{targetString.ToLower()}";
            }
            if (firstName.Contains('\'') || lastName.Contains('\''))
            {
                return $"https://www.2kratings.com/{firstName.ToLower().Replace("'", "")}-{lastName.ToLower().Replace("'", "")}";
            }
            else
            {
                return $"https://www.2kratings.com/{firstName.ToLower()}-{lastName.ToLower()}";
            }
        }
    }
}