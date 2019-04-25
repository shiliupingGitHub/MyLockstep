#include "system/ai_system.h"
#include "world.h"
#include "data.h"
#include "component/ai_component.h"
#include "behavour/behaviac_generated/types/behaviac_types.h"
#include "be_file_manager.h"
using namespace lockstep;

IMPLEMENT_SYSTEM(AISystem, ai);
static BeFileManager s_AiFileManager;
AISystem::AISystem(World* pWorld) :GSystem(pWorld)
{
	behaviac::Workspace::GetInstance()->SetFilePath("");
	behaviac::Workspace::GetInstance()->SetFileFormat(behaviac::Workspace::EFF_xml);
}

AISystem::~AISystem()
{
	behaviac::Workspace::GetInstance()->Cleanup();
}
void AISystem::tick()
{
	for (auto c : mComponents)
	{
		AIComponent* pAiComponent = (AIComponent*)c;

		pAiComponent->Tick();
	}
}

Component* AISystem::create_compoent(Entity* pEntity, int  id)
{

	return new AIComponent(mPWorld, pEntity, id);

}