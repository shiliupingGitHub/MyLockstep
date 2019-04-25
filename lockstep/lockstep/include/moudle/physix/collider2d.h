#ifndef LOCKSTEP_COLLIDER_2D_H
#define LOCKSTEP_COLLIDER_2D_H
#include "moudle/physix/collider.h"
#include "data.h"
namespace lockstep
{
	class Collider2D : public Collider
	{
	public:
		Collider2D();
		virtual ~Collider2D() override;

		virtual void Tick() override;

		void SetBound(Bound& b);
		Bound& GetBound();
		void SetPosition(Vector2 pos);
		Vector2& GetPosition();
		Rect GetRect();
	protected:

		Bound mBound;
		Vector2 mPos;
	};

}
#endif // !LOCKSTEP_COLLIDER_2D_H

