#include "system/skill_system.h"
#include "world.h"
#include "data.h"
#include "component/skill_component.h"
#include "lockstep.h"
using namespace lockstep;
IMPLEMENT_SYSTEM(SkillSystem, skill);
SkillSystem::SkillSystem(World* pWorld) :GSystem(pWorld)
{

}

SkillSystem::~SkillSystem()
{

}
void SkillSystem::tick()
{
	for (auto c : mComponents)
	{
		SkillComponent* pComp = (SkillComponent*)(c);
		pComp->Tick();
	}
}

Component* SkillSystem::create_compoent(Entity* pEntity, int id)
{
	
	return new SkillComponent(mPWorld, pEntity, id);

}