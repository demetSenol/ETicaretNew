using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretNew.Models
{
    public partial class Galeri
    {

        [NotMapped]
        public IFormFile? ImgFile { get; set; }
    }
}
