using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    [SerializeField] private PointTrigger _backPointCounter;
    [SerializeField] private bool _backCollider;
    public bool IsFrontPointCrossed;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMain player = other.GetComponent<PlayerMain>();
            PointCounter pointCounter = other.GetComponent<PointCounter>();
            if (player._playerMovement.CanMove)                                 //If is the front point, activate the back one for the collider to sum a point to prevent the player to accumulate points going back and forth
            {
                if (_backCollider)
                {
                    if (IsFrontPointCrossed)                                    //If the back collider check if the front one is crossed to sum a point
                    {
                        SumPoint(pointCounter);
                        IsFrontPointCrossed = false;
                    }
                }
                else                                                            //if is front collider, signal the back one that the front point is crossed so it can sum a point if crossed
                {
                    _backPointCounter.IsFrontPointCrossed = true;
                    StartCoroutine(ResetPointCross());                          //Start a coroutine to reset the status on the back pointer in case the player falls so it doesn't sum the point
                }
            }
        }
    }

    IEnumerator ResetPointCross()
    {
        yield return new WaitForSeconds(1f);
        _backPointCounter.IsFrontPointCrossed = false;
    }
    public void SumPoint(PointCounter pointCounter)
    {
        pointCounter.Points++;
        pointCounter.UpdatePoints();

    }
}
