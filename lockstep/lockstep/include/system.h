#ifndef LOCKSTEP_SYSTEM_H
#define LOCKSTEP_SYSTEM_H
#include <vector>
#include <string>
#include "rapidjson/document.h"
#include "data.h"
using namespace std;
using namespace rapidjson;
namespace lockstep
{
	class World;
	class Entity;
	class Component;
	class GSystem
	{
		
		friend class World;

	public:
		GSystem(World* pWorld) { mPWorld = pWorld; }
		virtual ~GSystem() {}
		virtual void tick() = 0;
		virtual Component* create_compoent(Entity* pEntity, int id) = 0;

	private:
		 void add(Component* pComponent);
		 void remove(Component* pComponent);
	

	protected:
		vector<Component*> mComponents;
		World* mPWorld;
	};

}

#endif // !_SYSTEM_H

