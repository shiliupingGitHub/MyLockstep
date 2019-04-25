#ifndef LOCKSTEP_AI_COMPONENT_H
#define LOCKSTEP_AI_COMPONENT_H
#include "component.h"
#include "behavour/behaviac_generated/types/behaviac_types.h"
namespace lockstep
{
	
	class AIComponent :public Component
	{
	public:
		AIComponent(World* pWorld, Entity* pEntity, int id);
		virtual ~AIComponent() override;
	public:
		void Tick();
	private:
		AIAgent* mPAgent;
	};
}

#endif // !AI_COMPONENT_H

