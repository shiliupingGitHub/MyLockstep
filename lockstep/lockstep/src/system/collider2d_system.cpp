#include "system/collider2d_system.h"
#include "component/collider2d_component.h"
namespace lockstep
{
	IMPLEMENT_SYSTEM(Collider2dSystem, collider2d);

	Collider2dSystem::Collider2dSystem(World* pWorld) :GSystem(pWorld)
	{

	}
	Collider2dSystem::~Collider2dSystem()
	{

	}

	void Collider2dSystem::tick()
	{
		for (auto c : mComponents)
		{
			((Collider2DComponent*)c)->Tick();
		}
	}

	Component* Collider2dSystem::create_compoent(Entity* pEntity, int  id)
	{

		return new Collider2DComponent(mPWorld, pEntity, id);

	}
}