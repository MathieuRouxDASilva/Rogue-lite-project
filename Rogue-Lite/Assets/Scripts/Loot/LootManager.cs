using UnityEngine;

public class LootManager : MonoBehaviour
{
    //Serializefield
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject coinOf5;
    [SerializeField] private GameObject hp;


    public void GenerateLoot(Vector3 position)
    {
        int random = Random.Range(0, 5);


        switch (random)
        {
            case 0:
                Instantiate(coin, position, Quaternion.identity);
                break;
            case 1:
                Instantiate(coinOf5, position, Quaternion.identity);
                break;
            case 2:
                Instantiate(hp, position, Quaternion.identity);
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
}