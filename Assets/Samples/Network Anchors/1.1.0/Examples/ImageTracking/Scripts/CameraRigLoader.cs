using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CameraRigLoader : MonoBehaviour
{
    public string MagicLeapRigSceneName;
    public string ARFoundationRigSceneName;

    public UnityEvent OnSceneLoaded;

    void Start()
    {
        SceneManager.sceneLoaded+= SceneManagerOnsceneLoaded;
#if PLATFORM_LUMIN || PLATFORM_STANDALONE
        SceneManager.LoadScene(MagicLeapRigSceneName, LoadSceneMode.Additive);
#elif PLATFORM_ANDROID || PLATFORM_IOS
        SceneManager.LoadScene(ARFoundationRigSceneName, LoadSceneMode.Additive);
#endif
    }

    private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == MagicLeapRigSceneName || arg0.name == ARFoundationRigSceneName)
        {
            OnSceneLoaded.Invoke();
        }
    }
}
