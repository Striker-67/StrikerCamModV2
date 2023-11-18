using BepInEx;
using Cinemachine;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Utilla;

namespace strikercammod
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        GameObject Camera;
        GameObject tpcbutton;
        GameObject secondpersoncam;
        GameObject nextpage;
        GameObject lastpage;
        GameObject Fcam;
        GameObject dougcam;
        GameObject fp;
        GameObject shhhh;


        void Start()
        {

            Utilla.Events.GameInitialized += OnGameInitialized;
        }


        void OnGameInitialized(object sender, EventArgs e)
        {

            var bundle = LoadAssetBundle("strikercammod.Reasoure.cammod");
            Camera = bundle.LoadAsset<GameObject>("cammod");
            Camera = Instantiate(Camera);
            Destroy(Camera.transform.Find("Model/Camera").gameObject.GetComponent<AudioListener>());
            GorillaTagger.Instance.thirdPersonCamera.transform.parent = Camera.transform;
            Destroy(GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<CinemachineBrain>());
            GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = new Vector3(3.6648f, 0.0383f, - 1.3422f);
            GorillaTagger.Instance.thirdPersonCamera.transform.localRotation = Quaternion.Euler(0f, 177.0001f, 0f);
            GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>().fieldOfView = 90;
            Camera.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
            Camera.transform.position = new Vector3(-65.0436f, 11.9509f, -84.3991f);
            tpcbutton = Camera.transform.Find("Model/buttons/first page/TPC button/Cube (3)").gameObject;
            secondpersoncam = Camera.transform.Find("Model/buttons/first page/Cube (4)").gameObject;
            Fcam = Camera.transform.Find("Model/buttons/first page/Cube (5)").gameObject;
            nextpage = Camera.transform.Find("Model/buttons/next page").gameObject;
            lastpage = Camera.transform.Find("Model/buttons/last page").gameObject;
            dougcam = Camera.transform.Find("Model/buttons/second page/doug cam/Cube (3)").gameObject;
            fp = Camera.transform.Find("Model/buttons/second page/First person cam").gameObject;
            shhhh = Camera.transform.Find("Model/buttons/second page/for broxy").gameObject;
            dougcam.name = ("dougcamtrigger");
            tpcbutton.layer = 18;
            nextpage.layer = 18;
            lastpage.layer = 18;
            secondpersoncam.layer = 18;
            Fcam.layer = 18;
            dougcam.layer = 18;
            fp.layer = 18;
            shhhh.layer = 18;


            tpcbutton.AddComponent<Keys>().cam = Camera;
            nextpage.AddComponent<Keys>().nextbutton = nextpage;
            lastpage.AddComponent<Keys>().lastbutton = lastpage;
            secondpersoncam.AddComponent<Keys>().cam = Camera;
            Fcam.AddComponent<Keys>().cam = Camera;
            fp.AddComponent<Keys>().cam = Camera;
            dougcam.AddComponent<Keys>().cam = Camera;
            shhhh.AddComponent<Keys>().cam = Camera;

            fp.GetComponent<Keys>().lastbutton = lastpage;
            dougcam.GetComponent<Keys>().lastbutton = lastpage;
            shhhh.GetComponent<Keys>().lastbutton = lastpage;
            fp.GetComponent<Keys>().nextbutton = nextpage;
            dougcam.GetComponent<Keys>().nextbutton = nextpage;
 
            tpcbutton.GetComponent<Keys>().nextbutton = nextpage;
            tpcbutton.GetComponent<Keys>().lastbutton = lastpage;
            nextpage.GetComponent<Keys>().cam = Camera;
            nextpage.GetComponent<Keys>().lastbutton = lastpage;
            lastpage.GetComponent<Keys>().cam = Camera;
            lastpage.GetComponent<Keys>().nextbutton = nextpage;
            secondpersoncam.GetComponent<Keys>().nextbutton = nextpage;
            secondpersoncam.GetComponent<Keys>().lastbutton = lastpage;


            Destroy(shhhh);
        }
                   
        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }

        void Update()
        {
            if (ControllerInputPoller.instance.leftControllerGripFloat >= 1)
            {
                if (Camera.transform.Find("Model").GetComponentInChildren<Keys>().freecam == false)
                {
                    Camera.transform.parent = null;
                    Camera.transform.position = GorillaTagger.Instance.mainCamera.transform.position;
                    Camera.transform.Find("Model").gameObject.SetActive(true);
                    Camera.transform.Find("Model").GetComponentInChildren<Keys>().freecam = true;

                }


            }
            if (ControllerInputPoller.instance.leftControllerGripFloat >= 1)
            {
                if (Fcam.GetComponent<Keys>().freecam == false)
                {
                    Camera.transform.parent = null;
                    Camera.transform.position = GorillaTagger.Instance.mainCamera.transform.position;
                    Camera.transform.Find("Model").gameObject.SetActive(true);
                   Fcam.GetComponent<Keys>().freecam = true;

                }


            }
            if (ControllerInputPoller.instance.leftControllerGripFloat >= 1)
            {
                if (secondpersoncam.GetComponent<Keys>().freecam == false)
                {
                    Camera.transform.parent = null;
                    Camera.transform.position = GorillaTagger.Instance.mainCamera.transform.position;
                    Camera.transform.Find("Model").gameObject.SetActive(true);
                   secondpersoncam.GetComponent<Keys>().freecam = true;

                }


            }
            if (ControllerInputPoller.instance.leftControllerGripFloat >= 1)
            {
                if (dougcam.GetComponent<Keys>().freecam == false)
                {
                    Camera.transform.parent = null;
                    Camera.transform.position = GorillaTagger.Instance.mainCamera.transform.position;
                    Camera.transform.Find("Model").gameObject.SetActive(true);
                   dougcam.GetComponent<Keys>().freecam = true;

                }


            }
            if (ControllerInputPoller.instance.leftControllerGripFloat >= 1)
            {
                if (fp.GetComponent<Keys>().freecam == false)
                {
                    Camera.transform.parent = null;
                    Camera.transform.position = GorillaTagger.Instance.mainCamera.transform.position;
                    Camera.transform.Find("Model").gameObject.SetActive(true);
                    fp.GetComponent<Keys>().freecam = true;

                }


            }
        }


    }
}
