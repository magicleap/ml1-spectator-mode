using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAnchor : MonoBehaviour
{
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    public void CreateAnchorSimple()
    {
        //NetworkAnchorService.Instance.RequestCreateNetworkAnchor("origin", Vector3.zero, Quaternion.identity);
        var networkAnchor = FindObjectOfType<NetworkAnchorLocalizer>();

        networkAnchor.CreateOrGetAnchor();
    }
}
