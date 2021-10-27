using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    [SerializeField] float raycastLength;
    [SerializeField] List<Transform> waterParticles;
    List<Transform> waterParticlesDelayed = new List<Transform>();
    [SerializeField] Vector3[] directions;
    RaycastHit2D hit;
    [SerializeField] float updateSpeed, delayedUpdateSpeed;
    float timer = 0, timer2 = 0;
    public static WaterFlow instance;
    [SerializeField] LayerMask ignoreMask, mainMask;
    Dictionary<Vector3, Transform> positions = new Dictionary<Vector3, Transform>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void AssignParticle(Transform part)
    {
        waterParticles.Add(part);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        UpdateList();
        UpdateDelayedList();
    }
    void UpdateList()
    {
        if (timer > updateSpeed)
        {

            for(int i = 0; i < waterParticles.Count; i++)
            {
                Transform part = waterParticles[i];
                part.gameObject.layer = ignoreMask;
                hit = Physics2D.Raycast(part.position, directions[0], raycastLength, mainMask);
                if (hit.collider == null && !positions.ContainsKey(part.position + directions[0] * part.localScale.x))
                {
                    positions.Remove(part.position);
                    part.position += directions[0] * part.localScale.x;
                    positions.Add(part.position, part);
                }
                else
                {
                    //Follow rules
                    List<Vector3> dir = new List<Vector3>(directions);
                    for (int j = 1; j < dir.Count; j++)
                    {
                        int rand = Random.Range(1, dir.Count);
                        hit = Physics2D.Raycast(part.position, dir[rand], raycastLength, mainMask);
                        if (hit.collider == null && !positions.ContainsKey(part.position + dir[rand] * part.localScale.x))
                        {
                            //Open so move to it
                            positions.Remove(part.position);
                            part.position += dir[rand] * part.localScale.x;
                            positions.Add(part.position, part);
                            break;
                        }
                        else if (j == dir.Count - 1)
                        {
                            //Block must be stuck so put into delayed list
                            waterParticlesDelayed.Add(part);
                            waterParticles.Remove(part);
                            break;
                        }
                    }
                }

                part.gameObject.layer = mainMask;
            }
            timer = 0;
        }
    }
    void UpdateDelayedList()
    {
        if (timer2 > delayedUpdateSpeed)
        {

            for(int i = 0; i < waterParticlesDelayed.Count; i++)
            {
                Transform part = waterParticlesDelayed[i];
                part.gameObject.layer = ignoreMask;
                hit = Physics2D.Raycast(part.position, directions[0], raycastLength, mainMask);
                if (hit.collider == null && !positions.ContainsKey(part.position + directions[0] * part.localScale.x))
                {
                    positions.Remove(part.position);
                    part.position += directions[0] * part.localScale.x;
                    positions.Add(part.position, part);
                    waterParticles.Add(part);
                    waterParticlesDelayed.Remove(part);
                }
                else
                {
                    //Follow rules
                    List<Vector3> dir = new List<Vector3>(directions);
                    for (int j = 1; j < dir.Count; j++)
                    {
                        int rand = Random.Range(1, dir.Count);
                        hit = Physics2D.Raycast(part.position, dir[rand], raycastLength, mainMask);
                        if (hit.collider == null && !positions.ContainsKey(part.position + dir[rand] * part.localScale.x))
                        {
                            //Open so move to it
                            positions.Remove(part.position);
                            part.position += dir[rand] * part.localScale.x;
                            positions.Add(part.position, part);
                            waterParticles.Add(part);
                            waterParticlesDelayed.Remove(part);
                            break;
                        }
                    }
                }

                part.gameObject.layer = mainMask;
            }
            timer2 = 0;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < directions.Length; i++)
        {
            Gizmos.DrawRay(this.transform.position, directions[i] * raycastLength);
        }
    }
}
