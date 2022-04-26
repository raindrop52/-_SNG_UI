using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineCard : MonoBehaviour
{
    [SerializeField] float _showTime = 60.0f;
    [SerializeField] float _time = 0.0f;
    Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _time = 0.0f;
    }

    void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;

        if(_time >= _showTime)
        {
            _time = 0.0f;
            _anim.SetTrigger("Shine");
        }
    }
}
