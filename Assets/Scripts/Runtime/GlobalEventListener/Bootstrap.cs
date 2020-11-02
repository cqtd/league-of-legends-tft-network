using System;
using CQ.UI;
using UdpKit;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CQ.LeagueOfLegends.TFT.Network
{
	using UI;
	
	public enum EEditorMode {
	    Bootstrap,
	    TFT_Test,
	}
	
	public sealed class Bootstrap : Bolt.GlobalEventListener
	{
		[Header("Editor Config")] [SerializeField]
		EEditorMode mode = EEditorMode.Bootstrap;
		
		[Header("Bootstrap")]
		[SerializeField] string sceneName = default;

		[NonSerialized] 
		public string ipAddress = default;
		[NonSerialized] 
		public string port = default;

		void Start()
		{
			if (mode == EEditorMode.Bootstrap)
			{
				BootstrapCanvas bsCanvas = UIManager.Instance.Open<BootstrapCanvas>();
				bsCanvas.Initialize(this);
			}
			
			else if (mode == EEditorMode.TFT_Test)
			{
				SceneManager.LoadScene("Scenes/TFT");
			}
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