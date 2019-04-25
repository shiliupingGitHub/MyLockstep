#include "component/hit_component.h"
#include "entity.h"
#include "math/rect.h"
#include "component/skill_component.h"
#include "lockstep.h"
#include "controller.h"
#include "world.h"
using namespace lockstep;
HitComponent::HitComponent(World* pWorld, Entity* pEntity, int  id):
	Component(pWorld, pEntity)
{
	//const char* content = Controller::GetInstance()->InvokeReadFile(COMPONENT_CONFIG_FILE, id, "hit");
	//Document data;
	//data.Parse(content);

}

HitComponent::~HitComponent()
{

}
void HitComponent::Tick()
{
	int frozen = mPEntity->GetFrozen();
	if (frozen > 0)
	{
		mPEntity->SetFrozen(frozen - 1);

		//if (mPEntity->GetFrozen() == 0)
		//{

		//}
	}
}

