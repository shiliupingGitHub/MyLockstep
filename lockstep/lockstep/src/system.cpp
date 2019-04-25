#include "system.h"
using namespace lockstep;

void GSystem::add(Component* pCom)
{
	mComponents.push_back(pCom);
}

void GSystem::remove(Component* pCom)
{
	for (auto it = mComponents.begin(); it != mComponents.end(); it++)
	{
		if (*it == pCom)
		{
			mComponents.erase(it);
			break;
		}
	}
}
