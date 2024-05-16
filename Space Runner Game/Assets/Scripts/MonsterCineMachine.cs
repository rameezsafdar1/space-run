using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterCineMachine : MonoBehaviour
{
    public GameObject VirtualCameraobj,Target;
    [SerializeField] bool Isrun;

    private int onetime;
    // Start is called before the first frame update
    void Start()
    {
        Isrun = false;
        onetime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerHitFront.MissionState == 1 && onetime == 0)
        {
           // gameObject.GetComponent<Animator>().SetInteger("monster", 1);
            VirtualCameraobj.gameObject.SetActive(true);
            StartCoroutine(MonsterRun());
            onetime = 1;
        }
        if (Isrun)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Target.transform.position, 7 * Time.deltaTime);
        }
    }
    IEnumerator MonsterRun()
    {
       // gameObject.GetComponent<Animator>().SetInteger("monster", 2);
        Isrun = true;
        yield return new WaitForSeconds(2f);
        VirtualCameraobj.gameObject.SetActive(false);

    }
}
