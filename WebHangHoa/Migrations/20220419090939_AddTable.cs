using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebHangHoa.Migrations
{
    public partial class AddTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HangHoas_LoaiHangHoa_MaLoai",
                table: "HangHoas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoaiHangHoa",
                table: "LoaiHangHoa");

            migrationBuilder.RenameTable(
                name: "LoaiHangHoa",
                newName: "loaiHangHoas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_loaiHangHoas",
                table: "loaiHangHoas",
                column: "MaLoai");

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    MaDonHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NguoiNhan = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    DiaChiGiao = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    NgayGiao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongTien = table.Column<double>(type: "float", nullable: false),
                    TinhTrang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.MaDonHang);
                });

            migrationBuilder.CreateTable(
                name: "DonHangChiTiet",
                columns: table => new
                {
                    MaDonHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaHangHoa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuongMua = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangChiTiet", x => new { x.MaDonHang, x.MaHangHoa });
                    table.ForeignKey(
                        name: "FK_DonHangChiTiet_DonHang",
                        column: x => x.MaDonHang,
                        principalTable: "DonHang",
                        principalColumn: "MaDonHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonHangChiTiet_HangHoa",
                        column: x => x.MaHangHoa,
                        principalTable: "HangHoas",
                        principalColumn: "MaHangHoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonHangChiTiet_MaHangHoa",
                table: "DonHangChiTiet",
                column: "MaHangHoa");

            migrationBuilder.AddForeignKey(
                name: "FK_HangHoas_loaiHangHoas_MaLoai",
                table: "HangHoas",
                column: "MaLoai",
                principalTable: "loaiHangHoas",
                principalColumn: "MaLoai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HangHoas_loaiHangHoas_MaLoai",
                table: "HangHoas");

            migrationBuilder.DropTable(
                name: "DonHangChiTiet");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_loaiHangHoas",
                table: "loaiHangHoas");

            migrationBuilder.RenameTable(
                name: "loaiHangHoas",
                newName: "LoaiHangHoa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoaiHangHoa",
                table: "LoaiHangHoa",
                column: "MaLoai");

            migrationBuilder.AddForeignKey(
                name: "FK_HangHoas_LoaiHangHoa_MaLoai",
                table: "HangHoas",
                column: "MaLoai",
                principalTable: "LoaiHangHoa",
                principalColumn: "MaLoai");
        }
    }
}
