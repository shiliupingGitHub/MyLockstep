#ifndef LOCKSTEP_COMPONENT_H
#define LOCKSTEP_COMPONENT_H
#include "rapidjson/document.h"
using namespace rapidjson;
namespace lockstep
{
	class World;
	class Entity;
	class Component
	{
	public:
		Component(World* pWorld, Entity* pEntity);
		virtual ~Component();
	protected:
		World* mPWorld;
		Entity* mPEntity;
	};

}
#endif // !_COMPONENT_H

