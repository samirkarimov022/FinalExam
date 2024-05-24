using FinalExam.DAL;
using Microsoft.EntityFrameworkCore;

namespace FinalExam.Services
{
    public class HomeService
    {
        private readonly AppDbContext _context;

        public HomeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<string, string>> GetSettingsAsync()
        {
            var settings = await _context.Settings.ToDictionaryAsync(s => s.Key, s => s.Value);
            return settings;
        }
    }
}
