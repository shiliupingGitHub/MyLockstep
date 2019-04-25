#ifndef LOCKSTEP_CRANDOM_H
#define LOCKSTEP_CRANDOM_H
#include <vector>
namespace lockstep
{
	class  CRandom
	{
	public:
		CRandom();
		~CRandom();

	public:
		float GetRand();
		void SetSeed(unsigned int seed);
	private:
		std::vector<int> mRandom;
		int mCur;
	};
}


#endif // 

