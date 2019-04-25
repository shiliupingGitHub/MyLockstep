#include "animation_event/hit_animation_event.h"
#include "entity.h"
#include "component/hit_component.h"
#include "system/hit_system.h"
#include "world.h"
#include "controller.h"
#include "config/ani_config.h"
namespace lockstep
{
	IMPLEMENT_ANIMATION_EVENT(HitAnimationEvent, hit);
	HitAnimationEvent::HitAnimationEvent(int id) 
	{
	
	}

	HitAnimationEvent::~HitAnimationEvent()
	{

	}

	void HitAnimationEvent::Fire(World* pWorld, Entity* pEntity, ani_config_data&  ani_data)
	{
		HitComponent* pComp =(HitComponent*) pEntity->Get("hit");
		HitSystem* pSystem = (HitSystem*)pWorld->GetSystem("hit");
		if (nullptr != pComp && nullptr != pSystem)
		{
			hit h;
			h.id = pEntity->GetId();
			h.param = 0;
			h.frozen = ani_data.frozen;
			h.x = ani_data.distance;
			h.y = ani_data.y_distance;
			pSystem->AddHit(h);
		}
	}



}