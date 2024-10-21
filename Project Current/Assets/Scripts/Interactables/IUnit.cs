using UnityEngine;

namespace JC.FDG.Interactables
{
    public class IUnit : Interactable
    {
        public override void OnInteractEnter()
        {
            base.OnInteractEnter();
            // add stuff
        }
        public override void OnInteractExit()
        {
            base.OnInteractExit();
        }
    }
}