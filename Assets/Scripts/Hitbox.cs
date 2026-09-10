using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public enum ColliderState
    {
        Closed,
        Open,
        Colliding
    }

    public LayerMask layer;
    public Vector3 hitboxSize = Vector3.one;
    public Vector3 offset = Vector3.zero;
    public float radius = 0.5f;

    private ColliderState _state;
    private IHitboxResponder responder = null;

    private List<Collider> lastHitColliderList;

    void Awake()
    {
        lastHitColliderList = new ();
    }
    
    private void CheckGizmoColor()
    {
        switch(_state)
        {
            case ColliderState.Closed:
                Gizmos.color = Color.gray;
                break;
            
            case ColliderState.Open:
                Gizmos.color = Color.green;
                break;
            
            case ColliderState.Colliding:
                Gizmos.color = Color.red;
                break;
        }
    }

    public void StartCheckingCollision()
    {
        lastHitColliderList.Clear();
        _state = ColliderState.Open;
    }

    public void StopCheckingCollision()
    {
        _state = ColliderState.Closed;
    }

    public void SetResponder(IHitboxResponder newResponder)
    {
        responder = newResponder;
    }

    private void FixedUpdate()
    {
        if(_state == ColliderState.Closed) return;

        Vector3 size = new (
            hitboxSize.x * transform.lossyScale.x,
            hitboxSize.y * transform.lossyScale.y,
            hitboxSize.z * transform.lossyScale.z
        );

        Collider[] colls = Physics.OverlapBox(transform.TransformPoint(offset), size * 0.5f, transform.rotation, layer);

        for(int i=0;i<colls.Length;i++)
        {
            if(lastHitColliderList.Contains(colls[i])) continue;

            responder?.CollisionedWith(colls[i]);
            lastHitColliderList.Add(colls[i]);
        }   

        _state = colls.Length > 0 ? ColliderState.Colliding : ColliderState.Open;
    }

    public interface IHitboxResponder
    {
        void CollisionedWith(Collider coll);
    }

    private void OnDrawGizmos()
    {
        CheckGizmoColor();

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(offset, hitboxSize);
    }
}
