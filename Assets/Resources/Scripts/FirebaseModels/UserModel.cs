public class UserModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public int MMR { get; set; }

    public UserModel()
    {
    }

    public UserModel(string username, string email, int mmr)
    {
        Username = username;
        Email = email;
        MMR = mmr;
    }
}