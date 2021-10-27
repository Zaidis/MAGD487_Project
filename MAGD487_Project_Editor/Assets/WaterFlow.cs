using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    [SerializeField] float raycastLength;
    [SerializeField] List<WaterParticle> waterParticles;
    [SerializeField] List<WaterParticle> waterParticlesDelayed = new List<WaterParticle>();
    [SerializeField] List<WaterParticle> waterParticlesStationary = new List<WaterParticle>();
    [SerializeField] Vector3[] directions;
    [SerializeField] float updateSpeed, delayedUpdateSpeed;
    float timer = 0, timer2 = 0;
    public static WaterFlow instance;
    Dictionary<Vector3, WaterParticle> positions = new Dictionary<Vector3, WaterParticle>();
    [SerializeField] int stationaryThreshold = 3;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void AssignParticle(WaterParticle part)
    {
        waterParticles.Add(part);
    }
   
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        timer2 += Time.fixedDeltaTime;
        timer = UpdateList(waterParticles, waterParticlesDelayed, timer, updateSpeed, false);
        timer2 = UpdateList(waterParticlesDelayed, waterParticlesStationary, timer2, delayedUpdateSpeed, true);
    }
    float UpdateList(List<WaterParticle> waterParticles, List<WaterParticle> waterParticlesDelayed, float timer, float updateSpeed, bool canSpeedUp)
    {
        if (timer > updateSpeed)
        {

            for(int i = 0; i < waterParticles.Count; i++)
            {
                List<Vector3> dir = new List<Vector3>();
                WaterParticle part = waterParticles[i];

                if (!positions.ContainsKey(part.transform.position + directions[0] * part.transform.localScale.x) && Physics2D.Raycast(part.transform.position, directions[0], raycastLength).collider == null)
                {
                    MoveParticleInDirection(directions[0], part.transform, part, waterParticles, canSpeedUp);
                    continue;
                }else if(!positions.ContainsKey(part.transform.position + part.currentDirection * part.transform.localScale.x) && Physics2D.Raycast(part.transform.position, part.currentDirection, raycastLength).collider == null)
                {
                    MoveParticleInDirection(part.currentDirection, part.transform, part, waterParticles, canSpeedUp);
                    continue;
                }
                
                else
                {
                    //Follow rules
                    dir.Add(directions[1]);
                    dir.Add(directions[2]);
                    dir.Add(directions[3]);
                    dir.Add(directions[4]);

                    for (int j = 0; j < dir.Count; j++)
                    {
                        int rand = Random.Range(0, dir.Count);
                        if (!positions.ContainsKey(part.transform.position + dir[rand] * part.transform.localScale.x) && Physics2D.Raycast(part.transform.position, dir[rand], raycastLength).collider == null)
                        {
                            //Open so move to it
                            MoveParticleInDirection(dir[rand], part.transform, part, waterParticles, canSpeedUp);
                            break;
                        }
                        else if (j == dir.Count - 1)
                        {
                            //Block must be stuck so put into delayed list
                            if (!canSpeedUp)
                            {
                                SwapLists(waterParticlesDelayed, waterParticles, part);
                            }
                            if(part.stationaryIteration >= stationaryThreshold)
                            {
                                SwapLists(waterParticlesStationary, waterParticles, part);
                            }
                            else
                            {
                                part.stationaryIteration++;
                            }
                            
                            break;
                        }
                        else
                        {
                            dir.RemoveAt(rand);
                            j--;
                        }
                    }
                }

            }
            return 0;
        }
        return timer;
    }

    void MoveParticleInDirection(Vector3 dir, Transform part, WaterParticle particle, List<WaterParticle> currentList, bool canSpeedUp)
    {
        positions.Remove(part.transform.position);
        part.transform.position += dir * part.transform.localScale.x;
        positions.Add(part.transform.position, particle);
        particle.currentDirection = dir;
        particle.stationaryIteration = 0;

        if (canSpeedUp)
            SwapLists(waterParticles, currentList, particle);
    }
    void SwapLists(List<WaterParticle> particles1, List<WaterParticle> particles2, WaterParticle part)
    {
        particles1.Add(part);
        particles2.Remove(part);
    }

    public void Explosion()
    {
        foreach(WaterParticle part in waterParticlesStationary)
        {
            SwapLists(waterParticlesDelayed, waterParticlesStationary, part);
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
