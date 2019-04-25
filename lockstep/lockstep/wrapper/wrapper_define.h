#ifndef _WRAPPER_DEFINE_H
#define _WRAPPER_DEFINE_H

#ifdef _MSC_VER

#ifdef _DLL_lockstep_IMPORT

#define _DLL_EXPORT _declspec( dllimport )

#else

#define _DLL_EXPORT _declspec( dllexport )

#endif // _DLL_lockstep_IMPORT

#else
#define _DLL_EXPORT
#endif // _WINDLL

#endif // !_WRAPPER_DEFINE_H

