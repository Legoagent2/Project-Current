using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JC.FDG.UI.HUD
{
    public class ActionFrame : MonoBehaviour
    {
        public static ActionFrame instance = null;

        [SerializeField] private Button actionButton = null;
        [SerializeField] private Transform layoutGroup = null;

        private List<Button> buttons = new List<Button>();
        private PlayerActions actionsList = null;

        private void Awake()
        {
            instance = this;
        }

        public void SetActionButtons(PlayerActions actions)
        {
            actionsList = actions;
            if (actions.basicUnits.Count > 0)
            {
                foreach(Units.BasicUnit unit in actions.basicUnits)
                {
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = unit.name;
                    GameObject icon = Instantiate(unit.icon, btn.transform);
                    //add text
                    buttons.Add(btn);
                }
            }

            if (actions.basicBuildings.Count > 0)
            {
                foreach(Buildings.BasicBuilding building in actions.basicBuildings)
                {
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = building.name;
                    GameObject icon = Instantiate(building.icon, btn.transform);
                    //add text
                    buttons.Add(btn);
                }
            }
        }

        public void ClearActions()
        {
            foreach(Button btn in buttons)
            {
                Destroy(btn.gameObject);
            }
            buttons.Clear();
        }

        public void StartSpawnTimer(string objectToSpawn)
        {
            if (IsUnit(objectToSpawn))
            {

            }
            else if (Isbuilding(objectToSpawn))
            {

            }
            else
            {
                Debug.Log($"{objectToSpawn} is not a spawnable object");
            }
        }

        private Units.BasicUnit IsUnit(string name)
        {
            if (actionsList.basicUnits.Count > 0)
            {
                foreach(Units.BasicUnit unit in actionsList.basicUnits)
                {
                    if (unit.name == name)
                    {
                        return unit;
                    }
                }
            }
            return null;
        }

        private Buildings.BasicBuilding Isbuilding(string name)
        {
            if (actionsList.basicBuildings.Count > 0)
            {
                foreach (Buildings.BasicBuilding building in actionsList.basicBuildings)
                {
                    if (building.name == name)
                    {
                        return building;
                    }
                }
            }
            return null;
        }
    }
}


//public BuildingActions.BuildingUnits Units;
