using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebHangHoa.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiHangHoa",
                columns: table => new
                {
                    MaLoai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiHangHoa", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "HangHoas",
                columns: table => new
                {
                    MaHangHoa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenHangHoa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonGia = table.Column<double>(type: "float", nullable: false),
                    GiamGia = table.Column<byte>(type: "tinyint", nullable: false),
                    MaLoai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoas", x => x.MaHangHoa);
                    table.ForeignKey(
                        name: "FK_HangHoas_LoaiHangHoa_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "LoaiHangHoa",
                        principalColumn: "MaLoai");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HangHoas_MaLoai",
                table: "HangHoas",
                column: "MaLoai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HangHoas");

            migrationBuilder.DropTable(
                name: "LoaiHangHoa");
        }
    }
}
