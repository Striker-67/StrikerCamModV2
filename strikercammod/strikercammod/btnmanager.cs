using System;
using System.Collections.Generic;
using System.Text;
using strikercammod.mainmanager;
using UnityEngine;

namespace strikercammod.buttons
{
    internal class btnmanager : GorillaPressableButton
    {
        public override void Start()
        {
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            gameObject.layer = 18;
            mainmanager.Manager manager = FindAnyObjectByType<Manager>();
            if(this.gameObject.GetComponent<BoxCollider>().isTrigger & this.gameObject.layer == 18)
            {
                manager.assetsloaded += 1;
                manager.check();
            }
           

            onPressButton = new UnityEngine.Events.UnityEvent();
            onPressButton.AddListener(new UnityEngine.Events.UnityAction(ButtonActivation));

        }

        public void ButtonActivation()
        {
           

            Manager manager = FindAnyObjectByType<Manager>();
            manager.clicked(this.gameObject.name);
        }
    }
}
