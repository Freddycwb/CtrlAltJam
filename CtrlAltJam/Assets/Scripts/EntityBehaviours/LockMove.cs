using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMove : MonoBehaviour
{
    [SerializeField] private PlaneMove planeMove;
    [SerializeField] private Jump jump;
    private List<GameObject> enemiesHolding = new List<GameObject>();
    [SerializeField] private GameEvent gameOver;
    private float maxSpeed;

    public Action onLock;
    public Action onFree;

    private void Start()
    {
        maxSpeed = planeMove.GetMaxSpeed();
    }

    public void CheckEnemies()
    {
        StartCoroutine(Checking());
    }

    private IEnumerator Checking()
    {
        yield return new WaitForSeconds(0.2f);
        int length = enemiesHolding.Count;
        for (int i = 0; i < length; i++)
        {
            if (length > enemiesHolding.Count)
            {
                break;
            }
            if (enemiesHolding[i] == null)
            {
                enemiesHolding.Remove(enemiesHolding[i]);
                if (enemiesHolding.Count <= 0)
                {
                    planeMove.SetMaxSpeed(maxSpeed);
                    jump.enabled = true;
                }
                else
                {
                    planeMove.SetMaxSpeed(maxSpeed / (3 * enemiesHolding.Count));
                }
                if (onFree != null)
                {
                    onFree.Invoke();
                }
            }
        }
    }

    public void Lock(Throwable enemy)
    {
        if (enemiesHolding.Contains(enemy.gameObject))
        {
            return;
        }
        enemiesHolding.Add(enemy.gameObject);
        enemy.getHit += Free;
        enemy.getHitForTheFirstTime += Free;
        planeMove.SetMaxSpeed(maxSpeed / (3 * enemiesHolding.Count));
        jump.enabled = false;
        if (onLock != null)
        {
            onLock.Invoke();
        }
        if (enemiesHolding.Count >= 3)
        {
            gameOver.Raise();
        }
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
            jump.enabled = true;
        }
        else
        {
            planeMove.SetMaxSpeed(maxSpeed / (3 * enemiesHolding.Count));
        }
        if (onFree != null)
        {
            onFree.Invoke();
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
