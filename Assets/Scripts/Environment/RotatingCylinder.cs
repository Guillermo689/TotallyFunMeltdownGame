using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCylinder : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _speedPerSecond;
    void Start()
    {
        StartCoroutine(RotationMultiplier());
    }
    void Update()
    {
        Rotate();
    }
    IEnumerator RotationMultiplier()                //This will make the Cylinder roll faster every second
    {
        yield return new WaitForSeconds(1f);
        _rotationSpeed += _speedPerSecond;
        StartCoroutine(RotationMultiplier());
    }
    private void Rotate()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }
}
