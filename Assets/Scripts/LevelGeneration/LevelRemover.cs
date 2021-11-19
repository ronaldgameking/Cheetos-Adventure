using UnityEngine;

public class LevelRemover : MonoBehaviour
{
    //used to be able to delte the level preset
    [SerializeField] private GameObject parent;
    //removes the previous level
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //destroys the parent (level preset)
        Destroy(parent);
    }
}
