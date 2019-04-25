#include "be_file.h"
#include "world.h"
#include "controller.h"
using namespace lockstep;
BeFile::BeFile(const char* fileName)
{
	 int id = atoi(fileName + 3);

	mContent = Controller::GetInstance()->ReadFile(BEHAVOUR_CONFIG_TYPE, id);

	int a = 0;
}

BeFile::~BeFile()
{

}

 uint32_t BeFile::Read(void* pBuffer, uint32_t numberOfBytesToRead)
{
	 memcpy(pBuffer, mContent.c_str(), numberOfBytesToRead);
	 return numberOfBytesToRead;
}
uint32_t BeFile::Write(const void* pBuffer, uint32_t numberOfBytesToWrite)
{
	return 0;
}
int64_t	BeFile::Seek(int64_t distanceToMove, CFileSystem::ESeekMode moveMethod)
{

	return 0;
}
uint64_t	BeFile::GetSize()
{
	return mContent.size();
}