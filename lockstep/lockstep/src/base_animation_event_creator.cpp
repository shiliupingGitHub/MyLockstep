#include "base_animation_event_creator.h"
#include "animation_event_factory.h"
namespace lockstep
{
	BaseAnimationEventCreator::BaseAnimationEventCreator(std::string type)
	{
		AnimationEventFactory::GetInstance().AddCreator(type, this);
	}
}