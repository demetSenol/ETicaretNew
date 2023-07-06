using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ETicaretNew.Models;

public partial class EticaretContext : DbContext
{
    public EticaretContext()
    {
    }

    public EticaretContext(DbContextOptions<EticaretContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adre> Adres { get; set; }

    public virtual DbSet<Galeri> Galeris { get; set; }

    public virtual DbSet<Kategori> Kategoris { get; set; }

    public virtual DbSet<Sipari> Siparis { get; set; }

    public virtual DbSet<SiparisUrun> SiparisUruns { get; set; }

    public virtual DbSet<SoruYorum> SoruYorums { get; set; }

    public virtual DbSet<Urun> Uruns { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Uye> Uyes { get; set; }

    public virtual DbSet<Yonetici> Yoneticis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOPI6AM0E5;Initial Catalog=ETicaret;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adre>(entity =>
        {
            entity.HasKey(e => e.AdresId);

            entity.Property(e => e.AdresId).HasColumnName("adresId");
            entity.Property(e => e.Adres)
                .HasMaxLength(500)
                .HasColumnName("adres");
            entity.Property(e => e.UyeId).HasColumnName("uyeId");

            entity.HasOne(d => d.Uye).WithMany(p => p.AdresNavigation)
                .HasForeignKey(d => d.UyeId)
                .HasConstraintName("FK_Adres_Uye");
        });

        modelBuilder.Entity<Galeri>(entity =>
        {
            entity.HasKey(e => e.ResimId).HasName("PK_Table_1");

            entity.ToTable("Galeri");

            entity.Property(e => e.ResimId).HasColumnName("resimId");
            entity.Property(e => e.Resim)
                .HasColumnType("text")
                .HasColumnName("resim");
            entity.Property(e => e.UrunId).HasColumnName("urunId");

            entity.HasOne(d => d.Urun).WithMany(p => p.Galeris)
                .HasForeignKey(d => d.UrunId)
                .HasConstraintName("FK_Galeri_Urun1");
        });

        modelBuilder.Entity<Kategori>(entity =>
        {
            entity.ToTable("Kategori");

            entity.Property(e => e.KategoriId).HasColumnName("kategoriId");
            entity.Property(e => e.Adi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("adi");
        });

        modelBuilder.Entity<Sipari>(entity =>
        {
            entity.HasKey(e => e.SiparisId);

            entity.Property(e => e.SiparisId).HasColumnName("siparisId");
            entity.Property(e => e.AdresId).HasColumnName("adresId");
            entity.Property(e => e.Durum)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("durum");
            entity.Property(e => e.Tutar)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("tutar");
            entity.Property(e => e.UyeId).HasColumnName("uyeId");

            entity.HasOne(d => d.Adres).WithMany(p => p.Siparis)
                .HasForeignKey(d => d.AdresId)
                .HasConstraintName("FK_Siparis_Adres");

            entity.HasOne(d => d.Uye).WithMany(p => p.Siparis)
                .HasForeignKey(d => d.UyeId)
                .HasConstraintName("FK_Siparis_Uye");
        });

        modelBuilder.Entity<SiparisUrun>(entity =>
        {
            entity.HasKey(e => e.KayitId);

            entity.ToTable("SiparisUrun");

            entity.Property(e => e.KayitId).HasColumnName("kayitId");
            entity.Property(e => e.Adet).HasColumnName("adet");
            entity.Property(e => e.BririmFiyat)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("bririmFiyat");
            entity.Property(e => e.SiparisDurumu)
                .HasMaxLength(50)
                .HasColumnName("siparisDurumu");
            entity.Property(e => e.SiparisId).HasColumnName("siparisId");
            entity.Property(e => e.SiparisTarihi)
                .HasColumnType("datetime")
                .HasColumnName("siparisTarihi");
            entity.Property(e => e.UrunId).HasColumnName("urunId");

            entity.HasOne(d => d.Siparis).WithMany(p => p.SiparisUruns)
                .HasForeignKey(d => d.SiparisId)
                .HasConstraintName("FK_SiparisUrun_Siparis");

            entity.HasOne(d => d.Urun).WithMany(p => p.SiparisUruns)
                .HasForeignKey(d => d.UrunId)
                .HasConstraintName("FK_SiparisUrun_Urun");
        });

        modelBuilder.Entity<SoruYorum>(entity =>
        {
            entity.HasKey(e => e.YorumId);

            entity.ToTable("Soru_Yorum");

            entity.Property(e => e.YorumId).HasColumnName("yorumId");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.KontrolEdildiMi)
                .HasMaxLength(50)
                .HasColumnName("kontrolEdildiMi");
            entity.Property(e => e.UrunId).HasColumnName("urunId");
            entity.Property(e => e.UyeId).HasColumnName("uyeId");
            entity.Property(e => e.Yorum)
                .HasMaxLength(500)
                .HasColumnName("yorum");
            entity.Property(e => e.YorumTarihSaati)
                .HasColumnType("datetime")
                .HasColumnName("yorumTarihSaati");

            entity.HasOne(d => d.Urun).WithMany(p => p.SoruYorums)
                .HasForeignKey(d => d.UrunId)
                .HasConstraintName("FK_Soru_Yorum_Urun");

            entity.HasOne(d => d.Uye).WithMany(p => p.SoruYorums)
                .HasForeignKey(d => d.UyeId)
                .HasConstraintName("FK_Soru_Yorum_Uye");
        });

        modelBuilder.Entity<Urun>(entity =>
        {
            entity.ToTable("Urun");

            entity.Property(e => e.UrunId).HasColumnName("urunId");
            entity.Property(e => e.Aciklama)
                .HasMaxLength(1000)
                .HasColumnName("aciklama");
            entity.Property(e => e.Adi)
                .HasMaxLength(100)
                .HasColumnName("adi");
            entity.Property(e => e.Anasayfa)
                .HasMaxLength(50)
                .HasColumnName("anasayfa");
            entity.Property(e => e.Fiyat)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("fiyat");
            entity.Property(e => e.KategoriId).HasColumnName("kategoriId");
            entity.Property(e => e.ResimId).HasColumnName("resimId");
            entity.Property(e => e.Stok)
                .HasMaxLength(50)
                .HasColumnName("stok");

            entity.HasOne(d => d.Kategori).WithMany(p => p.Uruns)
                .HasForeignKey(d => d.KategoriId)
                .HasConstraintName("FK_Urun_Kategori");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Adi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("adi");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Sifre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sifre");
        });

        modelBuilder.Entity<Uye>(entity =>
        {
            entity.ToTable("Uye");

            entity.Property(e => e.UyeId).HasColumnName("uyeId");
            entity.Property(e => e.Adi)
                .HasMaxLength(50)
                .HasColumnName("adi");
            entity.Property(e => e.Adres)
                .HasMaxLength(100)
                .HasColumnName("adres");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Il)
                .HasMaxLength(50)
                .HasColumnName("il");
            entity.Property(e => e.Ilce)
                .HasMaxLength(50)
                .HasColumnName("ilce");
            entity.Property(e => e.PostaKodu)
                .HasMaxLength(10)
                .HasColumnName("postaKodu");
            entity.Property(e => e.Sifre)
                .HasMaxLength(50)
                .HasColumnName("sifre");
            entity.Property(e => e.Soyadi)
                .HasMaxLength(50)
                .HasColumnName("soyadi");
            entity.Property(e => e.TelefonNo)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("telefonNo");
        });

        modelBuilder.Entity<Yonetici>(entity =>
        {
            entity.ToTable("Yonetici");

            entity.Property(e => e.Durum).HasColumnName("durum");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.KullaniciAdi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("kullaniciAdi");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
