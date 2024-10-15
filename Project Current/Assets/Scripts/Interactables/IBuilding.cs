using UnityEngine;

namespace JC.FDG.Interactables
{
    public class IBuilding : Interactable
    {
        public UI.HUD.PlayerActions actions;
        

        public override void OnInteractEnter()
        {
            UI.HUD.ActionFrame.instance.SetActionButtons(actions);
            base.OnInteractEnter();
            // add stuff
        }
        public override void OnInteractExit()
        {
            UI.HUD.ActionFrame.instance.ClearActions();
            base.OnInteractExit();
        }
    }
}