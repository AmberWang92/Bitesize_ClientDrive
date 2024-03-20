using System;
using System.ComponentModel;
using Unity.Netcode;
using UnityEngine;

public class NetworkRigidbodyEnabler : NetworkBehaviour
{
	public event Action ingredientDespawned;

	public override void OnNetworkSpawn()
	{
		base.OnNetworkSpawn();
		if (!IsServer)
		{
			enabled = false;
			return;
		}
	}

	public override void OnNetworkDespawn()
	{
		ingredientDespawned?.Invoke();
	}
}
