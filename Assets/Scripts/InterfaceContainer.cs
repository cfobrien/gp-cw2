using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Placeables;

public interface IPlayer {
    void ApplyDamage(int damage);
    void Move(Vector3 keyboardDir);
}

public interface IPlaceable {
    void MoveTo(Transform transform);
    void MoveTo(Vector3 postion);
    void RotateAround(Vector3 point, Vector3 axis, float angle);
    // void RotateTo(Vector3 axis, float angle);
    void MoveBy(Vector3 offset);
    void Rotate(float angle);
}

public interface INPC {
    void RandomWalk(Vector3 normal, float variance);
    void Face(Transform transform);
}
