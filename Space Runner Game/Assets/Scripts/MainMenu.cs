using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEditor.SearchService;

public class MainMenu : MonoBehaviour
{
    public int[] savestate;
    public int playerstate;
    public TMP_Text TotalTextCoins;
    public int Totalcoins;
    public GameObject[] BuyPlayerButtons, Players;
    public static int selectedPlayer;
    public GameObject ContinueButton, unlockButton;
    public int[] PlayerPrices;
    public GameObject PopUpbox;
    private int oneTime;
    // Start is called before the first frame update
    void Start()
    {
        oneTime = PlayerPrefs.GetInt("onetime", 0);
        if (oneTime == 0)
        {
            oneTime = 1;
            savestate[0] = 1;
            PlayerPrefs.SetInt("unlockPlayer" + 0, savestate[0]);
            PlayerPrefs.SetInt("onetime", oneTime);
        }

        for (int i = 0; i < savestate.Length; i++)
        {
            if (playerstate == 0)
            {
                savestate[i] = PlayerPrefs.GetInt("unlockPlayer" + i, 0);
            }
            else if (playerstate == 1)
            {
                savestate[i] = 1;
            }
        }
        Totalcoins = PlayerPrefs.GetInt("coins", Totalcoins);
        Players[selectedPlayer].SetActive(true);
        TotalTextCoins.text = Totalcoins.ToString();
        if (savestate[selectedPlayer] == 0)
        {
            ContinueButton.gameObject.SetActive(false);
            unlockButton.gameObject.SetActive(true);
            BuyPlayerButtons[selectedPlayer].gameObject.SetActive(true);
        }else
        if (savestate[selectedPlayer] == 1)
        {
            ContinueButton.gameObject.SetActive(true);
            unlockButton.gameObject.SetActive(false);
            BuyPlayerButtons[selectedPlayer].gameObject.SetActive(false);
        }

    }
    // Update is called once per frame
    void Update()
    {
      
    }
    public void NextChar()
    {
        TotalTextCoins.text = Totalcoins.ToString();
        BuyPlayerButtons[selectedPlayer].SetActive(false);
        Players[selectedPlayer].SetActive(false);
        selectedPlayer++;
        if (selectedPlayer == Players.Length)
        {
            selectedPlayer = 0;
        }
        Players[selectedPlayer].SetActive(true);
        BuyPlayerButtons[selectedPlayer].SetActive(true);
        if (selectedPlayer == 0)
        {
            BuyPlayerButtons[0].SetActive(false);
        }
        if (savestate[selectedPlayer] == 0)
        {
            ContinueButton.gameObject.SetActive(false);
            unlockButton.gameObject.SetActive(true);
        }
        if (savestate[selectedPlayer] == 1)
        {
            ContinueButton.gameObject.SetActive(true);
            unlockButton.gameObject.SetActive(false);
            BuyPlayerButtons[selectedPlayer].gameObject.SetActive(false);
        }
    }
    public void PreChar()
    {
        TotalTextCoins.text = Totalcoins.ToString();
        Players[selectedPlayer].SetActive(false);
        BuyPlayerButtons[selectedPlayer].SetActive(false);
        selectedPlayer--;
        if (selectedPlayer < 0)
        {
            selectedPlayer = Players.Length - 1;
            selectedPlayer = BuyPlayerButtons.Length - 1;
        }
        Players[selectedPlayer].SetActive(true);
        BuyPlayerButtons[selectedPlayer].SetActive(true);
        if (selectedPlayer == 0)
        {
            BuyPlayerButtons[0].SetActive(false);
        }
        if (savestate[selectedPlayer] == 0)
        {
            ContinueButton.gameObject.SetActive(false);
            unlockButton.gameObject.SetActive(true);
        }
        if (savestate[selectedPlayer] == 1)
        {
            ContinueButton.gameObject.SetActive(true);
            unlockButton.gameObject.SetActive(false);
            BuyPlayerButtons[selectedPlayer].gameObject.SetActive(false);
        }
    }
    public void CharacterSelection(int unlock)
    {
        if (Totalcoins >= PlayerPrices[selectedPlayer])
        {
            Totalcoins -= PlayerPrices[selectedPlayer];
            savestate[selectedPlayer] = 1;
            PlayerPrefs.SetInt("unlockPlayer" + selectedPlayer, savestate[selectedPlayer]); // Int Value Save into Variable
            PlayerPrefs.SetInt("totalcoin", Totalcoins);
            PlayerPrefs.SetInt("selecterPlayer", selectedPlayer);
            BuyPlayerButtons[selectedPlayer].gameObject.SetActive(false);
            ContinueButton.gameObject.SetActive(true);
            TotalTextCoins.text = Totalcoins.ToString();
        }
        else
        {
            PopUpbox.gameObject.SetActive(true);
        }
    }
    public void EarthLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void MoonLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

    }
    public void MarsLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);

    }
    public void Gems1000()
    {
        IAPManager.Instance.BuyConsumableGOLD1000();
    }
    public void Gems5000()
    {
        IAPManager.Instance.BuyConsumableGOLD5000();
    }
    public void Gems10000()
    {
        IAPManager.Instance.BuyConsumableGOLD10000();
    }
    public void Gems20000()
    {
        IAPManager.Instance.BuyConsumableGOLD20000();
    }
    public void popupback()
    {
        PopUpbox.gameObject.SetActive(false);
    }
}
