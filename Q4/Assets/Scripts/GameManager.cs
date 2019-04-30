using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //Reference Variables
    public GameObject Base;
    Transform BasePos;
    Collider BaseCol;
    EnemyRepository er;

    //Game Manager Variables
    public static int Health = 10;
    public const int HealthCap = 10;

    //Wave Manager Variables
    [SerializeField] bool test;
    Wave[] MasterWave;
    [SerializeField] int MasterPos;
    SemiWave[] SpawnWave;
    public Vector2 SpawnPos;
    const int SpawnWaveCap = 100;
    [SerializeField] int SpawnWavePos = 0;
    int PrevSpawnWavePos = 0;
    [SerializeField] int SemiWavePos = 0;
    [SerializeField] float SpawnAlarm = 0;
    public struct SemiWave
    {
        public GameObject EnemyObject;
        public int DeployCount;
        public float DeployRate;
        public bool Defined;

        public SemiWave(bool DefaultConstructor) //MAKESHIFT DEFAULT CONSTRUCTOR
        {
            //Set Variable Defaults
            EnemyObject = null;
            DeployCount = 0;
            DeployRate = 0;
            Defined = false;
        }

        public SemiWave(GameObject enemyObject, int deployCount, float deployRate)
        {
            //Define Input Variables
            EnemyObject = enemyObject;
            DeployCount = deployCount;
            DeployRate = deployRate;

            //Set Variable Defaults
            Defined = true;
        }
    }
    public struct Wave
    {
        public SemiWave[] SemiWave;
        public int RewardCash;
        public bool Defined;

        public Wave(bool DefaultConstructor) //MAKESHIFT DEFAULT CONSTRUCTOR
        {
            //Set Variable Defaults
            SemiWave = new SemiWave[SpawnWaveCap];
            RewardCash = 0;
            Defined = false;
        }

        public Wave(int rewardCash)
        {
            RewardCash = rewardCash;

            //Set Variable Defaults
            SemiWave = new SemiWave[SpawnWaveCap];
            Defined = true;
        }
    }



    //Define Reference Variables
    private void OnEnable()
    {
        //Define General Reference Variables
        er = GetComponent<EnemyRepository>();

        //Initialize Base Object
        if (Base.GetComponent<Collider>() == null)
        {
            Base.AddComponent<Collider>();
        }
        BasePos = Base.GetComponent<Transform>();
        BaseCol = Base.GetComponent<Collider>();
        BaseCol.CollisionType = -1;
    }

    // Use this for initialization
    void Start () {

        //Define Master Wave
        MasterWave = new Wave[SpawnWaveCap];

        MasterWave[0] = new Wave(150);
        MasterWave[0].SemiWave[0] = new SemiWave(er.FastBaby, 10, 1f);
        //MasterWave[0].SemiWave[0] = new SemiWave()
        //MasterWave[0].SemiWave[1] = new SemiWave(er.FastBaby, 300, .25f);

        MasterWave[1] = new Wave(150);
        MasterWave[1].SemiWave[0] = new SemiWave(er.NormalBaby, 10, 1f);

        MasterWave[2] = new Wave(2);
        MasterWave[2].SemiWave[0] = new SemiWave(er.WingedBaby, 10, 1f);


        //Reset Health
        Health = HealthCap;

        //Set Enemy Follow Pos
        Enemy.FollowPos = new Vector2(BasePos.position.x, BasePos.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		
        //Damage Base On Enemy Collision
        if(BaseCol.PlaceMeeting(BasePos.position.x, BasePos.position.y, 2))
        {
            Destroy(BaseCol.InstanceMeeting(BasePos.position.x, BasePos.position.y, 2));
            Health -= 1;
        }

        //Lose Game (One Base Out Of Health)
        if(Health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        test = MasterWave[1].SemiWave[0].Defined;


        //Manage Master Wave
        if (MasterWave != null)
        {
            //Manage Wave
            if (MasterWave[MasterPos].Defined)
            {
                //Spawn Existing Semiwaves
                if (MasterWave[MasterPos].SemiWave[SpawnWavePos].Defined == true)
                {
                    //Begin New Semiwave
                    if (SpawnWavePos != PrevSpawnWavePos)
                    {
                        SpawnAlarm = MasterWave[MasterPos].SemiWave[SpawnWavePos].DeployRate;
                        SemiWavePos = 0;
                    }
                    PrevSpawnWavePos = SpawnWavePos;

                    //Deduct Semiwave Alarm
                    if (SpawnAlarm - Time.deltaTime > 0)
                    {
                        SpawnAlarm -= Time.deltaTime;
                    }
                    //Spawn Enemy 
                    else
                    {
                        //Advance SemiWave Pos 
                        if (SemiWavePos < MasterWave[MasterPos].SemiWave[SpawnWavePos].DeployCount)
                        {
                            //Spawn Enemy
                            GameObject tvInst = Instantiate(MasterWave[MasterPos].SemiWave[SpawnWavePos].EnemyObject);
                            tvInst.GetComponent<Transform>().position = SpawnPos;

                            //Reset Alarm
                            SpawnAlarm = MasterWave[MasterPos].SemiWave[SpawnWavePos].DeployRate;

                            //Move On To Next Instance To Spawn
                            SemiWavePos++;
                        }
                        else
                        {
                            //Turn Off Used Semiwave
                            MasterWave[MasterPos].SemiWave[SpawnWavePos].Defined = false;

                            //Move On To Next Semiwave
                            SpawnWavePos++;
                        }
                    }                 
                }
                //Move On To Next Wave
                else
                {
                    //Reward Wave Money
                    Player.Cash += MasterWave[MasterPos].RewardCash;

                    //Turn Off Used Wave
                    MasterWave[MasterPos].Defined = false;

                    //Reset SemiWave Positions
                    SpawnWavePos = 0;
                    SemiWavePos = 0;

                    //Progress Waves
                    MasterPos++;
                }

            }//MANAGE ENEMY SPAWNING
        }
	}
}
