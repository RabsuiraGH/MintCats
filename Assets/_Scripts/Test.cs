using System;
using Core.Character.Player;
using Core.GameEventSystem;
using UnityEngine;

public class EventBusExample : MonoBehaviour
{
    public PlayerInputManager PlayerManager;

    public Transform cube;

    private void Update()
    {
        cube.transform.position = cube.position + cube.forward * PlayerManager.ZoomInput;
    }
}