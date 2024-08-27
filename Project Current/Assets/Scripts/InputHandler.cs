using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JC.FDG.InputManager
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance;
        private RaycastHit hit; // what we hit
                                // Start is called before the first frame update
        private List<Transform> selectedUnits = new List<Transform>();
        private bool isDragging = false;
        private Vector3 mousePos;

        void Start()
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
                mousePos = Input.mousePosition;
                //create ray
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))//check if we hit something
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;
                    switch (layerHit.value)
                    {
                        case 8: //units layer
                            SelectUnit(hit.transform, Input.GetKey(KeyCode.LeftShift));
                            break;
                        default: // if none of the above happens

                            isDragging = true;
                            DeselectUnit();
                            break;
                    }
                }
                //shoot ray to see if we hit our unit
                // if so, do something
            }

            if (Input.GetMouseButtonUp(0))
            {
                foreach (Transform child in Player.PlayerManager.instance.playerUnits)
                {
                    foreach (Transform unit in child)
                    {
                        if (IsWithinSelectionBounds(unit))
                        {
                            SelectUnit(unit, true);
                        }
                    }
                }
                isDragging = false;
            }
        }
        private void SelectUnit(Transform unit, bool canMultiselect = false)
        {
            if (!canMultiselect)
            {
                DeselectUnit();
            }
            selectedUnits.Add(unit);
            //set an obj on the unit 'highlight'
            unit.Find("Highlight").gameObject.SetActive(true);
        }

        private void DeselectUnit()
        {
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                selectedUnits[i].Find("Highlight").gameObject.SetActive(false);
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
    }
}