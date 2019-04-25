#include "component/ai_component.h"
#include <string>
#include "controller.h"
#include "lockstep.h"
using namespace lockstep;
AIComponent::AIComponent(World* pWorld, Entity* pEntity, int data_id )
	:Component(pWorld, pEntity),mPAgent(nullptr)
{

	const char* content = Controller::GetInstance()->InvokeReadFile(COMPONENT_CONFIG_FILE, data_id, "ai");
	Document data;
	data.Parse(content);
	mPAgent = behaviac::Agent::Create<AIAgent>();

	if (data.HasMember("ai"))
	{
		int id = data["ai"].GetInt();
		string str = "be_";
		string name = str + to_string(id);
		if (mPAgent->btload(name.c_str()))
		{
			mPAgent->btsetcurrent(name.c_str());
		}

		mPAgent->SetWorld(pWorld);
		mPAgent->SetEntity(pEntity);
	}

	
	
}

void AIComponent::Tick()
{
	if (mPEntity->GetIsCmdCtr())
	{
		mPEntity->SetIsCmdCtr(false);
		return;
	}
	if (nullptr != mPAgent)
	{
		behaviac::EBTStatus status = behaviac::BT_RUNNING;

		while (status == behaviac::BT_RUNNING)
		{
			status = mPAgent->btexec();
		}
	}

}

AIComponent::~AIComponent()
{
	if(nullptr != mPAgent)
		behaviac::Agent::Destroy(mPAgent);
}