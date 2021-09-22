#if PHOTON
using Photon.Pun;
#endif
using UnityEngine;

/// <summary>
/// Using a PhotonStreamQueue sends the attached object's local position and rotation. The PhotonStreamQueue
/// is used to control the sample rate of player and non player objects.
/// </summary>
public class SimpleNetSync :
#if PHOTON
    MonoBehaviourPun, IPunObservable
#else
    MonoBehaviour
#endif
{
#pragma warning disable 414
    [SerializeField] private bool isUser = default;
#pragma warning restore 414

#if PHOTON


    private Camera mainCamera;

    private Vector3 networkLocalPosition;
    private Quaternion networkLocalRotation;

    private Vector3 startingLocalPosition;
    private Quaternion startingLocalRotation;

    private PhotonStreamQueue m_StreamQueue;

    void Awake()
    {
        if (isUser)
        {
            this.m_StreamQueue = new PhotonStreamQueue(480);
        }
        else
        {
            this.m_StreamQueue = new PhotonStreamQueue(120);
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            this.m_StreamQueue.Serialize(stream);
        }
        else
        {
            this.m_StreamQueue.Deserialize(stream);
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;

        if (isUser)
        {
            transform.parent = FindObjectOfType<NetworkAnchorLocalizer>().transform;
        }

        var trans = transform;
        startingLocalPosition = trans.localPosition;
        startingLocalRotation = trans.localRotation;

        networkLocalPosition = startingLocalPosition;
        networkLocalRotation = startingLocalRotation;
    }

    // private void FixedUpdate()
    private void Update()
    {
        if (PhotonNetwork.InRoom == false || PhotonNetwork.CurrentRoom.PlayerCount <= 1)
        {
            this.m_StreamQueue.Reset();
        }

        if (!photonView.IsMine)
        {
            if (this.m_StreamQueue.HasQueuedObjects() == false)
            {
                return;
            }

            networkLocalPosition = (Vector3) m_StreamQueue.ReceiveNext();
            networkLocalRotation = (Quaternion) m_StreamQueue.ReceiveNext();

            var trans = transform;
            trans.localPosition = networkLocalPosition;
            trans.localRotation = networkLocalRotation;
        }

        if (photonView.IsMine && isUser)
        {
            var trans = transform;
            var mainCameraTransform = mainCamera.transform;
            trans.position = mainCameraTransform.position;
            trans.rotation = mainCameraTransform.rotation;

            this.m_StreamQueue.SendNext(transform.localPosition);
            this.m_StreamQueue.SendNext(transform.localRotation);
        }
    }
#endif
}