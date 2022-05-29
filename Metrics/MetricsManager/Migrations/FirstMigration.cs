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
           
                Create.Table("agentsrepo")
                    .WithColumn("agentid").AsInt32().PrimaryKey()   // здесь просто колонка, без первичного ключа и требований к уникальности. Хотел сделать первичный ключ и автоинкремент, но все значения = 0 
                    .WithColumn("agentaddress").AsString()
                    .WithColumn("enable").AsBoolean();
            
        }
    }
}
