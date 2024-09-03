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
           
            gameObject.layer = 18;
           

           

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
