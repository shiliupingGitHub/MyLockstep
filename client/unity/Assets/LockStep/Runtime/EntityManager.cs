using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using  core;
namespace lockstep
{
    public class EntityManager : MonoBehaviour
    {
        World mWorld = null;
        ulong mCurFrame = 0;
        Dictionary<ulong, CommandList> mCmds = new Dictionary<ulong, CommandList>();
        Dictionary<ulong, Entity> mEntitys = new Dictionary<ulong, Entity>();
        public static bool GIsLockStep = false;
        public static uint GSeed = 0;
        public ulong MyRoleId;
        public ulong CurFrame
        {
            get
            {
                return mCurFrame;
            }
        }

        public bool IsLockStep
        {
            get
            {
                return GIsLockStep;
            }
        }
        #region 回调

        #endregion

        #region 方法

         void Awake()
        {
            _instance = this;
        }
        void OnDestroy()
        {
            mWorld?.Dispose();
     
            mWorld = null;
        }

        public void AddCmd(ulong frame, Command cmd)
        {
            CommandList cmdList;

            if(!mCmds.TryGetValue(frame, out cmdList))
            {
                cmdList = ObjectPool.Instance.Fetch<CommandList>();
                mCmds[frame] = cmdList;
            }

            cmdList.Cmds.Add(cmd);
        }
        public void AddEnity(Entity entity)
        {
            mEntitys[entity.Id] = entity;
        }

        public Entity GetEntity(ulong id)
        {
            return mEntitys[id];
        }

        public void RemoveEnity(ulong id)
        {
            if(mEntitys.TryGetValue(id, out var entity))
            {
                entity.Dispose();
                mEntitys.Remove(id);
            }
           
        }

        public void LoadLevel(LevelData level)
        {
            mCmds.Clear();
            mWorld?.Dispose();
            mEntitys.Clear();
            mWorld = ObjectPool.Instance.Fetch<World>();
            Time.fixedDeltaTime = 0.0303f;
            mWorld.Load(level);
        }
        public void LoadEntitys(EntityData[] edatas)
        {

            mWorld.LoadEntitys(edatas);

        }
        static EntityManager _instance = null;
        public static EntityManager Instance
        {
            get
            {
                if(null == _instance)
                {
                    GameObject go = new GameObject("FrameManager");
                    _instance = go.AddComponent<EntityManager>();
                }

                return _instance;
            }
        }
        void Tick()
        {
            if (mCmds.TryGetValue(mCurFrame, out var cs))
            {
                mWorld.TickCmd(cs.ToArray());
            }
            mWorld.Tick();

            foreach(var e in mEntitys)
            {
                e.Value.Tick();
            }
            mCurFrame++;
        }
        void FixedUpdate()
        {
            if(null != mWorld)
            {
             
                if(!IsLockStep)
                {
                    Tick();
                }  
            }
            
            
        }
        private void Update()
        {
            foreach (var e in mEntitys)
            {
                e.Value.Update();
            }
        }
        #endregion

    }
}
