using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#if PLATFORM_LUMIN
using UnityEngine.XR.MagicLeap;
using UnityEngine.XR.Management;
//#endif

public class MagicLeapNetworkAnchorExample : MonoBehaviour
{
    public NetworkAnchorLocalizer NetworkAnchorLocalizer;
    private bool _didInit;

    // Start is called before the first frame update
    void Start()
    {
#if PLATFORM_LUMIN
        //Check if any XR managers are running. If one is, assume it's magic leap and listen to the bumper input.
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            _didInit = true;
            MLInput.OnControllerButtonDown += HandleOnButtonDown;
        }
#endif
    }


    /// <summary>
    /// Stop input api and unregister callbacks.
    /// </summary>
    void OnDestroy()
    {
#if PLATFORM_LUMIN
        if(_didInit)
            MLInput.OnControllerButtonDown -= HandleOnButtonDown;
#endif
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            NetworkAnchorLocalizer.CreateOrGetAnchor();
        }
    }
    ///
    /// <summary>
    /// Handles the event for button down.
    /// </summary>
    /// <param name="controller_id">The id of the controller.</param>
    /// <param name="button">The button that is being pressed.</param>
    private void HandleOnButtonDown(byte controllerId, MLInput.Controller.Button button)
    {

#if PLATFORM_LUMIN
        MLInput.Controller controller = MLInput.GetController(controllerId);

        if (controller != null && controller.Id == controllerId &&
            button == MLInput.Controller.Button.Bumper)
        {

            //Move the network anchor to our controller
            NetworkAnchorLocalizer.transform.position = controller.Position;
            NetworkAnchorLocalizer.transform.rotation = controller.Orientation;


            // Demonstrate haptics using callbacks.
            NetworkAnchorLocalizer.CreateOrGetAnchor();
            controller.StartFeedbackPatternVibe(MLInput.Controller.FeedbackPatternVibe.ForceDown, MLInput.Controller.FeedbackIntensity.Medium);
        }
#endif
    }

}
