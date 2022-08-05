using Microsoft.EntityFrameworkCore;
using WebHangHoa.Models;

namespace WebHangHoa.Data
{
    public class HangHoaContext:DbContext
    {
        public HangHoaContext(DbContextOptions options) : base(options)
        {

        }
        #region DbSet
        public DbSet<Users> User { get; set; }
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<LoaiHangHoa> loaiHangHoas { get; set; }
        public DbSet<DonHang> donHangs { get; set; }
        public DbSet<DonHangChiTiet> donHangChiTiets { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonHang>(e =>
            {
                e.ToTable("DonHang");
                e.HasKey(dh => dh.MaDonHang);
                e.Property(dh => dh.NgayDat).HasDefaultValueSql("getutcdate()");
            }
            );
            modelBuilder.Entity<DonHangChiTiet>(e =>
            {
                e.ToTable("DonHangChiTiet");
                e.HasKey(e =>new { e.MaDonHang, e.MaHangHoa });
                e.HasOne(e => e.DonHang)
                .WithMany(e => e.DonHangChiTiet)
                .HasForeignKey(e => e.MaDonHang)
                .HasConstraintName("FK_DonHangChiTiet_DonHang");

                e.HasOne(e => e.HangHoa)
                .WithMany(e => e.DonHangChiTiet)
                .HasForeignKey(e => e.MaHangHoa)
                .HasConstraintName("FK_DonHangChiTiet_HangHoa");
                });
            modelBuilder.Entity<Users>(e =>
            {
                e.HasIndex(e=>e.id).IsUnique();
                e.Property(e => e.Password).IsRequired().HasMaxLength(50);
                e.Property(e=>e.UserName).IsRequired().HasMaxLength(120);
                e.Property(e=>e.Email).IsRequired().HasMaxLength(120);
            });
        }
    }
}
