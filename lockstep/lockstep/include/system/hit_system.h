#ifndef _COLLIDER_SYSTEM_H
#define _COLLIDER_SYSTEM_H
#include "system.h"
#include "lockstep.h"
#include <vector>
namespace lockstep
{
	class HitSystem :public GSystem
	{
	public:

		LS_IMP_SYSTEM(HitSystem)

	public:
		 void AddHit(hit& hit);
	private:

		std::vector<hit> mHits;
	};

}
#endif // !_COLLIDER_SYSTEM_H

