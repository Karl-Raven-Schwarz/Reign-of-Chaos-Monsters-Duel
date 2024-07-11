public class CardModel
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Stars { get; set; }

    public CardModel(string name, int health, int attack, int stars)
    {
        Name = name;
        Health = health;
        Attack = attack;
        Stars = stars;
    }
}