using System;
using FluentMigrator;
using LendingLibrary.DataConstants;
using _Table=LendingLibrary.DataConstants.Tables.Person;
using _Columns=LendingLibrary.DataConstants.Tables.Person.Columns;

namespace LendingLibrary.DB.Migrations
{
    [Migration(201601291433)]
    public class Migration_201601291433_CreatePersonTable:Migration
    {
        public override void Up()
        {
            Create.Table(_Table.NAME)
                .WithColumn(_Columns.PersonId).AsInt32().PrimaryKey().Identity()
                .WithColumn(_Columns.FirstName).AsString(FieldSizes.HUMAN_NAME).NotNullable()
                .WithColumn(_Columns.Surname).AsString(FieldSizes.HUMAN_NAME).NotNullable()
                .WithColumn(_Columns.Email).AsString(FieldSizes.EMAIL).Nullable()
                .WithColumn(_Columns.PHONE_NUMBER).AsString(FieldSizes.PHONE).NotNullable()
                .WithColumn(_Columns.Photo).AsBinary(Int32.MaxValue);
        }

        public override void Down()
        {
        }
    }
}
