using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMove : MonoBehaviour
{
    [SerializeField] private PlaneMove planeMove;
    private List<GameObject> enemiesHolding = new List<GameObject>();
    private float maxSpeed;

    private void Start()
    {
        maxSpeed = planeMove.GetMaxSpeed();
    }

    public void Lock(Throwable enemy)
    {
        planeMove.SetMaxSpeed(0);
        enemiesHolding.Add(enemy.gameObject);
        enemy.getHit += Free;
        enemy.getHitForTheFirstTime += Free;
    }

    public void Free(GameObject enemyGameObject)
    {
        Throwable enemy = enemyGameObject.GetComponent<Throwable>();
        enemy.getHit -= Free;
        enemy.getHitForTheFirstTime -= Free;
        enemiesHolding.Remove(enemyGameObject);
        if (enemiesHolding.Count <= 0)
        {
            planeMove.SetMaxSpeed(maxSpeed);
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject enemyGameObject in enemiesHolding)
        {
            if (enemyGameObject != null)
            {
                Throwable enemy = enemyGameObject.GetComponent<Throwable>();
                enemy.getHit -= Free;
                enemy.getHitForTheFirstTime -= Free;
            }
        }
    }
}
