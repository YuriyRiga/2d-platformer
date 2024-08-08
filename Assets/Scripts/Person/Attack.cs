using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float _damage;

    public float Damage => _damage;
}
