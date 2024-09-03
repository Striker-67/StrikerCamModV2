using GorillaGameModes;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GorillaCars
{
    public class Moddedcheck : MonoBehaviourPunCallbacks
    {
        public static Moddedcheck modcheck;
        public object gameMode;
        public static int layer = 29, layerMask = 1 << layer;
        private LayerMask baseMask;
        public void Start()
        {
            modcheck = this;
            
        }
        public bool IsModded()
        {
                return PhotonNetwork.InRoom && gameMode.ToString().Contains("MODDED");
        }

        public override void OnJoinedRoom()
        {

            PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("gameMode", out gameMode);
        }
    }
}
