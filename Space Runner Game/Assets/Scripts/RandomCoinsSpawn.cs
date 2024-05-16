using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCoinsSpawn : MonoBehaviour
{
    public GameObject[] CoinsPack;
    int _random;
    // Start is called before the first frame update
    void Start()
    {
        _random = Random.Range(0, CoinsPack.Length);
        for(int i = 0; i < _random; i++)
        {
            CoinsPack[i].gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
