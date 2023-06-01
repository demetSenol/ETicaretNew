using System;
using System.Collections.Generic;

namespace ETicaretNew.Models;

public partial class Sipari
{
    public int SiparisId { get; set; }

    public int? UyeId { get; set; }

    public int? AdresId { get; set; }

    public decimal? Tutar { get; set; }

    public virtual Adre? Adres { get; set; }

    public virtual ICollection<SiparisUrun> SiparisUruns { get; set; } = new List<SiparisUrun>();

    public virtual Uye? Uye { get; set; }
}
