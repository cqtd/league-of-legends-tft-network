﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CQ.LeagueOfLegends.TFT.Network.UI
{
	public class BootstrapCanvas : UICanvas<Bootstrap>
	{
		[Header("Bootstrap")]
		
		[SerializeField] Button runServer = default;
		[SerializeField] Button runClient = default;

		[SerializeField] TMP_InputField ipAddressField = default;
		[SerializeField] TMP_InputField portField = default;

		public override void Initialize(Bootstrap entity)
		{
			base.Initialize(entity);
			
			runServer.onClick.AddListener(RunServerInternal);
			runClient.onClick.AddListener(RunClientInternal);
			
			ipAddressField.onValueChanged.AddListener(OnIpAddressChanged);
			ipAddressField.onEndEdit.AddListener(OnIpAddressChanged);
			
			portField.onValueChanged.AddListener(OnPortChanged);
			portField.onEndEdit.AddListener(OnPortChanged);
			
			OnIpAddressChanged(ipAddressField.text);
			OnPortChanged(portField.text);
		}
		
		void RunServerInternal()
		{
			Entity.RunServer();
			
			runServer.interactable = false;
			runClient.interactable = false;
			
			runServer.onClick.RemoveAllListeners();
			runClient.onClick.RemoveAllListeners();
		}

		void RunClientInternal()
		{
			Entity.RunClient();
			
			runServer.interactable = false;
			runClient.interactable = false;
			
			runServer.onClick.RemoveAllListeners();
			runClient.onClick.RemoveAllListeners();
		}

		void OnIpAddressChanged(string value)
		{
			Entity.ipAddress = value;
		}
		
		void OnPortChanged(string value)
		{
			Entity.port = value;
		}
	}
}