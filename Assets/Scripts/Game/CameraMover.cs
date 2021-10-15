using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _shiftX;

    private void FixedUpdate()
    {
        if(_player!=null)
            transform.position = new Vector3(_player.transform.position.x - _shiftX, transform.position.y, transform.position.z);
    }
}
