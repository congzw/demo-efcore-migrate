using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NbSites.Web.Core.Migrates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NbSites.Web.Data
{
    public static class MigrationExtensions
    {
        public static List<MigrationItem> GetMigrationItems(this DatabaseFacade database)
        {
            var migrationItems = new List<MigrationItem>();
            var migrations = database.GetMigrations().ToList();
            var appliedMigrations = database.GetAppliedMigrations().ToList();
            foreach (var migration in migrations)
            {
                var migrationItem = new MigrationItem();
                migrationItem.Id = migration;
                var applied = appliedMigrations.Any(x => migration.Equals(x, StringComparison.OrdinalIgnoreCase));
                migrationItem.Applied = applied;
                migrationItems.Add(migrationItem);
            }
            return migrationItems;
        }
    }
}