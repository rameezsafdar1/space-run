using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    public List<GameObject> SpawnsTrack = new List<GameObject>();
    public GameObject SpawnNext;
    public static int roadNumber,previousNum;
    public List<int> uniqueNumbers;
    public static int onetime;
    // Start is called before the first frame update
    void Start()
    {
        uniqueNumbers = new List<int>();
        /*if (onetime == 0)
        {
            onetime = 1;
            roadNumber = 0;
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRandomList()
    {
       // previousNum = roadNumber;
        for (int i = 0; i < SpawnsTrack.Count; i++)
        {
            uniqueNumbers.Add(i);
        }
        uniqueNumbers.Remove(roadNumber);
      //  uniqueNumbers.Remove(previousNum);
        if(roadNumber <= 5)
        {
            roadNumber = uniqueNumbers[Random.Range(5, uniqueNumbers.Count)];
            Debug.Log("5 se kum wali line call howi hai");
        }else if(roadNumber >= 5)
        {
            roadNumber = uniqueNumbers[Random.Range(0, 5)];
            Debug.Log("5 se ziada wali line call howi hai");
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
       /* if(other.gameObject.tag == "Player") 
        {
            GenerateRandomList();
            Debug.Log(roadNumber);
           // Debug.Log(previousNum);
            SpawnsTrack[roadNumber].gameObject.SetActive(false);
            SpawnsTrack[roadNumber].transform.position = new Vector3(SpawnNext.transform.position.x, SpawnNext.transform.position.y, SpawnNext.transform.position.z);
            SpawnsTrack[roadNumber].gameObject.SetActive(true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(colliderenabled());
        }*/
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GenerateRandomList();
            Debug.Log(roadNumber);
            // Debug.Log(previousNum);
            SpawnsTrack[roadNumber].gameObject.SetActive(false);
            SpawnsTrack[roadNumber].transform.position = new Vector3(SpawnNext.transform.position.x, SpawnNext.transform.position.y, SpawnNext.transform.position.z);
            SpawnsTrack[roadNumber].gameObject.SetActive(true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(colliderenabled());
        }
    }
    IEnumerator colliderenabled()
    {
        yield return new WaitForSeconds(4f);
        uniqueNumbers.Clear();
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
