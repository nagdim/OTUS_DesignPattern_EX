Адаптер и мост //ДЗ 

* IProgramRunner - интерфейс который отвечает за запуск программы
* Program_1 - программа которая читаеn данные из файла f0, суммирует и записывает результат в f1
* Program_2 - программа которая генерирует данные, и записывает их в файл f2
* Program_3 - программа которая в себе агрегирует в себе Program_1 и Program_2 и композирует ProgramAdapter
* ProgramAdapter - прослойка, которая читает данные данные из Program_2 и пишет результат в Program_1
* IWriter - интерфейс отвечающий за запись данные по ключу
* IReader - интерфейс отвечающий за чтение данных по ключу
* IReadWriteCreator - фабрика, которая создает IWriter и IReader

# OTUS_DesignPattern_EX![Adapter vpd](https://user-images.githubusercontent.com/8067079/233401793-c7566cc6-a436-4828-8fdb-9421f0d2bff9.jpg)
