using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    private Vector3 _mousePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _mousePosition = Input.mousePosition;
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
        if(_mousePosition.magnitude <= _maxDistance)
            transform.position = new Vector3(_mousePosition.x, _mousePosition.y, 0);
    }
}
