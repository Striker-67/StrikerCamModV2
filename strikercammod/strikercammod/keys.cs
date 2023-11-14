using GorillaLocomotion.Climbing;
using strikercammod;
using UnityEngine;

public class Keys : MonoBehaviour
{
    public string key;
    public GameObject cam;
    public GameObject firstpage;
    public GameObject secondpage;
    public GameObject nextbutton;
    public GameObject lastbutton;
 

    public bool buttonlclicked;
    public bool freecam = true;


    private float touchTime = 0f;
    private const float debounceTime = 0.25f;

    private const float horizontalMultiplier = 60f, verticalMultiplier = 50f;

    void Start()
    {
        key = this.transform.name;
        firstpage = cam.transform.Find("Model/buttons/first page").gameObject;
        secondpage = cam.transform.Find("Model/buttons/second page").gameObject;
        
        secondpage.SetActive(false);
        lastbutton.SetActive(false);



    }


    private void OnTriggerEnter(Collider other)
    {
        if (touchTime + debounceTime >= Time.time) return;

        if (other.TryGetComponent(out GorillaTriggerColliderHandIndicator component) && !component.isLeftHand)
        {
            GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(211, component.isLeftHand, 0.12f);
            GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 2f, GorillaTagger.Instance.tapHapticDuration);
            if (key == "Cube (3)")
            {
                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.bodyCollider.transform;
                cam.transform.localPosition = new Vector3(-0.1138f, 0.4514f, -1.3654f);
                cam.transform.localRotation = Quaternion.Euler(10.1839f, 10.6935f, 0.0496f);
            }
            if (key == "Cube (4)")
            {

                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.bodyCollider.transform;
                cam.transform.localPosition = new Vector3(-1.4138f, 0.3514f, - 0.3654f);
                cam.transform.localRotation = Quaternion.Euler(10.184f, 80.341f, 0.0496f);
            }
            if (key == "Cube (5)")
            {

                Debug.Log("TPC hit");
                freecam = false;
                cam.transform.Find("Model").gameObject.SetActive(false);
                cam.transform.parent = GorillaTagger.Instance.bodyCollider.transform;
                cam.transform.localPosition = new Vector3(-0.1138f, 0.1514f, 1.1346f);
                cam.transform.localRotation = Quaternion.Euler(358.184f, 180.239f, 0.0496f);
            }
            if (key == "next page")
            {
                lastbutton.SetActive(true);
                nextbutton.SetActive(false);
               firstpage.SetActive(false);
               secondpage.SetActive(true);
            }
            if (key == "last page")
            {
                nextbutton.SetActive(true);
                lastbutton.SetActive(false);
                firstpage.SetActive(true);
                secondpage.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (touchTime + debounceTime >= Time.time) return;

        if (other.TryGetComponent(out GorillaTriggerColliderHandIndicator component) && !component.isLeftHand )
        {
            touchTime = Time.time;

            GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(212, component.isLeftHand, 0.12f);
            GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 2f, GorillaTagger.Instance.tapHapticDuration);
        }
    }
    void Update()
    {
        if (freecam)
        {
            if (ControllerInputPoller.instance.leftControllerPrimaryButton)
            {
                cam.transform.parent = GorillaLocomotion.Player.Instance.leftControllerTransform.transform;
                cam.transform.localPosition = new Vector3(0.0084f, - 0.0395f, - 0.0255f);
                cam.transform.localRotation = Quaternion.Euler(5.0512f, 111.41f, 60.4071f);
            }
            else
            {
                cam.transform.parent = null;
            }
        }



    }


}


