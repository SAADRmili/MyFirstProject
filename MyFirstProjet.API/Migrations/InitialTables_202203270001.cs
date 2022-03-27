using FluentMigrator;

namespace MyFirstProject.API.Migrations
{
    [Migration(202203270001)]
    public class InitialTables_202203270001 : Migration
    {
        public override void Down()
        {
            Delete.Table("Products");
            Delete.Table("Brands");
        }

        public override void Up()
        {
            Create.Table("Brands")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("Name").AsString(50).NotNullable();



            Create.Table("Products")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("Price").AsInt32().NotNullable()
                .WithColumn("PromoPrice").AsString(50).NotNullable()
                .WithColumn("BrandId").AsGuid().NotNullable().ForeignKey("Brands", "Id");
        }
    }
}
