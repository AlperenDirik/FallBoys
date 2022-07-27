using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] Transform _target; //kameraya karakterimi hedef g�steriyorum
    [SerializeField] Vector3 _offset; // mesafe
    [SerializeField] float _chasingSpeed = 5; //takip etme h�z�

    private void Start()
    {
        if (!_target)
        {
            _target = GameObject.FindObjectOfType<PlayerMovement>().transform;
        }   //targeti b�yle buluyorum
    }
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _chasingSpeed * Time.deltaTime); 
        //position'dan target'a kovalama h�z� kadar kameram takip etsin
    }
}
