using UnityEngine;

public class LootManager : MonoBehaviour
{
    //private
    private int _nbOfCoin = 0;
    
    //Serializefield
    [SerializeField] private GameObject coin;


    public void GenerateLoot(Vector3 position, Quaternion rotation)
    {
        Instantiate(coin, position, rotation);
    }
}