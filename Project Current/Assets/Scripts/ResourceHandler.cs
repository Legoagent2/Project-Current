using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    public static ResourceHandler instance;
    public int noUnits;
    public int crystalAmount;
    public GameObject gameOverText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        noUnits = 3;
    }

    private void Update()
    {
        if (noUnits == 0)
        {
            Debug.Log("GAME OVERRRR");
            gameOverText.SetActive(true);
        }
    }
}
