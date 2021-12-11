using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPort : Singleton<ViewPort>
{
    float minX = -5.85f;
    float maxX = 1.9f;
    float minY = -4.56f;
    float maxY = 4.56f;

    public Vector3 PlayerMoveablePosition(Vector3 PlayerPostion) {
        Vector3 position = Vector3.zero;
        position.x = Mathf.Clamp(PlayerPostion.x, minX, maxX);
        position.y = Mathf.Clamp(PlayerPostion.y, minY, maxY);
        return position;
    }
}
