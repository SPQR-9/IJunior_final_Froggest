using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform _wayPoints;
    [SerializeField] private float _speed;

    private SpriteRenderer _sprite;

    private Transform[] _points;
    private int _currectPointIndex = 0;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _points = new Transform[_wayPoints.childCount];
        for (int i = 0; i < _wayPoints.childCount; i++)
        {
            _points[i] = _wayPoints.GetChild(i);
        }
    }

    private void Update()
    {
        Transform target = _points[_currectPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        if (target.position.x > transform.position.x)
            _sprite.flipX = true;
        else
            _sprite.flipX = false;
        if (transform.position == target.position)
        {
            _currectPointIndex++;
            if(_currectPointIndex>=_points.Length)
            {
                _currectPointIndex = 0;
            }
        }
    }
}
