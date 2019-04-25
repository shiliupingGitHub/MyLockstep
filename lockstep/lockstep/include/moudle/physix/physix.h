#ifndef LOCKSTEP_PHYSIX_H
#define LOCKSTEP_PHYSIX_H
#include <list>
#include "data.h"
#include <vector>
namespace lockstep
{
	class Collider;
	class Collider2D;
	class Physix
	{
	public:
		Physix();
		~Physix();

	public:
		Collider2D* CreateCollider2D();
		void RemoveCollider(Collider2D* pCollider);

		void Overlap(Rect a, std::vector<Collider2D*>& ret);
	public:
		void Tick();
	private:

		std::list< Collider*> mColliders;
	};
}


#endif // !LOCKSTEP_PHYSIX_H

