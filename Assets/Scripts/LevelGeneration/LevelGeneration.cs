using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    //Lists for the easy normal and hard presets
    [SerializeField] private GameObject[] levelPrefabsEasy;
    [SerializeField] private GameObject[] levelPrefabsNormal;
    [SerializeField] private GameObject[] levelPrefabsHard;

    //swap this with the score accual score system once that is finished this is just for testing
    [SerializeField] private int score;

    //Generates a random level preset based on score
    public void PresetSpawner()
    {
        //checks the score (accual score numbers are tbd)
        if (score < 10 && score < 20)
        {
            //gets a random preset from the easy presets
            int rando = Random.Range(0, levelPrefabsEasy.Length);
            Debug.Log(rando);
            //stantiates the level preset in the right place
            Instantiate(levelPrefabsEasy[rando], new Vector2(transform.position.x + transform.GetChild(0).localScale.x, transform.position.y), Quaternion.identity);
        } else if (score >= 20 && score < 40)
        {
            int rando = Random.Range(0, levelPrefabsNormal.Length);
            Instantiate(levelPrefabsNormal[rando], new Vector2(transform.position.x + transform.GetChild(0).localScale.x, transform.position.y), Quaternion.identity);
        } else if (score >= 40)
        {
            int rando = Random.Range(0, levelPrefabsHard.Length);
            Instantiate(levelPrefabsHard[rando], new Vector2(transform.position.x + transform.GetChild(0).localScale.x, transform.position.y), Quaternion.identity);
        }
    }
}
