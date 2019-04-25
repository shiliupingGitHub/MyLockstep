#include "component/collider2d_component.h"
#include "lockstep.h"
#include "controller.h"
#include "data.h"
#include "moudle/physix/physix.h"
#include "moudle/physix/collider2d.h"
#include "world.h"
#include "entity.h"
namespace lockstep
{
	Collider2DComponent::Collider2DComponent(World* pWorld, Entity* pEntity, int id):Component(pWorld, pEntity), mPCollier(nullptr)
	{
		const char* content = Controller::GetInstance()->ReadFile(COMPONENT_CONFIG_FILE, id, "collider2d");
		Document data;
		data.Parse(content);
		
		if (data.HasMember("bound"))
		{
			Value& b = data["bound"];
			Value& size = b["size"];
			Value& center = b["center"];

			Bound bound;

			bound.size.x = size["x"].GetInt();
			bound.size.y = size["y"].GetInt();

			bound.center.x = center["x"].GetInt();
			bound.center.y = center["y"].GetInt();

			mPCollier = pWorld->GetPhysix()->CreateCollider2D();
			mPCollier->SetBound(bound);
			mPCollier->SetPosition(pEntity->GetPos());
			mPCollier->SetUserData(mPEntity);
		}
		
	}

	Collider2DComponent::~Collider2DComponent()
	{
		if(nullptr != mPCollier)
			mPWorld->GetPhysix()->RemoveCollider(mPCollier);
	}

	void Collider2DComponent::Tick()
	{

		if(nullptr != mPCollier)
			mPCollier->SetPosition(mPEntity->GetPos());
	}
}