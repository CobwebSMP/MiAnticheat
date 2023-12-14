using MiNET;

namespace MiAnticheat
{
    public class PlayerData
    {
        public Player Player { get; set; }
        public int airTime { get; set; }

        public PlayerData(Player player)
        {
            Player = player;
            airTime = 0;
        }
    }
}
