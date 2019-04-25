#ifndef LOCKSTEP_COLLIDER_COMPONENT_H
#define LOCKSTEP_COLLIDER_COMPONENT_H
#include "component.h"
#include "lockstep.h"
namespace lockstep
{
	class HitComponent : public Component
	{
	public:
		 HitComponent(World* pWorld, Entity* pEntity, int id);
		virtual ~HitComponent() override;
	public:

		void Tick();
	};

}
#endif // !_COLLIDER_COMPONENT_H

