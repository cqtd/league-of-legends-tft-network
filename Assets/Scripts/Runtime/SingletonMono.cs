﻿using UnityEngine;

	/// <summary>
	/// Normal Singleton Class
	/// </summary>
	public abstract class Singleton<T> where T : class, new()
	{
		public static T _inst;

		public static T Instance
		{
			get
			{
				if (_inst == null)
				{
					_inst = new T();
				}
				return _inst;
			}
		}

		public virtual void Initialize() { }
	}

public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
	/// <summary>
	/// Awake에서 DontDestroyOnLoad에 추가합니다.
	/// </summary>
	[SerializeField] bool optionDontDestroy = true;

	static T _inst;
	
	/// <summary>
	/// 싱글턴 인스턴스 접근
	/// </summary>
	public static T Instance {
		get
		{
			if (_inst == null)
			{
				_inst = FindObjectOfType(typeof(T)) as T;
				
				if (_inst == null)
				{
					GameObject container = new GameObject();
					_inst = container.AddComponent<T>();
					_inst.name = $"[{_inst.GetType().Name}]";
				}
			}

			return _inst;
		}
	}

	/// <summary>
	/// DontDestroyOnLoad를 호출해줌
	/// </summary>
	protected virtual void Awake()
	{
		if (_inst == null)
		{
			_inst = this as T;
		}

		if (optionDontDestroy)
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	#region Editor Fast Enter PlayMode

	protected virtual void OnApplicationQuit()
	{
		Release();
	}

	protected virtual void OnDestroy()
	{
		Release();
	}

	/// <summary>
	/// 도메인 리로드 없이 static 변수를 초기화합니다.
	/// </summary>
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	static void ReloadDomain()
	{
		Release();
	}

	static void Release()
	{
		_inst = null;
	}
	
	#endregion
}
