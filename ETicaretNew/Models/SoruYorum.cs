using System;
using System.Collections.Generic;

namespace ETicaretNew.Models;

public partial class SoruYorum
{
    public int YorumId { get; set; }

    public int? UyeId { get; set; }

    public int? UrunId { get; set; }

    public string? Email { get; set; }

    public DateTime? YorumTarihSaati { get; set; }

    public string? Yorum { get; set; }

    public string? KontrolEdildiMi { get; set; }

    public virtual Urun? Urun { get; set; }

    public virtual Uye? Uye { get; set; }
}
