using MRTK.Tutorials.GettingStarted;
using UnityEngine;

#if PHOTON
using Photon.Pun;
#endif
namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class PunPlacementHintsController :
#if PHOTON
        MonoBehaviourPun
#else
    MonoBehaviour
#endif
    {
        private PlacementHintsController placementHintsController;
#if PHOTON

        private void Start()
        {
            // Cache references
            placementHintsController = GetComponent<PlacementHintsController>();

            // Subscribe to PunPlacementHintsController events
            placementHintsController.OnTogglePlacementHints += OnTogglePlacementHintsHandler;

            // Enable PUN feature
            placementHintsController.IsPunEnabled = true;
        }

        private void OnTogglePlacementHintsHandler()
        {
            photonView.RPC("PunRPC_TogglePlacementHints", RpcTarget.All);
        }

        [PunRPC]
        private void PunRPC_TogglePlacementHints()
        {
            placementHintsController.Toggle();
        }
#endif
    }
}
