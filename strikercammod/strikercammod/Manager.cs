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

namespace strikercammod.mainmanager
{
    public class Manager : MonoBehaviour
    {
        public string key;
        public GameObject cam;
        public GameObject firstpage;
        public GameObject secondpage;
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
        bool IsSteamVR;
        public bool buttonlclicked;
        public bool freecam = true;
        public int assetsloaded;
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






            foreach (BoxCollider g in firstpage.GetComponentsInChildren<BoxCollider>())
            {




                g.gameObject.AddComponent<btnmanager>();



            }
            foreach (BoxCollider g in minisettings.GetComponentsInChildren<BoxCollider>())
            {




                g.gameObject.AddComponent<btnmanager>();



            }
            foreach (btnmanager g in firstpage.GetComponentsInChildren<btnmanager>())
            {

                assetsloaded += 1;
                if (assetsloaded == 6)
                {
                    isdoneloadingassets = true;
                }

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
            if (isdoneloadingassets)
            {
                firstpage.SetActive(true);
                nextpage.SetActive(true);
                lastpage.SetActive(false);

              
                 IsSteamVR = Traverse.Create(PlayFabAuthenticator.instance).Field("platform").GetValue().ToString().ToLower() == "steam";

                // thanks lunakitty for the code
            }

        }


        public void Update()
        {
            GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>().fieldOfView = FOV;
            if (IsSteamVR) { rightStickClick = SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand); }
            else { ControllerInputPoller.instance.leftControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out rightStickClick); }

            if (rightStickClick)
            {
                
                
                    cam.transform.parent = null;
                    cam.transform.position = GorillaTagger.Instance.mainCamera.transform.position;
                    cam.transform.Find("Model").gameObject.SetActive(true);
                    freecam = true;

                
            }
            if(FOV > 130)
            {
                FOV = 30f;
            }
            if (FOV < 30)
            {
                FOV = 130;
            }
            minisettings.transform.Find("FOV/CUR FOV").GetComponent<TextMeshPro>().text = FOV.ToString();

        }






        public void clicked(string name)
        {
            if (name == "Third Person Camera")
            {
                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.bodyCollider.transform;
                cam.transform.localPosition = new Vector3(-0.0538f, 0.4514f, - 1.3554f);
                cam.transform.localRotation = Quaternion.Euler(4.1839f, 90.9117f, 2.0496f);
            }
            if (name == "2D cam")
            {

                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.bodyCollider.transform;
                cam.transform.localPosition = new Vector3(-1.4136f, 0.3509f, - 0.3654f);
                cam.transform.localRotation = Quaternion.Euler(0.1841f, 161.3228f, 0.0496f);
            }
            if (name == "Front Cam")
            {

                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.bodyCollider.transform;
                cam.transform.localPosition = new Vector3(-0.1138f, 0.1514f, 1.1346f);
                cam.transform.localRotation = Quaternion.Euler(0.1841f, 264.6863f, 0.0496f);
            }
            if (name == "Doug Cam")
            {

                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = doug.transform;
                cam.transform.localPosition = new Vector3(-0.0028f, -0.1186f, 0.04f);
                cam.transform.localRotation = Quaternion.Euler(0.1841f, 96.0716f, 0.0496f);
            }
            if (name == "First person cam")
            {

                Debug.Log("FPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.headCollider.transform;
                cam.transform.localPosition = new Vector3(0.0323f, -0.0796f, -0.0574f);
                cam.transform.localRotation = Quaternion.Euler(359.2126f, 79.4568f, 0.9775f);
            }
            if (name == "Next Page")
            {
                lastpage.SetActive(true);
                nextpage.SetActive(false);
                firstpage.SetActive(false);
                secondpage.SetActive(true);
            }
            if (name == "Last Page")
            {
                nextpage.SetActive(true);
                lastpage.SetActive(false);
                firstpage.SetActive(true);
                secondpage.SetActive(false);
            }
            if (name == "Red")
            {
                cam.transform.Find("Model/Cube").gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            if (name == "Black")
            {
                cam.transform.Find("Model/Cube").gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
            }
            if (name == "Green")
            {
                cam.transform.Find("Model/Cube").gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            }
            if (name == "Blue")
            {
                cam.transform.Find("Model/Cube").gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
            if(name == "minus fov")
            {
                FOV -= 1f;
            }
            if (name == "add fov")
            {
                FOV += 1f;
            }
            
        }


    }


}
   


            
            
        
    
    

