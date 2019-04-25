#ifndef LOCKSTEP_COLLIDER_2D_COMPONENT_H
#define LOCKSTEP_COLLIDER_2D_COMPONENT_H
#include "component.h"
namespace lockstep
{
	class Collider2D;
	class Collider2DComponent : public Component
	{
	public:
		Collider2DComponent(World* pWorld, Entity* pEntity, int id);
		virtual ~Collider2DComponent();

		void Tick();

	private:
		Collider2D *mPCollier;
	};

}
#endif // !LOCKSTEP_COLLIDER_2D_COMPONENT_H

