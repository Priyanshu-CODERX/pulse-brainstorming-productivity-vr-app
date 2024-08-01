using Photon.Pun;
using UnityEngine;

public class ColorManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color[] _colors;

    private int currentColorIndex = 0;
    
    private void Start()
    {
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            UpdateColor(currentColorIndex);
        }
    }

    public void UpdateColor(int index)
    {
        if (index >= 0 && index < _colors.Length)
        {
            currentColorIndex = index;
            _renderer.material.color = _colors[index];

            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetColor("_EmissionColor", _colors[index]);

            _renderer.SetPropertyBlock(propertyBlock);

            if (PhotonNetwork.IsConnected && photonView.IsMine)
            {
                photonView.RPC("RPC_UpdateColorOverNetwork", RpcTarget.OthersBuffered, index);
            }
        }
        else
        {
            Debug.LogWarning("Index out of range");
        }
    }

    [PunRPC]
    private void RPC_UpdateColorOverNetwork(int index)
    {
        UpdateColor(index);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentColorIndex);
        }
        else
        {
            currentColorIndex = (int)stream.ReceiveNext();
            UpdateColor(currentColorIndex);
        }
        
    }
}
