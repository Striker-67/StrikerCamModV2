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

using UnityEngine.Assertions; 

using GorillaExtensions;
using Steamworks;
using System.Threading;
using System.Timers;
using Unity.Mathematics;
using Photon.Pun;
using Photon.Realtime;

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
        public int index;
        public float orbitDistance = 5.0f; // Distance from the target
        public float orbitSpeed = 10.0f; // Speed of the orbit
        public float lerpSpeed = 2.0f; // Speed of the lerp
        public List<GameObject> players;
        void Start()
        {

            IsSteamVR = Traverse.Create(PlayFabAuthenticator.instance).Field("platform").GetValue().ToString().ToLower() == "steam";
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
            players = new List<GameObject>();
           



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
        }
        public override void OnJoinedRoom()
        {
            StartCoroutine("waitandthenupdate");

        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            GetPlayers();
        }
        public void GetPlayers()
        {

            players.Clear();
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
            float rigindex;
            rigindex = 1;
            VRRig[] rigs = FindObjectsOfType<VRRig>();
            foreach (VRRig r in rigs)
            {
                if (!r.isLocal)
                {
                    players.Add(r.gameObject);
                    if (rigindex <= 6)
                    {
                        transform.Find("Model/buttons/Second Page/" + rigindex.ToString()).gameObject.SetActive(true);
                        transform.Find("Model/buttons/Second Page/" + rigindex.ToString()).gameObject.GetComponentInChildren<TextMeshPro>().text = r.Creator.NickName;
                    }
                    if (rigindex >= 7)
                    {
                        transform.Find("Model/buttons/Third Page/" + rigindex.ToString()).gameObject.SetActive(true);
                        transform.Find("Model/buttons/Third Page/" + rigindex.ToString()).gameObject.GetComponentInChildren<TextMeshPro>().text = r.Creator.NickName;
                    }

                    rigindex++;

                }

            }

        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            GetPlayers();
        }
        public override void OnLeftRoom()
        {
            players.Clear();
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
        IEnumerator waitandthenupdate()
        {
            yield return new WaitForSeconds(3);
            GetPlayers();
        }





        public void Update()
        {

            
            GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>().fieldOfView = FOV;
            cam.GetComponentInChildren<Camera>().fieldOfView = FOV;
            CAMSCREEN.GetComponentInChildren<Camera>().fieldOfView = FOV;
            if (IsSteamVR) { rightStickClick = SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand); }
            else { ControllerInputPoller.instance.leftControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out rightStickClick); }
            if (rightStickClick)
            {
                cam.transform.Find("Model").gameObject.SetActive(true);
                cam.transform.position = GorillaTagger.Instance.headCollider.transform.position;
                cam.transform.localScale = new Vector3(.1f, .1f, .1f);
                cam.transform.parent = null;
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
        private void setpos(int placement)
        {
            cam.transform.parent = players[placement - 1].transform;
            cam.transform.localPosition = new Vector3(0.2323f, 0.2204f, - 1.1574f);
            cam.transform.localRotation = Quaternion.Euler(359.2126f, 93.1602f, 9.2339f);
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
            else if (name == "Flip")
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
            else if (name == "1")
            {
                setpos(1);
            }
            else if (name == "2")
            {

                setpos(2);
            }
            else if (name == "3")
            {

                setpos(3);
            }
            else if (name == "4")
            {

                setpos(4);
            }
            else if (name == "5")
            {

                setpos(5);
            }
            else if (name == "6")
            {

                setpos(6);
            }
            else if (name == "7")
            {
                setpos(7);
            }
            else if (name == "8")
            {

                setpos(8);
            }
            else if (name == "9")
            {

                setpos(9);
            }
            else if (name == "10")
            {

                setpos(10);
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
   


            
            
        
    
    

