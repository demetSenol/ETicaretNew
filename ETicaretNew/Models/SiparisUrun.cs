using System;
using System.Collections.Generic;

namespace ETicaretNew.Models;

public partial class SiparisUrun
{
    public int KayitId { get; set; }

    public int? SiparisId { get; set; }

    public int? UrunId { get; set; }

    public DateTime? SiparisTarihi { get; set; }

    public string? SiparisDurumu { get; set; }

    public int? Adet { get; set; }

    public decimal? BririmFiyat { get; set; }

    public virtual Urun? Urun { get; set; }
}
