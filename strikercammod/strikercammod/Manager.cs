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
using Valve.VR;

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
        GameObject fp;
        bool IsSteamVR;
        public bool buttonlclicked;
        public bool freecam = true;
        public int assetsloaded;
        void Start()
        {
           

            Debug.Log("adding buttons!");
            firstpage = cam.transform.Find("Model/buttons/First Page").gameObject;
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
                secondpage.SetActive(true);
                nextpage.SetActive(true);
                secondpage.SetActive(false);
                lastpage.SetActive(false);

              
                 IsSteamVR = Traverse.Create(PlayFabAuthenticator.instance).Field("platform").GetValue().ToString().ToLower() == "steam";

                // thanks lunakitty for the code
            }

        }


        public void Update()
        {
            if (IsSteamVR) { rightStickClick = SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand); }
            else { ControllerInputPoller.instance.leftControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out rightStickClick); }

            if (rightStickClick)
            {
                if (freecam == false)
                {
                    cam.transform.parent = null;
                    cam.transform.position = GorillaTagger.Instance.mainCamera.transform.position;
                    cam.transform.Find("Model").gameObject.SetActive(true);
                    freecam = true;

                }
            }

        }






        public void clicked(string name)
        {
            if (name == "Third Person Camera")
            {
                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.bodyCollider.transform;
                cam.transform.localPosition = new Vector3(-0.1138f, 0.4514f, -1.3654f);
                cam.transform.localRotation = Quaternion.Euler(10.1839f, 10.6935f, 0.0496f);
            }
            if (name == "2D cam")
            {

                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.bodyCollider.transform;
                cam.transform.localPosition = new Vector3(-1.4138f, 0.3514f, -0.3654f);
                cam.transform.localRotation = Quaternion.Euler(10.184f, 80.341f, 0.0496f);
            }
            if (name == "Front Cam")
            {

                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.bodyCollider.transform;
                cam.transform.localPosition = new Vector3(-0.1138f, 0.1514f, 1.1346f);
                cam.transform.localRotation = Quaternion.Euler(358.184f, 180.239f, 0.0496f);
            }
            if (name == "Doug Cam")
            {

                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = doug.transform;
                cam.transform.localPosition = new Vector3(-0.0028f, -0.1186f, 0.04f);
                cam.transform.localRotation = Quaternion.Euler(358.084f, 0.1588f, 0.0496f);
            }
            if (name == "First person cam")
            {

                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.headCollider.transform;
                cam.transform.localPosition = new Vector3(-0.0077f, -0.1196f, -0.0574f);
                cam.transform.localRotation = Quaternion.Euler(1.9842f, 3.412f, 0.8496f);
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
        }


    }


}
   


            
            
        
    
    

