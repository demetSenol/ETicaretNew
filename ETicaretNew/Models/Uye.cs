using System;
using System.Collections.Generic;

namespace ETicaretNew.Models;

public partial class Uye
{
    public int UyeId { get; set; }

    public string? Adi { get; set; }

    public string? Soyadi { get; set; }

    public string? Email { get; set; }

    public decimal? TelefonNo { get; set; }

    public string? Adres { get; set; }

    public string? Ilce { get; set; }

    public string? Il { get; set; }

    public string PostaKodu { get; set; } = null!;

    public string? Sifre { get; set; }

    public virtual ICollection<Adre> AdresNavigation { get; set; } = new List<Adre>();

    public virtual ICollection<Sipari> Siparis { get; set; } = new List<Sipari>();

    public virtual ICollection<SoruYorum> SoruYorums { get; set; } = new List<SoruYorum>();
}
