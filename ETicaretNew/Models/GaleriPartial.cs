using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretNew.Models
{
    public partial class Galeri
    {
        //viewden resimleri alabilmek için ekledik 
        [NotMapped]
        public IFormFile? ImgFile { get; set; }
    }
}
