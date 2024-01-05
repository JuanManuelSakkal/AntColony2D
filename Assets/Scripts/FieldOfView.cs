using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Preference{
    Proximity,
    Intensity
}
public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;


    public LayerMask homePheromoneMask;

    public LayerMask foodMask;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<GameObject> visibleTargets = new List<GameObject>();


    public Preference preference = Preference.Intensity;
    void Start() {
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    IEnumerator FindTargetsWithDelay(float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
            SetDesiredTarget();
        }
    }

    void SetClosestTarget(List<GameObject> targets) {
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject target in targets) {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance < closestDistance) {
                closestTarget = target;
                closestDistance = distance;
            }
        }
        if (closestTarget != null) {
            gameObject.GetComponent<MoveAnt>().target = closestTarget;
        } else {
            gameObject.GetComponent<MoveAnt>().target = closestTarget;
        }
    }

    void SetIntenseTarget() {
        List<GameObject> intenseTargets = new List<GameObject>();
        float highestIntensity = -Mathf.Infinity;
        foreach (GameObject target in visibleTargets) {
            if(target.GetComponent<Intensity>()){
                float intensity = target.GetComponent<Intensity>().intensity;
                if (intensity > highestIntensity) {
                    intenseTargets.Add(target);
                    highestIntensity = intensity;
                }
            }
        }
        SetClosestTarget(intenseTargets);
    }

    void SetDesiredTarget(){
        switch (preference)
        {
            case Preference.Proximity:
                SetClosestTarget(visibleTargets);
                break;
            case Preference.Intensity:
                SetIntenseTarget();
                break;
        }
    }

    void FindVisibleTargets() {
        visibleTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);    
        foreach (Collider2D target in targetsInViewRadius) {
            Vector2 directionToTarget = (target.transform.position - transform.position).normalized;
            float angle = Vector2.Angle(transform.up, directionToTarget);
            if (Vector2.Angle(transform.up, directionToTarget) < viewAngle / 2) {
                float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask);
                if (!hit) {
                    visibleTargets.Add(target.gameObject);
                }
            }
        }
    }


    public Vector2 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal) {
        if (!angleIsGlobal) {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
