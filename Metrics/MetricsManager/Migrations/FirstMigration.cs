using FluentMigrator;
using System.Collections.Generic;

namespace MetricsManager.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {        
        public override void Down()
        {
          
                Delete.Table("agentsrepo");

        }

        public override void Up()
        {
           
                Create.Table("agentsrepo").WithColumn("id").AsInt32()/*.PrimaryKey().Identity()*/
                    .WithColumn("agentaddress").AsString().WithColumn("enable").AsBoolean();
            
        }
    }
}
