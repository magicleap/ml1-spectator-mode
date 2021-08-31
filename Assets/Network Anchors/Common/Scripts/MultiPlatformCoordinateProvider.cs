using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.Management;

/// <summary>
/// Provides the anchors/coordinates for each system. TODO:Create a more robust multi-platform coordinate manager.
/// </summary>
public class MultiPlatformCoordinateProvider : MonoBehaviour, IGenericCoordinateProvider
{
    [Header("Providers")]
    public MLGenericCoordinateProvider MagicLeapProvider;
    public StandaloneCoordinateProvider StandaloneProvider;
    
    private bool _forceStandalone;


    void Start()
    {
        //Check if any XR managers are running. If not force standalone.
        if (!XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            _forceStandalone = true;
            Debug.Log("No XR device has been detected. Starting in standalone.");
        }
    }
    public Task<List<GenericCoordinateReference>> RequestCoordinateReferences(bool refresh)
    {
        if (_forceStandalone)
        {
            return StandaloneProvider.RequestCoordinateReferences(refresh);
        }

#if PLATFORM_LUMIN
        return MagicLeapProvider.RequestCoordinateReferences(refresh);
#else
        return StandaloneProvider.RequestCoordinateReferences(refresh);
#endif
    }

    public void InitializeGenericCoordinates()
    {
        if (_forceStandalone)
        {
            StandaloneProvider.InitializeGenericCoordinates();
            return;
        }

#if PLATFORM_LUMIN
        MagicLeapProvider.InitializeGenericCoordinates();
#else
        StandaloneProvider.InitializeGenericCoordinates();
#endif
    }

    public void DisableGenericCoordinates()
    {
        if (_forceStandalone)
        {
            StandaloneProvider.DisableGenericCoordinates();
            return;
        }

#if PLATFORM_LUMIN
        MagicLeapProvider.DisableGenericCoordinates();
#else
        StandaloneProvider.DisableGenericCoordinates();
#endif
    }
}
