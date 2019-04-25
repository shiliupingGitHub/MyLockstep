#ifndef LOCKSTEP_ANIMATION_EVENT_CREATOR_H
#define LOCKSTEP_ANIMATION_EVENT_CREATOR_H
#include "base_animation_event_creator.h"
namespace lockstep
{

	template<class T>
	class AnimationEventCreator : public BaseAnimationEventCreator
	{
	public:
		AnimationEventCreator(std::string type):BaseAnimationEventCreator(type)
		{
			
		}

		virtual AnimationEvent* Create(int id)
		{
			return new T(id);
		}

	};
	


}
#endif // !ANIMATION_EVENT_CREATOR_H

