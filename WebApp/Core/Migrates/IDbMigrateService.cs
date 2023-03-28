using System.Collections.Generic;
using System.Threading.Tasks;

namespace NbSites.Web.Core.Migrates
{
    public interface IDbMigrateService
    {
        Task EnsureDbExist();
        List<MigrationItem> GetMigrationItems();
        Task ApplyMigration(string to);
        Task Seed();
    }

    public class MigrationItem
    {
        public string Id { get; set; }
        public bool Applied { get; set; }
    }
}