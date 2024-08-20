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
        void Start()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void HandleUnitMovement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))//check if we hit something
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;
                    switch (layerHit.value)
                    {
                        case 8: //units layer
                            SelectUnit(hit.transform);
                            break;
                        default: // if none of the above happens
                            break;
                    }
                }
                //create ray
                //shoot ray to see if we hit our unit
                // if so, do something
            }
        }
        private void SelectUnit(Transform unit)
        {
            //set an obj on the unit 'highlight
            unit.Find("Highlight").gameObject.SetActive(true);
        }
    }
}