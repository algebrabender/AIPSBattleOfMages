using Microsoft.EntityFrameworkCore;

namespace webapi.DataLayer
{
    public class BOMContext : DbContext
    {
        public BOMContext(DbContextOptions options) : base(options)
        {

        }
    }
}