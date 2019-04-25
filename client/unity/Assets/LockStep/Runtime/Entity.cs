
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;
using core;
namespace lockstep
{
    public class Entity : IDisposable
    {
        const int SKILL_STATE = 200;
        IntPtr _interval;
        GameObject mGo = null;
        Animator mAni = null;
        EntityScript script;
        List<GameObject> mEffects = new List<GameObject>();
        public ulong Id
        {
            get
            {
                return EntityGetId(_interval);
            }
        }

        public void Init(IntPtr ptr)
        {
            _interval = ptr;
            int visualId = EntityGetVisual(_interval);
            var t = ResourceManager.Instance.Load<GameObject>($"Visual/visual_{visualId}", "prefab");

            mGo = GameObject.Instantiate(t) as GameObject;
            mAni = mGo.GetComponent<Animator>();
            script = mGo.GetComponent<EntityScript>();
            UpateTransform();
        }

       
        public void EndSkill(int skill_id, int skill_index, int ani_index)
        {
            DestroyEffects();

            if (null != mAni)
            {
                mAni.SetInteger("ani_index", -1);
            }
        }
        public void UseSkill( int skill_id, int skill_index, int ani_index)
        {
            if(null != mAni)
            {
                mAni.SetInteger("ani_index", ani_index);
                mAni.SetTrigger("skill");
            }
        }

        public void Jump( int state, int jump_dir)
        {

            if(null != mAni)
            {
                mAni.SetInteger("jump_state", state);
                mAni.SetInteger("jump_dir", jump_dir);
                mAni.SetTrigger("jump");
            }
        }
        public void DoHurt(HurtNum h)
        {
            if (null != mAni)
            {
                mAni.SetInteger("ani_index", -1);
                mAni.SetTrigger("behit");
            }
        }
        public void AddEffect(GameObject go)
        {
            mEffects.Add(go);
        }

        void DestroyEffects()
        {
            foreach(var e in mEffects)
            {
                if(null != e)
                {
                    GameObject.Destroy(e);
                }
            }

            mEffects.Clear();
        }

        void UpateTransform()
        {
            if (null == mGo)
                return;
            var logic_pos = EntityGetPos(_interval);
            int d = EntityGetD(_interval);
            Vector3 pos = mGo.transform.position;
            pos.x = (float)logic_pos.x / 100f;
            pos.y = (float)logic_pos.y / 100f;
            pos.z = 0;

            mGo.transform.position = pos;

            Vector3 rotation = mGo.transform.rotation.eulerAngles;

            rotation.y = d > 0 ? 90 : -90;

            mGo.transform.rotation = Quaternion.Euler(rotation);

            Vector3 scale = mGo.transform.localScale;
            scale.x = d > 0 ? 1 : -1;
            mGo.transform.localScale = scale;

            if(null != mAni)
            {
                int x_move = EntityGetXMove(_interval);

                mAni.SetFloat("x_move", x_move / 10.0f);

                if (!mAni.enabled)
                    mAni.Update(ConstDefine.Fixtime);

                int frozen = EntityGetFrozen(_interval);

                mAni.SetInteger("frozen", frozen);
            }
           


        }

        public void Update()
        {

        }
        public  void Dispose()
        {
            if (null != mGo)
            {
                GameObject.Destroy(mGo);
            }
            mAni = null;

            ObjectPool.Instance.Recycle(this);
        }
        public virtual void Tick()
        {
            UpateTransform();
        }

#if !UNITY_IPHONE
        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern int EntityGetVisual(IntPtr pEntity);
        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern lockstep.Vector2 EntityGetPos(IntPtr pEntity);
        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern int EntityGetD(IntPtr pEntity);
        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong EntityGetId(IntPtr pEntity);
        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern int EntityGetXMove(IntPtr pEntity);
        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern int EntityGetFrozen(IntPtr pEntity);
#else
        [DllImport ("__Internal")] 
        public static extern int EntityGetVisual(IntPtr pEntity);
        [DllImport ("__Internal")
        public static extern int EntityGetD(IntPtr pEntity);
         [DllImport ("__Internal")
         public static extern lockstep.Vector2 EntityGetPos(IntPtr pEntity);
        [DllImport ("__Internal")] 
         public static extern ulong EntityGetId(IntPtr pEntity);
        [DllImport ("__Internal")] 
        public static extern int EntityGetXMove(IntPtr pEntity);
        [DllImport ("__Internal")] 
        public static extern int EntityGetFrozen(IntPtr pEntity);
#endif
    }
}
