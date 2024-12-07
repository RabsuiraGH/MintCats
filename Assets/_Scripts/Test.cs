using System;
using Core.Character;
using Core.Character.Player;
using Core.GameEventSystem;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private CharacterManager character;

    [EasyButtons.Button]
    private void SetDirection(Vector3 movementDirection)
    {
        character.CharacterLocomotionManager.SetMovementDirection(movementDirection);
    }

    [EasyButtons.Button]
    private void SetSpeed(float movementSpeed)
    {
        character.CharacterLocomotionManager.SetTargetMovementSpeed(movementSpeed);
    }

    [EasyButtons.Button]
    private void SetRunning(bool running)
    {
        character.CharacterLocomotionManager.SetRunning(running);
    }
}