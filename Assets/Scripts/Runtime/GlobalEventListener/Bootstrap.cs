using System;
using UdpKit;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	using UI;
	
	public sealed class Bootstrap : Bolt.GlobalEventListener
	{
		[SerializeField] string sceneName = default;

		[NonSerialized] 
		public string ipAddress = default;
		[NonSerialized] 
		public string port = default;

		void Start()
		{
			UIManager.Instance.Open<BootstrapCanvas>(out BootstrapCanvas bsCanvas);
			bsCanvas.Initialize(this);
		}
		
		public void RunServer()
		{
			BoltLauncher.StartServer(GetUdpEndPoint());
		}

		public void RunClient()
		{
			BoltLauncher.StartClient();
		}

		public override void BoltStartDone()
		{
			base.BoltStartDone();

			if (BoltNetwork.IsServer)
			{
				BoltNetwork.LoadScene(sceneName);
			}
			else
			{
				BoltNetwork.Connect(GetUdpEndPoint().Port);
			}
		}

		UdpEndPoint GetUdpEndPoint()
		{
			return UdpKit.UdpEndPoint.Parse($"{ipAddress}:{port}");
		}
	}
}