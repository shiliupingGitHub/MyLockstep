#ifndef LOCKSTEP_BASE_ANIMATION_EVENT_CREATOR_H
#define LOCKSTEP_BASE_ANIMATION_EVENT_CREATOR_H
#include <string>
namespace lockstep
{
	class AnimationEvent;
	class BaseAnimationEventCreator
	{
	public:
		BaseAnimationEventCreator(std::string type);
	public:
		virtual AnimationEvent* Create(int data) = 0;
	};
}
#endif // !_BASE_ANIMATION_EVENT_CREATOR

