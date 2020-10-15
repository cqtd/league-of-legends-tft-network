using System.Collections;
using TMPro;
using UdpKit;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public sealed class Bootstrap : Bolt.GlobalEventListener
	{
		[SerializeField] Button runServer = default;
		[SerializeField] Button runClient = default;

		[SerializeField] TMP_InputField ipAddressField = default;
		[SerializeField] TMP_InputField portField = default;

		[SerializeField] string sceneName = default;

		void Start()
		{
			runServer.onClick.AddListener(RunServerInternal);
			runClient.onClick.AddListener(RunClientInternal);
		}
		
		void RunServerInternal()
		{
			BoltLauncher.StartServer(GetUdpEndPoint());
			
			runServer.interactable = false;
			runClient.interactable = false;
		}

		void RunClientInternal()
		{
			BoltLauncher.StartClient();
			
			runServer.interactable = false;
			runClient.interactable = false;
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
			return UdpKit.UdpEndPoint.Parse($"{ipAddressField.text}:{portField.text}");
		}
	}
}