namespace Root
{
    public interface IMeleeMotionConfig
    {
        float RunSpeed { get; }
        int RunSpeedZombie { get; }
        float WalkSpeed { get; }
        int WalkSpeedZombie { get; }
    }
}