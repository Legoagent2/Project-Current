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
        public Vector3 testCoordinates = new Vector3(0, 0, 0);
        public GameObject spawnPoint = null;
        public Transform unitClass;
        public Units.BasicUnit unit;

        private void Awake()
        {
            Debug.Log("Awake" + unit.name);
            instance = this;
        }

        public void SetActionButtons(PlayerActions actions, GameObject spawnLocation)
        {
            Debug.Log("SetActionButtons" + unit.name);
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
            Debug.Log("ClearActions" + unit.name);
            foreach (Button btn in buttons)
            {
                Destroy(btn.gameObject);
            }
            buttons.Clear();
        }

        public void StartSpawnTimer(string objectToSpawn)
        {
            Debug.Log("StartSpawnTimer" + unit.name);
            spawnQueue.Add(unit.spawnTime);
            spawnOrder.Add(unit.playerPrefab);
            Debug.Log("IsUnit" + unit.name);
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
            Debug.Log("SpawnObject" + unit.name);
            GameObject spawnedObject = Instantiate(unit.playerPrefab, new Vector3(spawnPoint.transform.parent.position.x, spawnPoint.transform.parent.position.y, spawnPoint.transform.parent.position.z), Quaternion.identity, unitClass);
            spawnedObject.GetComponent<Units.Player.PlayerUnits>().baseStats = unit.baseStats;
        }
    }
}