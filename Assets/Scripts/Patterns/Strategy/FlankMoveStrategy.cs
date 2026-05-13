using UnityEngine;

public class FlankMoveStrategy : IMovementStrategy
{
    private float _speed;
    private float _flankOffset;

    public FlankMoveStrategy(float speed, float flankOffset = 3f)
    {
        _speed = speed;
        _flankOffset = flankOffset;
    }

    public void Move(Transform self, Transform target)
    {
        Vector3 dirToTarget = (target.position - self.position).normalized;
        Vector3 flank = Vector3.Cross(dirToTarget, Vector3.up) * _flankOffset;
        Vector3 destination = target.position + flank;
        Vector3 moveDir = (destination - self.position).normalized;
        self.position += moveDir * _speed * Time.deltaTime;
    }
}