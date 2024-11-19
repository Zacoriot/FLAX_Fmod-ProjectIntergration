// This code was auto-generated. Do not modify it.

#include "Engine/Scripting/BinaryModule.h"
#include "Game.Gen.h"

StaticallyLinkedBinaryModuleInitializer StaticallyLinkedBinaryModuleGame(GetBinaryModuleGame);

extern "C" BinaryModule* GetBinaryModuleGame()
{
    static NativeBinaryModule module("Game");
    return &module;
}
