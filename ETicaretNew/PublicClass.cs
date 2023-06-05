namespace ETicaretNew
{
    public class PublicClass
    {
        public string ImgToBase64(IFormFile img)
        {
            using (var ms = new MemoryStream())
            {
                img.CopyTo(ms);
                var imageBytes = ms.ToArray();

                //Base64'e dönüştürün
                var base64String = Convert.ToBase64String(imageBytes);

                //veritabanına kaydedin veya başka bir işlem yapın
                return base64String;

            }
        }
    }
}
