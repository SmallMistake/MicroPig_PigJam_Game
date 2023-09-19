using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DingItCodeController : MonoBehaviour
{
    public GameObject bell;
    public BoxCollider2D spawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 bellLocation = PickARandomPointInSpawnArea(spawnLocation.bounds);
        MoveBellToPoint(bellLocation);
    }

    private Vector3 PickARandomPointInSpawnArea(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private void MoveBellToPoint(Vector3 pointToMoveTo)
    {
        bell.transform.position = pointToMoveTo;
    }
}
