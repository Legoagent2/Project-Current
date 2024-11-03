using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceHandler : MonoBehaviour
{
    public static ResourceHandler instance;
    public int noUnits;
    public float crystalAmount;
    public TextMeshProUGUI crystalDisplay;
    public GameObject gameOverText;
    public bool gameVictory;

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
        crystalDisplay.text = crystalAmount.ToString();
    }

    public void runVictory()
    {
        if (gameVictory)
        {
            gameOverText.SetActive(true);
        } 
    }
}
