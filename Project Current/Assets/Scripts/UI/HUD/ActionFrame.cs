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

        public List<float> spawnQueue = new List<float>();
        public List<GameObject> spawnOrder = new List<GameObject>();
        public List<Units.BasicUnit> spawnList = new List<Units.BasicUnit>();
        public Vector3 testCoordinates = new Vector3(0, 0, 0);
        public GameObject spawnPoint = null;
        public List<GameObject> unitParents = new List<GameObject>();
        private int spawnNum = 0;
        //public Transform unitClass;
        //public Units.BasicUnit unit;

        private void Awake()
        {
            //Debug.Log("Awake" + unit.name);
            instance = this;
        }

        public void SetActionButtons(PlayerActions actions, GameObject spawnLocation)
        {
            //Debug.Log("SetActionButtons" + unit.name);
            actionsList = actions;
            spawnPoint = spawnLocation;

            if (actions.basicUnits.Count > 0)
            {
                foreach (Units.BasicUnit unit in actions.basicUnits)
                {
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = unit.name;
                    GameObject icon = Instantiate(unit.icon, btn.transform);
                    //add text
                    buttons.Add(btn);
                }
            }
        }

        public void ClearActions()
        {
            //Debug.Log("ClearActions" + unit.name);
            foreach (Button btn in buttons)
            {
                Destroy(btn.gameObject);
            }
            buttons.Clear();
        }

        private Units.BasicUnit IsUnit(string name)
        {
            if (actionsList.basicUnits.Count > 0)
            {
                foreach(Units.BasicUnit unit in actionsList.basicUnits)
                {
                    if(unit.name == name)
                    {
                        return unit;
                    }
                }
            }
            return null;
        }

        private GameObject CheckUnit(Units.BasicUnit Check)
        {
            for (int index = 0; index < unitParents.Count; index++)
            {
                if (unitParents[index].name == Check.name)
                {
                    return unitParents[index];
                }
            }
            return unitParents[0];
        }

        public void StartSpawnTimer(string objectToSpawn)
        {
            if (IsUnit(objectToSpawn))
            {
                Units.BasicUnit unit = IsUnit(objectToSpawn);
                Debug.Log("StartSpawnTimer" + unit.name);
                spawnQueue.Add(unit.spawnTime);
                spawnList.Add(unit);
                spawnOrder.Add(unit.playerPrefab);
            }
            //Debug.Log("IsUnit" + unit.name);
            if (spawnQueue.Count == 1)
            {
                ActionTimer.instance.StartCoroutine(ActionTimer.instance.SpawnQueueTimer());
            }
            else if (spawnQueue.Count == 0)
            {
                ActionTimer.instance.StopAllCoroutines();
            }
        }

        public void SpawnObject()
        {
            Debug.Log("SpawnObject" + spawnList[spawnNum].name);
            GameObject unitParent = CheckUnit(spawnList[spawnNum]);
            GameObject spawnedObject = Instantiate(spawnOrder[0], new Vector3(spawnPoint.transform.parent.position.x, spawnPoint.transform.parent.position.y, spawnPoint.transform.parent.position.z), Quaternion.identity, unitParent.transform);
            spawnedObject.GetComponent<Units.Player.PlayerUnits>().baseStats.health = 50f;
            spawnedObject.GetComponent<Units.Player.PlayerUnits>().baseStats = spawnList[0].baseStats;
            spawnNum++;
        }
    }
}