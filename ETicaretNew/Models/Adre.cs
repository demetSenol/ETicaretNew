using System;
using System.Collections.Generic;

namespace ETicaretNew.Models;

public partial class Adre
{
    public int AdresId { get; set; }

    public string? Adres { get; set; }

    public int? UyeId { get; set; }

    public virtual ICollection<Sipari> Siparis { get; set; } = new List<Sipari>();

    public virtual Uye? Uye { get; set; }
}
