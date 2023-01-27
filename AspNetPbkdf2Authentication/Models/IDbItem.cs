namespace AspNetPbkdf2Authentication.Models
{
    public interface IDbItem
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        void DbAdd();
        void DbUpdate();
        bool DbDelete();
    }
}
