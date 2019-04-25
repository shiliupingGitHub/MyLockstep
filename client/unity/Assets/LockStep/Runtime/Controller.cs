
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using core;
namespace lockstep
{
   public enum FILE_TYPE
    {
        NONE = 0,
        LEVEL_CONFIG = 1,
        ENTITY_CONFIG = 2,
        SKILL_CONFIG = 3,
        BEHAVOUR_CONFIG = 4,
        ANI_CONFIG = 5,
        ANI_EVENT_CONFIG = 6,
        COMPONENT_CONFIG = 7,
        CONFIG_FILE = 8,
        PREFAB = 30,
    }

    public class Controller : SingleTon<Controller>
    {

        
        IntPtr _interval;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate string StringIntIntStringMethod(int type, int id, string ex);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void IntPtrMethod(int type, IntPtr ptr);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ULongMethod(ulong id);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ULongIntStringMethod(ulong id, int effect, string bind);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ULongIntIntIntMethod(ulong id, int index_1, int index_2, int index3);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ULongHurtNumMethod(ulong id, HurtNum h);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ULongIntIntMethod(ulong id, int state, int dir);

        StringIntIntStringMethod mOnReadFile;
        IntPtrMethod mOnCreate;
        ULongMethod mOnDelete;
        ULongIntIntIntMethod mOnUseSkill;
        ULongIntIntIntMethod mOnSkillEnd;
        ULongHurtNumMethod mOnHurt;
        ULongIntIntMethod mOnJump;
        public void Init()
        {
            mOnReadFile = OnReadFile;
            mOnCreate = OnCreate;
            mOnDelete = OnDelete;
            mOnUseSkill = OnUseSkill;
            mOnSkillEnd = OnSkillEnd;
            mOnHurt = OnHurt;
            mOnJump = OnJump;
            ControllerRegisterReadFile(Marshal.GetFunctionPointerForDelegate(mOnReadFile));
            ControllerRegisterOnCreate(Marshal.GetFunctionPointerForDelegate(mOnCreate));
            ControllerRegisterOnDelete(Marshal.GetFunctionPointerForDelegate(mOnDelete));
            ControllerRegisterOnUseSkill(Marshal.GetFunctionPointerForDelegate(mOnUseSkill));
            ControllerRegisterOnSkillEnd(Marshal.GetFunctionPointerForDelegate(mOnSkillEnd));
            ControllerRegisterOnHurt(Marshal.GetFunctionPointerForDelegate(mOnHurt));
            ControllerRegisterOnJump(Marshal.GetFunctionPointerForDelegate(mOnJump));
        }


  
        string OnReadFile(int type, int id, string ex)
        {
            FILE_TYPE t = (FILE_TYPE)type;
            string path = null;
            string ext = "json";
            switch (t)
            {
                case FILE_TYPE.LEVEL_CONFIG:
                    path = $"LevelConfig/level_config_{id}";
                    break;
                case FILE_TYPE.ENTITY_CONFIG:
                    path = $"EntityConfig/entity_config_{id}";
                    break;
                case FILE_TYPE.SKILL_CONFIG:
                    path = $"SkillConfig/skill_config_{id}";
                    break;
                case FILE_TYPE.BEHAVOUR_CONFIG:
                    path = $"Behavour/be_{id}";
                    ext = "xml";
                    break;
                case FILE_TYPE.ANI_CONFIG:
                    path = $"AniConfig/ani_config_{id}";
                    break;
                case FILE_TYPE.ANI_EVENT_CONFIG:
                    path = $"AniEventConfig/event_{ex}_{id}";
                    break;
                case FILE_TYPE.COMPONENT_CONFIG:
                    path = $"Component/component_{ex}_{id}";
                    break;
                case FILE_TYPE.CONFIG_FILE:
                    path = $"Config/{ex}";
                    ext = "txt";
                    break;
            }

            TextAsset ta = ResourceManager.Instance.Load<TextAsset>(path, ext);

            return ta.text;          
        }

        void OnCreate(int type, IntPtr ptr)
        {
                Entity entity = EntityFactory.Instance.Create(ptr);
                entity.Init(ptr);
                EntityManager.Instance.AddEnity(entity);               
        }

        void OnUseSkill(ulong id, int skill_id, int skill_index, int ani_index)
        {
            Entity entity = EntityManager.Instance.GetEntity(id);
            entity.UseSkill(skill_id, skill_index, ani_index);
        }
        void OnSkillEnd(ulong id, int skill_id, int skill_index, int ani_index)
        {
            Entity entity = EntityManager.Instance.GetEntity(id);
            entity.EndSkill(skill_id, skill_index, ani_index);
        }
        void OnHurt(ulong id, HurtNum h)
        {
            Entity entity = EntityManager.Instance.GetEntity(id);

            entity.DoHurt(h);
        }
        void OnJump(ulong id,int state, int jump_dir)
        {
            Entity entity = EntityManager.Instance.GetEntity(id);

            entity.Jump(state,jump_dir);
        }
        void OnDelete(ulong id)
        {
            EntityManager.Instance.RemoveEnity(id);
        }

#if !UNITY_IPHONE
        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ControllerRegisterReadFile(IntPtr method);
        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ControllerRegisterOnCreate(IntPtr method);
        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ControllerRegisterOnDelete(IntPtr method);
        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ControllerRegisterOnUseSkill(IntPtr method);
        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ControllerRegisterOnSkillEnd(IntPtr method);
        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ControllerRegisterOnHurt(IntPtr method);
        [DllImport("lockstep", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ControllerRegisterOnJump(IntPtr method);
#else
         [DllImport ("__Internal")] 
        public static extern void ControllerRegisterReadFile(IntPtr method);
        [DllImport ("__Internal")] 
        public static extern void ControllerRegisterOnCreate(IntPtr method);
        [DllImport ("__Internal")] 
        public static extern void ControllerRegisterOnDelete(IntPtr method);
        [DllImport ("__Internal")] 
        public static extern void ControllerRegisterOnUseSkill(IntPtr method);
        [DllImport ("__Internal")] 
        public static extern void ControllerRegisterOnSkillEnd(IntPtr method);
        [DllImport ("__Internal")] 
        public static extern void ControllerRegisterOnHurt(IntPtr method);
        [DllImport ("__Internal")] 
        public static extern void ControllerRegisterOnJump(IntPtr method);

#endif
    }


}
