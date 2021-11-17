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
    public void ToggleOcclusion(bool value)
    {
        if(ArOcclusionManager == null)
            return;

        if (value == true)
        {
            ArOcclusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Fastest;
        }
        else
        {
            ArOcclusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Disabled;

        }
    }
}
