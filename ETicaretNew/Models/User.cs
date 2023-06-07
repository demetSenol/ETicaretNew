using ETicaretNew.MetaData;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ETicaretNew.Models;
[MetadataType(typeof(UserMetaData))]
public partial class User   // : IdentityUser
{
    public int UserId { get; set; }

    public string? Adi { get; set; }

    public string? Email { get; set; }

    public string? Sifre { get; set; }

    public int? IsActive { get; set; }
}
