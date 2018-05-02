namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDoListModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OpisZadania = c.String(maxLength: 4000),
                        TerminWykonania = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ToDoListModels");
        }
    }
}
