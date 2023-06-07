using System;
using System.Collections.Generic;

namespace ETicaretNew.Models;

public partial class Kategori
{
    public int KategoriId { get; set; }

    public string? Adi { get; set; }

    public virtual ICollection<Urun> Uruns { get; set; } = new List<Urun>();
}
