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
        GameObject Fcam;



        void Start()
        {
            /* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */

            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

            HarmonyPatches.RemoveHarmonyPatches();
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
            tpcbutton = Camera.transform.Find("Model/buttons/TPC button/Cube (3)").gameObject;
            secondpersoncam = Camera.transform.Find("Model/buttons/Cube (4)").gameObject;
            Fcam = Camera.transform.Find("Model/buttons/Cube (5)").gameObject;
            tpcbutton.layer = 18;
            secondpersoncam.layer = 18;
            Fcam.layer = 18;
            tpcbutton.AddComponent<Keys>().cam = Camera;
            secondpersoncam.AddComponent<Keys>().cam = Camera;
            Fcam.AddComponent<Keys>().cam = Camera;
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
                if (Camera.transform.Find("Model/buttons/Cube (5)").GetComponentInChildren<Keys>().freecam == false)
                {
                    Camera.transform.parent = null;
                    Camera.transform.position = GorillaTagger.Instance.mainCamera.transform.position;
                    Camera.transform.Find("Model").gameObject.SetActive(true);
                    Camera.transform.Find("Model/buttons/Cube (5)").GetComponentInChildren<Keys>().freecam = true;

                }


            }
            if (ControllerInputPoller.instance.leftControllerGripFloat >= 1)
            {
                if (Camera.transform.Find("Model/buttons/Cube (4)").GetComponent<Keys>().freecam == false)
                {
                    Camera.transform.parent = null;
                    Camera.transform.position = GorillaTagger.Instance.mainCamera.transform.position;
                    Camera.transform.Find("Model").gameObject.SetActive(true);
                    Camera.transform.Find("Model/buttons/Cube (4)").GetComponent<Keys>().freecam = true;

                }


            }
        }

        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            /* Activate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = true;
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = false;
        }
    }
}
