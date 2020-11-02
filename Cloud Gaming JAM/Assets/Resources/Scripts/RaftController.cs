using UnityEngine;

public class RaftController : MonoBehaviour
{
    #region PublicVariables
    [Range(0f, 5f)] public float raftMaximumSpeed = 1f;
    #endregion
    #region PrivateVariables

    #endregion
    #region PrivateMethods
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    #endregion
    #region PublicMethods
    ///<summary>
    ///Updates the raft instance's speed, meant to be used in a Update() loop, takes an Input Vector.
    ///<param name="calculatedInput">Mixed input vector.</param>
    ///</summary>
    public void UpdateRaftSpeed(Vector2 calculatedInput)
    {
        Rigidbody raftRigidBody = GetComponent<Rigidbody>();
        raftRigidBody.velocity = new Vector3(calculatedInput.x, calculatedInput.y, 0f);
    }
    #endregion
}
