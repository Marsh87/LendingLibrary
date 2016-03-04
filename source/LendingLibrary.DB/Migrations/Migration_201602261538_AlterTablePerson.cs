using FluentMigrator;
using LendingLibrary.DataConstants;
using _Table = LendingLibrary.DataConstants.Tables.Person;
using _Columns = LendingLibrary.DataConstants.Tables.Person.Columns;

namespace LendingLibrary.DB.Migrations
{
    [Migration(201602261538)]
    public class Migration_201602261538_AlterTablePerson:ForwardOnlyMigration
    {
        public override void Up()
        {
            Alter.Table(_Table.NAME).AddColumn(_Columns.Mimetype).AsString(FieldSizes.MIME_TYPE).Nullable();
        }
    }
}
