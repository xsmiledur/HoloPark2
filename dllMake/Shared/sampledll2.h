#pragma once

#define DLLEXPORT __declspec(dllexport)

extern "C"
{
	//DLLEXPORT int Hello();
	DLLEXPORT int __stdcall Add(int, int);
	DLLEXPORT int __stdcall Sum(unsigned char*, int, int);
}