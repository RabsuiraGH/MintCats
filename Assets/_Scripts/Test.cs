using System;
using Core.Character.Player;
using Core.GameEventSystem;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Transform _cube;
    [SerializeField] private Transform _cube2;
    [SerializeField] private Transform _cube3;

    private void Update()
    {
        Debug.Log(($"New dl"));
        MoveCubes();
    }

    private void MoveCubes()
    {
        _cube.Translate(_cube.up * Time.deltaTime * 1f);
        _cube2.Translate(_cube2.forward * Time.deltaTime * 1f);
        _cube3.Translate(_cube3.right * Time.deltaTime * 1f);
    }
}