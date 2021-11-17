using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject[] levelPrefabsEasy;
    [SerializeField] private GameObject[] levelPrefabsNormal;
    [SerializeField] private GameObject[] levelPrefabsHard;

    [SerializeField] private int score;

    //Generates a random level preset based on score
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (score < 10 && score < 20)
        {
            int rando = Random.Range(1, levelPrefabsEasy.Length);
            Instantiate(levelPrefabsEasy[rando], transform.position, Quaternion.identity);
        } else if (score > 20 && score < 40)
        {
            int rando = Random.Range(1, levelPrefabsNormal.Length);
            Instantiate(levelPrefabsNormal[rando], transform.position, Quaternion.identity);
        } else if (score > 40)
        {
            int rando = Random.Range(1, levelPrefabsHard.Length);
            Instantiate(levelPrefabsHard[rando], transform.position, Quaternion.identity);
        }
    }
}
