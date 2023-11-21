using System;
using System.Collections.Generic;
using System.Text;
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


  

            onPressButton = new UnityEngine.Events.UnityEvent();
            onPressButton.AddListener(new UnityEngine.Events.UnityAction(ButtonActivation));

        }

        public void ButtonActivation()
        {
            isOn = !isOn;

            Manager manager = FindAnyObjectByType<Manager>();
            manager.clicked(this.gameObject.name);
        }
    }
}
