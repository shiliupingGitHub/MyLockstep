#include "wrapper_define.h"
#include "world.h"
#include "controller.h"
using namespace lockstep;
extern "C"
{
	_DLL_EXPORT World* NewWorld()
	{
		World* pWorld = new World();

		return pWorld;
	}

	_DLL_EXPORT void DeleteWorld(World* pWorld)
	{	
		delete pWorld;		
	}

	_DLL_EXPORT bool WorldLoad(World* pWorld, const level_data data)
	{

		return  pWorld->Load(data);
	}

	_DLL_EXPORT bool WorldLoadEntitys(World* pWorld, const entity_data* data, int n)
	{

		return  pWorld->LoadEntitys(data, n);
	}


	_DLL_EXPORT void WorldTickCmd(World* pWorld, command_data* cmd, int n)
	{
		pWorld->DoCmd(cmd, n);
	}

	_DLL_EXPORT void WorldTick(World* pWorld)
	{
		pWorld->Tick();
		
	}

}