#ifndef _H_MOVE_SYSTEM_H
#define _H_MOVE_SYSTEM_H
#include "system.h"
#include "lockstep.h"
namespace lockstep
{
	class World;
	class HMoveSystem :public GSystem
	{

	public:
		LS_IMP_SYSTEM(HMoveSystem)
	private:
	};
}
#endif // !_H_MOVE_SYSTEM_H

