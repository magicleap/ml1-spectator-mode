#if PHOTON
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
#endif
using UnityEngine;

/// <summary>
/// Interfaces with Photon and transmits the information to the Network Anchor Service. Also starts the service.
/// </summary>
public class PhotonNetworkAnchorController : MonoBehaviour
{
    public NetworkAnchorService NetworkAnchorService;

    public MultiPlatformCoordinateProvider MultiPlatformCoordinateProvider;

#if PHOTON
    void Awake()
    {
        PhotonNetwork.NetworkingClient.LoadBalancingPeer.ReuseEventInstance = true;
        NetworkAnchorService.OnBroadcastNetworkEvent += SendNetworkAnchorEvent;
#if UNITY_STANDALONE
        //To help with syncing
        Application.targetFrameRate = 30;
#endif
    }

    void Reset()
    {
        if (NetworkAnchorService == null)
        {
            NetworkAnchorService = FindObjectOfType<NetworkAnchorService>();
        }
        if (MultiPlatformCoordinateProvider == null)
        {
            MultiPlatformCoordinateProvider = FindObjectOfType<MultiPlatformCoordinateProvider>();
        }
    }

    public IEnumerator Start()
    {
        while (PhotonNetwork.IsConnectedAndReady == false || PhotonNetwork.InRoom == false)
        {
            yield return null;
        }
    
        NetworkAnchorService.StartService(PhotonNetwork.LocalPlayer.ActorNumber.ToString(), MultiPlatformCoordinateProvider);

    }

    // These functions are Photon specific for now but could be linked to any other network layer.
    //Listen to Photon Events
    void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += CaptureEvent;
    }
    // Remove Photon Events Listener
    void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= CaptureEvent;
    }
    //Catch photon events and pass it into the Network Anchor's generic network event listener
    private void CaptureEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
            NetworkAnchorService.ProcessNetworkEvents(eventCode, photonEvent.CustomData);
    }

    private void SendNetworkAnchorEvent(byte networkEventCode, string jsonData, int[] players)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        if (players.Length > 0)
        {
            if (players[0] == -1)
            {
                raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };
            }
            else if (players.Length > 0)
            {
                raiseEventOptions = new RaiseEventOptions { TargetActors = players };
            }
        }

        PhotonNetwork.RaiseEvent(networkEventCode, jsonData, raiseEventOptions, SendOptions.SendReliable);
    }
#endif

}
