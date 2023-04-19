Расширяемая фабрика и IoC // ДЗ 

* IOC - реализация Ioc контейнера и паттерна сервис локатора
* Container - реализация Ioc контейнера
* IStrategyContainer - Предоставляет методы для полученния или регистрации зависимости {IResolveDependencyStrategy} по уникальному идентификатору{key}
* IResolveDependencyStrategy - Предоставляет контракт, для обработки зависимости 

 * Фабрика реализована в виде фабричного метода, который в себе агрегирует некую логику заданну, при регистрации зависимости. Она предаставлена в след:
        * IStrategyContainer
        * IResolveDependencyStrategy        

# Диаграмма классов![ExpandableFactory vpd](https://user-images.githubusercontent.com/8067079/233018653-0f51215c-0305-429e-8762-216ce751e15a.jpg)
