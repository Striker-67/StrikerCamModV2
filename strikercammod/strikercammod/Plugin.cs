using BepInEx;
using BepInEx.configuration;
using Cinemachine;
using DevHoldableEngine;
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
        bool enabled;
        GameObject Camera;
        privite ConfigEntry<float> FOV;
      

void onEnabled()
{
  enabled = true;
}
void onDisabled()
{
  enabled = false;
}
        void Start()
        {
        FOV = config.Bind("FOV"
                   "change fov via config"
                   float
                   "simple way to change fov");
            Utilla.Events.GameInitialized += OnGameInitialized;
        }


        void OnGameInitialized(object sender, EventArgs e)
        {
            Debug.Log("setting stuff up!");
            var bundle = LoadAssetBundle("strikercammod.Reasoure.cammod");
            Camera = bundle.LoadAsset<GameObject>("cammod");
            Camera = Instantiate(Camera);
            Destroy(Camera.transform.Find("Model/Camera").gameObject.GetComponent<AudioListener>());
            GorillaTagger.Instance.thirdPersonCamera.transform.parent = Camera.transform;
            (GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<CinemachineBrain>().Enabled = false);
            GorillaTagger.Instance.thirdPersonCamera.transform.localPosition = new Vector3(3.6648f, 0.0383f, - 1.3422f);
            GorillaTagger.Instance.thirdPersonCamera.transform.localRotation = Quaternion.Euler(0f, 177.0001f, 0f);
            GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>().fieldOfView = FOV.Value;
            Camera.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
            Camera.transform.position = new Vector3(-65.0436f, 11.9509f, -84.3991f);

            if(!enabled)
            {
              undosetup();
            }
            else
            {
              redo(); 
            }


            Camera.AddComponent<Manager>().cam = Camera;
            
            if(Camera.GetComponent<Manager>() != null)
            {
                Debug.Log("added manager");
            }
          
        }
        void undosetup()
        {
            Camera.SetActive(false);
             (GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<CinemachineBrain>().Enabled = true);
        }
        void redo()
        {
            Camera.SetActive(true);
             (GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<CinemachineBrain>().Enabled = false);
        }
        
                   
        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }

        


    }
}
