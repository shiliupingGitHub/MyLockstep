#include "animation_event_factory.h"
#include "lockstep.h"
namespace lockstep
{

	AnimationEventFactory::AnimationEventFactory()
	{

	}
	AnimationEvent* AnimationEventFactory::Create(std::string type, int data)
	{
		auto it = mCreators.find(type);

		if (it != mCreators.end())
		{
			return  it->second->Create(data);
		}

		return nullptr;
	}

	 AnimationEventFactory& AnimationEventFactory::GetInstance()
	{
		 static AnimationEventFactory s_instance;

	
		 return s_instance;

	}

	 void AnimationEventFactory::AddCreator(std::string type, BaseAnimationEventCreator* pCreator)
	 {
		 mCreators[type] = pCreator;
	 }
}