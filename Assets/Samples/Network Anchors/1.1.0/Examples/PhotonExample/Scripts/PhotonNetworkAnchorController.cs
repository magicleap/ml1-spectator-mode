#if PHOTON
using System.Collections;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

#endif
using UnityEngine;

/// <summary>
/// Interfaces with Photon and transmits the information to the Network Anchor Service. Also starts the service.
/// </summary>
public class PhotonNetworkAnchorController : MonoBehaviour
#if PHOTON
    , IInRoomCallbacks
#endif
{
    public NetworkAnchorService NetworkAnchorService;

    //Gets the coordinates for the target platform
    private MultiPlatformCoordinateProvider _multiPlatformCoordinateProvider;

#if PHOTON
    private void Awake()
    {
        PhotonNetwork.NetworkingClient.LoadBalancingPeer.ReuseEventInstance = true;
        PhotonNetwork.AddCallbackTarget(this);
        PhotonNetwork.AutomaticallySyncScene = false;

        if (NetworkAnchorService == null)
        {
            NetworkAnchorService = FindObjectOfType<NetworkAnchorService>();
        }
        NetworkAnchorService.OnBroadcastNetworkEvent += SendNetworkAnchorEvent;
    }

    private void OnValidate()
    {
        if (NetworkAnchorService == null)
        {
            NetworkAnchorService = FindObjectOfType<NetworkAnchorService>();
        }
        
    }

    public IEnumerator Start()
    {
        while (PhotonNetwork.IsConnectedAndReady == false || PhotonNetwork.InRoom == false)
        {
            yield return null;
        }

        yield return new WaitForSeconds(3);

        if (_multiPlatformCoordinateProvider == null)
        {
            _multiPlatformCoordinateProvider = FindObjectOfType<MultiPlatformCoordinateProvider>();
        }
        yield return NetworkAnchorService.RequestConnectToService(PhotonNetwork.LocalPlayer.ActorNumber, _multiPlatformCoordinateProvider);

        yield return new WaitForSeconds(3);

        if (NetworkAnchorService.IsConnected == false)
        {
            Debug.LogWarning("Could not connect, retrying...");

            StartCoroutine(Start());
        }
    }

    // These functions are Photon specific for now but could be linked to any other network layer.
    //Listen to Photon Events
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += CaptureEvent;
    }
    // Remove Photon Events Listener
    private void OnDisable()
    {
        NetworkAnchorService.DisconnectFromService();
        PhotonNetwork.NetworkingClient.EventReceived -= CaptureEvent;
    }

    
    //Catch photon events and pass it into the Network Anchor's generic network event listener
    private void CaptureEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if(eventCode<200) 
            NetworkAnchorService.ProcessNetworkEvents(eventCode, photonEvent.CustomData);
    }

    private void SendNetworkAnchorEvent(byte networkEventCode, string jsonData, int[] players)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };

        if (players.Length > 0)
        {
            if (players[0] == (int)NetworkAnchorService.SendCode.MASTER_CLIENT)
            {
                raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };
            }
            else if (players[0] == (int)NetworkAnchorService.SendCode.OTHERS)
            {
                raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
            }
            else if (players[0] == (int)NetworkAnchorService.SendCode.ALL)
            {
                raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All};
            }
            else if (players[0] >= 0)
            {
                raiseEventOptions = new RaiseEventOptions { TargetActors = players };
            }
        }

        PhotonNetwork.RaiseEvent(networkEventCode, jsonData, raiseEventOptions, SendOptions.SendReliable);
    }

    /// <summary>
    /// Removes players who have been disconnected from the server
    /// </summary>
    /// <param name="otherPlayer"></param>
    public void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
            NetworkAnchorService.DisconnectFromService(otherPlayer.ActorNumber);
    }

    #region NotImplemented

    public void OnPlayerEnteredRoom(Player newPlayer)
    {
    }

    public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
    }

    public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
    }

    public void OnMasterClientSwitched(Player newMasterClient)
    {
    }

    #endregion

#endif

}
