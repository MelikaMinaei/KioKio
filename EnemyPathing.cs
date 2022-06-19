using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> wayPoints;
    int wayPointInd = 0;

    void Start()
    {
        wayPoints = waveConfig.GetWayPoints();
        transform.position = wayPoints[wayPointInd].transform.position;
    }

    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (wayPointInd <= wayPoints.Count - 1)
        {
            var tarPos = wayPoints[wayPointInd].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, tarPos, movementThisFrame);
            if (transform.position == tarPos)
            {
                wayPointInd++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
