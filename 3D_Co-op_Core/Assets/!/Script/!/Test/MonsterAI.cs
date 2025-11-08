using Unity.Netcode;

public class MonsterAI : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!HasAuthority)
        {
            return;
        }
        // Authority monster init script here
        base.OnNetworkSpawn();
    }

    private void Update()
    {
        if (!IsSpawned || !HasAuthority)
        {
            return;
        }
        // Authority updates monster AI here
    }
}

