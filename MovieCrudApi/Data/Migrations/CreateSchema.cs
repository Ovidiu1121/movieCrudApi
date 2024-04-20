using FluentMigrator;

namespace MovieCrudApi.Data.Migrations
{
    [Migration(10032024)]
    public class CreateSchema : Migration
    {
        public override void Up()
        {
            Create.Table("movie")
                  .WithColumn("id").AsInt32().PrimaryKey().Identity()
                   .WithColumn("title").AsString(128).NotNullable()
                    .WithColumn("duration").AsInt32().NotNullable()
                     .WithColumn("genre").AsString(128).NotNullable();
        }
        public override void Down()
        {

        }

    }
}
