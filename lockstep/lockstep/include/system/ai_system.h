#ifndef LOCKSTEP_AI_SYSTEM_H
#define LOCKSTEP_AI_SYSTEM_H
#include "system.h"
#include "lockstep.h"
namespace lockstep
{
	class World;
	class BeFileManager;
	class AISystem :public GSystem
	{

	public:
		LS_IMP_SYSTEM(AISystem)
	};
}
#endif // !_AI_SYSTEM_H

