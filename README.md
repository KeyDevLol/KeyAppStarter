# KeyAppStarter
CLI program for launching multiple different scenarios that start a batch of programs/web-urls

[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)
[![License](https://img.shields.io/badge/.NET-10-purple)](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)

---

The program takes as an argument the path to a launch scenario file, which contains absolute paths to programs or links

## Example instruction file
```
C:/Programs/Visual Studio.exe
C:/Programs/Git Client.exe
https://stackoverflow.com
```

## Example usage of an instruction file:
```
KeyAppStarter "C:/Folder/Programming_Time_Instruction.txt"
```

## To make launching different scenarios more convenient and faster, you can create shortcuts:
```
KeyAppStarter -s "C:/Users/User/Desktop/Programming Time.lnk" "C:Folder/Programming_Time_Instruction.txt"
```
_On Linux, a .sh file will be created instead of a shortcut_

### Build
```
dotnet publish . -c Release
```
Requirements
- [.NET 10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)