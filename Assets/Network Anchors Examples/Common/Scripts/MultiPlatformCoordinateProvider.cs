using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MultiPlatformCoordinateProvider : MonoBehaviour, IGenericCoordinateProvider
{
    [Header("Providers")]
    public MLGenericCoordinateProvider MagicLeapProvider;
    public StandaloneCoordinateProvider StandaloneProvider;
    [SerializeField]
    private bool _forceStandalone;

    public Task<List<GenericCoordinateReference>> RequestCoordinateReferences(bool refresh)
    {

#if PLATFORM_LUMIN
        if (_forceStandalone)
            return StandaloneProvider.RequestCoordinateReferences(refresh);

        return MagicLeapProvider.RequestCoordinateReferences(refresh);
#else
        return StandaloneProvider.RequestCoordinateReferences(refresh);
#endif
    }

    public void InitializeGenericCoordinates()
    {
#if PLATFORM_LUMIN
        if (_forceStandalone)
        {
            StandaloneProvider.InitializeGenericCoordinates();
            return;
        }

        MagicLeapProvider.InitializeGenericCoordinates();
#else
        StandaloneProvider.InitializeGenericCoordinates();
#endif
    }

    public void DisableGenericCoordinates()
    {
#if PLATFORM_LUMIN
        if (_forceStandalone)
        {
            StandaloneProvider.DisableGenericCoordinates();
            return;
        }

        MagicLeapProvider.DisableGenericCoordinates();
#else
        StandaloneProvider.DisableGenericCoordinates();
#endif
    }
}
