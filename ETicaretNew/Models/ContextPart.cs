namespace ETicaretNew.Models
{
    public partial class EticaretContext
    {
        private readonly IConfiguration _context;

        public EticaretContext(IConfiguration context)
        {
            _context = context;
        }
    }
}
