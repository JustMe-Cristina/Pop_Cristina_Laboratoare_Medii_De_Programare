using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pop_Cristina_Lab2.Migrations
{
    /// <inheritdoc />
    public partial class AddMemberEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrowing_Book_BookId",
                table: "Borrowing");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrowing_Member_MemberId",
                table: "Borrowing");

            migrationBuilder.DropColumn(
                name: "JoinDate",
                table: "Member");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Publisher",
                newName: "PublisherName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Member",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Borrowing",
                newName: "MemberID");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Borrowing",
                newName: "BookID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Borrowing",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Borrowing_MemberId",
                table: "Borrowing",
                newName: "IX_Borrowing_MemberID");

            migrationBuilder.RenameIndex(
                name: "IX_Borrowing_BookId",
                table: "Borrowing",
                newName: "IX_Borrowing_BookID");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Member",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Member",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MemberID",
                table: "Borrowing",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BookID",
                table: "Borrowing",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowing_Book_BookID",
                table: "Borrowing",
                column: "BookID",
                principalTable: "Book",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowing_Member_MemberID",
                table: "Borrowing",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrowing_Book_BookID",
                table: "Borrowing");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrowing_Member_MemberID",
                table: "Borrowing");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Member");

            migrationBuilder.RenameColumn(
                name: "PublisherName",
                table: "Publisher",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Member",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MemberID",
                table: "Borrowing",
                newName: "MemberId");

            migrationBuilder.RenameColumn(
                name: "BookID",
                table: "Borrowing",
                newName: "BookId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Borrowing",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Borrowing_MemberID",
                table: "Borrowing",
                newName: "IX_Borrowing_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Borrowing_BookID",
                table: "Borrowing",
                newName: "IX_Borrowing_BookId");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Member",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinDate",
                table: "Member",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Borrowing",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Borrowing",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowing_Book_BookId",
                table: "Borrowing",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowing_Member_MemberId",
                table: "Borrowing",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
