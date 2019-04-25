#include "wrapper_define.h"
#include "entity.h"
#include "data.h"
#include "component/skill_component.h"
using namespace lockstep;
extern "C"
{

	_DLL_EXPORT int EntityGetVisual(Entity* pEnity)
	{
		return pEnity->GetVisual();
	}


	_DLL_EXPORT Vector2 EntityGetPos(Entity* pEnity)
	{
		return pEnity->GetPos();
	}

	_DLL_EXPORT int EntityGetD(Entity* pEnity)
	{
		return pEnity->GetD();
	}

	_DLL_EXPORT unsigned long long EntityGetId(Entity *pEntity)
	{
		return pEntity->GetId();
	}

	_DLL_EXPORT int EntityGetXMove(Entity *pEntity)
	{
		return pEntity->GetXMove();
	}

	_DLL_EXPORT int EntityGetFrozen(Entity *pEntity)
	{
		return pEntity->GetFrozen();
	}

}