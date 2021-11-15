using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private bool _log = true;

    [Tooltip("The Tag used to identify player objects")]
    [SerializeField] private string _playerTag = "Player";

    void Start()
    {
        NDICamera.gameObject.SetActive(CurrentTarget != null);

        //Debug Status
        string status = NDICamera.gameObject.activeInHierarchy ? "Active" : "Disabled";
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
    // Update is called once per frame
    void Update()
    {
        NDICamera.gameObject.SetActive(CurrentTarget != null);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
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
