namespace Fighting
{
    [System.Flags]
    public enum Team
    {
        Default = 0x1,
        Player = 0x2,
        Enemy = 0x4,
    }
}