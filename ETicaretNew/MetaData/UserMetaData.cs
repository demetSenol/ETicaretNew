using System.ComponentModel.DataAnnotations;

namespace ETicaretNew.MetaData
{
	public class UserMetaData
	{
		[Display(Name ="Kullancı Adı")]
		public string? Adi { get; set; }

		[Display(Name =	"Email Adresi")]
		public string? Email { get; set; }

		[Display(Name ="Şifre")]
		[DataType(DataType.Password)]
		public string? Sifre { get; set; }
	}
}
