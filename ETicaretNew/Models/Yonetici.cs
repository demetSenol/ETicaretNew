using System;
using System.Collections.Generic;

namespace ETicaretNew.Models;

public partial class Yonetici
{
    public int Id { get; set; }

    public string? KullaniciAdi { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? Durum { get; set; }
}
