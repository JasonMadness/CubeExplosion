using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Vector3 _scale;
    private float _childMakeChance = 1f;

    public Vector3 Scale => _scale;
    public float ChildMakeChance => _childMakeChance;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(Vector3 scale, float chance)
    {
        _scale = scale;
        transform.localScale = _scale;
        _childMakeChance = chance;
        RandomizeColor();
        ResetVelocity();
        
        gameObject.SetActive(true);
    }

    private void RandomizeColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    private void ResetVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}