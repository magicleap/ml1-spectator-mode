using UnityEngine;
#if PLATFORM_LUMIN
using UnityEngine.XR.MagicLeap;
#endif
/// <summary>
/// Example script to trigger Network Anchor Service events on the Magic Leap
/// </summary>
public class MagicLeapNetworkAnchorExample : MonoBehaviour
{
    private NetworkAnchorLocalizer NetworkAnchorLocalizer;

    // Start is called before the first frame update
    void Start()
    {
#if PLATFORM_LUMIN
        MLInput.OnControllerButtonDown += HandleOnButtonDown;
        MLInput.OnTriggerDown += MLInput_OnTriggerDown;
#endif
    }

    private void MLInput_OnTriggerDown(byte controllerId, float triggerValue)
    {
#if PLATFORM_LUMIN
        var coordinateProvider = FindObjectOfType<MLGenericCoordinateProvider>();
        if (coordinateProvider)
        {
            coordinateProvider.SearchForImage();
           var coordinate = coordinateProvider.GetImageCoordinateReference();
           if (coordinate !=null)
           {
               NetworkAnchorLocalizer.transform.position = coordinate.Position;
               NetworkAnchorLocalizer.transform.rotation = coordinate.Rotation;
           }
        }
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

    private bool CanSendInputEvents()
    {
        if (NetworkAnchorLocalizer == null)
        {
            NetworkAnchorLocalizer = FindObjectOfType<NetworkAnchorLocalizer>();
            if (NetworkAnchorLocalizer == null)
            {
                Debug.LogError("Cound not find NetworkAnchorLocalizer in the scene. Input events will not work");
                return false;
            }
        }

        return true;
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanSendInputEvents() == false)
            {
                return;
            }
            NetworkAnchorLocalizer.CreateOrGetAnchor();
        }
    }

#if PLATFORM_LUMIN
    /// <summary>
    /// Handles the event for button down.
    /// </summary>
    /// <param name="controller_id">The id of the controller.</param>
    /// <param name="button">The button that is being pressed.</param>
    private void HandleOnButtonDown(byte controllerId, MLInput.Controller.Button button)
    {
        if (CanSendInputEvents() == false)
        {
            return;
        }

        MLInput.Controller controller = MLInput.GetController(controllerId);

        if (controller != null && controller.Id == controllerId &&
            button == MLInput.Controller.Button.Bumper)
        {
            // Demonstrate haptics using callbacks.
            NetworkAnchorLocalizer.CreateOrGetAnchor();
            controller.StartFeedbackPatternVibe(MLInput.Controller.FeedbackPatternVibe.ForceDown, MLInput.Controller.FeedbackIntensity.Medium);
        }

    }
#endif

}
