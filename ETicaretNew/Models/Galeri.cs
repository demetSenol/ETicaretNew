using System;
using System.Collections.Generic;

namespace ETicaretNew.Models;

public partial class Galeri
{
    public int ResimId { get; set; }

    public string Resim { get; set; } = null!;

    public int? UrunId { get; set; }

    public virtual Urun? Urun { get; set; }

    public virtual ICollection<Urun> Uruns { get; set; } = new List<Urun>();
}
