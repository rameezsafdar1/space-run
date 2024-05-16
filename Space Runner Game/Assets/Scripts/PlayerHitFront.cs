using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitFront : MonoBehaviour
{
    public static int MissionState;
   // public GameObject[] PlayerArray;
    private AudioSource AS;
    public AudioClip hitSound,AfterHitSound;
    public GameObject deathParticle;
    // Start is called before the first frame update
    void Start()
    {
        MissionState = 0;
        AS=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "hurdle")
        {
           // player.GetComponent<Animator>().SetBool("die",true);
            if (!AS.isPlaying)
            {
                GameObject prefab = Instantiate(deathParticle, gameObject.transform.position, Quaternion.identity);
                AS.PlayOneShot(hitSound);
                StartCoroutine(afterhitSound());
                Destroy(prefab, 5);
            }
            MissionState = 3;
        }
    }
    IEnumerator afterhitSound()
    {
        yield return new WaitForSeconds(1.2f);
        if (!AS.isPlaying)
        {
            AS.PlayOneShot(AfterHitSound);
        }
    }
}
