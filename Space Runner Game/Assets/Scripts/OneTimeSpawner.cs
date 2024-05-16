using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeSpawner : MonoBehaviour
{
    public List<GameObject> SpawnsTrack = new List<GameObject>();
    public GameObject SpawnNext;

    // Start is called before the first frame update
    void Start()
    {
        PathSpawner.roadNumber = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PathSpawner.roadNumber = Random.Range(5, SpawnsTrack.Count);
            SpawnsTrack[PathSpawner.roadNumber].gameObject.SetActive(true);
            SpawnsTrack[PathSpawner.roadNumber].transform.position = new Vector3(SpawnNext.transform.position.x, SpawnNext.transform.position.y, SpawnNext.transform.position.z);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
