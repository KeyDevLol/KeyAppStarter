# KeyAppStarter
Программа для запуска множества разных сценариев запуска пачки программ/веб-ссылок

[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)
[![License](https://img.shields.io/badge/.NET-10-purple)](https://dotnet.microsoft.com/ru-ru/download/dotnet/10.0)

---

Программа принимает в аргументах путь до файла сценария запуска, в котором прописаны абсолютные пути до программы, или ссылки

## Пример файла инструкции
```
C:/Programs/Visual Studio.exe
C:/Programs/GitHub Client.exe
https://stackoverflow.com
```

## Пример использования файла инструкции:
```
KeyAppStarter "C:Folder/Programming_Time_Instruction.txt"
```

## Чтобы было удобнее и быстрее запускать различные сценарии, есть возможность создавать ярлыки:
```
KeyAppStarter -s "C:/Users/User/Desktop/Programming Time.lnk" "C:Folder/Programming_Time_Instruction.txt"
```
_На Linux заместо ярлыков будет создаваться .sh файл_

### Build
```
dotnet publish . -c Release
```
Requiments
- [.NET 10](https://dotnet.microsoft.com/ru-ru/download/dotnet/10.0)