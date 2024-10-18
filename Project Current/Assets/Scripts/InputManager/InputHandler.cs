using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using JC.FDG.Units.Player;

namespace JC.FDG.InputManager
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance;
        private RaycastHit hit; // what we hit
        public List<Transform> selectedUnits = new List<Transform>();
        public Transform selectedBuilding = null;
        public LayerMask interactableLayer = new LayerMask();
        private bool isDragging = false;
        private Vector3 mousePos;

        private void Awake()
        {
            instance = this;
        }

        private void OnGUI()
        {
            if (isDragging)
            {
                Rect rect = MultiSelect.GetScreenRect(mousePos, Input.mousePosition);
                MultiSelect.DrawScreenRect(rect, new Color(0f, 0f, 0f, 0.25f));
                MultiSelect.DrawScreenRectBorder(rect, 3, Color.blue);
            }
        }

        public void HandleUnitMovement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                mousePos = Input.mousePosition;
                //create ray
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100, interactableLayer))//check if we hit something
                {
                    if (addedUnit(hit.transform, Input.GetKey(KeyCode.LeftShift)))
                    {
                        // be able to do stuff with units
                    }
                    else if (addedBuilding(hit.transform))
                    {
                        //be able to do stuff with building
                    }
                    /*LayerMask layerHit = hit.transform.gameObject.layer;
                    switch (layerHit.value)
                    {
                        case 8: //units layer
                            SelectUnit(hit.transform, Input.GetKey(KeyCode.LeftShift));
                            break;
                        default: // if none of the above happens

                            isDragging = true;
                            DeselectUnit();
                            break;
                    }*/
                }
                else
                {
                    isDragging = true;
                    DeselectUnit();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                foreach (Transform child in Player.PlayerManager.instance.playerUnits)
                {
                    foreach (Transform unit in child)
                    {
                        if (IsWithinSelectionBounds(unit))
                        {
                            addedUnit(unit, true);
                        }
                    }
                }
                isDragging = false;
            }

            if (Input.GetMouseButtonDown(1) && HaveSelectedUnits())
            {
                //create ray
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))//check if we hit something
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;
                    switch (layerHit.value)
                    {
                        case 8: //units layer
                            //placeholder
                            break;
                        case 9:
                            //attackk or set traget
                            break;
                        default: // if none of the above happens
                            foreach (Transform unit in selectedUnits)
                            {
                                PlayerUnits pU = unit.gameObject.GetComponent<PlayerUnits>();
                                pU.MoveUnit(hit.point);
                            }
                            break;
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1) && selectedBuilding != null)
            {
                selectedBuilding.gameObject.GetComponent<Interactables.IBuilding>().SetSpawnMarkerLocation();
            }
        }
        /*private void SelectUnit(Transform unit, bool canMultiselect = false)
        {
            if (!canMultiselect)
            {
                DeselectUnit();
            }
            selectedUnits.Add(unit);
            //set an obj on the unit 'highlight'
            unit.Find("Highlight").gameObject.SetActive(true);
        }*/

        private void DeselectUnit()
        {
            if (selectedBuilding)
            {
                selectedBuilding.gameObject.GetComponent<Interactables.IBuilding>().OnInteractExit();
                selectedBuilding = null;
            }
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                selectedUnits[i].gameObject.GetComponent<Interactables.IUnit>().OnInteractExit();
                //selectedUnits[i].Find("Highlight").gameObject.SetActive(false);
            }
            selectedUnits.Clear();
        }

        private bool IsWithinSelectionBounds(Transform tf)
        {
            if (!isDragging)
            {
                return false;
            }

            Camera cam = Camera.main;
            Bounds vpBounds = MultiSelect.GetVPBounds(cam, mousePos, Input.mousePosition);
            return vpBounds.Contains(cam.WorldToViewportPoint(tf.position));
        }

        private bool HaveSelectedUnits()
        {
            if (selectedUnits.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Interactables.IUnit addedUnit(Transform tf, bool canMultiselect = false)
        {
            Interactables.IUnit iUnit = tf.GetComponent<Interactables.IUnit>();
            if (iUnit)
            {
                if (!canMultiselect)
                {
                    DeselectUnit();
                }
                selectedUnits.Add(iUnit.gameObject.transform);
                iUnit.OnInteractEnter();
                return iUnit;
            }
            else
            {
                return null;
            }
        }

        private Interactables.IBuilding addedBuilding(Transform tf)
        {
            Interactables.IBuilding iBuilding = tf.GetComponent<Interactables.IBuilding>();
            if (iBuilding)
            {
                DeselectUnit();
                selectedBuilding = iBuilding.gameObject.transform;
                iBuilding.OnInteractEnter();
                return iBuilding;
            }
            else
            {
                return null;
            }
        }
    }
}