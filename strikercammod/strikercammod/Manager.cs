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
        public GameObject nextbutton;
        public GameObject lastbutton;
        bool rightStickClick;
        GameObject doug;
        int children;
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
            firstpage = cam.transform.Find("Model/buttons/first page").gameObject;
            secondpage = cam.transform.Find("Model/buttons/second page").gameObject;
            doug = GameObject.Find("Floating Bug Holdable");
            tpcbutton = cam.transform.Find("Model/buttons/first page/TPC button/Cube (3)").gameObject;
            secondpersoncam = cam.transform.Find("Model/buttons/first page/Cube (4)").gameObject;
            Fcam = cam.transform.Find("Model/buttons/first page/Cube (5)").gameObject;
            nextpage = cam.transform.Find("Model/buttons/next page").gameObject;
            lastpage = cam.transform.Find("Model/buttons/last page").gameObject;
            dougcam = cam.transform.Find("Model/buttons/second page/doug cam/Cube (3)").gameObject;
            fp = cam.transform.Find("Model/buttons/second page/First person cam").gameObject;
            dougcam.name = ("dougcamtrigger");





            foreach (BoxCollider g in firstpage.GetComponentsInChildren<BoxCollider>())
            {




                g.gameObject.AddComponent<btnmanager>();


                Debug.Log("got button on the First page " + g);

            }
            foreach (BoxCollider g in secondpage.GetComponentsInChildren<BoxCollider>())
            {




                g.gameObject.AddComponent<btnmanager>();
                if (g.GetComponent<btnmanager>() != null)

                    Debug.Log("got button on the First page " + g);

            }  
            foreach (btnmanager g in secondpage.GetComponentsInChildren<btnmanager>())
            {

                assetsloaded += 1;
                if (assetsloaded == 3)
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
            if (name == "Cube (3)")
            {
                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.bodyCollider.transform;
                cam.transform.localPosition = new Vector3(-0.1138f, 0.4514f, -1.3654f);
                cam.transform.localRotation = Quaternion.Euler(10.1839f, 10.6935f, 0.0496f);
            }
            if (name == "Cube (4)")
            {

                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.bodyCollider.transform;
                cam.transform.localPosition = new Vector3(-1.4138f, 0.3514f, -0.3654f);
                cam.transform.localRotation = Quaternion.Euler(10.184f, 80.341f, 0.0496f);
            }
            if (name == "Cube (5)")
            {

                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.bodyCollider.transform;
                cam.transform.localPosition = new Vector3(-0.1138f, 0.1514f, 1.1346f);
                cam.transform.localRotation = Quaternion.Euler(358.184f, 180.239f, 0.0496f);
            }
            if (name == "dougcamtrigger")
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
            if (name == "next page")
            {
                lastpage.SetActive(true);
                nextpage.SetActive(false);
                firstpage.SetActive(false);
                secondpage.SetActive(true);
            }
            if (name == "last page")
            {
                nextpage.SetActive(true);
                lastpage.SetActive(false);
                firstpage.SetActive(true);
                secondpage.SetActive(false);
            }
        }


    }


}
   


            
            
        
    
    

