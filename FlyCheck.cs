using MiNET.Blocks;
using MiNET.Utils.Vectors;
using MiNET.Worlds;
using MiNET;

namespace MiAnticheat
{
    internal class FlyCheck
    {
        public static void Check(Player player)
        {
            if (!Main.playerDataMap.TryGetValue(player, out PlayerData data)) { return; }
            if (player.GameMode == GameMode.C) { return; }
            if (player.IsGliding) { return; }
            List<PlayerLocation> bCoords = new List<PlayerLocation>
                {
                    new PlayerLocation((int)player.KnownPosition.X + 1, (int)player.KnownPosition.Y - 1, (int)player.KnownPosition.Z + 1),
                    new PlayerLocation((int)player.KnownPosition.X - 1, (int)player.KnownPosition.Y - 1, (int)player.KnownPosition.Z - 1),
                    new PlayerLocation((int)player.KnownPosition.X - 1, (int)player.KnownPosition.Y - 1, (int)player.KnownPosition.Z + 1),
                    new PlayerLocation((int)player.KnownPosition.X + 1, (int)player.KnownPosition.Y - 1, (int)player.KnownPosition.Z - 1),
                    new PlayerLocation((int)player.KnownPosition.X, (int)player.KnownPosition.Y - 1, (int)player.KnownPosition.Z),
                    new PlayerLocation((int)player.KnownPosition.X + 1, (int)player.KnownPosition.Y - 1, (int)player.KnownPosition.Z),
                    new PlayerLocation((int)player.KnownPosition.X - 1, (int)player.KnownPosition.Y - 1, (int)player.KnownPosition.Z),
                    new PlayerLocation((int)player.KnownPosition.X, (int)player.KnownPosition.Y - 1, (int)player.KnownPosition.Z + 1),
                    new PlayerLocation((int)player.KnownPosition.X, (int)player.KnownPosition.Y - 1, (int)player.KnownPosition.Z - 1),
                    new PlayerLocation((int)player.KnownPosition.X, (int)player.KnownPosition.Y - 2, (int)player.KnownPosition.Z)
                };
            if (bCoords.All(coord => player.Level.GetBlock(coord) is Air))
            {
                data.airTime++;
            }
            else
            {
                data.airTime = 0;
            }
            if (data.airTime == 15)
            {
                Player pl = data.Player;
                pl.Disconnect("Anticheat: Movement");
            }
        }
    }
}
