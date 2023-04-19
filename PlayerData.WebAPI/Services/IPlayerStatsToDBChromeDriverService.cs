using System.Threading.Tasks;

namespace PlayerData.WebAPI.Services
{
    public interface IPlayerStatsToDBChromeDriverService
    {
        Task ExecuteChromeDriverForPlayerStatsToDbTable();
    }
}
