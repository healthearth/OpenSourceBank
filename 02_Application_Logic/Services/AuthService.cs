//
public class AuthService
{
    private readonly OpenLedgerDbContext _db;

    public AuthService(OpenLedgerDbContext db)
    {
        _db = db;
    }

    public async Task<User> Register(string email, string password)
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User
        {
            Email = email,
            PasswordHash = hash
        };

        _db.Users.Add(user);

        var account = new Account
        {
            User = user,
            Balance = 1000m
        };

        _db.Accounts.Add(account);
        await _db.SaveChangesAsync();

        return user;
    }

    public async Task<User?> Validate(string email, string password)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);

        if (user == null) return null;

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        return user;
    }
}
