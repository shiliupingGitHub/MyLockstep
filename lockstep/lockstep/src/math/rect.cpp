#include "math/rect.h"
#include <stdlib.h>
extern "C"
{
	
	namespace lockstep
	{
		bool CrossLine(Rect& r1, Rect& r2) {
			if (abs((r1.minX + r1.maxX) / 2 - (r2.minX + r2.maxX) / 2) < ((r1.maxX + r2.maxX - r1.minX - r2.minX) / 2)
				&& abs((r1.minY + r1.maxY) / 2 - (r2.minY + r2.maxY) / 2) < ((r1.maxY + r2.maxY - r1.minY - r2.minY) / 2))
				return true;
			return false;
		}
	}

}

