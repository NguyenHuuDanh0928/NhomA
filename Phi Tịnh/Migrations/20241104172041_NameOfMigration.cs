using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDichVu.Migrations
{
    /// <inheritdoc />
    public partial class NameOfMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BacSiThuY",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenBacSi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChuyenMon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoDienThoai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BacSiThu__3214EC07E1B97C42", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DichVu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDichVu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChiPhiDiChuyen = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DichVu__3214EC07CC5E9DE5", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matkhau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenKhachHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateOnly>(type: "date", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KhachHan__3214EC07DD2E8CA9", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LichLamViec",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BacSiThuYId = table.Column<int>(type: "int", nullable: false),
                    Ngay = table.Column<DateOnly>(type: "date", nullable: false),
                    GioBatDau = table.Column<TimeOnly>(type: "time", nullable: false),
                    GioKetThuc = table.Column<TimeOnly>(type: "time", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LichLamV__3214EC077A680252", x => x.Id);
                    table.ForeignKey(
                        name: "FK__LichLamVi__BacSi__398D8EEE",
                        column: x => x.BacSiThuYId,
                        principalTable: "BacSiThuY",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LichHens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhachHangId = table.Column<int>(type: "int", nullable: false),
                    BacSiThuYid = table.Column<int>(type: "int", nullable: false),
                    DichVuId = table.Column<int>(type: "int", nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichHens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LichHens_BacSiThuY_BacSiThuYid",
                        column: x => x.BacSiThuYid,
                        principalTable: "BacSiThuY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LichHens_DichVu_DichVuId",
                        column: x => x.DichVuId,
                        principalTable: "DichVu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LichHens_KhachHang_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "KhachHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichSuDatDichVu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhachHangId = table.Column<int>(type: "int", nullable: false),
                    DichVuId = table.Column<int>(type: "int", nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LichSuDa__3214EC0752EB358A", x => x.Id);
                    table.ForeignKey(
                        name: "FK__LichSuDat__DichV__31EC6D26",
                        column: x => x.DichVuId,
                        principalTable: "DichVu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__LichSuDat__Khach__30F848ED",
                        column: x => x.KhachHangId,
                        principalTable: "KhachHang",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhanHoi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhachHangId = table.Column<int>(type: "int", nullable: false),
                    DichVuId = table.Column<int>(type: "int", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhanHoi__3214EC0718C47948", x => x.Id);
                    table.ForeignKey(
                        name: "FK__PhanHoi__DichVuI__2D27B809",
                        column: x => x.DichVuId,
                        principalTable: "DichVu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__PhanHoi__KhachHa__2C3393D0",
                        column: x => x.KhachHangId,
                        principalTable: "KhachHang",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__KhachHan__0389B7BD1161810E",
                table: "KhachHang",
                column: "SoDienThoai",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LichHens_BacSiThuYid",
                table: "LichHens",
                column: "BacSiThuYid");

            migrationBuilder.CreateIndex(
                name: "IX_LichHens_DichVuId",
                table: "LichHens",
                column: "DichVuId");

            migrationBuilder.CreateIndex(
                name: "IX_LichHens_KhachHangId",
                table: "LichHens",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_LichLamViec_BacSiThuYId",
                table: "LichLamViec",
                column: "BacSiThuYId");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuDatDichVu_DichVuId",
                table: "LichSuDatDichVu",
                column: "DichVuId");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuDatDichVu_KhachHangId",
                table: "LichSuDatDichVu",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanHoi_DichVuId",
                table: "PhanHoi",
                column: "DichVuId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanHoi_KhachHangId",
                table: "PhanHoi",
                column: "KhachHangId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LichHens");

            migrationBuilder.DropTable(
                name: "LichLamViec");

            migrationBuilder.DropTable(
                name: "LichSuDatDichVu");

            migrationBuilder.DropTable(
                name: "PhanHoi");

            migrationBuilder.DropTable(
                name: "BacSiThuY");

            migrationBuilder.DropTable(
                name: "DichVu");

            migrationBuilder.DropTable(
                name: "KhachHang");
        }
    }
}
