using MRTK.Tutorials.GettingStarted;

#if PHOTON
using Photon.Pun;
#else
using UnityEngine;
#endif

namespace MRTK.Tutorials.MultiUserCapabilities
{
    /// <summary>
    ///     Handles PUN RPC for ExplodeViewController.
    /// </summary>
    public class PunExplodeViewController :
#if PHOTON
        MonoBehaviourPun
#else
    MonoBehaviour
#endif
    {
        private ExplodeViewController explodeViewController;
#if PHOTON
        private void Start()
        {
            // Cache references
            explodeViewController = GetComponent<ExplodeViewController>();

            // Subscribe to ExplodeViewController events
            explodeViewController.OnToggleExplodedView += OnToggleExplodedViewHandler;

            // Enable PUN feature
            explodeViewController.IsPunEnabled = true;
        }
        private void OnToggleExplodedViewHandler()
        {
            photonView.RPC("PunRPC_ToggleExplodedView", RpcTarget.All);
        }

        [PunRPC]
        private void PunRPC_ToggleExplodedView()
        {
            explodeViewController.Toggle();
        }
#endif
    }
}
