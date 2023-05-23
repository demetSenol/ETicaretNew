namespace ETicaretNew.Models
{
    public partial class EticaretContext
    {
        private readonly IConfiguration _config;
        private IConfiguration _context;

        public EticaretContext(IConfiguration context)
        {
            _context = context;
        }
    }
}
