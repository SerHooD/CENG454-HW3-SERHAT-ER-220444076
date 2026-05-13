using UnityEngine;

public class DirectMoveStrategy : IMovementStrategy
{
    private float _speed;

    public DirectMoveStrategy(float speed)
    {
        _speed = speed;
    }

    public void Move(Transform self, Transform target)
    {
        Vector3 dir = (target.position - self.position).normalized;
        self.position += dir * _speed * Time.deltaTime;
    }
}