using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lockstep;
using UnityEngine.UI;
using  core;
public class Demo : MonoBehaviour
{
    public int level_id = 10001;
    public int entity_config_id = 10002;
    ulong testRoleID = 10000000;
    ulong testEnermyId = 10000001;
    public List<int> skills = new List<int>() { 10001 };
    private void Awake()
    {
        Game.Instance.Init();
        LevelData ld = new LevelData();

        ld.level_id = level_id;
        ld.seed = (uint)System.Environment.TickCount;

        EntityManager.Instance.LoadLevel(ld);

        var rd1 = new EntityData();
        rd1.config_id = entity_config_id;
        rd1.id = testRoleID;
        rd1.pos = new lockstep.Vector2(-400, 0);
        rd1.d = 1;
        rd1.skills = skills.ToArray();
        rd1.skill_size = skills.Count;
        rd1.camp = 1;

        var rd2 = new EntityData();
        rd2.config_id = entity_config_id;
        rd2.id = testEnermyId;
        rd2.pos = new lockstep.Vector2(400, 0); 
        rd2.d = -1;
        rd2.skills = skills.ToArray();
        rd2.skill_size = skills.Count;
        rd1.camp = 2;
        EntityManager.Instance.MyRoleId = testRoleID;
        EntityManager.Instance.LoadEntitys(new EntityData[] { rd1, rd2 });

    }

    private void Start()
    {

        UIManager.Instance.Show("BattleFrame");
        
    }

    private void OnDisable()
    {
    }

    void CheckMoveChanged()
    {

        bool is_left = true;
        bool is_move = false;

        if(Input.GetKey(KeyCode.A))
        {
            is_left = true;
            is_move = true;
        }

        if(Input.GetKey(KeyCode.D))
        {
            is_left = false;
            is_move = true;
        }

        HMoveComand cmd = new HMoveComand();

        cmd.is_left = is_left;
        cmd.is_move = is_move; ;
        ComandManager.Instance.SendCmd(testRoleID, cmd);
    }
    private void Update()
    {
        bool checkChange = false;
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            checkChange = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            checkChange = true;
        }
         if (Input.GetKeyDown(KeyCode.A))
        {
            checkChange = true;
        }

        if (checkChange)
            CheckMoveChanged();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            VMoveComand cmd = new VMoveComand();
            ComandManager.Instance.SendCmd(testRoleID, cmd);
        }

       
    }

}
