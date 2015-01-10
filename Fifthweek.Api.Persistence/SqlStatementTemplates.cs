using System.Collections.Generic;
using System.Text;

namespace Fifthweek.Api.Persistence
{
    public static class SqlStatementTemplates
    {
        public static string IdempotentInsert(string insert)
        {
            return string.Format(
                @"BEGIN TRY
                    {0}
                END TRY
                BEGIN CATCH
                    IF ERROR_NUMBER() <> 2601 AND ERROR_NUMBER() <> 2627 /* Unique constraint violation */
                    BEGIN
		                DECLARE @errorNumber nchar(5), @errorMessage nvarchar(2048);
		                SELECT
			                @errorNumber = RIGHT('00000' + ERROR_NUMBER(), 5),
			                @errorMessage = @errorNumber + ' ' + ERROR_MESSAGE();
		                RAISERROR (@errorMessage, 16, 1);
                    END
                END CATCH",
                insert);
        }

        public static void AppendUpdateParameters(this StringBuilder sql, IReadOnlyList<string> fields)
        {
            for (var i = 0; i < fields.Count; i++)
            {
                var isLast = i == fields.Count - 1;
                var field = fields[i];

                sql.Append(field);
                sql.Append("=@");
                sql.Append(field);

                if (!isLast)
                {
                    sql.Append(", ");
                }
            }
        }
    }
}
