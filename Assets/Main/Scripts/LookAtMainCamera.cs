using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMainCamera : MonoBehaviour
{
    private Transform _camTrn;

    private Transform _thisTrn;

    private void Awake()
    {
        _camTrn = Camera.main.transform;
        _thisTrn = transform;
    }

    void Update()
    {
        _thisTrn.LookAt(_camTrn);
    }
}
