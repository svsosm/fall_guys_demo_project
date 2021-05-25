using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;


public enum OpponentStateR
{
    IDLE = 0,
    RUNNING = 1,
    FALLING = 2
}

public class GirlR : Agent
{
    //Vectors
    private Vector3 rot;
    private Vector3 startPos;
    private Quaternion startRot;

    //Components
    private Rigidbody rb;
    private Animator anim;

    //variables
    [SerializeField] private float forwardSpeed = 1f;
    private float swerveAmount;
    private float rotLength = 1f;
    private bool isFinish = false;
    [SerializeField] private float rotSpeed = 90f;
    private OpponentStateR opponentState;
    private const float fallingLimit = -1f;

    //Rewards
    private const float penaltyPoint = -1f;
    private const float successPoint = +1f;
    private const float criticSuccessPoint = +2f;
    private const float finishPoint = +5f;

    //Tags and Layers
    private const string wallTag = "Wall";
    private const string checkpointTag = "Checkpoint";
    private const string criticCheckpointTag = "CriticCheckpoint";
    private const string lastCheckpointTag = "LastCheckpoint";
    private const string playerStateTag = "PlayerState";
    private const int obstacleLayerIndex = 9;


    private void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    public override void Initialize()
    {

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public override void OnEpisodeBegin()
    {
        this.rb.angularVelocity = Vector3.zero;
        this.rb.velocity = Vector3.zero;
        this.transform.localPosition = startPos;
        this.transform.localRotation = startRot;
        opponentState = OpponentStateR.IDLE;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(rb.velocity);
        sensor.AddObservation(transform.position);
        sensor.AddObservation(transform.rotation.y);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {

        float rotY = Mathf.Clamp(actions.ContinuousActions[0], -rotLength, rotLength);
        MoveAndRotateCharacter(rotY);


        if (transform.position.y < fallingLimit)
        {
            opponentState = OpponentStateR.FALLING;
            SetReward(penaltyPoint);
            EndEpisode();
        }

        if (!GameManager.Instance.gameState.Equals(GameState.RUNNER) || isFinish)
        {
            StopCharacter();
        }

        anim.SetInteger(playerStateTag, (int)opponentState);
    }

    #region Move, Rotate and Stop Opponent
    void MoveAndRotateCharacter(float rotY)
    {
        swerveAmount = rotY * rotSpeed;
        rot = new Vector3(0, rotY, 0);

        rb.MovePosition(transform.position + transform.forward * forwardSpeed * Time.deltaTime);
        rb.MoveRotation(Quaternion.Euler(rot) * rb.rotation);
        opponentState = OpponentStateR.RUNNING;
    }

    void StopCharacter()
    {
        rb.MovePosition(transform.position);
        rb.velocity = Vector3.zero;
        opponentState = OpponentStateR.IDLE;
    }
    #endregion


    #region Character Trigger and Collision
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.layer.Equals(obstacleLayerIndex))
        {
            AddReward(penaltyPoint);
            EndEpisode();
        }
        else if (other.transform.tag.Equals(wallTag))
        {
            AddReward(penaltyPoint / 2);
            EndEpisode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals(checkpointTag))
        {
            AddReward(successPoint);
        }
        else if (other.transform.tag.Equals(criticCheckpointTag))
        {
            AddReward(criticSuccessPoint);
        }
        else if (other.transform.tag.Equals(lastCheckpointTag))
        {
            AddReward(finishPoint);
            isFinish = true;
        }
    }
    #endregion


}
