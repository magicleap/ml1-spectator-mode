using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script enables the NDI camera and moves it and the main camera to match the headset position of a remote player when connected to the network.
/// 
/// </summary>
public class NDIController : MonoBehaviour
{
    public Transform MainCamera;
    public Transform NDICamera;

    private Transform _currentTarget;
    private int _currentIndex;

    // Update is called once per frame
    void Update()
    {
        NDICamera.gameObject.SetActive(_currentTarget != null);

        if (NetworkAnchorService.Instance && NetworkAnchorService.Instance.IsConnected)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CyclePlayers();
            }

            if (_currentTarget != null)
            {
                MainCamera.position = NDICamera.position = _currentTarget.position;
                MainCamera.rotation = NDICamera.rotation = _currentTarget.rotation;
            }
        }

    }

    private void CyclePlayers()
    {
#if PHOTON
        var playerRigs = FindObjectsOfType<SimplePhotonUser>();
        if (playerRigs.Length == 0)
            return;
        if (playerRigs.Length - 1 < _currentIndex)
        {
            _currentIndex = 0;
        }

        _currentTarget = playerRigs[_currentIndex].transform;
        _currentIndex++;
#endif

    }

}
