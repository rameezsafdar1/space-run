using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteManager : MonoBehaviour
{
    public int MuteState;
    public GameObject[] MuteImage;


    // Start is called before the first frame update
    void Start()
    {
        MuteState = PlayerPrefs.GetInt("mute", 0);
        if (MuteState == 0)
        {
            AudioListener.pause = false;
            MuteImage[0].gameObject.SetActive(true);
            MuteImage[1].gameObject.SetActive(false);
            gameObject.GetComponent<Button>().image = MuteImage[0].GetComponent<Image>();        }
        else if (MuteState == 1)
        {
            AudioListener.pause = true;
            MuteImage[0].gameObject.SetActive(false);
            MuteImage[1].gameObject.SetActive(true);
            gameObject.GetComponent<Button>().image = MuteImage[1].GetComponent<Image>();
        }
    }
    public void MuteGame()
    {
        if (MuteState == 0)
        {
            MuteState = 1;
            PlayerPrefs.SetInt("mute", MuteState);
            AudioListener.pause = true;
            MuteImage[0].gameObject.SetActive(false);
            MuteImage[1].gameObject.SetActive(true);
            gameObject.GetComponent<Button>().image = MuteImage[0].GetComponent<Image>();
        }
        else if (MuteState == 1)
        {
            MuteState = 0;
            PlayerPrefs.SetInt("mute", MuteState);
            AudioListener.pause = false;
            MuteImage[1].gameObject.SetActive(false);
            MuteImage[0].gameObject.SetActive(true);
            gameObject.GetComponent<Button>().image = MuteImage[1].GetComponent<Image>();
        }
        }
    // Update is called once per frame
    void Update()
    {
        
    }
}
