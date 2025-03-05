namespace Root
{
    public interface IMeleeAgentConfig
    {
        int Damage { get; }
        int DamageZombie { get; }
        int HeatPoint { get; }
        int HeatPointZombie { get; }
    }
}