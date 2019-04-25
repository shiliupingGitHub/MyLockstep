#ifndef LOCKSTEP_ANIMATION_EVENT_H
#define LOCKSTEP_ANIMATION_EVENT_H
#include "rapidjson/document.h"
using namespace rapidjson;
namespace lockstep
{
	class World;
	class Entity;
	struct ani_config_data;
	class AnimationEvent
	{
	public:
		AnimationEvent() = default;
		virtual ~AnimationEvent() = default;
	public:
		virtual void Fire(World* pWorld, Entity* pEntity, ani_config_data& pAni) = 0;
	};
}


#endif // !_ANIMATION_EVENT_H

