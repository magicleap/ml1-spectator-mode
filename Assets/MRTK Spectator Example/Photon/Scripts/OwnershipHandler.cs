using System;

#if CONTAINS_MRTK
using Microsoft.MixedReality.Toolkit.Input;
#endif

#if PHOTON
using Photon.Pun;
using Photon.Realtime;
#endif
using UnityEngine;

namespace MRTK.Tutorials.MultiUserCapabilities
{
#if PHOTON
    [RequireComponent(typeof(PhotonView), typeof(GenericNetSync))]
#endif

    public class OwnershipHandler :
#if PHOTON
        MonoBehaviourPun, IPunOwnershipCallbacks
#else
        MonoBehaviour
        #endif
#if CONTAINS_MRTK
        ,IMixedRealityInputHandler
#endif
    {

#if CONTAINS_MRTK

        public void OnInputDown(InputEventData eventData)
        {
            photonView.RequestOwnership();
        }

        public void OnInputUp(InputEventData eventData)
        {
        }
#endif
#if PHOTON

        public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
        {
            targetView.TransferOwnership(requestingPlayer);
        }

        public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
        {
        }

        public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
        {
        }

        private void TransferControl(Player idPlayer)
        {
            if (photonView.IsMine) photonView.TransferOwnership(idPlayer);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (photonView != null) photonView.RequestOwnership();
        }

        private void OnTriggerExit(Collider other)
        {
        }

        public void RequestOwnership()
        {
            photonView.RequestOwnership();
        }
#endif

    }
}
