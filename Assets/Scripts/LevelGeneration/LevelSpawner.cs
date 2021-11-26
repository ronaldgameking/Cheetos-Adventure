using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    //needed to be able to call the preset spawner
    private LevelGeneration levelGeneration;

    private void Awake()
    {
       levelGeneration = GetComponentInParent<LevelGeneration>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //calls the preset spawner to spawn a preset
        levelGeneration.PresetSpawner();
    }
}
