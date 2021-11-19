using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    //needed to be able to call the preset spawner
    [SerializeField] private LevelGeneration levelGeneration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //calls the preset spawner to spawn a preset
        levelGeneration.PresetSpawner();
    }
}
