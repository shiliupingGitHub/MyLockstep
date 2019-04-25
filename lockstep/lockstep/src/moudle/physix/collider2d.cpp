#include "moudle/physix/collider2d.h"
namespace lockstep
{
	Collider2D::Collider2D()
	{

	}
	Collider2D::~Collider2D()
	{
				
	}

	void Collider2D::Tick()
	{

	}

	void Collider2D::SetBound(Bound& b)
	{
		mBound = b;
	}
	Bound& Collider2D::GetBound()
	{

		return mBound;
	}

	void Collider2D::SetPosition(Vector2 pos)
	{
		mPos = pos;
	}
	Vector2& Collider2D::GetPosition()
	{
		return mPos;
	}

	Rect Collider2D::GetRect()
	{
		Rect ret;

		ret.minX = mPos.x + mBound.center.x - mBound.size.x / 2;
		ret.maxX = mPos.x + mBound.center.x + mBound.size.x / 2;

		ret.minY = mPos.y + mBound.center.y;
		ret.maxY = ret.minY + mBound.size.y;

		return ret;
	}
}