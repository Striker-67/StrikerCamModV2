using strikercammod.buttons;
using UnityEngine;
using Photon.Pun;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Valve.VR;
using UnityEngine.XR;
using GorillaNetworking;
using HarmonyLib;
using UnityEngine.Device;
using Photon.Realtime;
using UnityEngine.UI;
using DevHoldableEngine;
using GorillaCars;

namespace strikercammod.mainmanager
{

    public class Manager : MonoBehaviourPunCallbacks
    {
        public object gameMode;
        public int assets;
        bool ismainpageopen = false;
        public List<GameObject> CamViews = new List<GameObject>();
        int currentview;
        public float FOV;
        TextMeshPro FOVTEXT;
        TextMeshPro NCTEXT;
        TextMeshPro VTCTEXT;
        GameObject fakecam;
        GameObject shouldercam;
        bool rightStickClick;
        bool IsSteamVR;
        GameObject doug;
        bool CamViewsOpen;
        GameObject rigs;
        public List<GameObject> players = new List<GameObject>();
        GameObject Error;
        public static bool InMode;
        
        int VTC;
        float NearClip;
        GameObject TrailObj;
        bool trailtoggle;
        public void Start()
        {
            GetButtons();
            IsSteamVR = Traverse.Create(PlayFabAuthenticator.instance).Field("platform").GetValue().ToString().ToLower() == "steam";
            FOVTEXT = transform.FindChildRecursive("FOV TEXT").GetComponent<TextMeshPro>();
            FOV = GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>().fieldOfView;
            FOVTEXT.text = "FOV: " + FOV.ToString();
            NCTEXT = transform.FindChildRecursive("NEAR CLIP TEXT").GetComponent<TextMeshPro>();
            NearClip = GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>().nearClipPlane;
            NCTEXT.text = "NEAR CLIP: " + NearClip.ToString();
            VTCTEXT = transform.FindChildRecursive("VTC TEXT").GetComponent<TextMeshPro>();
            VTC = 1;
            VTCTEXT.text = "VALUE TO CHANGE: " + VTC.ToString();
            fakecam = transform.Find("Model/Fake Cam").gameObject;
            shouldercam = GorillaTagger.Instance.thirdPersonCamera.transform.Find("Shoulder Camera").gameObject;
            doug = GameObject.Find("Floating Bug Holdable");
<<<<<<< Updated upstream
            tpcbutton = cam.transform.Find("Model/buttons/First Page/Third Person Camera").gameObject;
            secondpersoncam = cam.transform.Find("Model/buttons/First Page/2D cam").gameObject;
            Fcam = cam.transform.Find("Model/buttons/First Page/Front Cam").gameObject;
            nextpage = cam.transform.Find("Model/buttons/Next Page").gameObject;
            lastpage = cam.transform.Find("Model/buttons/Last Page").gameObject;
            dougcam = cam.transform.Find("Model/buttons/First Page/Doug Cam").gameObject;
            fp = cam.transform.Find("Model/buttons/First Page/First person cam").gameObject;
            thirdpage = cam.transform.Find("Model/buttons/Third Page").gameObject;
            secondpage = cam.transform.Find("Model/buttons/Second Page").gameObject;
            players = new List<GameObject>();

            if (IsSteamVR) { rightStickClick = SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand); }
            else { ControllerInputPoller.instance.leftControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out rightStickClick); }


            foreach (BoxCollider g in firstpage.GetComponentsInChildren<BoxCollider>())
            {



              
                g.gameObject.AddComponent<btnmanager>();
                index++;


            }
            foreach (BoxCollider g in minisettings.GetComponentsInChildren<BoxCollider>())
            {



               
                g.gameObject.AddComponent<btnmanager>();
                index++;


            }
            foreach (BoxCollider g in secondpage.GetComponentsInChildren<BoxCollider>())
            {



                
                g.gameObject.AddComponent<btnmanager>();
                index++;


            }
            foreach (BoxCollider g in thirdpage.GetComponentsInChildren<BoxCollider>())
            {



               
                g.gameObject.AddComponent<btnmanager>();
                index++;


            }
            foreach (btnmanager g in firstpage.GetComponentsInChildren<btnmanager>())
            {

             
                check();

            }
            foreach (btnmanager g in secondpage.GetComponentsInChildren<btnmanager>())
            {

              
                check();

            }
            foreach (btnmanager g in thirdpage.GetComponentsInChildren<btnmanager>())
            {

              
                check();

            }

            nextpage.AddComponent<btnmanager>();
            if (nextpage.GetComponent<btnmanager>() != null)
            {
                Debug.Log("got button next page");
            }
            lastpage.AddComponent<btnmanager>();
            if (lastpage.GetComponent<btnmanager>() != null)
            {
                Debug.Log("got button last page");
            }
            firstpage.SetActive(true);
            secondpage.SetActive(false);
            thirdpage.SetActive(false);
         
        }
        /*
        void OnGUI()
           {
               GUI.Box(new Rect(30f, 50f, 170f, 270f), "Striker's Camera Mod");
               if (GUI.Button(new Rect(35f, 70f, 160f, 20f), "UNPARENT"))
               {
                   Debug.Log("clicked UI");
                   cam.transform.parent = null;
                   cam.transform.position = GorillaTagger.Instance.mainCamera.transform.position;
                   cam.transform.Find("Model").gameObject.SetActive(true);

                   freecam = true;

               }

           }
         */


        public void check()
        {
            if (assetsloaded == 27)
            {
                isdoneloadingassets = true;
            }
            if (isdoneloadingassets)
            {
                firstpage.SetActive(true);
                thirdpage.SetActive(false);
                secondpage.SetActive(false);
                nextpage.SetActive(true);
                lastpage.SetActive(true);
                Debug.Log("is done");
                transform.Find("Model/buttons/Second Page/1").gameObject.SetActive(false);
                transform.Find("Model/buttons/Second Page/2").gameObject.SetActive(false);
                transform.Find("Model/buttons/Second Page/3").gameObject.SetActive(false);
                transform.Find("Model/buttons/Second Page/4").gameObject.SetActive(false);
                transform.Find("Model/buttons/Second Page/5").gameObject.SetActive(false);
                transform.Find("Model/buttons/Second Page/6").gameObject.SetActive(false);
                transform.Find("Model/buttons/Third Page/7").gameObject.SetActive(false);
                transform.Find("Model/buttons/Third Page/8").gameObject.SetActive(false);
                transform.Find("Model/buttons/Third Page/9").gameObject.SetActive(false);
                transform.Find("Model/buttons/Third Page/10").gameObject.SetActive(false);




            }
=======
            transform.localScale = new Vector3(3.02721024f, 12.5924244f, 9.46782017f);
            TrailObj = transform.Find("Model/Trail").gameObject;
            if (PlayerPrefs.GetString("Color") == null)
            {
                changecolor(Color.red);
            }
            if (PlayerPrefs.GetString("Color") == "red")
            {
                changecolor(Color.red);
            }
            if (PlayerPrefs.GetString("Color") == "green")
            {
                changecolor(Color.green);
            }
            if (PlayerPrefs.GetString("Color") == "black")
            {
                changecolor(Color.black);
            }
            if (PlayerPrefs.GetString("Color") == "blue")
            {
                changecolor(Color.blue);
            }
            if (!PlayerPrefs.HasKey("Trail"))
            {
                TrailObj.SetActive(true);
                transform.FindChildRecursive("trail toggle info").GetComponent<TextMeshPro>().text = "ON";
            }
            if(PlayerPrefs.GetInt("Trail")== 0)
            {
                TrailObj.SetActive(false);
                transform.FindChildRecursive("trail toggle info").GetComponent<TextMeshPro>().text = "OFF";

            }
            else
            {
                TrailObj.SetActive(true);
                transform.FindChildRecursive("trail toggle info").GetComponent<TextMeshPro>().text = "ON";
            }
            rigs = transform.Find("Model/UI/pages/Cam Views/1st person").gameObject;
            
           

>>>>>>> Stashed changes
        }
        public override void OnJoinedRoom()
        {
            GetPlayers();
        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            GetPlayers();
        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            GetPlayers();
        }
        public override void OnLeftRoom()
        {
            players.Clear();
        }
        public void GetButtons()
        {
            BoxCollider[] boxColliders = transform.GetComponentsInChildren<BoxCollider>();
            foreach (BoxCollider collider in boxColliders)
            {
                if (collider.isTrigger)
                {
                    collider.gameObject.AddComponent<btnmanager>();
                    assets++;
                    check();
                }

            }
        }
        public void GetPlayers()
        {

<<<<<<< Updated upstream
         
            GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>().fieldOfView = FOV;
            cam.GetComponentInChildren<Camera>().fieldOfView = FOV;
            CAMSCREEN.GetComponentInChildren<Camera>().fieldOfView = FOV;
            if (IsSteamVR) { rightStickClick = SteamVR_Actions.gorillaTag_RightJoystickClick.GetState(SteamVR_Input_Sources.RightHand); }
            else { ControllerInputPoller.instance.rightControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out rightStickClick); }
            if (rightStickClick)
            {
                cam.transform.Find("Model").gameObject.SetActive(true);
                cam.transform.position = GorillaTagger.Instance.headCollider.transform.position;
                cam.transform.localScale = new Vector3(.1f, .1f, .1f);
                cam.transform.parent = null;
                freecam = true;
            }
            if (FOV > 130)
            {
                FOV = 30f;
            }
            if (FOV < 30)
            {
                FOV = 130;
            }
            minisettings.transform.Find("FOV/CUR FOV").GetComponent<TextMeshPro>().text = FOV.ToString();
=======
            players.Clear();
            float rigindex;
            rigindex = 1;
            VRRig[] rigs = FindObjectsOfType<VRRig>();
            foreach (VRRig r in rigs)
            {
                if (!r.isLocal)
                {
                    players.Add(r.gameObject);
                    rigindex++;
>>>>>>> Stashed changes

                }

            }

        }
        public void check()
        {
            if (assets == 14)
            {
                foreach (Transform t in transform.Find("Model/UI/pages"))
                {
                    t.gameObject.SetActive(false);
                }
                transform.FindChildRecursive("Cam Views").gameObject.SetActive(value: false);
                Error = transform.FindChildRecursive("YOUR NOT IN A ROOM").gameObject;
                Error.SetActive(false);
            }
        }
        public void clicked(string name)
        {
            if (name == "Mod Stuff btn")
            {
                if (!ismainpageopen)
                {
                    foreach (Transform t in transform.Find("Model/UI/pages"))
                    {
                        t.gameObject.SetActive(false);
                    }
                    transform.FindChildRecursive("Main Page").gameObject.SetActive(true);
                    transform.FindChildRecursive("Cam Views").gameObject.SetActive(true);
                    transform.FindChildRecursive("BG").gameObject.SetActive(false);
                    ismainpageopen = true;
                }
                else if (ismainpageopen)
                {
                    foreach (Transform t in transform.Find("Model/UI/pages"))
                    {
                        t.gameObject.SetActive(false);
                    }
                    transform.FindChildRecursive("BG").gameObject.SetActive(true);
                    ismainpageopen = false;
                }

            }
            else if (name == "Settings page")
            {
                foreach (Transform t in transform.Find("Model/UI/pages"))
                {
                    t.gameObject.SetActive(false);
                }
                transform.FindChildRecursive("Settings Page").gameObject.SetActive(true);
                transform.FindChildRecursive("Cam Views").gameObject.SetActive(true);
                transform.FindChildRecursive("BG").gameObject.SetActive(false);
            }

            else if (name == "Cam Views")
            {
                CamViews.Clear();
                currentview = 0;
                transform.FindChildRecursive("Main Page").gameObject.SetActive(false);
                transform.Find("Model/UI/pages/Cam Views").gameObject.SetActive(true);
                foreach (Transform t in transform.Find("Model/UI/pages/Cam Views"))
                {
                    t.gameObject.SetActive(false);
                }

                CamViews.Add(transform.FindChildRecursive("TPC").gameObject);
                CamViews.Add(transform.FindChildRecursive("FPC").gameObject);
                CamViews.Add(transform.FindChildRecursive("SPC").gameObject);
                CamViews.Add(transform.FindChildRecursive("DOUG CAM").gameObject);
                CamViews.Add(transform.FindChildRecursive("FC").gameObject);
                CamViews.Add(transform.FindChildRecursive("1st person").gameObject);
                CamViews.Add(transform.FindChildRecursive("2nd person").gameObject);
                CamViews.Add(transform.FindChildRecursive("3rd person").gameObject);
                CamViews.Add(transform.FindChildRecursive("4th person").gameObject);
                CamViews.Add(transform.FindChildRecursive("5th person").gameObject);
                CamViews.Add(transform.FindChildRecursive("6th person").gameObject);
                CamViews.Add(transform.FindChildRecursive("7th person").gameObject);
                CamViews.Add(transform.FindChildRecursive("8th person").gameObject);
                CamViews.Add(transform.FindChildRecursive("9th person").gameObject);
                CamViews.Add(transform.FindChildRecursive("10th person").gameObject);
                Debug.Log("THE LIST IS:" + CamViews.Count.ToString());
                CamViews.ElementAt(0).gameObject.SetActive(true);
            }
            else if (name == "Last View")
            {
                currentview--;

                if (currentview >= 0)
                {
                    foreach (Transform t in transform.Find("Model/UI/pages/Cam Views"))
                    {
                        t.gameObject.SetActive(false);
                    }
                    CamViews.ElementAt(currentview).gameObject.SetActive(true);
                }
                else
                {
                    currentview = 0;
                }

            }
<<<<<<< Updated upstream
            else if (name == "First person cam")
            {

                Debug.Log("FPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.headCollider.transform;
                cam.transform.localPosition = new Vector3(0.0323f, -0.0796f, -0.0574f);
                cam.transform.localRotation = Quaternion.Euler(359.2126f, 79.4568f, 0.9775f);
                CAMSCREEN.transform.localPosition = new Vector3(-0.633f, 0.847f, 0.002f);
                CAMSCREEN.transform.localRotation = Quaternion.Euler(0f, 262.4698f, 0f);
                PCSCREEN.transform.localPosition = new Vector3(68.0681f, -12.1543f, 80.8426f);
                PCSCREEN.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (name == "Third Person Camera")
              {
                  Debug.Log("TPC hit");
                  freecam = false;
                  cam.transform.Find("Model").gameObject.SetActive(false);
                  cam.transform.parent = GorillaTagger.Instance.bodyCollider.transform;
                  cam.transform.localPosition = new Vector3(-0.0538f, 0.4514f, -1.3554f);
                  cam.transform.localRotation = Quaternion.Euler(4.1839f, 90.9117f, 2.0496f);

                  CAMSCREEN.transform.localPosition = new Vector3(-0.633f, 0.847f, 0.002f);
                  CAMSCREEN.transform.localRotation = Quaternion.Euler(0f, 262.4698f, 0f);
                  PCSCREEN.transform.localPosition = new Vector3(68.0681f, -12.1543f, 80.8426f);
                  PCSCREEN.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

             }   
         
        

            else if (name == "2D cam")
=======
            else if(name == "SAVE POS")
>>>>>>> Stashed changes
            {
                PlayerPrefs.SetFloat("posX", Plugin.cam.transform.position.x);
                PlayerPrefs.SetFloat("posY", Plugin.cam.transform.position.y);
                PlayerPrefs.SetFloat("posZ", Plugin.cam.transform.position.z);
                PlayerPrefs.SetFloat("rotX", Plugin.cam.transform.rotation.eulerAngles.x);
                PlayerPrefs.SetFloat("rotY", Plugin.cam.transform.rotation.eulerAngles.y);
                PlayerPrefs.SetFloat("rotZ", Plugin.cam.transform.rotation.eulerAngles.z);
                transform.FindChildRecursive("current pos").GetComponent<TextMeshPro>().text = PlayerPrefs.GetFloat("posX").ToString() + " " + PlayerPrefs.GetFloat("posY").ToString() + " " + PlayerPrefs.GetFloat("posZ").ToString();
            }
            else if (name == "RED")
            {
                changecolor(Color.red);
                PlayerPrefs.SetString("Color", "red");
            }
            else if (name == "GREEN")
            {
                changecolor(Color.green);
                PlayerPrefs.SetString("Color", "green");
            }
            else if (name == "BLUE")
            {
                changecolor(Color.blue);
                PlayerPrefs.SetString("Color", "blue");
            }
            else if (name == "BLACK")
            {
                changecolor(Color.black);
                PlayerPrefs.SetString("Color", "black");
            }
            else if (name == "Next View")
            {
                currentview++;

                if (currentview < CamViews.Count)
                {
                    foreach (Transform t in transform.Find("Model/UI/pages/Cam Views"))
                    {
                        t.gameObject.SetActive(false);
                    }
                    CamViews.ElementAt(currentview).gameObject.SetActive(true);
                }
                else
                {
                    currentview = CamViews.Count;
                }

            }
            else if (name == "ADD FOV")
            {
                if(FOV <= 150)
                {
                    FOV += VTC;
                    GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>().fieldOfView = FOV;
                    transform.gameObject.GetComponentInChildren<Camera>().fieldOfView = FOV;
                    FOVTEXT.text = "FOV: " + FOV.ToString();
                }
                
            }
            else if (name == "MINUS FOV")
            {
                if(FOV >= 15)
                {
                    FOV -= VTC;
                    GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>().fieldOfView = FOV;
                    transform.gameObject.GetComponentInChildren<Camera>().fieldOfView = FOV;
                    FOVTEXT.text = "FOV: " + FOV.ToString();
                }
               
            }
            else if (name == "ADD VTC")
            {
                VTC++;
                VTCTEXT.text = "VALUE TO CHANGE: " + VTC.ToString();
            }
            else if (name == "MINUS VTC")
            {
                VTC--;
                VTCTEXT.text = "VALUE TO CHANGE: " + VTC.ToString();
            }
            else if (name == "ADD NC")
            {
                if(NearClip <= 1)
                {
                    NearClip += .1f;
                    GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>().nearClipPlane = NearClip;
                    transform.gameObject.GetComponentInChildren<Camera>().nearClipPlane = NearClip;
                    NCTEXT.text = "NEAR CLIP: " + NearClip.ToString();
                }
            }
                
            else if (name == "MINUS NC")
            {
                if (NearClip >= 0.1f)
                {
                    NearClip -= .1f;
                    GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>().nearClipPlane = NearClip;
                    transform.gameObject.GetComponentInChildren<Camera>().nearClipPlane = NearClip;
                    NCTEXT.text = "NEAR CLIP: " + NearClip.ToString();
                }
            }
            else if (name == "Trail Toggle")
            {
                if (!trailtoggle)
                {
                    trailtoggle = true;
                    TrailObj.SetActive(true);
                    PlayerPrefs.SetInt("Trail", 1);
                    transform.FindChildRecursive("trail toggle info").GetComponent<TextMeshPro>().text = "ON";
                }
                else
                {
                    trailtoggle = false;
                    PlayerPrefs.SetInt("Trail", 0);
                    TrailObj.SetActive(false);
                    transform.FindChildRecursive("trail toggle info").GetComponent<TextMeshPro>().text = "OFF";
                }
            }
            if (name == "Confirm View")
            {
                if (CamViewsOpen)
                {
                    if (currentview == 0)
                    {
                        InMode = true;
                        transform.parent = GorillaTagger.Instance.offlineVRRig.transform.FindChildRecursive("body");
                        GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = Vector3.zero;
                        shouldercam.transform.localPosition = new Vector3(0f, -0.6862f, -0.1616f);
                        shouldercam.transform.localRotation = Quaternion.Euler(273.7594f, 79.5801f, 0.9963f);
                        transform.localPosition = new Vector3(-0.3f, 1.0138f, -0.9616f);
                        transform.localRotation = Quaternion.Euler(19.3933f, 2.1f, 0f);
                        transform.Find("Model").gameObject.SetActive(false);
                        transform.Find("Buttons").gameObject.SetActive(false);
                        transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }
                    if (currentview == 1)
                    {
                        InMode = true;
                        transform.parent = GorillaTagger.Instance.mainCamera.transform;
                        GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = Vector3.zero;
                        shouldercam.transform.localPosition = new Vector3(0f, -0.0423f, -0.0774f);
                        shouldercam.transform.localRotation = Quaternion.Euler(271.2639f, 180.9998f, 264.3818f);
                        transform.localPosition = new Vector3(0, 0, 0);
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                        transform.Find("Model").gameObject.SetActive(false);
                        transform.Find("Buttons").gameObject.SetActive(false);
                        transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }
                    if (currentview == 2)
                    {
                        InMode = true;
                        transform.parent = GorillaTagger.Instance.mainCamera.transform;
                        GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = Vector3.zero;
                        shouldercam.transform.localPosition = new Vector3(0.3f, -1.6f, -34.1237f);
                        shouldercam.transform.localRotation = Quaternion.Euler(358.9312f, 2.3422f, 100.5931f);
                        transform.localPosition = new Vector3(0, 0, 0);
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                        transform.Find("Model").gameObject.SetActive(false);
                        transform.Find("Buttons").gameObject.SetActive(false);
                        transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }
                    if (currentview == 3)
                    {
                        InMode = true;
                        transform.parent = doug.transform;
                        GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = Vector3.zero;
                        shouldercam.transform.localPosition = new Vector3(0f, 0.51f, 0.08f);
                        shouldercam.transform.localRotation = Quaternion.Euler(274.9061f, 262.6611f, 183.0004f);
                        transform.localPosition = new Vector3(0, 0, 0);
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                        transform.Find("Model").gameObject.SetActive(false);
                        transform.Find("Buttons").gameObject.SetActive(false);
                        transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }
                    if (currentview == 4)
                    {
                        InMode = true;
                        transform.parent = GorillaTagger.Instance.offlineVRRig.transform.FindChildRecursive("body");
                        GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = Vector3.zero;
                        shouldercam.transform.localPosition = new Vector3(-0.3f, 8.7636f, 0f);
                        shouldercam.transform.localRotation = Quaternion.Euler(84.0012f, 268.5911f, 0.0001f);
                        transform.localPosition = new Vector3(0, 0, 0);
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                        transform.Find("Model").gameObject.SetActive(false);
                        transform.Find("Buttons").gameObject.SetActive(false);
                        transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }
                    if (PhotonNetwork.InRoom && Moddedcheck.modcheck.IsModded())
                    {
                        if (currentview > 4)
                        {
                            GetPlayers();
                        }

                        if (currentview == 5)
                        {

                            InMode = true;
                            transform.parent = players.ElementAt(0).transform;
                            GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = Vector3.zero;
                            shouldercam.transform.localPosition = new Vector3(-1.4873f, -9.4026f, -0.1616f);
                            shouldercam.transform.localRotation = Quaternion.Euler(297.5859f, 85.2658f, 0.9974f);
                            transform.localPosition = new Vector3(0, 0, 0);
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                            transform.Find("Model").gameObject.SetActive(false);
                            transform.Find("Buttons").gameObject.SetActive(false);
                            transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                        }
                        if (currentview == 6)
                        {
                            InMode = true;
                            transform.parent = players.ElementAt(1).transform;
                            GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = Vector3.zero;
                            shouldercam.transform.localPosition = new Vector3(-1.4873f, -9.4026f, -0.1616f);
                            shouldercam.transform.localRotation = Quaternion.Euler(297.5859f, 85.2658f, 0.9974f);
                            transform.localPosition = new Vector3(0, 0, 0);
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                            transform.Find("Model").gameObject.SetActive(false);
                            transform.Find("Buttons").gameObject.SetActive(false);
                            transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                        }
                        if (currentview == 7)
                        {
                            InMode = true;
                            transform.parent = players.ElementAt(2).transform;
                            GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = Vector3.zero;
                            shouldercam.transform.localPosition = new Vector3(-1.4873f, -9.4026f, -0.1616f);
                            shouldercam.transform.localRotation = Quaternion.Euler(297.5859f, 85.2658f, 0.9974f);
                            transform.localPosition = new Vector3(0, 0, 0);
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                            transform.Find("Model").gameObject.SetActive(false);
                            transform.Find("Buttons").gameObject.SetActive(false);
                            transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                        }
                        if (currentview == 8)
                        {
                            InMode = true;
                            transform.parent = players.ElementAt(3).transform;
                            GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = Vector3.zero;
                            shouldercam.transform.localPosition = new Vector3(-1.4873f, -9.4026f, -0.1616f);
                            shouldercam.transform.localRotation = Quaternion.Euler(297.5859f, 85.2658f, 0.9974f);
                            transform.localPosition = new Vector3(0, 0, 0);
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                            transform.Find("Model").gameObject.SetActive(false);
                            transform.Find("Buttons").gameObject.SetActive(false);
                            transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                        }
                        if (currentview == 9)
                        {
                            InMode = true;
                            transform.parent = players.ElementAt(4).transform;
                            GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = Vector3.zero;
                            shouldercam.transform.localPosition = new Vector3(-1.4873f, -9.4026f, -0.1616f);
                            shouldercam.transform.localRotation = Quaternion.Euler(297.5859f, 85.2658f, 0.9974f);
                            transform.localPosition = new Vector3(0, 0, 0);
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                            transform.Find("Model").gameObject.SetActive(false);
                            transform.Find("Buttons").gameObject.SetActive(false);
                            transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                        }
                        if (currentview == 10)
                        {
                            InMode = true;
                            transform.parent = players.ElementAt(5).transform;
                            GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = Vector3.zero;
                            shouldercam.transform.localPosition = new Vector3(-1.4873f, -9.4026f, -0.1616f);
                            shouldercam.transform.localRotation = Quaternion.Euler(297.5859f, 85.2658f, 0.9974f);
                            transform.localPosition = new Vector3(0, 0, 0);
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                            transform.Find("Model").gameObject.SetActive(false);
                            transform.Find("Buttons").gameObject.SetActive(false);
                            transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                        }
                        if (currentview == 11)
                        {
                            InMode = true;
                            transform.parent = players.ElementAt(6).transform;
                            GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = Vector3.zero;
                            shouldercam.transform.localPosition = new Vector3(-1.4873f, -9.4026f, -0.1616f);
                            shouldercam.transform.localRotation = Quaternion.Euler(297.5859f, 85.2658f, 0.9974f);
                            transform.localPosition = new Vector3(0, 0, 0);
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                            transform.Find("Model").gameObject.SetActive(false);
                            transform.Find("Buttons").gameObject.SetActive(false);
                            transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                        }
                        if (currentview == 12)
                        {
                            InMode = true;
                            transform.parent = players.ElementAt(7).transform;
                            GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = Vector3.zero;
                            shouldercam.transform.localPosition = new Vector3(-1.4873f, -9.4026f, -0.1616f);
                            shouldercam.transform.localRotation = Quaternion.Euler(297.5859f, 85.2658f, 0.9974f);
                            transform.localPosition = new Vector3(0, 0, 0);
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                            transform.Find("Model").gameObject.SetActive(false);
                            transform.Find("Buttons").gameObject.SetActive(false);
                            transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                        }
                        if (currentview == 13)
                        {
                            InMode = true;
                            transform.parent = players.ElementAt(8).transform;
                            GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = Vector3.zero;
                            shouldercam.transform.localPosition = new Vector3(-1.4873f, -9.4026f, -0.1616f);
                            shouldercam.transform.localRotation = Quaternion.Euler(297.5859f, 85.2658f, 0.9974f);
                            transform.localPosition = new Vector3(0, 0, 0);
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                            transform.Find("Model").gameObject.SetActive(false);
                            transform.Find("Buttons").gameObject.SetActive(false);
                            transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                        }
                        if (currentview == 14)
                        {
                            InMode = true;
                            transform.parent = players.ElementAt(9).transform;
                            GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = Vector3.zero;
                            shouldercam.transform.localPosition = new Vector3(-1.4873f, -9.4026f, -0.1616f);
                            shouldercam.transform.localRotation = Quaternion.Euler(297.5859f, 85.2658f, 0.9974f);
                            transform.localPosition = new Vector3(0, 0, 0);
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                            transform.Find("Model").gameObject.SetActive(false);
                            transform.Find("Buttons").gameObject.SetActive(false);
                            transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                        }
                    }
                }
                
            }
        }
        public void Unparent()
        {
            InMode = false;
            transform.parent = null;
            transform.position = GorillaTagger.Instance.mainCamera.transform.position;
            transform.rotation = Quaternion.Euler(270.7868f, 180.3345f, 180f);
            transform.localScale = new Vector3(3.02721024f, 12.5924244f, 9.46782017f);
            transform.Find("Model").gameObject.SetActive(true);
            transform.Find("Buttons").gameObject.SetActive(true);
            transform.gameObject.GetComponent<MeshRenderer>().enabled = true;
            GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = new Vector3(0.0231f, -0.0191f, -0.0103f);
            GorillaTagger.Instance.thirdPersonCamera.transform.localRotation = Quaternion.Euler(4.2888f, 266.9108f, 271.8853f);
            shouldercam.transform.localPosition = new Vector3(-0.4301f, 0.8753f, 1.8582f);
            shouldercam.transform.localRotation = Quaternion.Euler(359.2005f, 356.2808f, 357.8461f);
        }
        public void changecolor(Color color)
        {
            Plugin.cam.GetComponent<MeshRenderer>().materials[0].color = color;
            GetComponent<MeshRenderer>().materials[1].color = color;
            
            Debug.Log(PlayerPrefs.GetString("Color"));
        }
        public void Update()
        {
            try
            {
                if (IsSteamVR)
                {
                    rightStickClick = SteamVR_Actions.gorillaTag_RightJoystickClick.GetState(SteamVR_Input_Sources.RightHand);
                }
                else
                {
                    ControllerInputPoller.instance.leftControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out rightStickClick);
                }
            }
            catch
            {
                Debug.Log("somthing to do with steam vr");
                Error.SetActive(true);
            }

            if (rightStickClick)
            {
                Unparent();

            }
            try
            {
                CamViewsOpen = transform.Find("Model/UI/pages/Cam Views").gameObject.activeSelf;
            }
            catch
            {
                CamViewsOpen = false;
                Error.SetActive(true);
            }
            try
            {
                if (CamViewsOpen)
                {
                    if (currentview == 0)
                    {
                        fakecam.transform.parent = GorillaTagger.Instance.offlineVRRig.transform.FindChildRecursive("body");
                        fakecam.transform.localPosition = new Vector3(-0.3f, 1.0138f, -0.9616f);
                        fakecam.transform.localRotation = Quaternion.Euler(19.3933f, 2.1f, 0f);
                    }
                    if (currentview == 1)
                    {
                        fakecam.transform.parent = GorillaTagger.Instance.mainCamera.transform;
                        fakecam.transform.localPosition = new Vector3(0, 0, 0);
                        fakecam.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                    if (currentview == 2)
                    {
                        fakecam.transform.parent = GorillaTagger.Instance.offlineVRRig.transform.FindChildRecursive("body");
                        fakecam.transform.localPosition = new Vector3(-1.3f, 0.4f, 0f);
                        fakecam.transform.localRotation = Quaternion.Euler(0, 90, 0);
                    }
                    if (currentview == 3)
                    {
                        fakecam.transform.parent = doug.transform;
                        fakecam.transform.localPosition = new Vector3(0, 0, 0.09f);
                        fakecam.transform.localRotation = Quaternion.Euler(0.0544f, 0.5223f, 359.1815f);
                    }
                    if (currentview == 4)
                    {
                        fakecam.transform.parent = GorillaTagger.Instance.offlineVRRig.transform.FindChildRecursive("body");
                        fakecam.transform.localPosition = new Vector3(0f, 0.4f, 0.9f);
                        fakecam.transform.localRotation = Quaternion.Euler(0f, 184.0546f, 0f);
                    }
                    try
                    {
                        if (PhotonNetwork.InRoom && Moddedcheck.modcheck.IsModded())
                        {
                            Error.SetActive(false);
                            if (currentview == 5)
                            {
                                fakecam.transform.parent = players.ElementAt(0).transform;
                                fakecam.transform.localPosition = new Vector3(-0.3f, 0.4709f, -0.7f);
                                fakecam.transform.localRotation = Quaternion.Euler(16.2274f, 11.2899f, 0.0561f);
                                CamViews.ElementAt(5).transform.FindChildRecursive("playername").GetComponentInChildren<TextMeshPro>().text = players.ElementAt(0).GetComponent<VRRig>().transform.Find("rig/NameTagAnchor/NameTagCanvas/Text").GetComponent<Text>().text;
                            }
                            if (currentview == 6)
                            {
                                fakecam.transform.parent = players.ElementAt(1).transform;
                                fakecam.transform.localPosition = new Vector3(-0.3f, 0.4709f, -0.7f);
                                fakecam.transform.localRotation = Quaternion.Euler(16.2274f, 11.2899f, 0.0561f);
                                CamViews.ElementAt(6).transform.FindChildRecursive("playername").GetComponentInChildren<TextMeshPro>().text = players.ElementAt(1).GetComponent<VRRig>().transform.Find("rig/NameTagAnchor/NameTagCanvas/Text").GetComponent<Text>().text;
                            }
                            if (currentview == 7)
                            {
                                fakecam.transform.parent = players.ElementAt(2).transform;
                                fakecam.transform.localPosition = new Vector3(-0.3f, 0.4709f, -0.7f);
                                fakecam.transform.localRotation = Quaternion.Euler(16.2274f, 11.2899f, 0.0561f);
                                CamViews.ElementAt(7).transform.FindChildRecursive("playername").GetComponentInChildren<TextMeshPro>().text = players.ElementAt(2).GetComponent<VRRig>().transform.Find("rig/NameTagAnchor/NameTagCanvas/Text").GetComponent<Text>().text;
                            }
                            if (currentview == 8)
                            {
                                fakecam.transform.parent = players.ElementAt(3).transform;
                                fakecam.transform.localPosition = new Vector3(-0.3f, 0.4709f, -0.7f);
                                fakecam.transform.localRotation = Quaternion.Euler(16.2274f, 11.2899f, 0.0561f);
                                CamViews.ElementAt(8).transform.FindChildRecursive("playername").GetComponentInChildren<TextMeshPro>().text = players.ElementAt(3).GetComponent<VRRig>().transform.Find("rig/NameTagAnchor/NameTagCanvas/Text").GetComponent<Text>().text;
                            }
                            if (currentview == 9)
                            {
                                fakecam.transform.parent = players.ElementAt(4).transform;
                                fakecam.transform.localPosition = new Vector3(-0.3f, 0.4709f, -0.7f);
                                fakecam.transform.localRotation = Quaternion.Euler(16.2274f, 11.2899f, 0.0561f);
                                CamViews.ElementAt(9).transform.FindChildRecursive("playername").GetComponentInChildren<TextMeshPro>().text = players.ElementAt(4).GetComponent<VRRig>().transform.Find("rig/NameTagAnchor/NameTagCanvas/Text").GetComponent<Text>().text;
                            }
                            if (currentview == 10)
                            {
                                fakecam.transform.parent = players.ElementAt(5).transform;
                                fakecam.transform.localPosition = new Vector3(-0.3f, 0.4709f, -0.7f);
                                fakecam.transform.localRotation = Quaternion.Euler(16.2274f, 11.2899f, 0.0561f);
                                CamViews.ElementAt(10).transform.FindChildRecursive("playername").GetComponentInChildren<TextMeshPro>().text = players.ElementAt(5).GetComponent<VRRig>().transform.Find("rig/NameTagAnchor/NameTagCanvas/Text").GetComponent<Text>().text;
                            }
                            if (currentview == 11)
                            {
                                fakecam.transform.parent = players.ElementAt(6).transform;
                                fakecam.transform.localPosition = new Vector3(-0.3f, 0.4709f, -0.7f);
                                fakecam.transform.localRotation = Quaternion.Euler(16.2274f, 11.2899f, 0.0561f);
                                CamViews.ElementAt(11).transform.FindChildRecursive("playername").GetComponentInChildren<TextMeshPro>().text = players.ElementAt(6).GetComponent<VRRig>().transform.Find("rig/NameTagAnchor/NameTagCanvas/Text").GetComponent<Text>().text;
                            }
                            if (currentview == 12)
                            {
                                fakecam.transform.parent = players.ElementAt(7).transform;
                                fakecam.transform.localPosition = new Vector3(-0.3f, 0.4709f, -0.7f);
                                fakecam.transform.localRotation = Quaternion.Euler(16.2274f, 11.2899f, 0.0561f);
                                CamViews.ElementAt(12).transform.FindChildRecursive("playername").GetComponentInChildren<TextMeshPro>().text = players.ElementAt(7).GetComponent<VRRig>().transform.Find("rig/NameTagAnchor/NameTagCanvas/Text").GetComponent<Text>().text;
                            }
                            if (currentview == 13)
                            {
                                fakecam.transform.parent = players.ElementAt(8).transform;
                                fakecam.transform.localPosition = new Vector3(-0.3f, 0.4709f, -0.7f);
                                fakecam.transform.localRotation = Quaternion.Euler(16.2274f, 11.2899f, 0.0561f);
                                CamViews.ElementAt(13).transform.FindChildRecursive("playername").GetComponentInChildren<TextMeshPro>().text = players.ElementAt(8).GetComponent<VRRig>().transform.Find("rig/NameTagAnchor/NameTagCanvas/Text").GetComponent<Text>().text;
                            }
                            if (currentview == 14)
                            {
                                fakecam.transform.parent = players.ElementAt(9).transform;
                                fakecam.transform.localPosition = new Vector3(-0.3f, 0.4709f, -0.7f);
                                fakecam.transform.localRotation = Quaternion.Euler(16.2274f, 11.2899f, 0.0561f);
                                CamViews.ElementAt(14).transform.FindChildRecursive("playername").GetComponentInChildren<TextMeshPro>().text = players.ElementAt(9).GetComponent<VRRig>().transform.Find("rig/NameTagAnchor/NameTagCanvas/Text").GetComponent<Text>().text;
                            }
                        }
                        else
                        {
                            try
                            {
                                if (currentview > 4)
                                {
                                    Error.SetActive(true);
                                }
                                else
                                {
                                    Error.SetActive(false);
                                }

                            }
                            catch
                            {
                                Debug.Log("ONG?");
                            }

                        }
                    }
                    catch
                    {
                        Debug.Log("error with this ig?");
                        Error.SetActive(true);

                    }

                }
                else
                {
                    fakecam.transform.parent = transform.Find("Model");
                    Error.SetActive(false);
                }
            }
            catch
            {
                Debug.Log("error with main mods");
                Error.SetActive(true);
            }

        }
    }
}


