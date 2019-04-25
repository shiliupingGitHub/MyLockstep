#ifndef LOCKSTEP_BE_FILE_MANAGER_H
#define LOCKSTEP_BE_FILE_MANAGER_H
#include "behaviac/common/file/filemanager.h"
namespace lockstep
{
	class World;
	class BEHAVIAC_API BeFileManager : public behaviac::CFileManager
	{
	public:
		BEHAVIAC_DECLARE_MEMORY_OPERATORS(BeFileManager);

		BeFileManager();
		virtual ~BeFileManager();

		virtual behaviac::IFile* FileOpen(const char* fileName, behaviac::CFileSystem::EOpenMode iOpenAccess = behaviac::CFileSystem::EOpenMode_Read);

		virtual void FileClose(behaviac::IFile* file);
		virtual bool FileExists(const char* fileName);
		virtual bool FileExists(const behaviac::string& filePath, const behaviac::string& ext);

		virtual uint64_t FileGetSize(const char* fileName);
		virtual behaviac::wstring GetCurrentWorkingDirectory();
	};
}

#endif // !_BE_FILE_MANAGER_H

