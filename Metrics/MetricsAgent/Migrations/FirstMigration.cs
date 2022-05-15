using FluentMigrator;
using System.Collections.Generic;

namespace MetricsAgent.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        private readonly List<string> _tabNames = new List<string>()
                {
                    "cpumetrics",
                    "dotnetmetrics",
                    "hddmetrics",
                    "networkmetrics",
                    "rammetrics"
                };

        public override void Down()
        {
            for (int i = 0; i < _tabNames.Count; i++)
            {
                Delete.Table($"{_tabNames[i]}");
            }
        }

        public override void Up()
        {
            for (int i = 0; i < _tabNames.Count; i++)
            {
                Create.Table($"{_tabNames[i]}").WithColumn("id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("value").AsInt32().WithColumn("time").AsInt64();
            }
        }
    }
}
