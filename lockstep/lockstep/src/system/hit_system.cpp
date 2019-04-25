#include "system/hit_system.h"
#include "component/hit_component.h"
#include "entity.h"
#include "world.h"
#include "component/collider2d_component.h"
#include "moudle/physix/physix.h"
#include "moudle/physix/collider2d.h"
#include "controller.h"
using namespace lockstep;
IMPLEMENT_SYSTEM(HitSystem, hit);
HitSystem::HitSystem(World* pWorld) :GSystem(pWorld)
{

}

HitSystem::~HitSystem()
{
	mHits.clear();
}
void HitSystem::tick()
{
	
	for (auto h :  mHits)
	{
		auto pEntity = mPWorld->FindEntity(h.id);

		if (nullptr != pEntity)
		{
			Rect a;
			Vector2 pos = pEntity->GetPos();
			a.minX = pos.x;
			a.maxX = a.minX + h.x * pEntity->GetD();
			a.minY = pos.y;
			a.maxY = a.minY + h.y;

			vector<Collider2D*> colliders;
			mPWorld->GetPhysix()->Overlap(a, colliders);

			for (auto c : colliders)
			{
				auto pOutEntity = (Entity*) c->GetUserData();

				if (pOutEntity->GetCamp() == pEntity->GetCamp())
					continue;
				pOutEntity->SetFrozen(h.frozen);

				hurt_data hurt = { 0 };
				Controller::GetInstance()->Hurt(pOutEntity->GetId(), hurt);
			}
		}
	}

	mHits.clear();
}

Component* HitSystem::create_compoent(Entity* pEntity,int id)
{

	return new HitComponent(mPWorld, pEntity, id);

}


void HitSystem::AddHit(hit& h)
{
	mHits.push_back(h);
}