#if PHOTON
using Photon.Pun;
#endif
using UnityEngine;

    public class SimplePhotonUser : MonoBehaviour
    {
#if PHOTON

        private PhotonView pv;
        private string username;

        private void Start()
        {
            pv = GetComponent<PhotonView>();

            if (!pv.IsMine) return;

            username = "User" + PhotonNetwork.NickName;
            pv.RPC("PunRPC_SetNickName", RpcTarget.AllBuffered, username);
        }

        [PunRPC]
        private void PunRPC_SetNickName(string nName)
        {
            gameObject.name = nName;
        }
      
#endif
    }
