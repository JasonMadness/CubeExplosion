using UnityEngine;

public class GameSettings
{
    public Vector3 StartingCubeScale { get; } = Vector3.one * 4;
    public float StartingCubeChildChance { get; } = 1f;
    public int MinCubes { get; } = 2;
    public int MaxCubes { get; } = 7;
}
