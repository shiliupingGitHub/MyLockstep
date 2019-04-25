#include "moudle/physix/physix.h"
#include "moudle/physix/collider.h"
#include "moudle/physix/collider2d.h"
#include "data.h"
#include <typeinfo>
#include "math/rect.h"
namespace lockstep
{
	Physix::Physix()
	{
		
	}
	Collider2D* Physix::CreateCollider2D()
	{
		auto c = new Collider2D();

		mColliders.push_back(c);

		return c;
	}

	void Physix::RemoveCollider(Collider2D* pCollider)
	{
		mColliders.remove(pCollider);

		delete pCollider;
	}

	void Physix::Overlap(Rect a, std::vector<Collider2D*>& ret)
	{
		
		for (auto c : mColliders)
		{

			if (typeid(*c) == typeid(Collider2D))
			{
				Collider2D* c_2d = (Collider2D*)c;

				Rect b = c_2d->GetRect();

				if (CrossLine(a, b))
					ret.push_back(c_2d);
			}
		}
	}
	Physix::~Physix()
	{
		for (auto c : mColliders)
		{
			delete c;
		}
	}

	void Physix::Tick()
	{
		for (auto c : mColliders)
		{
			c->Tick();
		}
	}
}