using System.Threading.Tasks;

namespace PlayerData.WebAPI.Services
{
    public interface IPersonsDBChromeDriverService
    {
        Task ExecuteChromeDriverForPersonsDbTable();
        Task GetPlayersImage();
    }
}
