public interface IAbility
{
    string Name { get; }
    string Description { get; }
    int ManaCost { get; }

    void Cast();
}