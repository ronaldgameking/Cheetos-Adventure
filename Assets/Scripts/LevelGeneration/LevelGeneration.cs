using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    //Lists for the easy normal and hard presets
    private GameObject[] levelPrefabsEasy;
    private GameObject[] levelPrefabsNormal;
    private GameObject[] levelPrefabsHard;

    private void Awake()
    {
        //gets all the prefabs from the right folders || sponsored by stackoverflow: https://stackoverflow.com/questions/53968958/how-can-i-get-all-prefabs-from-a-assets-folder-getting-not-valid-cast-exception and unity documentation: https://docs.unity3d.com/ScriptReference/Resources.Load.html
        levelPrefabsEasy = Resources.LoadAll<GameObject>("LevelPresets/Easy");
        levelPrefabsNormal = Resources.LoadAll<GameObject>("LevelPresets/Normal");
        levelPrefabsHard = Resources.LoadAll<GameObject>("LevelPresets/Hard");
    }

    /// <summary>
    /// Generates the next preset based on score
    /// </summary>
    public void PresetSpawner()
    {
        //checks the score (accual score numbers are tbd)
        if (ScoreSystem.Instance.Score >= 0 && ScoreSystem.Instance.Score < 80)
        {
            Debug.Log("spawn1");
            //gets a random preset from the easy presets
            int rando = Random.Range(0, levelPrefabsEasy.Length); 
            Debug.Log(rando);
            //stantiates the level preset in the right place
            Instantiate(levelPrefabsEasy[rando], new Vector2(transform.position.x + transform.GetChild(0).localScale.x, transform.position.y), Quaternion.identity);
        } 
        else if (ScoreSystem.Instance.Score >= 80 && ScoreSystem.Instance.Score < 150)
        {
            Debug.Log("spawn2");

            int rando = Random.Range(0, levelPrefabsNormal.Length);
            Instantiate(levelPrefabsNormal[rando], new Vector2(transform.position.x + transform.GetChild(0).localScale.x, transform.position.y), Quaternion.identity);
        } 
        else if (ScoreSystem.Instance.Score >= 150)
        {
            Debug.Log("spawn3");

            int rando = Random.Range(0, levelPrefabsHard.Length);
            Instantiate(levelPrefabsHard[rando], new Vector2(transform.position.x + transform.GetChild(0).localScale.x, transform.position.y), Quaternion.identity);
        }
    }
}
