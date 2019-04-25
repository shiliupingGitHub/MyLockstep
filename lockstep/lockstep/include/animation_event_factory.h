#ifndef LOCKSTEP_ANIMATION_EVENT_FACTORY
#define LOCKSTEP_ANIMATION_EVENT_FACTORY
#include <map>
#include "rapidjson/document.h"
using namespace rapidjson;
namespace lockstep
{
	class AnimationEvent;
	class World;
	class Entity;
	class BaseAnimationEventCreator;
	class AnimationEventFactory 
	{
	private:
		AnimationEventFactory();
		AnimationEventFactory(AnimationEventFactory&) = delete;
		AnimationEventFactory& operator=(const AnimationEventFactory&) = delete;
	public:
		static AnimationEventFactory& GetInstance();
		AnimationEvent* Create( std::string type, int id);

		void AddCreator(std::string type, BaseAnimationEventCreator* pCreator);
	private:
		std::map<std::string, BaseAnimationEventCreator*> mCreators;
	};

}
#endif // !ANIMATION_EVENT_FACTORY

