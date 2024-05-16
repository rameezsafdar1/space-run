using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SolarCams : MonoBehaviour
{
    [SerializeField] private LevelUnlock _lvlValue;
    [SerializeField] private GameObject[] cams;
    [SerializeField] private GameObject[] buttons,PurchaseButtons;
    [SerializeField] private GameObject LockImg;
    [SerializeField] private string[] names;
    [SerializeField] private TextMeshProUGUI planetNameText;
    private int currentPlanet,_totalCoins;

    private void Start()
    {
        _lvlValue.PlanetValue[0] = 1;
        PlanetValue();
    }
    private void PlanetValue()
    {
        
        if (_lvlValue.PlanetValue[currentPlanet] == 0)
        {
            for (int i = 0; i < PurchaseButtons.Length; i++)
            {
                PurchaseButtons[i].gameObject.SetActive(false);
            }
            PurchaseButtons[currentPlanet].gameObject.SetActive(true);
            LockImg.gameObject.SetActive(true);
            buttons[currentPlanet].gameObject.GetComponent<UnityEngine.UI.Button>().enabled = false;
            buttons[currentPlanet].gameObject.SetActive(false);
        }
        else if (_lvlValue.PlanetValue[currentPlanet] == 1)
        {
            for(int i = 0; i< PurchaseButtons.Length; i++)
            {
                PurchaseButtons[i].gameObject.SetActive(false);
            }
            
            LockImg.gameObject.SetActive(false);
            buttons[currentPlanet].gameObject.GetComponent<UnityEngine.UI.Button>().enabled = true;
            buttons[currentPlanet].gameObject.SetActive(true);
        }
    }
    public void unlockPlanet(int price)
    {
        _totalCoins = PlayerPrefs.GetInt("coins", 0);
        if (_totalCoins >= price)
        {
            _totalCoins -= price;
            _lvlValue.PlanetValue[currentPlanet] = 1;
            PlanetValue();
            PlayerPrefs.SetInt("coins", _totalCoins);
        }
        
    }
    public void NextPlanet(int x)
    {
        currentPlanet += x;
        if (currentPlanet < 0)
        {
            currentPlanet = cams.Length - 1;
        }
        if (currentPlanet >= cams.Length)
        {
            currentPlanet = 0;
        }

        for (int i = 0; i < cams.Length; i++)
        {
            cams[i].SetActive(false);
            buttons[i].SetActive(false);
        }
        cams[currentPlanet].SetActive(true);
        buttons[currentPlanet].SetActive(true);
        planetNameText.text = names[currentPlanet];
        PlanetValue();
    }
}
