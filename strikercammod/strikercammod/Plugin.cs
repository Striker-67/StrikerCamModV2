
using Cinemachine;
using DevHoldableEngine;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

using BepInEx;
using strikercammod.mainmanager;
using strikercammod.info;

using System.Collections.Generic;
<<<<<<< Updated upstream
using GorillaNetworking;
using Photon.Pun;
=======
using strikercammod.buttons;
using TMPro;
>>>>>>> Stashed changes

namespace strikercammod
{
    [BepInPlugin(main.GUID, main.Name, main.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool isenabled = true;
        public GameObject vrrigs;
        public static GameObject cam;
        GameObject PCSCREEN;
        public List<GameObject> player;
        GameObject ThirdPersonCamera;
<<<<<<< Updated upstream
        GorillaScoreBoard ScoreBoard;
        static bool inmoddedroom;

=======
>>>>>>> Stashed changes
        void OnEnable()
        {
            isenabled = true;
            Debug.Log(isenabled);
        }
        void OnDisable()
        {
            isenabled = false;
            Debug.Log(isenabled);
        }

        void Start()
        {
<<<<<<< Updated upstream
           
=======
>>>>>>> Stashed changes
            GorillaTagger.OnPlayerSpawned(spawed);
            ZoneManagement.instance.onZoneChanged += zonechanged;
        }


        void spawed()
        {
            ThirdPersonCamera = GorillaTagger.Instance.thirdPersonCamera;
            PCSCREEN = GorillaTagger.Instance.thirdPersonCamera.transform.Find("Shoulder Camera").gameObject;
            Debug.Log("setting stuff up!");
            var bundle = LoadAssetBundle("strikercammod.Reasoure.cam_mod");
            cam = bundle.LoadAsset<GameObject>("cam_mod");
            cam = Instantiate(cam);
            cam.SetActive(true);
            ThirdPersonCamera.GetComponentInChildren<CinemachineBrain>().enabled = false;
            ThirdPersonCamera.transform.parent = cam.transform;
            ThirdPersonCamera.transform.localPosition = new Vector3(0.0231f, - 0.0191f, - 0.0103f);
            ThirdPersonCamera.transform.localRotation = Quaternion.Euler(4.2888f, 266.9108f, 271.8853f);
            PCSCREEN.transform.localPosition = new Vector3(-0.4301f, 0.8753f, 1.8582f);
            PCSCREEN.transform.localRotation = Quaternion.Euler(359.2005f, 356.2808f, 357.8461f);
            cam.AddComponent<Manager>();
            transform.localScale = new Vector3(1.9491f, 7.0782f, 5.9655f);
            cam.AddComponent<DevHoldable>().camera = cam;
            if(!PlayerPrefs.HasKey("posX"))
            {
                cam.transform.position = new Vector3(-68.7146f, 11.9877f, -82.6656f);
                cam.transform.rotation = Quaternion.Euler(270f, 189.6795f, 0f);
                cam.transform.FindChildRecursive("current pos").GetComponent<TextMeshPro>().text = "-68.7146 " + "11.9877 " + "-82.6656 ";
            }
            else
            {
                cam.transform.position = new Vector3(PlayerPrefs.GetFloat("posX"), PlayerPrefs.GetFloat("posY"), PlayerPrefs.GetFloat("posZ"));
                cam.transform.rotation = Quaternion.Euler(PlayerPrefs.GetFloat("rotX"), PlayerPrefs.GetFloat("rotY"), PlayerPrefs.GetFloat("rotZ"));
                cam.transform.FindChildRecursive("current pos").GetComponent<TextMeshPro>().text = PlayerPrefs.GetFloat("posX").ToString() +" " + PlayerPrefs.GetFloat("posY").ToString() + " " + PlayerPrefs.GetFloat("posZ").ToString();
            }
           
        }


        public void Update()
        {
<<<<<<< Updated upstream
           
            try
            {
                if (Camera.transform.parent != null)
=======
            try
            {
                if(cam.transform.parent != null)
>>>>>>> Stashed changes
                {
                    if (!Camera.transform.parent.gameObject.activeSelf)
                    {

<<<<<<< Updated upstream
                        Camera.transform.Find("Model").gameObject.SetActive(true);
                        Camera.transform.position = GorillaTagger.Instance.headCollider.transform.position;
                        Camera.transform.localScale = new Vector3(.1f, .1f, .1f);
                        Camera.transform.parent = null;


=======
                    if (!cam.transform.parent.gameObject.activeSelf)
                    {
                        if (cam.transform.GetComponent<Manager>() == null)
                        {
                            Manager.InMode = false;
                        }
                        cam.transform.parent = null;
                        cam.transform.position = GorillaTagger.Instance.mainCamera.transform.position;
                        cam.transform.rotation = Quaternion.Euler(270.7868f, 180.3345f, 180f);
                        cam.transform.localScale = new Vector3(3.02721024f, 12.5924244f, 9.46782017f);
                        cam.transform.Find("Model").gameObject.SetActive(true);
                        cam.transform.Find("Buttons").gameObject.SetActive(true);
                        cam.transform.gameObject.GetComponent<MeshRenderer>().enabled = true;
                        GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = new Vector3(0.0231f, -0.0191f, -0.0103f);
                        GorillaTagger.Instance.thirdPersonCamera.transform.localRotation = Quaternion.Euler(4.2888f, 266.9108f, 271.8853f);
                        PCSCREEN.transform.localPosition = new Vector3(-0.4301f, 0.8753f, 1.8582f);
                        PCSCREEN.transform.localRotation = Quaternion.Euler(359.2005f, 356.2808f, 357.8461f);
>>>>>>> Stashed changes
                    }
                }
                if(cam.transform.GetComponent<Manager>() != null)
                {
                    if (Manager.InMode)
                    {
                        cam.GetComponent<DevHoldable>().enabled = false;
                    }
                    else
                    {
                        cam.GetComponent<DevHoldable>().enabled = true;
                    }
                }
                


            }
            catch
            {
                Debug.Log("what the sigma?");
            }
            catch
            {
                Debug.Log("error with return to sender");
            }
            try
            {

<<<<<<< Updated upstream
                if (PhotonNetwork.InRoom)
                {
                    if (!PhotonNetworkController.Instance.currentJoinTrigger.GetFullDesiredGameModeString().Contains("MODDED"))
                    {
                        
                        Camera.transform.Find("Model/buttons/Second Page/1").gameObject.SetActive(false);
                        Camera.transform.transform.Find("Model/buttons/Second Page/2").gameObject.SetActive(false);
                        Camera.transform.transform.Find("Model/buttons/Second Page/3").gameObject.SetActive(false);
                        Camera.transform.transform.Find("Model/buttons/Second Page/4").gameObject.SetActive(false);
                        Camera.transform.transform.Find("Model/buttons/Second Page/5").gameObject.SetActive(false);
                        Camera.transform.transform.Find("Model/buttons/Second Page/6").gameObject.SetActive(false);
                        Camera.transform.transform.Find("Model/buttons/Third Page/7").gameObject.SetActive(false);
                        Camera.transform.transform.Find("Model/buttons/Third Page/8").gameObject.SetActive(false);
                        Camera.transform.transform.Find("Model/buttons/Third Page/9").gameObject.SetActive(false);
                        Camera.transform.transform.Find("Model/buttons/Third Page/10").gameObject.SetActive(false);
                    }
                  
                }
                
            }
            catch
            {
                Debug.Log("Error with modded check");
            }
                
        }

        void zonechanged()
        {
            Debug.Log("Zone Change");
            ScoreBoard = FindAnyObjectByType<GorillaScoreBoard>();
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


=======
        }

>>>>>>> Stashed changes
        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }
    }
}


