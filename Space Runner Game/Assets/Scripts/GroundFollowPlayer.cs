using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    public Vector3 _Value;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(_player.transform.position.x + _Value.x, _player.transform.position.y+_Value.y, _player.transform.position.z + _Value.z);
    }
}
