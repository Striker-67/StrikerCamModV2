using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace strikercammod
{
    public class playermanager : MonoBehaviourPunCallbacks
    {
        public GameObject player;
        public GameObject cam;
        public List<GameObject> allplayer;
        private void Start()
        {
            mainmanager.Manager manager = GameObject.FindAnyObjectByType<mainmanager.Manager>();
            mainmanager.Manager camera = GameObject.FindAnyObjectByType<mainmanager.Manager>();
            allplayer = manager.players;
            cam = manager.cam;
            manager.players.Add(this.gameObject);
        }

    }
}

