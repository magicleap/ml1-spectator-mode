using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



/// <summary>
/// Enable or disable an object based on the platform
/// </summary>
public class PlatformDependentToggler : MonoBehaviour
{

    [System.Flags]
    public enum Platforms
    {
        Android = 0x1,
        IOS = 0x2,
        Lumin = 0x4,
        Standalone = 0x8,
        Editor = 0x16
    }

    public Platforms EnabledPlatforms;

    public GameObject TargetObject;
    // Start is called before the first frame update
    void Start()
    {
#if PLATFORM_IOS
        TargetObject.SetActive(EnabledPlatforms.HasFlag(Platforms.IOS));
#elif PLATFORM_ANDROID
        TargetObject.SetActive(EnabledPlatforms.HasFlag(Platforms.Android));
#elif PLATFORM_LUMIN
        TargetObject.SetActive(EnabledPlatforms.HasFlag(Platforms.Lumin));
#else
        TargetObject.SetActive(EnabledPlatforms.HasFlag(Platforms.Standalone));
#endif

#if UNITY_EDITOR
        TargetObject.SetActive(EnabledPlatforms.HasFlag(Platforms.Editor));
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
