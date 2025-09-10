using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiddleColliderSizeFitter : MonoBehaviour
{
    // Circumvents issues with PIPE_GAP_SIZE being too high, 
    // and previously the player being able to go around the score collider

    [SerializeField] BoxCollider2D cMiddleCollider;

    void Start()
    {
        cMiddleCollider.size = new Vector2(CONFIG.PIPE_SCORE_COLLISION_WIDTH, CONFIG.PIPE_GAP_SIZE + 0.5f);
    }
}
