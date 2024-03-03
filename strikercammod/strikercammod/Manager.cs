using GorillaNetworking;
using HarmonyLib;
using strikercammod.buttons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using TMPro;
using Valve.VR;
using static OVRPlugin;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Assertions;
using Utilla;
using Photon.Realtime;
using GorillaExtensions;
using Steamworks;
using System.Threading;
using System.Timers;
using Unity.Mathematics;

namespace strikercammod.mainmanager
{

    public class Manager : MonoBehaviourPunCallbacks
    {
        float timer = 0f;
        public string key;
        public GameObject cam;
        public GameObject firstpage;
        public GameObject secondpage;
        public GameObject thirdpage;
        bool rightStickClick;
        GameObject doug;
        bool isdoneloadingassets;
        GameObject tpcbutton;
        GameObject secondpersoncam;
        GameObject nextpage;
        GameObject lastpage;
        GameObject Fcam;
        GameObject dougcam;
        float FOV = 90f;
        GameObject fp;
        GameObject minisettings;
        public bool trailclcik = false;
        GameObject player;
        bool IsSteamVR;
        public bool buttonlclicked;
        public bool freecam = true;
        public bool hasaddedplayer;
        public GameObject PCSCREEN;
        public GameObject CAMSCREEN;
        public GameObject Rigs;
        public int assetsloaded;
        bool flipclick;
        bool cleared;
        bool isfpc;
        public bool inmoddedroom;
        int index;

        public List<GameObject> players = new List<GameObject>();
        void Start()
        {


            Debug.Log("adding buttons!");
            firstpage = cam.transform.Find("Model/buttons/First Page").gameObject;
            minisettings = cam.transform.Find("Model/buttons/Quick Settings").gameObject;
            doug = GameObject.Find("Floating Bug Holdable");
            tpcbutton = cam.transform.Find("Model/buttons/First Page/Third Person Camera").gameObject;
            secondpersoncam = cam.transform.Find("Model/buttons/First Page/2D cam").gameObject;
            Fcam = cam.transform.Find("Model/buttons/First Page/Front Cam").gameObject;
            nextpage = cam.transform.Find("Model/buttons/Next Page").gameObject;
            lastpage = cam.transform.Find("Model/buttons/Last Page").gameObject;
            dougcam = cam.transform.Find("Model/buttons/First Page/Doug Cam").gameObject;
            fp = cam.transform.Find("Model/buttons/First Page/First person cam").gameObject;
            thirdpage = cam.transform.Find("Model/buttons/Third Page").gameObject;
            secondpage = cam.transform.Find("Model/buttons/Second Page").gameObject;
            Rigs = GameObject.Find("Player Objects/RigCache/Rig Parents");





            foreach (BoxCollider g in firstpage.GetComponentsInChildren<BoxCollider>())
            {




                g.gameObject.AddComponent<btnmanager>();



            }
            foreach (BoxCollider g in minisettings.GetComponentsInChildren<BoxCollider>())
            {




                g.gameObject.AddComponent<btnmanager>();



            }
            foreach (BoxCollider g in secondpage.GetComponentsInChildren<BoxCollider>())
            {




                g.gameObject.AddComponent<btnmanager>();



            }
            foreach (BoxCollider g in thirdpage.GetComponentsInChildren<BoxCollider>())
            {




                g.gameObject.AddComponent<btnmanager>();



            }
            foreach (btnmanager g in firstpage.GetComponentsInChildren<btnmanager>())
            {

                assetsloaded += 1;
                check();

            }
            foreach (btnmanager g in secondpage.GetComponentsInChildren<btnmanager>())
            {

                assetsloaded += 1;
                check();

            }
            foreach (btnmanager g in thirdpage.GetComponentsInChildren<btnmanager>())
            {

                assetsloaded += 1;
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

        /* void OnGUI()
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
            GUI.Label(new Rect(30f, 50f, 170f, 270f), "X " + PlayerPrefs.GetFloat("rx").ToString() + " Y " + PlayerPrefs.GetFloat("ry").ToString() + " Z " + PlayerPrefs.GetFloat("rz").ToString());
         }
        */ 

        public void check()
        {
            if (assetsloaded == 42)
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


                IsSteamVR = Traverse.Create(PlayFabAuthenticator.instance).Field("platform").GetValue().ToString().ToLower() == "steam";

                // thanks lunakitty for the code
            }
        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);

            StartCoroutine("pluh");


        }
        IEnumerator pluh()
        {
            Clear();
            yield return new WaitForSeconds(0.2f);
            addplayers();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);


            StartCoroutine("pluh");
        }


        public void addplayers()
        {
            cleared = false;
            if (!cleared)
            {

                Debug.Log("working");
                index = 0;
                foreach (VRRig g in GorillaParent.instance.vrrigs)
                {
                   

                    if (!g.isOfflineVRRig)
                    {
                        players.Add(g.gameObject);

                        if (index <= 10)
                        {

                            if (!firstpage.activeSelf)
                            {









                                if (!g.gameObject.active)
                                {
                                    Debug.Log("not player");
                                    players.Remove(g.gameObject);
                                    index -= 1;
                                }




                                if (index == 0)
                                {
                                    if (g.gameObject.activeSelf)
                                    {
                                        secondpage.transform.Find("1").gameObject.SetActive(true);
                                        secondpage.transform.Find("1").gameObject.GetComponentInChildren<TextMeshPro>().text = g.playerText.text;
                                        secondpage.transform.Find("1").gameObject.GetComponent<MeshRenderer>().material.color = g.playerColor;

                                    }
                                    else
                                    {
                                        secondpage.transform.Find("1").gameObject.SetActive(false);
                                    }

                                }
                                if (index == 1)
                                {
                                    if (g.gameObject.activeSelf)
                                    {
                                        secondpage.transform.Find("2").gameObject.SetActive(true);
                                        secondpage.transform.Find("2").gameObject.GetComponentInChildren<TextMeshPro>().text = g.playerText.text;
                                        secondpage.transform.Find("2").gameObject.GetComponent<MeshRenderer>().material.color = g.playerColor;
                                    }
                                    else
                                    {
                                        secondpage.transform.Find("2").gameObject.SetActive(false);
                                    }

                                }
                                if (index == 2)
                                {
                                    if (g.gameObject.activeSelf)
                                    {
                                        secondpage.transform.Find("3").gameObject.SetActive(true);
                                        secondpage.transform.Find("3").gameObject.GetComponentInChildren<TextMeshPro>().text = g.playerText.text;
                                        secondpage.transform.Find("3").gameObject.GetComponent<MeshRenderer>().material.color = g.playerColor;
                                    }
                                    else
                                    {
                                        secondpage.transform.Find("3").gameObject.SetActive(false);
                                    }

                                }
                                if (index == 3)
                                {
                                    if (g.gameObject.activeSelf)
                                    {
                                        secondpage.transform.Find("4").gameObject.SetActive(true);
                                        secondpage.transform.Find("4").gameObject.GetComponentInChildren<TextMeshPro>().text = g.playerText.text;
                                        secondpage.transform.Find("4").gameObject.GetComponent<MeshRenderer>().material.color = g.playerColor;
                                    }
                                    else
                                    {
                                        secondpage.transform.Find("4").gameObject.SetActive(false);
                                    }

                                }
                                if (index == 4)
                                {
                                    if (g.gameObject.activeSelf)
                                    {
                                        secondpage.transform.Find("5").gameObject.SetActive(true);
                                        secondpage.transform.Find("5").gameObject.GetComponentInChildren<TextMeshPro>().text = g.playerText.text;
                                        secondpage.transform.Find("5").gameObject.GetComponent<MeshRenderer>().material.color = g.playerColor;
                                    }
                                    else
                                    {
                                        secondpage.transform.Find("5").gameObject.SetActive(false);
                                    }

                                }
                                if (index == 5)
                                {
                                    if (g.gameObject.activeSelf)
                                    {
                                        secondpage.transform.Find("6").gameObject.SetActive(true);
                                        secondpage.transform.Find("6").gameObject.GetComponentInChildren<TextMeshPro>().text = g.playerText.text;
                                        secondpage.transform.Find("6").gameObject.GetComponent<MeshRenderer>().material.color = g.playerColor;
                                    }
                                    else
                                    {
                                        secondpage.transform.Find("6").gameObject.SetActive(false);
                                    }

                                }
                                if (index == 6)
                                {
                                    if (g.gameObject.activeSelf)
                                    {
                                        thirdpage.transform.Find("7").gameObject.SetActive(true);
                                        thirdpage.transform.Find("7").gameObject.GetComponentInChildren<TextMeshPro>().text = g.playerText.text;
                                        thirdpage.transform.Find("7").gameObject.GetComponent<MeshRenderer>().material.color = g.playerColor;
                                    }
                                    else
                                    {
                                        thirdpage.transform.Find("7").gameObject.SetActive(false);
                                    }

                                }
                                if (index == 7)
                                {
                                    if (g.gameObject.activeSelf)
                                    {
                                        thirdpage.transform.Find("8").gameObject.SetActive(true);
                                        thirdpage.transform.Find("8").gameObject.GetComponentInChildren<TextMeshPro>().text = g.playerText.text;
                                        thirdpage.transform.Find("8").gameObject.GetComponent<MeshRenderer>().material.color = g.playerColor;
                                    }
                                    else
                                    {
                                        thirdpage.transform.Find("8").gameObject.SetActive(false);
                                    }

                                }
                                if (index == 8)
                                {
                                    if (g.gameObject.activeSelf)
                                    {
                                        thirdpage.transform.Find("9").gameObject.SetActive(true);
                                        thirdpage.transform.Find("9").gameObject.GetComponentInChildren<TextMeshPro>().text = g.playerText.text;
                                        thirdpage.transform.Find("9").gameObject.GetComponent<MeshRenderer>().material.color = g.playerColor;
                                    }
                                    else
                                    {
                                        thirdpage.transform.Find("9").gameObject.SetActive(false);
                                    }

                                }
                                if (index == 9)
                                {
                                    if (g.gameObject.activeSelf)
                                    {
                                        thirdpage.transform.Find("10").gameObject.SetActive(true);
                                        thirdpage.transform.Find("10").gameObject.GetComponentInChildren<TextMeshPro>().text = g.playerText.text;
                                        thirdpage.transform.Find("10").gameObject.GetComponent<MeshRenderer>().material.color = g.playerColor;
                                    }
                                    else
                                    {
                                        thirdpage.transform.Find("10").gameObject.SetActive(false);
                                    }

                                }


                                index++;
                            }







                            Debug.Log("added");
                        }

                        else
                        {
                            index = 10;
                        }



                    }
                }
            }
        
            else
            {
                secondpage.transform.Find("1").gameObject.SetActive(false);
                secondpage.transform.Find("2").gameObject.SetActive(false);
                secondpage.transform.Find("3").gameObject.SetActive(false);
                secondpage.transform.Find("4").gameObject.SetActive(false);
                secondpage.transform.Find("5").gameObject.SetActive(false);
                secondpage.transform.Find("6").gameObject.SetActive(false);
                thirdpage.transform.Find("7").gameObject.SetActive(false);
                thirdpage.transform.Find("8").gameObject.SetActive(false);
                thirdpage.transform.Find("9").gameObject.SetActive(false);
                thirdpage.transform.Find("10").gameObject.SetActive(false);
            }
          
        }
        public void Clear()
        {
            
            
                players.Clear();
            cleared = true;
            secondpage.transform.Find("1").gameObject.SetActive(false);
            secondpage.transform.Find("2").gameObject.SetActive(false);
            secondpage.transform.Find("3").gameObject.SetActive(false);
            secondpage.transform.Find("4").gameObject.SetActive(false);
            secondpage.transform.Find("5").gameObject.SetActive(false);
            secondpage.transform.Find("6").gameObject.SetActive(false);
            thirdpage.transform.Find("7").gameObject.SetActive(false);
            thirdpage.transform.Find("8").gameObject.SetActive(false);
            thirdpage.transform.Find("9").gameObject.SetActive(false);
            thirdpage.transform.Find("10").gameObject.SetActive(false);
        }

        public void Update()
        {

            if (PhotonNetwork.InRoom)
            {
                if (inmoddedroom)
                {
                    addplayers();
                }

               
            }
            else
            {
                Clear();
                secondpage.transform.Find("1").gameObject.SetActive(false);
                secondpage.transform.Find("2").gameObject.SetActive(false);
                secondpage.transform.Find("3").gameObject.SetActive(false);
                secondpage.transform.Find("4").gameObject.SetActive(false);
                secondpage.transform.Find("5").gameObject.SetActive(false);
                secondpage.transform.Find("6").gameObject.SetActive(false);
                thirdpage.transform.Find("7").gameObject.SetActive(false);
                thirdpage.transform.Find("8").gameObject.SetActive(false);
                thirdpage.transform.Find("9").gameObject.SetActive(false);
                thirdpage.transform.Find("10").gameObject.SetActive(false);
            }
            
            GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>().fieldOfView = FOV;
            cam.GetComponentInChildren<Camera>().fieldOfView = FOV;
            CAMSCREEN.GetComponentInChildren<Camera>().fieldOfView = FOV;
            if (IsSteamVR) { rightStickClick = SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand); }
            else { ControllerInputPoller.instance.leftControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out rightStickClick); }

            if (rightStickClick)
            {

                cam.transform.localScale = new Vector3(.1f, .1f, .1f);
                cam.transform.parent = null;
                cam.transform.position = GorillaTagger.Instance.mainCamera.transform.position;
                cam.transform.Find("Model").gameObject.SetActive(true);
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



        }
        private void setpos()
        {
            cam.transform.localPosition = new Vector3(0f, 0.3f, - 0.9f);
            cam.transform.localRotation = Quaternion.Euler(0f, 90f, 18.4618f);
            cam.transform.localScale = new Vector3(.1f, .1f, .1f);
            PCSCREEN.transform.localPosition = new Vector3(68.0681f, -12.1543f, 80.8426f);
            PCSCREEN.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            CAMSCREEN.transform.localPosition = new Vector3(-0.633f, 0.847f, 0.002f);
            CAMSCREEN.transform.localRotation = Quaternion.Euler(0f, 262.4698f, 0f);
        }






        public void clicked(string name)
        {
         
            if (name == "for broxy")
            {
                cam.transform.Find("Model/buttons/First Page/for broxy").GetComponent<AudioSource>().Play();
            }
            else if(name == "Flip")
            {
                if (!flipclick)
                {
                    PCSCREEN.transform.localPosition = new Vector3(68.0681f, -12.1543f, 80.4426f);
                    PCSCREEN.transform.localRotation = Quaternion.Euler(0, 185.8334f, 0f);
                    CAMSCREEN.transform.localRotation = Quaternion.Euler(0f, 86.7389f, 0f);
                    CAMSCREEN.transform.localPosition = new Vector3(-0.633f, 2.7143f, 0.002f);
                    flipclick = true;
                   
                }
                else
                {
                    PCSCREEN.transform.localPosition = new Vector3(68.0681f, -12.1543f, 80.8426f);
                    PCSCREEN.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    CAMSCREEN.transform.localPosition = new Vector3(-0.633f, 0.847f, 0.002f);
                    CAMSCREEN.transform.localRotation = Quaternion.Euler(0f, 262.4698f, 0f);
                    flipclick = false;
                }
                
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
            {

                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.bodyCollider.transform;
                cam.transform.localPosition = new Vector3(-1.4136f, 0.3509f, -0.3654f);
                cam.transform.localRotation = Quaternion.Euler(0.1841f, 161.3228f, 0.0496f);
                CAMSCREEN.transform.localPosition = new Vector3(-0.633f, 0.847f, 0.002f);
                CAMSCREEN.transform.localRotation = Quaternion.Euler(0f, 262.4698f, 0f);
                PCSCREEN.transform.localPosition = new Vector3(68.0681f, -12.1543f, 80.8426f);
                PCSCREEN.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if(name == "trail")
            {
                
             
                if (!trailclcik)
                {
                    PlayerPrefs.SetString("trail", "false");
                    cam.transform.Find("Model/GameObject").gameObject.SetActive(false);
                    trailclcik = true;
                }
                else
                {
                    PlayerPrefs.SetString("trail", "true");
                    cam.transform.Find("Model/GameObject").gameObject.SetActive(true);
                    trailclcik = false;
                }
            }
            else if (name == "Front Cam")
            {

                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.bodyCollider.transform;
                cam.transform.localPosition = new Vector3(-0.1138f, 0.1514f, 1.1346f);
                cam.transform.localRotation = Quaternion.Euler(0.1841f, 264.6863f, 0.0496f);
                CAMSCREEN.transform.localPosition = new Vector3(-0.633f, 0.847f, 0.002f);
                CAMSCREEN.transform.localRotation = Quaternion.Euler(0f, 262.4698f, 0f);
                PCSCREEN.transform.localPosition = new Vector3(68.0681f, -12.1543f, 80.8426f);
                PCSCREEN.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (name == "Doug Cam")
            {

                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = doug.transform;
                cam.transform.localPosition = new Vector3(-0.0028f, -0.1186f, 0.04f);
                cam.transform.localRotation = Quaternion.Euler(0.1841f, 96.0716f, 0.0496f);
                CAMSCREEN.transform.localPosition = new Vector3(-0.633f, 0.847f, 0.002f);
                CAMSCREEN.transform.localRotation = Quaternion.Euler(0f, 262.4698f, 0f);
                PCSCREEN.transform.localPosition = new Vector3(68.0681f, -12.1543f, 80.8426f);
                PCSCREEN.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
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
            else if (name == "Next Page")
            {
                if(firstpage.active)
                {
                    firstpage.SetActive(false);
                    secondpage.SetActive(true);
                    thirdpage.SetActive(false);
                }
                else if (secondpage.active)
                {
                    firstpage.SetActive(false);
                    secondpage.SetActive(false);
                    thirdpage.SetActive(true);
                }
                else if (thirdpage.active)
                {
                    firstpage.SetActive(true);
                    secondpage.SetActive(false);
                    thirdpage.SetActive(false);
                }


            }

            else if (name == "Last Page")
            {

                if (secondpage.active)
                {
                    firstpage.SetActive(true);
                    thirdpage.SetActive(false);
                    secondpage.SetActive(false);
                }
                else if (thirdpage.active)
                {
                    firstpage.SetActive(false);
                    thirdpage.SetActive(false);
                    secondpage.SetActive(true);
                }
                else if (firstpage.active)
                {
                    firstpage.SetActive(false);
                    thirdpage.SetActive(true);
                    secondpage.SetActive(false);
                }
            }

            else if (name == "1")
            {
                if (players[0].gameObject != isActiveAndEnabled)
                {
                    cam.transform.parent = null;
                    Debug.Log("THIS PLAYER IS NOT IN THE ROOM!!");
                }
                else
                {
                    cam.transform.parent = players[0].gameObject.transform;

                    setpos();
                }
            }
            else if (name == "2")
            {

                if (players[1].gameObject != isActiveAndEnabled)
                {
                    cam.transform.parent = null;
                    Debug.Log("THIS PLAYER IS NOT IN THE ROOM!!");
                }
                else
                {
                    cam.transform.parent = players[1].gameObject.transform;
                    setpos();
                }
            }
            else if (name == "3")
            {
                if (players[2].gameObject != isActiveAndEnabled)
                {
                    cam.transform.parent = null;
                    Debug.Log("THIS PLAYER IS NOT IN THE ROOM!!");
                }
                else
                {
                    cam.transform.parent = players[2].gameObject.transform;
                    setpos();
                }
            }
            else if (name == "4")
            {
                if (players[3].gameObject != isActiveAndEnabled)
                {
                    cam.transform.parent = null;
                    Debug.Log("THIS PLAYER IS NOT IN THE ROOM!!");
                }
                else
                {
                    cam.transform.parent = players[3].gameObject.transform;
                    setpos();
                }
            }
            else if (name == "5")
            {
                if (players[4].gameObject != isActiveAndEnabled)
                {
                    cam.transform.parent = null;
                    Debug.Log("THIS PLAYER IS NOT IN THE ROOM!!");
                }
                else
                {
                    cam.transform.parent = players[4].gameObject.transform;
                    setpos();
                }
            }
            else if (name == "6")
            {
                if (players[5].gameObject != isActiveAndEnabled)
                {
                    cam.transform.parent = null;
                    Debug.Log("THIS PLAYER IS NOT IN THE ROOM!!");
                }
                else
                {
                    cam.transform.parent = players[5].gameObject.transform;
                    setpos();
                }
            }
            else if (name == "7")
            {
                if (players[6].gameObject != isActiveAndEnabled)
                {
                    cam.transform.parent = null;
                    Debug.Log("THIS PLAYER IS NOT IN THE ROOM!!");
                }
                else
                {
                    cam.transform.parent = players[6].gameObject.transform;
                    setpos();
                }
            }
            else if (name == "8")
            {
                if (players[7].gameObject != isActiveAndEnabled)
                {
                    cam.transform.parent = null;
                    Debug.Log("THIS PLAYER IS NOT IN THE ROOM!!");
                }
                else
                {
                    cam.transform.parent = players[7].gameObject.transform;
                    setpos();
                }
            }
            else if (name == "9")
            {
                if (players[8].gameObject != isActiveAndEnabled)
                {
                    cam.transform.parent = null;
                    Debug.Log("THIS PLAYER IS NOT IN THE ROOM!!");
                }
                else
                {
                    cam.transform.parent = players[8].gameObject.transform;
                    setpos();
                }
            }
            else if (name == "10")
            {
                if (players[9].gameObject != isActiveAndEnabled)
                {
                    cam.transform.parent = null;
                    Debug.Log("THIS PLAYER IS NOT IN THE ROOM!!");
                }
                else
                {
                    cam.transform.parent = players[9].gameObject.transform;
                    setpos();
                }
            }
            else if (name == "Red")
            {
                cam.transform.Find("Model/Camera 1/Cube").gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                PlayerPrefs.SetString("color", "Color.red");
            }
            else if(name == "Black")
            {
                cam.transform.Find("Model/Camera 1/Cube").gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
                PlayerPrefs.SetString("color", "Color.black");
            }
           else if (name == "Green")
            {
                cam.transform.Find("Model/Camera 1/Cube").gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                PlayerPrefs.SetString("color", "Color.green");
            }
            else if (name == "Blue")
            {
                cam.transform.Find("Model/Camera 1/Cube").gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                PlayerPrefs.SetString("color", "Color.blue");
            }
            else if (name == "minus fov")
            {
                FOV -= 1f;
            }
            else if (name == "add fov")
            {
                FOV += 1f;
            }
            else if (name == "save pos")
            {
                savepos();
            }


        }
        private void savepos()
        {
            PlayerPrefs.SetFloat("x", cam.transform.position.x);
            PlayerPrefs.SetFloat("y", cam.transform.position.y);
            PlayerPrefs.SetFloat("z", cam.transform.position.z);
            PlayerPrefs.SetFloat("rx", cam.transform.rotation.eulerAngles.x);
            PlayerPrefs.SetFloat("ry", cam.transform.rotation.eulerAngles.y);
            PlayerPrefs.SetFloat("rz", cam.transform.rotation.eulerAngles.z);
        }




    }
}
   


            
            
        
    
    

