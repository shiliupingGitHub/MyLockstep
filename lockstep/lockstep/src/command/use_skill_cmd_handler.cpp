#include "cmd_handlers.h"
#include "component/skill_component.h"
#include "component/h_move_component.h"
//#include "component/transform_component.h"
using namespace lockstep;
using namespace rapidjson;
#define SKILL_START_INDEX 10000
IMPLEMENT_CMD_HANDLER(UseSkillCmdHandler, use_skill);
void UseSkillCmdHandler::handle(Entity* pEntity, char* data)
{
	Document doc;
	doc.Parse(data);
	if (doc.HasMember("skill_id") && doc["skill_id"].IsInt())
	{
		int skill_id = doc["skill_id"].GetInt();
		SkillComponent* pSkill = (SkillComponent*)pEntity->Get("skill");
		HMoveComponent* pComponent = (HMoveComponent*)pEntity->Get("hmove");

		if(nullptr != pSkill)
			pSkill->UseSkill(skill_id);
		pEntity->SetNextXMove(0);
	}
}

