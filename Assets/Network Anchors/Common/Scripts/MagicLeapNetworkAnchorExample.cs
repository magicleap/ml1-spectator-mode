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
        MLInput.OnControllerButtonDown += HandleOnButtonDown;
#endif
    }


    /// <summary>
    /// Stop input api and unregister callbacks.
    /// </summary>
    void OnDestroy()
    {
#if PLATFORM_LUMIN
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
            // Demonstrate haptics using callbacks.
            NetworkAnchorLocalizer.CreateOrGetAnchor();
            controller.StartFeedbackPatternVibe(MLInput.Controller.FeedbackPatternVibe.ForceDown, MLInput.Controller.FeedbackIntensity.Medium);
        }
#endif
    }

}
