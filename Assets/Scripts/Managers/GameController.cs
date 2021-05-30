using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerHealth player;
    public List<RoomController> rooms;

    private void Start() {
        player = FindObjectOfType<PlayerHealth>();
        player.onPlayerDie.AddListener(() => Respawn());
        rooms = FindObjectsOfType<RoomController>().ToList();
    }

    private void Respawn() {
        PlayerSpawner.getInstance().Respawn();
        ResetRooms();
    }
    private void ResetRooms() {
        foreach (RoomController room in rooms)
        {
            room.Reset();
        }
    }
}
