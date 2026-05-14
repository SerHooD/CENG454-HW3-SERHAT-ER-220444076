using UnityEngine;

public class RapidFirePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            player.UpgradeToRapidFire();
            gameObject.SetActive(false);
        }
    }
}