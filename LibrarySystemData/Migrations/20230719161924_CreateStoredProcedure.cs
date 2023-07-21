using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystemData.Migrations
{
    public partial class CreateStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE GetByPopularity
              AS
              BEGIN
              SELECT Book.Name AS BookName, COUNT(Borrows.Book_id) AS TimesTaken
              FROM Book
              LEFT JOIN Borrows ON Book.Id = Borrows.Book_id
              GROUP BY Book.Name;
              END;");

            migrationBuilder.Sql(@"CREATE PROCEDURE ClientsPerformance
             AS
             BEGIN
             SELECT Person.Firstname + ' ' + Person.Lastname AS PersonName, COUNT(Borrows.Person_id) AS BooksTaken
             FROM Person
             LEFT JOIN Borrows ON Person.Id = Borrows.Person_id
             GROUP BY Person.Firstname, Person.Lastname ;
             END;");

            migrationBuilder.Sql(@"CREATE PROCEDURE CurrentTransactions
             AS
             BEGIN
             SELECT 
             Person.Firstname + ' ' + Person.Lastname AS PersonName, 
             Book.Name AS BookName, 
             Borrows.Returned_time as ReturTime,
             Borrows.Take_time as TakeTime
             FROM 
             Person
             JOIN 
             Borrows ON Person.Id = Borrows.Person_id
             JOIN 
             Book ON Book.Id = Borrows.Book_id
             WHERE 
             Borrows.Actual_returned_time IS NULL
             AND Borrows.Returned_time > GETDATE();
             END;");
            migrationBuilder.Sql(@"CREATE PROCEDURE OverdueTransactions
               AS
              BEGIN
             SELECT 
             Person.Firstname + ' ' + Person.Lastname AS PersonName, 
             Book.Name AS BookName, 
              Borrows.Returned_time as ReturTime
             FROM 
             Person
             JOIN 
             Borrows ON Person.Id = Borrows.Person_id
             JOIN 
             Book ON Book.Id = Borrows.Book_id
             WHERE 
             Borrows.Actual_returned_time IS NULL
              AND Borrows.Returned_time < GETDATE();
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
