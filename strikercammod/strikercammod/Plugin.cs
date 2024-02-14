
using Cinemachine;
using DevHoldableEngine;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Utilla;
using BepInEx.Configuration;
using BepInEx;
using strikercammod.mainmanager;
using strikercammod.info;
using GorillaNetworking;
using Photon.Pun;
using System.Collections;
using UnityEngine.UI;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine.Animations.Rigging;
using GorillaExtensions;
using UnityEngine.XR.Interaction.Toolkit;

namespace strikercammod
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(main.GUID, main.Name, main.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        bool isenabled = true;
        public GameObject vrrigs;
        GameObject Camera;
        GameObject camscreen;
        GameObject PCSCREEN;
        public List<GameObject> player;
        GameObject ThirdPersonCamera;



        void OnEnable()
        {
            isenabled = true;
            redo();
            Debug.Log(isenabled);


        }
        void OnDisable()
        {
            isenabled = false;
            undosetup();
            Debug.Log(isenabled);
        }

        void Start()
        {


            Utilla.Events.GameInitialized += OnGameInitialized;
        }


        void OnGameInitialized(object sender, EventArgs e)
        {
            ThirdPersonCamera = GorillaTagger.Instance.thirdPersonCamera;
            PCSCREEN = GorillaTagger.Instance.thirdPersonCamera.transform.Find("Shoulder Camera").gameObject;
            Debug.Log("setting stuff up!");
            var bundle = LoadAssetBundle("strikercammod.Reasoure.cammod");
            Camera = bundle.LoadAsset<GameObject>("cammod");
            Camera = Instantiate(Camera);
            ThirdPersonCamera.GetComponentInChildren<CinemachineBrain>().enabled = false;
            ThirdPersonCamera.transform.parent = Camera.transform;
            Destroy(Camera.transform.Find("Model/Camera").gameObject.GetComponent<AudioListener>());
            PCSCREEN.transform.localPosition = new Vector3(68.0681f, -12.1543f, 80.8426f);
            PCSCREEN.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
           
            Camera.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            Camera.transform.position = new Vector3(-65.0436f, 11.8873f, -84.3991f);
            camscreen = Camera.transform.Find("Model/Camera").gameObject;
            if (!isenabled)
            {
                undosetup();
            }
            else
            {
                redo();
            }

            Camera.AddComponent<DevHoldable>();
            Camera.AddComponent<Manager>().cam = Camera;
            Camera.GetComponent<Manager>().PCSCREEN = PCSCREEN;
            Camera.GetComponent<Manager>().CAMSCREEN = camscreen;
            if (Camera.GetComponent<Manager>() != null)
            {
                Debug.Log("added manager");
            }

           
        }
        public void Update()
        {
            if (!Camera.transform.parent.gameObject.activeSelf)
            {
                Camera.transform.position = GorillaTagger.Instance.headCollider.transform.position;
                camscreen.transform.localScale = new Vector3(1f, 1f, .1f);
                Camera.transform.parent = null;
            }
        }


        void undosetup()
        {
            Debug.Log("disabled");
            ThirdPersonCamera.GetComponentInChildren<CinemachineBrain>().enabled = true;
            Camera.SetActive(false);

        }
        void redo()
        {
            Debug.Log("enabled");
            ThirdPersonCamera.GetComponentInChildren<CinemachineBrain>().enabled = false;
            Camera.SetActive(true);

        }


        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {

            Debug.Log("JOINED");
            FindAnyObjectByType<Manager>().addplayers();
            FindAnyObjectByType<Manager>().inmoddedroom = true;
            

        }
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            FindAnyObjectByType<Manager>().Clear();
            Camera.transform.parent = null;
            FindAnyObjectByType<Manager>().inmoddedroom = false;
        }


        // below is all code made by kyle the scientist




    }
}


