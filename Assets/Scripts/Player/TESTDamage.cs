using FishNet.Object;
using UnityEngine;

public class TESTDamage : NetworkBehaviour, IDamageable
{
    public void ServerGetDamage(int damage)
    {
        Debug.Log($"Get {damage} damage on Server");
    }
}
