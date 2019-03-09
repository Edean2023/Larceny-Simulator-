using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    // Start is called at the start of the game
    void Start()
    {
        // Randomizes when the enemy turns
        InvokeRepeating("Patrol", 0f, Random.Range(2f, 5f));
    }

    // sets values for raycasting for sight
    public Transform sightStart, sightEnd;
    // bool for detecting if the player is spotted (default is false)
    public bool spotted = false;

    // bool for if the enemy is facing left (default is true)
    public bool facingLeft = true;
    // game object for the dettection mark
    public GameObject arrow;
    public GameObject Question;

    // bool for detecting if the player is spotted (default is false)
    public bool heard = false;
    // sets values for raycasting for sight
    public Transform heardStart, heardEnd;

    // Update is called once per frame
    void Update()
    {
        // calls different functions and calls them once per frame
        Raycasting();
        Behaviors();
    }

    // handles the raycasting so the enemy can see the player
    void Raycasting()
    {
        // Sees if the enemy spots the player
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
        spotted = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));

        // Sees if the enemy spots the player
        Debug.DrawLine(heardStart.position, heardEnd.position, Color.red);
        heard = Physics2D.Linecast(heardStart.position, heardEnd.position, 1 << LayerMask.NameToLayer("Player"));
    }  

    // handles the behavior of the enemy
     public void Behaviors()
    {
        // Sets the spotted arrow to true and false depending on if the player is spotted by the enemy
        if (spotted == true)
        {
            heard = false;
            arrow.SetActive(true);
        }

        else
        {
            arrow.SetActive(false);
        }

        if (heard == true)
        {
            Question.SetActive(true);
        }

        else
        {
            Question.SetActive(false);
        }

    }

    // handles the enemys patrol
    void Patrol()
    {
        facingLeft = !facingLeft;

        if (facingLeft == true)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

    }

}