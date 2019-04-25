#ifndef LOCKSTEP_HIT_ANIMATION_EVENT_H
#define LOCKSTEP_HIT_ANIMATION_EVENT_H
#include "animaton_event.h"
namespace lockstep
{
	class HitAnimationEvent : public AnimationEvent
	{
	public:
		HitAnimationEvent(int id);
		virtual ~HitAnimationEvent();

		virtual void Fire(World* pWorld, Entity* pEntity, ani_config_data&  ani_data) override;
	private:

	};

}
#endif // !_HIT_ANIMATION_EVENT_H

