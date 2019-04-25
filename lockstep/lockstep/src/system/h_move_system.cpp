#include "system/h_move_system.h"
#include "lockstep.h"
#include "component/h_move_component.h"
namespace lockstep
{
	IMPLEMENT_SYSTEM(HMoveSystem, hmove);

	HMoveSystem::HMoveSystem(World* pWorld):GSystem(pWorld)
	{

	}

	HMoveSystem::~HMoveSystem()
	{

	}

	void HMoveSystem::tick()
	{
		for (auto c : mComponents)
		{
			HMoveComponent* pComponent = (HMoveComponent*)c;

			pComponent->Tick();
		}
	}

	Component* HMoveSystem::create_compoent(Entity* pEntity, int  id)
	{

		return new HMoveComponent(mPWorld, pEntity, id);

	}
}