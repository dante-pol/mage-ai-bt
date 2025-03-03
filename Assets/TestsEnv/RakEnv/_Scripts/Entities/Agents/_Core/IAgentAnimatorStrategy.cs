namespace Root
{
    public interface IAgentAnimatorStrategy
    {
        void SetBaseAttack();
        void SetDeath();
        void SetIdle();
        void SetRun();
        void SetWalk();
    }
}