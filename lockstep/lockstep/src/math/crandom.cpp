#include "math/crandom.h"
#include <random>
using namespace lockstep;
#define RAND_NUM  100
CRandom::CRandom():mCur(0)
{
	
}
void CRandom::SetSeed(unsigned int seed)
{
	srand(seed);
	for (int i = 0; i < RAND_NUM; i++)
	{
		mRandom.push_back( abs(rand() % 11));
	}

}
float CRandom::GetRand()
{
	if (mCur >= mRandom.size())
		mCur = 0;

	if (mCur >= mRandom.size())
		return 0;
	else
	{
		float ret = mRandom[mCur] / 10.0f;
		mCur++;
		return ret;
	}


	
}

CRandom::~CRandom()
{

}