using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _splitChance = 1.0f;

    public event Action<float, Vector3, Vector3, Cube> CubeClicked;

    private void OnMouseDown()
    {
        CubeClicked?.Invoke(_splitChance, transform.position, transform.localScale, this);
        Destroy(gameObject);
    }
}