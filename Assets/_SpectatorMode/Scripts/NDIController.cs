using System.Collections;
using System.Collections.Generic;
#if UNITY_STANDALONE || UNITY_EDITOR || PLATFORM_IOS
using Klak.Ndi;
#endif
using TMPro;
using UnityEngine;

/// <summary>
/// This script enables the NDI camera and moves it and the main camera to match the headset position of a remote player when connected to the network.
/// 
/// </summary>
public class NDIController : MonoBehaviour
{
    public Transform MainCamera;
    public Transform NDICamera;

    public Transform CurrentTarget { get; private set; }
    public int CurrentIndex { get; private set; }

    [SerializeField] private TMP_Text _statusText;

    [Tooltip("The Tag used to identify player objects")]
    [SerializeField] private string _playerTag = "Player";

#if UNITY_STANDALONE || UNITY_EDITOR || PLATFORM_IOS
    [Tooltip("The component used to stream the camera image to NDI")]
    [SerializeField] NdiResources _resources = null;
#endif

    [Tooltip("Should the Camera streaming start automatically when the game is launched?")]
    [SerializeField] private bool _streamOnStart = false;

    [SerializeField] private bool _log = true;

    private bool _isStreaming;

    void Awake()
    {
        //Disable the NDI Camera. It will only be enabled on Standalone Devices
        NDICamera.gameObject.SetActive(false);
    }

    IEnumerator Start()
    {
        LogStatus();

        //Wait for the main camera to load before initializing
        var waitForFrame = new WaitForEndOfFrame();
        while (Camera.main == null)
        {
            yield return waitForFrame;
        }

        yield return waitForFrame;

        if (_streamOnStart)
        {
            StartStreaming();
        }
      
    }

    public void StartStreaming()
    {
        //Only start the stream if we are not already.
        if(_isStreaming)
            return;

#if PLATFORM_IOS  // When Streaming from iOS, stream the camera image directly,

        var camera = Camera.main;
        NdiSender sender = camera.gameObject.AddComponent(typeof(NdiSender)) as NdiSender;
        sender.SetResources(_resources);
        sender.ndiName = "Spectator View " + SystemInfo.deviceUniqueIdentifier;
        sender.keepAlpha = false;
        sender.captureMethod = CaptureMethod.GameView;
        
        //Only supported in URP or HDRP
        //sender.captureMethod = CaptureMethod.Camera;
        // sender.sourceCamera = camera;


#elif (UNITY_STANDALONE || UNITY_EDITOR) // When Streaming from Standalone, stream from the NDI camera with alpha

        NDICamera.gameObject.SetActive(true);
        var camera = NDICamera.GetComponentInChildren<Camera>(true);
        NdiSender sender = camera.gameObject.AddComponent(typeof(NdiSender)) as NdiSender;
        sender.SetResources(_resources);
        sender.ndiName = "Spectator View " + SystemInfo.deviceUniqueIdentifier;
        sender.keepAlpha = true;
        sender.captureMethod = CaptureMethod.GameView;
        
        //Only supported in URP or HDRP
        //sender.captureMethod = CaptureMethod.Camera;
        // sender.sourceCamera = camera;
        camera.gameObject.SetActive(true);

#else
        Debug.LogWarning("Streaming is not supported on this device.");

#endif
        _isStreaming = true;
        LogStatus();

    }

    private void LogStatus()
    {
        //Debug Status
        string status = _isStreaming ? "Active" : "Disabled";
        var activationDebug = "The NDI camera is " + status;
        if (_statusText)
        {
            _statusText.text = activationDebug;
        }

        if (_log)
        {
            Debug.Log(activationDebug);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartStreaming();
            CyclePlayers();
        }

        if (CurrentTarget != null)
        {
            MainCamera.position = NDICamera.position = CurrentTarget.position;
            MainCamera.rotation = NDICamera.rotation = CurrentTarget.rotation;
        }

    }

    private void CyclePlayers()
    {
        var playerRigs = GameObject.FindGameObjectsWithTag(_playerTag);
        if (playerRigs.Length == 0)
            return;
        if (playerRigs.Length - 1 < CurrentIndex)
        {
            CurrentIndex = 0;
        }

        //Debug the status if this is our first target.
        if (CurrentTarget == null)
        {
            var activationStatus = "Activating NDI camera.";
            if (_statusText)
            {
                _statusText.text = activationStatus;
            }

            if (_log)
            {
                Debug.Log(activationStatus);
            }
        }

        CurrentTarget = playerRigs[CurrentIndex].transform;
        CurrentIndex++;

        //Debug the current target information 
        var targetDebug = "Setting current player to: " + CurrentTarget.gameObject.name + " at index " + (CurrentIndex-1);
        if (_statusText)
        {
            _statusText.text = targetDebug;
        }

        if (_log)
        {
            Debug.Log(targetDebug);
        }


    }

}
