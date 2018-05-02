namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TranslateProjectToEnglish : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoListModels", "TaskDescription", c => c.String(maxLength: 4000));
            AddColumn("dbo.ToDoListModels", "Deadline", c => c.DateTime(nullable: false));
            DropColumn("dbo.ToDoListModels", "OpisZadania");
            DropColumn("dbo.ToDoListModels", "TerminWykonania");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ToDoListModels", "TerminWykonania", c => c.DateTime(nullable: false));
            AddColumn("dbo.ToDoListModels", "OpisZadania", c => c.String(maxLength: 4000));
            DropColumn("dbo.ToDoListModels", "Deadline");
            DropColumn("dbo.ToDoListModels", "TaskDescription");
        }
    }
}
