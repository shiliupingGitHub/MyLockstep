#include "component/h_move_component.h"
#include "lockstep.h"
#include "controller.h"
#include "entity.h"
namespace lockstep
{
	HMoveComponent::HMoveComponent(World* pWorld, Entity* pEntity, int id):Component(pWorld, pEntity),
		mSpeed(0), mEnable(false), mXMoveReduce(-2),mD(0)
	{
		const char* content = Controller::GetInstance()->ReadFile(COMPONENT_CONFIG_FILE, id, "hmove");
		Document data;
		data.Parse(content);

		if (data.HasMember("speed"))
		{
			mSpeed = data["speed"].GetInt();
		}

		if (data.HasMember("x_move_reduce"))
			mXMoveReduce = data["x_move_reduce"].GetInt();
	}

	HMoveComponent::~HMoveComponent()
	{

	}
	void HMoveComponent::SetEnable(bool enable)
	{
		mEnable = enable;
		
	}
	void HMoveComponent::Tick()
	{
		if (mPEntity->GetIsSkill()
			|| mPEntity->GetFrozen() > 0
			)
		{
			mPEntity->SetXMove(0);
			return;
		}
		if (mEnable)
		{
			int d = mPEntity->GetD();
			Vector2 pos = mPEntity->GetPos();
			int x_move = mPEntity->GetNextXMove();
			pos.x += d * mSpeed * x_move;
			mPEntity->SetPos(pos);
			mPEntity->SetXMove(10);
			mPEntity->SetNextXMove(10);
			mPEntity->SetD(mD);
		}
		else
		{
			int d = mPEntity->GetD();

			Vector2 pos = mPEntity->GetPos();
			int x_move = mPEntity->GetNextXMove();
			pos.x += d * mSpeed * x_move;
			mPEntity->SetPos(pos);
			mPEntity->SetXMove(x_move);

			if (x_move <= 0)
			{
				mEnable = false;
				x_move = 0;
				return;
			}

			x_move += mXMoveReduce;

			mPEntity->SetNextXMove(x_move);
		}
	}
}