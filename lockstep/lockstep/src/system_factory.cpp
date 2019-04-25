#include "system_factory.h"
#include "world.h"
#include "lockstep.h"
namespace lockstep
{
	SystemFactory& SystemFactory::GetInstance()
	{
		static SystemFactory s_SystemFactory;

		return s_SystemFactory;
	}

	void SystemFactory::Visit(World* pWorld)
	{
		for (auto it = mCreators.begin(); it != mCreators.end(); it++)
		{
			pWorld->mSystem[it->first] = it->second->Create(pWorld);
		}
	}

	void SystemFactory::Add(std::string name, BaseSystemCreator* pCreator)
	{
		mCreators[name] = pCreator;
	}
}