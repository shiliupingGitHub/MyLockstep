#ifndef LOCKSTEP_WORLD_H
#define LOCKSTEP_WORLD_H
#include <vector>
#include <string>
#include "data.h"
#include<map>
using namespace std;
namespace lockstep
{
	class Entity;
	class GSystem;
	class CmdAdapter;
	class Controller;
	class Component;
	class CRandom;
	class Physix;
	class World
	{
		friend class  Entity;
		friend class CmdAdapter;
		friend class Controller;
		friend class SystemFactory;
	public:
		World();
		~World();

		World(World&) = delete;
		World& operator=(const World&) = delete;
		
	public:
		//处理命令
		void DoCmd(command_data* cmd, int size);
		//循环帧
		void Tick();
		//加载地图数据
		bool Load(const level_data map);
		//加载地图中初始化的实体
		bool LoadEntitys(const entity_data* data, int num);

	public:
		GSystem* GetSystem(string name);
	public:
		Entity* FindEntity(unsigned long long id);
		inline int GetMinX() { return mMinX;  }
		inline int GetMaX() { return mMaxX; }
		inline int GetMaxY() { return mMaxY; }

		inline CRandom* GetRand() { return mRand; }
		inline Physix* GetPhysix() { return mPPhysx; }
	private:
		
		void set_enable(string name, Component* pComponent);
		void set_disable(string name, Component* pComponent);
		Entity* create_entity(int type);
		Entity* create_entity_with_id(unsigned long long id, int type);
		void delete_entity(Entity* pEnity);
		
		bool load_entity(entity_data& data);
		

	private:
		std::map<unsigned long long, Entity*> mEntitys; //地图中所有的实体都要存储在这里
		std::map<string, GSystem*> mSystem; //处理组件的系统
		int mSeed; //随机种子
		unsigned long long mEnityId; //这个是用来为没有Id的实体分配Id的


	private:
		int mMinX; //地图左边界
		int mMaxX; //地图右边界
		int mMaxY; //地图上面街
		CRandom* mRand;
		Physix* mPPhysx;
	};

}
#endif // !_WORLD_H

