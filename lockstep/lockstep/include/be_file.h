#ifndef LOCKSTEP_BE_FILE_H
#define LOCKSTEP_BE_FILE_H
#include "behaviac/common/file/file.h"
using namespace behaviac;
namespace lockstep 
{
	class World;
	class BeFile : public IFile
	{
	public:
		BeFile(const char* fileName);
		virtual ~BeFile();

		virtual uint32_t Read(void* pBuffer, uint32_t numberOfBytesToRead);
		virtual uint32_t	Write(const void* pBuffer, uint32_t numberOfBytesToWrite);
		virtual int64_t		Seek(int64_t distanceToMove, CFileSystem::ESeekMode moveMethod);
		virtual uint64_t	GetSize();
		virtual void		Flush() { };

	private:

		string mContent;
	};

}
#endif // !_BE_FILE_H

