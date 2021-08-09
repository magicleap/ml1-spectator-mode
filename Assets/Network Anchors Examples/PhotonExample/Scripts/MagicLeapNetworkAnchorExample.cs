using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class MagicLeapNetworkAnchorExample : MonoBehaviour
{
    public NetworkAnchorLocalizer NetworkAnchorLocalizer;

    // Start is called before the first frame update
    void Start()
    {
#if PLATFORM_LUMIN
        MLInput.OnTriggerDown += HandleOnTriggerDown;
#endif
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            NetworkAnchorLocalizer.CreateOrGetAnchor();
        }
    }

    /// <summary>
    /// Handles the event for trigger down.
    /// </summary>
    /// <param name="controller_id">The id of the controller.</param>
    /// <param name="value">The value of the trigger button.</param>
    private void HandleOnTriggerDown(byte controllerId, float value)
    {
#if PLATFORM_LUMIN
        NetworkAnchorLocalizer.CreateOrGetAnchor();
#endif
    }
}
