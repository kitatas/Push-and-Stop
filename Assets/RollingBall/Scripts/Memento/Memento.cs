using UnityEngine;

public sealed class Memento
{
    private readonly Vector3 _position;

    public Memento(Vector3 position)
    {
        _position = position;
    }

    public Vector3 GetPosition() => _position;
}