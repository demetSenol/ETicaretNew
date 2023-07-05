using System;
using System.Collections.Generic;

namespace ETicaretNew.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Adi { get; set; }

    public string? Email { get; set; }

    public string? Sifre { get; set; }

    public int? IsActive { get; set; }
}
