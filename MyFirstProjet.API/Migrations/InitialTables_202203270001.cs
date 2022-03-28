using FluentMigrator;

namespace MyFirstProject.API.Migrations
{
    [Migration(202203270001)]
    public class InitialTables_202203270001 : Migration
    {
        public override void Down()
        {
            Delete.Table("products");
            Delete.Table("brands");
        }

        public override void Up()
        {
            Create.Table("brands")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("name").AsString(50).NotNullable();



            Create.Table("products")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("name").AsString(50).NotNullable()
                .WithColumn("price").AsInt32().NotNullable()
                .WithColumn("promoPrice").AsString(50).NotNullable()
                .WithColumn("brandId").AsGuid().NotNullable().ForeignKey("brands", "id");
        }
    }
}
