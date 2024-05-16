using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPick : MonoBehaviour
{
    
    public float _Speed;
    public GameObject _hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _Speed));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //  other.gameObject.GetComponent<PlayerMovement>()._CurrentCoins ++;
            //  gameObject.GetComponent<MeshRenderer>().enabled = false;
            //  gameObject.GetComponent<BoxCollider>().enabled = false;
            if (PlayerHitFront.MissionState == 2)
            {
                Instantiate(_hitEffect, gameObject.transform.position, Quaternion.identity);
                StartCoroutine(ObjOff());
            }
        }
    }
    IEnumerator ObjOff()
    {
            yield return new WaitForSeconds(1.5f);
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
