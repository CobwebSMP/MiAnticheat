using log4net;
using MiNET;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;

namespace MiAnticheat
{
    [Plugin]
    public class Main : Plugin
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Main));
        public static Dictionary<Player, PlayerData> playerDataMap = new Dictionary<Player, PlayerData>();

        protected override void OnEnable()
        {
            var server = Context.Server;

            server.PlayerFactory.PlayerCreated += (sender, args) =>
            {
                Player player = args.Player;
                player.Ticking += OnTicking;
                PlayerData playerData = new PlayerData(player);
                player.PlayerJoin += (o, eventArgs) =>
                {
                    playerDataMap[player] = playerData;
                };

                player.PlayerLeave += (o, eventArgs) =>
                {
                    playerDataMap.Remove(player);
                };
            };
        }

        private void OnTicking(object sender, PlayerEventArgs e)
        {
            var player = e.Player;
            if (e.Level.TickTime % 10 == 0)
            {
                FlyCheck.Check(player);
            }
        }
    }
}