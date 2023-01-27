namespace AspNetPbkdf2Authentication.Models
{
    public class User : IDbItem
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool Deleted { get; set; }
        private readonly MyContext context = new();
        public User(string username, string passwordHash, string passwordSalt)
        {
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
        public void DbAdd()
        {
            _ = context.Users.Add(this);
            _ = context.SaveChanges();
        }

        public bool DbDelete()
        {
            if (Id == 0) { return false; }
            User? db = context.Users.FirstOrDefault(u => u.Id == Id);
            if (db is null) { return false; }
            Deleted = true;
            db.Deleted = true;
            _ = context.SaveChanges();
            return true;
        }

        public void DbUpdate()
        {
            if (Id == 0) { return; }
            User? db = context.Users.FirstOrDefault(u => u.Id == Id);
            if (db is null) { return; }
            db.Username = Username;
            db.PasswordHash = PasswordHash;
            db.PasswordSalt = PasswordSalt;
            db.Deleted = Deleted;
            _ = context.SaveChanges();
        }
    }
}
