using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VitaeAnimation : MonoBehaviour
{
    public PlayerData player;
    private void Update() {
        SetArrowOrientation();
    }
    public void SetArrowOrientation() {
        float rot_z = Mathf.Atan2(player.lookDirection.y, player.lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}