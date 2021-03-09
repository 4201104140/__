#include "pch.h"

#include <common/comUtils/comUtils.h>

namespace
{
    const wchar_t PT_URI_PROTOCOL_SCHEME[] = L"powertoys://";
    const wchar_t POWER_TOYS_MODULE_LOAD_FAIL[] = L"Failed to load "; // Module name will be appended on this message and it is not localized.
}



int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow) 
{
    winrt::init_apartment();
    const wchar_t* securityDescriptor =
        L"O:BA" // Owner: Builtin (local) administrator
        L"G:BA" // Group: Builtin (local) administrator
        L"D:"
        L"(A;;0x7;;;PS)" // Access allowed on COM_RIGHTS_EXECUTE, _LOCAL, & _REMOTE for Personal self
        L"(A;;0x7;;;IU)" // Access allowed on COM_RIGHTS_EXECUTE for Interactive Users
        L"(A;;0x3;;;SY)" // Access allowed on COM_RIGHTS_EXECUTE, & _LOCAL for Local system
        L"(A;;0x7;;;BA)" // Access allowed on COM_RIGHTS_EXECUTE, _LOCAL, & _REMOTE for Builtin (local) administrator
        L"(A;;0x3;;;S-1-15-3-1310292540-1029022339-4008023048-2190398717-53961996-4257829345-603366646)" // Access allowed on COM_RIGHTS_EXECUTE, & _LOCAL for Win32WebViewHost package capability
        L"S:"
        L"(ML;;NX;;;LW)"; // Integrity label on No execute up for Low mandatory level
    initializeCOMSecurity(securityDescriptor);
}