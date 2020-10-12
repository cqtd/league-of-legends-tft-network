using UnityEngine.UI;

namespace CQ.LeagueOfLegends.TFT.Network
{
	[BoltGlobalBehaviour(BoltNetworkModes.Server)]
	public class ServerCallbacks : Bolt.GlobalEventListener
	{
		public override void Connected(BoltConnection connection)
		{
			base.Connected(connection);

			LogEvent log = LogEvent.Create();

			log.Message = $"{connection.RemoteEndPoint} connected";
			log.Send();
		}

		public override void Disconnected(BoltConnection connection)
		{
			base.Disconnected(connection);

			LogEvent log = LogEvent.Create();

			log.Message = $"{connection.RemoteEndPoint} disconnected";
			log.Send();
		}
	}
}