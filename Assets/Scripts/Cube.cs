using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Vector3 _scale;
    private float _childMakeChance = 1f;

    public Vector3 Scale => _scale;
    public float ChildMakeChance => _childMakeChance;


    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Initialize(Vector3 scale, float chance)
    {
        _scale = scale;
        transform.localScale = _scale;
        _childMakeChance = chance;
        RandomizeColor();
        gameObject.SetActive(true);
    }

    private void RandomizeColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }
}