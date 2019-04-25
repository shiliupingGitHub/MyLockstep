using System;
using System.Collections.Generic;

namespace hotfix
{
    public class ObjectPool : SingleTon<ObjectPool>
    {
        private readonly Dictionary<Type, Queue<System.Object>> dictionary = new Dictionary<Type, Queue<System.Object>>();

        public System.Object Fetch(Type type)
        {
	        Queue<System.Object> queue;
            if (!this.dictionary.TryGetValue(type, out queue))
            {
                queue = new Queue<System.Object>();
                this.dictionary.Add(type, queue);
            }
	        System.Object obj;
			if (queue.Count > 0)
            {
				obj = queue.Dequeue();
            }
			else
			{
				obj = (System.Object)Activator.CreateInstance(type);	
			}
            return obj;
        }

        public T Fetch<T>()
		{
            T t = (T) this.Fetch(typeof(T));
			return t;
		}
        
        public void Recycle(System.Object obj)
        {
            Type type = obj.GetType();
	        Queue<System.Object> queue;
            if (!this.dictionary.TryGetValue(type, out queue))
            {
                queue = new Queue<System.Object>();
				this.dictionary.Add(type, queue);
            }
            queue.Enqueue(obj);
        }
    }
}