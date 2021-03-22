using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Plots;

public interface IPlayer {
    void ApplyDamage(int damage);
    void Move(Vector3 keyboardDir);
}

public interface ITile {
    void MoveTo(Transform transform);
    void MoveTo(Vector3 postion);
    void RotateAround(Vector3 point, Vector3 axis, float angle);
    // void RotateTo(Vector3 axis, float angle);
    void Rotate(float angle);
}
