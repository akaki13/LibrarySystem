using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystemData.Migrations
{
    public partial class CreateStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE GetByPopularity
    @BookNameSearch NVARCHAR(100) = NULL,
    @MinTimesTaken INT = NULL,
    @MaxTimesTaken INT = NULL
AS
BEGIN
    SELECT Book.Name AS BookName, COUNT(Borrows.Book_id) AS TimesTaken
    FROM Book
    LEFT JOIN Borrows ON Book.Id = Borrows.Book_id
    WHERE (@BookNameSearch IS NULL OR Book.Name LIKE '%' + @BookNameSearch + '%')
    GROUP BY Book.Name
    HAVING (@MinTimesTaken IS NULL OR COUNT(Borrows.Book_id) >= @MinTimesTaken)
       AND (@MaxTimesTaken IS NULL OR COUNT(Borrows.Book_id) <= @MaxTimesTaken);
END;");

            migrationBuilder.Sql(@"CREATE PROCEDURE ClientsPerformance
    @PersonNameSearch NVARCHAR(100) = NULL,
    @MinBooksTaken INT = NULL,
    @MaxBooksTaken INT = NULL
AS
BEGIN
    SELECT Person.Firstname + ' ' + Person.Lastname AS PersonName, COUNT(Borrows.Person_id) AS BooksTaken
    FROM Person
    LEFT JOIN Borrows ON Person.Id = Borrows.Person_id
    WHERE (@PersonNameSearch IS NULL OR (Person.Firstname + ' ' + Person.Lastname) LIKE '%' + @PersonNameSearch + '%')
    GROUP BY Person.Firstname, Person.Lastname
    HAVING (@MinBooksTaken IS NULL OR COUNT(Borrows.Person_id) >= @MinBooksTaken)
       AND (@MaxBooksTaken IS NULL OR COUNT(Borrows.Person_id) <= @MaxBooksTaken);
END;");

            migrationBuilder.Sql(@"CREATE PROCEDURE CurrentTransactions
    @PersonNameSearch NVARCHAR(100) = NULL,
    @BookNameSearch NVARCHAR(100) = NULL,
    @ReturnTimeAfter DATETIME = NULL,
	@ReturnTimeBefore DATETIME = NULL,
    @TakeTimeBefore DATETIME = NULL,
	@TakeTimeAfter DATETIME = NULL
AS
BEGIN
    SELECT 
        Person.Firstname + ' ' + Person.Lastname AS PersonName, 
        Book.Name AS BookName, 
        Borrows.Returned_time as ReturnTime,
	    Borrows.Take_time as TakeTime
    FROM 
        Person
    JOIN 
        Borrows ON Person.Id = Borrows.Person_id
    JOIN 
        Book ON Book.Id = Borrows.Book_id
    WHERE 
        Borrows.Actual_returned_time IS NULL
        AND Borrows.Returned_time > GETDATE()
        AND (@PersonNameSearch IS NULL OR (Person.Firstname + ' ' + Person.Lastname) LIKE '%' + @PersonNameSearch + '%')
        AND (@BookNameSearch IS NULL OR Book.Name LIKE '%' + @BookNameSearch + '%')
        AND (@ReturnTimeAfter IS NULL OR Borrows.Returned_time > @ReturnTimeAfter)
		AND (@ReturnTimeBefore IS NULL OR Borrows.Returned_time < @ReturnTimeBefore)
        AND (@TakeTimeBefore IS NULL OR Borrows.Take_time < @TakeTimeBefore)
		AND (@TakeTimeAfter IS NULL OR Borrows.Take_time > @TakeTimeAfter);
END;");
            migrationBuilder.Sql(@"CREATE PROCEDURE OverdueTransactions
    @PersonNameSearch NVARCHAR(100) = NULL,
    @BookNameSearch NVARCHAR(100) = NULL,
	@ReturnTimeAfter DATETIME = NULL,
    @ReturnTimeBefore DATETIME = NULL
AS
BEGIN
    SELECT 
        Person.Firstname + ' ' + Person.Lastname AS PersonName, 
        Book.Name AS BookName, 
        Borrows.Returned_time as ReturnTime
    FROM 
        Person
    JOIN 
        Borrows ON Person.Id = Borrows.Person_id
    JOIN 
        Book ON Book.Id = Borrows.Book_id
    WHERE 
        Borrows.Actual_returned_time IS NULL
        AND Borrows.Returned_time < GETDATE()
        AND (@PersonNameSearch IS NULL OR (Person.Firstname + ' ' + Person.Lastname) LIKE '%' + @PersonNameSearch + '%')
        AND (@BookNameSearch IS NULL OR Book.Name LIKE '%' + @BookNameSearch + '%')
		AND (@ReturnTimeAfter IS NULL OR Borrows.Returned_time > @ReturnTimeAfter)
        AND (@ReturnTimeBefore IS NULL OR Borrows.Returned_time < @ReturnTimeBefore);
END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[GetByPopularity]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[ClientsPerformance]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[CurrentTransactions]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[OverdueTransactions]");

        }
    }
}
