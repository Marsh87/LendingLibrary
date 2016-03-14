using System;
using FluentMigrator;
using LendingLibrary.DataConstants;
using _Table = LendingLibrary.DataConstants.Tables.Item;
using _Columns = LendingLibrary.DataConstants.Tables.Item.Columns;

namespace LendingLibrary.DB.Migrations
{
    [Migration(201603141658)]
    public class Migration_201603141658_CreateItemTable:Migration
    {
        public override void Up()
        {
            Create.Table(_Table.NAME)
                .WithColumn(_Columns.ItemId).AsInt32().PrimaryKey().Identity()
                .WithColumn(_Columns.Title).AsString(FieldSizes.NAME).NotNullable()
                .WithColumn(_Columns.Description).AsString(FieldSizes.NAME).NotNullable()
                .WithColumn(_Columns.Photo).AsBinary(Int32.MaxValue).NotNullable()
                .WithColumn(_Columns.Mimetype).AsString(FieldSizes.MIME_TYPE).Nullable();

        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
