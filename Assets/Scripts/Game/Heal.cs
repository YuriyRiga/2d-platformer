using UnityEngine;

public class Heal : Collectible
{
    [SerializeField] private float _value = 25f;
    public float Value => _value;
}