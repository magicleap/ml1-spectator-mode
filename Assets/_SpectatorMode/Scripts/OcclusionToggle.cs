using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Simple script that toggles occlusion via the AR Foundation AROcclusionManager
/// </summary>
public class OcclusionToggle : MonoBehaviour
{

    public AROcclusionManager ArOcclusionManager;

    private EnvironmentDepthMode _initialDepthMode = EnvironmentDepthMode.Fastest;

    void Awake()
    {
        if (ArOcclusionManager == null || ArOcclusionManager.requestedEnvironmentDepthMode == EnvironmentDepthMode.Disabled)
            return;

        _initialDepthMode = ArOcclusionManager.requestedEnvironmentDepthMode;
    }

    public void ToggleOcclusion(bool value)
    {
        if(ArOcclusionManager == null)
            return;

        if (value == true)
        {
            ArOcclusionManager.requestedEnvironmentDepthMode = _initialDepthMode;
        }
        else
        {
            ArOcclusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Disabled;

        }
    }
}
