using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretNew.Models
{
	public partial class  Urun

       
	{

        //viewden resimleri almak için ekledik
        [NotMapped]
		public List<IFormFile> ImageFile{ get; set; }

		[NotMapped]
        public List<Galeri>? Resimler { get; set; }

        //galeriden resimleri çekeriz
        public List<Galeri> GetGaleris(int urunId)
        {
            using (var _context = new EticaretContext())
            {
                var urunResim = _context.Galeris
                    .Where(x => x.UrunId == urunId)
                    .ToList();

                return urunResim;
            }
        }
    }
}
