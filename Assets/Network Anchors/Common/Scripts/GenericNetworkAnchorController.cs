using System.Collections;
using UnityEngine;

/// <summary>
/// Script used as a demonstration of on how a generic network service can be added. 
/// </summary>
public class GenericNetworkAnchorController : MonoBehaviour
{
    public NetworkAnchorService NetworkAnchorService;

    public MultiPlatformCoordinateProvider MultiPlatformCoordinateProvider;

#if PHOTON
    private void Awake()
    {
        //Listen to the NetworkAnchorService's network events so that we can relay them to our service.
        NetworkAnchorService.OnBroadcastNetworkEvent += SendNetworkAnchorEvent;

        //Listen to our network service events (RPC/Events) so we can relay them to our NetworkAnchorService if needed.
        //example:
        //YourNetworkManager.OnNetworkEventReceived += CaptureEvent
    }

    private void OnValidate()
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
        //Wait until you are connected to your network service and in a room
        //example:
        //while (PhotonNetwork.IsConnectedAndReady == false || PhotonNetwork.InRoom == false)
        //{
        //    yield return null;
        //}
        //Wait 3 seconds in case something needs to initialize

        yield return new WaitForSeconds(3);

        int uniquePlayerId = 2124314;
        yield return NetworkAnchorService.RequestConnectToService(uniquePlayerId, MultiPlatformCoordinateProvider);

        //Wait 3 seconds in case something needs to initialize
        yield return new WaitForSeconds(3);

        if (NetworkAnchorService.IsConnected == false)
        {
            Debug.LogWarning("Could not connect, retrying...");

            StartCoroutine(Start());
        }
    }


    // Remove Network Events Listener and disconnect from the service
    private void OnDisable()
    {
        //This will only get called if the user quits using an in game button. Otherwise the host needs to disconnect them.
        NetworkAnchorService.DisconnectFromService();
        //YourNetworkManager.OnNetworkEventReceived += CaptureEvent
    }


    //Catch the network events and pass them to the NetworkAnchorService
    // private void CaptureEvent(EventData event)
    // {
    //         In this case, this is the code that the network anchor service appended.
    //         byte eventCode = event.Code;
    //
    //         NetworkAnchorService.ProcessNetworkEvents(eventCode, photonEvent.CustomData);
    // }


    /// <summary>
    /// We catch events from the network anchor service and relay them to our network provider.
    /// </summary>
    /// <param name="networkEventCode">The code that corresponds with the network message</param>
    /// <param name="jsonData">The messages data as a json string</param>
    /// <param name="players">the target player/players</param>
    private void SendNetworkAnchorEvent(byte networkEventCode, string jsonData, int[] players)
    {
        //Initialize our actual network message options. By default we assume that we will broadcast to all players. 
        //ex:
       // RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
       //If the player length is zero we assume that the message is going to everyone.
        if (players.Length > 0)
        {
            if (players[0] == (int)NetworkAnchorService.SendCode.MASTER_CLIENT)
            {
                //Set the message options to target the master client only.
                //ex:
                //raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };
            }
            else if (players[0] == (int)NetworkAnchorService.SendCode.OTHERS)
            {
                //Set the message options to target all other players.
                //ex:
                //raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
            }
            else if (players[0] == (int)NetworkAnchorService.SendCode.ALL)
            {
                //Set the message options to target all players.
                //ex:
                //raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            }
            else if (players[0] >= 0)
            {
                //Set the message options to target the players with specified ids.
                //These Ids match the Ids that are used to connect to the service
                //ex:
                //raiseEventOptions = new RaiseEventOptions { TargetActors = players };
            }
        }

        //Send the message to the target player/players.
        //ex:
        //PhotonNetwork.RaiseEvent(networkEventCode, jsonData, raiseEventOptions, SendOptions.SendReliable);
    }

    // /// <summary>
    // /// Removes players who have been disconnected from the server. Needed if a user force quits or suspends the app.
    // /// </summary>
    // /// <param name="otherPlayer">The player that left</param>
    // public void OnPlayerLeftRoom(Player otherPlayer)
    // {
    //     if (PhotonNetwork.IsMasterClient)
    //         NetworkAnchorService.DisconnectFromService(otherPlayer.ActorNumber);
    // }

#endif
}
