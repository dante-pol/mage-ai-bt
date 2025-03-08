namespace Root
{
    public enum Teams { PLAYER, AGENTS }

    public interface IEntityAttacked
    {
        public Teams TeamID { get; }
        public void TakeAttack(IAttackProcess attackProcess);
    }
}