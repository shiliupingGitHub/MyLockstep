using core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lockstep
{

    public class EntityScript : MonoBehaviour
    {
        public Entity entity;
        Dictionary<string, Transform> mBinds = new Dictionary<string, Transform>();
         void Awake()
        {
            InitBind(transform);
        }
        private void InitBind(Transform root)
        {
            mBinds[root.name] = root;

            for (int i = 0; i < root.childCount; i++)
            {
                InitBind(root.GetChild(i));
            }
        }
        void OnAddEffect(string effect)
        {

            string[] param = effect.Split(',');

            if (param.Length < 1)
                return;
            string effect_name = param[0];
            string bind = null;

            if (param.Length >= 2)
                bind = param[1];
            
            var asset = ResourceManager.Instance.Load<GameObject>($"Effect/{effect_name}", "prefab");

            Transform parent = transform;

            if (!string.IsNullOrEmpty(bind))
            {
                if (mBinds.TryGetValue(bind, out var tr))
                {
                    parent = tr;
                }
            }
            GameObject go = GameObject.Instantiate<GameObject>(asset, parent);

            if (null != entity)
            {
                entity.AddEffect(go);
            }
        }
    }
}

