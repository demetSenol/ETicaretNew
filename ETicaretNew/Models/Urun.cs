using System;
using System.Collections.Generic;

namespace ETicaretNew.Models;

public partial class Urun
{
    public int UrunId { get; set; }

    public string? Adi { get; set; }

    public string Aciklama { get; set; } = null!;

    public decimal Fiyat { get; set; }

    public string Anasayfa { get; set; } = null!;

    public string Stok { get; set; } = null!;

    public int? KategoriId { get; set; }

    public int? ResimId { get; set; }

    public virtual ICollection<Galeri> Galeris { get; set; } = new List<Galeri>();

    public virtual Galeri? Resim { get; set; }

    public virtual ICollection<SiparisUrun> SiparisUruns { get; set; } = new List<SiparisUrun>();

    public virtual ICollection<SoruYorum> SoruYorums { get; set; } = new List<SoruYorum>();
}
