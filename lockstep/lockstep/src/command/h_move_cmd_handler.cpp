#include "cmd_handlers.h"
#include "component/h_move_component.h"
using namespace lockstep;
using namespace rapidjson;
IMPLEMENT_CMD_HANDLER(HMoveCmdHandler, h_move);
void HMoveCmdHandler::handle(Entity* pEntity, char* data)
{
	Document doc;
	doc.Parse(data);

	if ( doc.HasMember("is_left") && doc["is_left"].IsBool() && doc.HasMember("is_move") && doc["is_move"].IsBool())
	{
		bool is_left = doc["is_left"].GetBool();
		int d = is_left ? -1 : 1;
		bool is_move = doc["is_move"].GetBool();
		HMoveComponent* pComponent = (HMoveComponent*)pEntity->Get("hmove");

		if (nullptr != pComponent)
		{	
			if (is_move)
			{
				pEntity->SetNextXMove(10);
			}
			pComponent->SetEnable(is_move);
			pComponent->SetD(d);
		}
				
	}
}
